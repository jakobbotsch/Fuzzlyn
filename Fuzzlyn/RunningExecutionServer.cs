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

    private readonly string _poolName;

    private RunningExecutionServer(
        string poolName,
        int serverIndex,
        Process process,
        LogExecutionServerRequestsOptions logExecServerRequestOptions)
    {
        _poolName = poolName;
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
            File.WriteAllText(
                Path.Combine(_logExecServerRequestOptions.LogDirectory, $"{_poolName}-{_serverIndex:00}-{_numRequestsSent:0000}.json"),
                serialized);
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

    public RunSingleResults Run(ProgramSingle program, TimeSpan timeout)
    {
        ReceiveResult result =
            RequestAndReceive(new Request
            {
                Kind = RequestKind.RunSingle,
                Program = program,
            }, timeout);

        if (result.Ended)
        {
            return new RunSingleResults(RunSingleResultsKind.Crash, null, result.Stderr);
        }

        if (result.Timeout)
        {
            return new RunSingleResults(RunSingleResultsKind.Timeout, null, null);
        }

        return new RunSingleResults(RunSingleResultsKind.Success, result.Response.RunResult, null);
    }

    public Extension[] GetSupportedIntrinsicExtensions()
    {
        ReceiveResult result =
            RequestAndReceive(new Request
            {
                Kind = RequestKind.GetSupportedIntrinsicExtensions,
            }, TimeSpan.FromSeconds(30));

        if (result.Ended)
        {
            throw new Exception("Host died while querying for supported intrinsic extensions");
        }

        if (result.Timeout)
        {
            throw new Exception($"Host timed out while querying for supported intrinsic extensions");
        }

        return result.Response.Extensions;
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

    public static RunningExecutionServer Create(int serverIndex, ExecutionServerConfiguration configuration)
    {
        ProcessStartInfo info = new()
        {
            FileName = configuration.Host,
            WorkingDirectory = Path.GetDirectoryName(configuration.ExecutionServerPath),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
        };

        info.ArgumentList.Add(configuration.ExecutionServerPath);

        Helpers.SetExecutionEnvironmentVariables(info.EnvironmentVariables, configuration.EnvironmentVariables);

        if (configuration.SpmiOptions != null)
        {
            Helpers.SetSpmiCollectionEnvironmentVariables(info.EnvironmentVariables, configuration.SpmiOptions);
        }

        Process proc = Process.Start(info);
        return new RunningExecutionServer(
            configuration.Name,
            serverIndex,
            proc,
            configuration.LogExecutionServerRequestsOptions);
    }

    private struct ReceiveResult
    {
        public bool Timeout { get; init; }
        public bool Ended { get; init; }
        public string Stderr { get; init; }
        public Response Response { get; init; }
    }
}

internal enum RunSingleResultsKind
{
    Crash,
    Timeout,
    Success
}

internal class RunSingleResults(RunSingleResultsKind kind, ProgramResult result, string crashError)
{
    public RunSingleResultsKind Kind { get; } = kind;
    public ProgramResult Result { get; } = result;
    public string CrashError { get; } = crashError;
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
