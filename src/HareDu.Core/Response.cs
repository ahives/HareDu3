namespace HareDu.Core;

internal static class Response
{
    public static Result Failed(DebugInfo debugInfo) => new UnsuccessfulResult {DebugInfo = debugInfo};

    public static Result<T> Failed<T>(DebugInfo debugInfo) => new UnsuccessfulResult<T> {DebugInfo = debugInfo};

    public static Result<T> Succeeded<T>(T data, DebugInfo debugInfo) => new SuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result Succeeded(DebugInfo debugInfo) => new SuccessfulResult {DebugInfo = debugInfo};

    public static Result Faulted(DebugInfo debugInfo) => new FaultedResult {DebugInfo = debugInfo};

    public static Result<T> Faulted<T>(DebugInfo debugInfo) => new FaultedResult<T> {DebugInfo = debugInfo};
}