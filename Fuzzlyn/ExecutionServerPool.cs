using Fuzzlyn.ExecutionServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn;

internal interface IExecutionServerPool
{
    ExecutionServerConfiguration Configuration { get; }
    RunSingleResults RunOnPool(ProgramSingle program, TimeSpan timeout, bool keepPoolNonEmptyEagerly);
    Extension[] GetSupportedIntrinsicExtensions();
}

internal class ExecutionServerPool(ExecutionServerConfiguration configuration) : IExecutionServerPool
{
    // Stop a server once it has not been used for this duration
    private static readonly TimeSpan s_inactivityPeriod = TimeSpan.FromMinutes(3);

    public ExecutionServerConfiguration Configuration { get; } = configuration;

    private int _serverIndex;
    private List<RunningExecutionServer> _pool = new();

    private RunningExecutionServer Get(bool keepNonEmptyEagerly)
    {
        RunningExecutionServer bestServer = null;
        bool startNew = false;

        lock (_pool)
        {
            if (_pool.Count > 0)
            {
                int best = 0;
                for (int i = 1; i < _pool.Count; i++)
                {
                    if (_pool[i].LastUseTimer.Elapsed < _pool[best].LastUseTimer.Elapsed)
                        best = i;
                }

                bestServer = _pool[best];
                _pool[best] = _pool[^1];
                _pool.RemoveAt(_pool.Count - 1);
            }

            if (keepNonEmptyEagerly && _pool.Count == 0)
                startNew = true;
        }

        if (startNew)
        {
            RunningExecutionServer created = RunningExecutionServer.Create(_serverIndex++, Configuration);
            lock (_pool)
            {
                _pool.Add(created);
            }
        }

        if (bestServer != null)
            return bestServer;

        return RunningExecutionServer.Create(_serverIndex++, Configuration);
    }

    private void Return(RunningExecutionServer server)
    {
        List<RunningExecutionServer> toStop;
        lock (_pool)
        {
            toStop = _pool.Where(s => s.LastUseTimer.Elapsed > s_inactivityPeriod).ToList();
            _pool.RemoveAll(toStop.Contains);
            _pool.Add(server);
        }

        foreach (var otherServer in toStop)
            otherServer.Shutdown();
    }

    public RunSingleResults RunOnPool(ProgramSingle program, TimeSpan timeout, bool keepPoolNonEmptyEagerly)
    {
        RunningExecutionServer server = null;
        try
        {
            server = Get(keepPoolNonEmptyEagerly);
            RunSingleResults results = server.Run(program, timeout);
            if (results.Kind != RunSingleResultsKind.Success)
            {
                server = null; // Do not return it. The process has already been killed by Run.
            }

            return results;
        }
        finally
        {
            if (server != null)
                Return(server);
        }
    }

    public Extension[] GetSupportedIntrinsicExtensions()
    {
        RunningExecutionServer server = null;
        try
        {
            server = Get(false);
            return server.GetSupportedIntrinsicExtensions();
        }
        finally
        {
            if (server != null)
                Return(server);
        }
    }
}
