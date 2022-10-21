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
    private Process _process;

    private RunningExecutionServer(Process process)
    {
        _process = process;
    }

    public Stopwatch LastUseTimer { get; } = new Stopwatch();

    private ReceiveResult RequestAndReceive(Request req, TimeSpan timeout)
    {
        Console.WriteLine("Writing request to {0}, HasExited: {1}", _process.Id, _process.HasExited);
        Console.ReadLine();
        _process.StandardInput.WriteLine(JsonSerializer.Serialize(req));

        bool killed = false;
        string line;
        {
            using var cts = new CancellationTokenSource(timeout);
            using var reg = cts.Token.Register(() => { killed = true; Kill(); });
            line = _process.StandardOutput.ReadLine();
        }

        LastUseTimer.Restart();

        if (killed)
        {
            return new ReceiveResult { Timeout = true };
        }

        if (string.IsNullOrWhiteSpace(line))
        {
            string stderr = _process.StandardError.ReadToEnd();
            return new ReceiveResult { Ended = true, Stderr = stderr };
        }

        Response resp;
        try
        {
            resp = JsonSerializer.Deserialize<Response>(line);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Received malformed JSON response");
            Console.WriteLine(ex.ToString());
            Console.WriteLine(line);
            return new ReceiveResult { Ended = true, Stderr = "Malformed result" };
        }

        return new ReceiveResult { Response = JsonSerializer.Deserialize<Response>(line) };
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

    public static RunningExecutionServer Create(string host)
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
        Process proc = Process.Start(info);
        Console.WriteLine("Started process {0}", proc.Id);
        return new RunningExecutionServer(proc);
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

internal class RunSeparatelyResults
{
    public RunSeparatelyResults(RunSeparatelyResultsKind kind, ProgramPairResults results, string crashError)
    {
        Kind = kind;
        Results = results;
        CrashError = crashError;
    }

    public RunSeparatelyResultsKind Kind { get; }
    public ProgramPairResults Results { get; }
    public int ExitCode { get; }
    public string CrashError { get; }
}
