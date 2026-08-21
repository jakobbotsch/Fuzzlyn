using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fuzzlyn;

internal sealed class ExecutionServerConfiguration(
    string name,
    string host,
    string executionServerPath,
    IReadOnlyDictionary<string, string> environmentVariables,
    SpmiSetupOptions spmiOptions,
    LogExecutionServerRequestsOptions logRequestsOptions)
{
    public string Name { get; } = name;
    public string Host { get; } = host;
    public string ExecutionServerPath { get; } = executionServerPath;
    public IReadOnlyDictionary<string, string> EnvironmentVariables { get; } =
        new ReadOnlyDictionary<string, string>(new Dictionary<string, string>(environmentVariables));
    public SpmiSetupOptions SpmiOptions { get; } = spmiOptions;
    public LogExecutionServerRequestsOptions LogExecutionServerRequestsOptions { get; } = logRequestsOptions;
}
