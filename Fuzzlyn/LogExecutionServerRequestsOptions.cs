using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzlyn;

internal class LogExecutionServerRequestsOptions
{
    public string LogDirectory { get; set; }

    public static LogExecutionServerRequestsOptions Create(string logDir, out string error)
    {
        if (!Directory.Exists(logDir))
        {
            error = $"Log directory {logDir} does not exist";
            return null;
        }

        error = null;
        return new LogExecutionServerRequestsOptions
        {
            LogDirectory = logDir,
        };
    }
}
