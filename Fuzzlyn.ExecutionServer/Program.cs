using System;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text.Json;

namespace Fuzzlyn.ExecutionServer
{
    public static class Program
    {
        public static void Main()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Prevent post-mortem debuggers from launching here. We get
                // runtime crashes sometimes and the parent Fuzzlyn process will
                // handle it.
                SetErrorMode(ErrorModes.SEM_NOGPFAULTERRORBOX);
            }

            AssemblyLoadContext currentAlc = new ServerAssemblyLoadContext();
            int currentRunsInAlc = 0;
            while (true)
            {
                string line = Console.ReadLine();
                if (line == null)
                    return;

                Request req = JsonSerializer.Deserialize<Request>(line);
                Response resp;
                switch (req.Kind)
                {
                    case RequestKind.RunPair:
                        ProgramPairResults results = Executor.RunPair(currentAlc, req.Pair);
                        currentRunsInAlc++;
                        resp = new Response
                        {
                            RunPairResult = results
                        };
                        break;
                    case RequestKind.Shutdown:
                        resp = new Response();
                        break;
                    default:
                        return;
                }

                Console.WriteLine(JsonSerializer.Serialize(resp));
                if (req.Kind == RequestKind.Shutdown)
                    return;

                if (currentRunsInAlc > 100)
                {
                    currentAlc.Unload();
                    currentAlc = new ServerAssemblyLoadContext();
                    currentRunsInAlc = 0;
                }
            }
        }

        [Flags]
        private enum ErrorModes
        {
            SYSTEM_DEFAULT = 0x0,
            SEM_FAILCRITICALERRORS = 0x0001,
            SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
            SEM_NOGPFAULTERRORBOX = 0x0002,
            SEM_NOOPENFILEERRORBOX = 0x8000
        }

        [DllImport("kernel32.dll")]
        private static extern ErrorModes SetErrorMode(ErrorModes uMode);

        private class ServerAssemblyLoadContext : AssemblyLoadContext
        {
            public ServerAssemblyLoadContext() : base(true)
            {
            }
        }
    }
}
