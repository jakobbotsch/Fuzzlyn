using Fuzzlyn.ExecutionServer;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;

namespace Fuzzlyn;

internal class RunningExecutionServer
{
    private int _serverIndex;
    private Process _process;
    private LogExecutionServerRequestsOptions _logExecServerRequestOptions;
    private int _numRequestsSent;

    private RunningExecutionServer(int serverIndex, Process process, LogExecutionServerRequestsOptions logExecServerRequestOptions)
    {
        _serverIndex = serverIndex;
        _process = process;
        _logExecServerRequestOptions = logExecServerRequestOptions;
    }

    public Stopwatch LastUseTimer { get; } = new Stopwatch();

    private ReceiveResult RequestAndReceive(Request req, TimeSpan timeout)
    {
        string serialized = JsonSerializer.Serialize(req);
        if (_logExecServerRequestOptions != null)
        {
            File.WriteAllText(Path.Combine(_logExecServerRequestOptions.LogDirectory, $"{_serverIndex:00}-{_numRequestsSent:0000}.json"), serialized);
        }

        try
        {
            _process.StandardInput.WriteLine(serialized);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Got IOException during WriteLine: " + ex);
            Console.WriteLine("Process ended: {0}", _process.HasExited);
        }

        _numRequestsSent++;
        bool killed = false;
        string line;
        {
            using var cts = new CancellationTokenSource(timeout);
            using var reg = cts.Token.Register(() => { killed = true; Kill(); });
            bool first = true;
            while (true)
            {
                line = _process.StandardOutput.ReadLine();

                if (line == null)
                {
                    break;
                }

                if (line.StartsWith("@!EXEC_SERVER_RESPONSE!@"))
                {
                    line = line["@!EXEC_SERVER_RESPONSE!@".Length..];
                    break;
                }

                if (first)
                {
                    Console.WriteLine("Received unexpected output from execution server:");
                }

                Console.WriteLine(line);

                first = false;
            }
        }

        LastUseTimer.Restart();

        if (killed)
        {
            return new ReceiveResult { Timeout = true };
        }

        if (line == null)
        {
            string stderr = _process.StandardError.ReadToEnd();
            return new ReceiveResult { Ended = true, Stderr = stderr };
        }

        try
        {
            Response resp = JsonSerializer.Deserialize<Response>(line);
            return new ReceiveResult { Response = resp };
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Could not parse JSON response");
            Console.WriteLine(ex.ToString());
            Console.WriteLine(line);
            return new ReceiveResult { Ended = true, Stderr = "Malformed result" };
        }

    }

    public RunSeparatelyResults RunPair(ProgramPair pair, TimeSpan timeout)
    {
        ReceiveResult result =
            RequestAndReceive(new Request
            {
                Kind = RequestKind.RunPair,
                Pair = pair,
            }, timeout);

        if (result.Ended)
        {
            return new RunSeparatelyResults(RunSeparatelyResultsKind.Crash, null, result.Stderr);
        }

        if (result.Timeout)
        {
            return new RunSeparatelyResults(RunSeparatelyResultsKind.Timeout, null, null);
        }

        return new RunSeparatelyResults(RunSeparatelyResultsKind.Success, result.Response.RunPairResult, null);
    }

    public Extension[] GetSupportedExtensions()
    {
        ReceiveResult result =
            RequestAndReceive(new Request
            {
                Kind = RequestKind.GetSupportedExtensions,
            }, TimeSpan.FromSeconds(30));

        if (result.Ended)
        {
            throw new Exception("Host died while querying for supported extensions");
        }

        if (result.Timeout)
        {
            throw new Exception($"Host timed out while querying for supported extensions");
        }

        return result.Response.SupportedExtensions;
    }

    public void Shutdown()
    {
        ReceiveResult result = RequestAndReceive(new Request { Kind = RequestKind.Shutdown }, TimeSpan.FromSeconds(1));
        if (result.Timeout)
            Kill();
        _process.Dispose();
    }

    public void Kill()
    {
        try
        {
            _process.Kill();
        }
        catch
        {

        }
    }

    public static RunningExecutionServer Create(int serverIndex, string host, SpmiSetupOptions spmiOptions, LogExecutionServerRequestsOptions logExecServerRequestsOptions)
    {
        string executorPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Fuzzlyn.ExecutionServer.dll");
        ProcessStartInfo info = new()
        {
            FileName = host,
            WorkingDirectory = Environment.CurrentDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
        };

        info.ArgumentList.Add(executorPath);

        Helpers.SetExecutionEnvironmentVariables(info.EnvironmentVariables);

        if (spmiOptions != null)
        {
            Helpers.SetSpmiCollectionEnvironmentVariables(info.EnvironmentVariables, spmiOptions);
        }

        Process proc = Process.Start(info);
        return new RunningExecutionServer(serverIndex, proc, logExecServerRequestsOptions);
    }

    private struct ReceiveResult
    {
        public bool Timeout { get; init; }
        public bool Ended { get; init; }
        public string Stderr { get; init; }
        public Response Response { get; init; }
    }
}

internal enum RunSeparatelyResultsKind
{
    Crash,
    Timeout,
    Success
}

internal class RunSeparatelyResults(RunSeparatelyResultsKind kind, ProgramPairResults results, string crashError)
{
    public RunSeparatelyResultsKind Kind { get; } = kind;
    public ProgramPairResults Results { get; } = results;
    public int ExitCode { get; }
    public string CrashError { get; } = crashError;
}
