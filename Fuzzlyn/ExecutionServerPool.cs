using Fuzzlyn.ExecutionServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn
{
    internal class ExecutionServerPool
    {
        // Stop a server once it has not been used for this duration
        private static readonly TimeSpan s_inactivityPeriod = TimeSpan.FromMinutes(3);

        public ExecutionServerPool(string host)
        {
            Host = host;
        }

        public string Host { get; }

        private List<RunningExecutionServer> _pool = new();

        private RunningExecutionServer Get()
        {
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

                    RunningExecutionServer bestServer = _pool[best];
                    _pool[best] = _pool[^1];
                    _pool.RemoveAt(_pool.Count - 1);
                    return bestServer;
                }
            }

            return RunningExecutionServer.Create(Host);
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

        public RunSeparatelyResults RunPairOnPool(ProgramPair pair, TimeSpan timeout)
        {
            RunningExecutionServer server = null;
            try
            {
                server = Get();
                RunSeparatelyResults results = server.RunPair(pair, timeout);
                if (results.Kind != RunSeparatelyResultsKind.Success)
                {
                    server = null; // Do not return, create a new one. The process has already been killed by RunPair.
                }

                return results;
            }
            finally
            {
                if (server != null)
                    Return(server);
            }
        }
    }
}
