namespace HareDu;

using Core;

internal static class Response
{
    public static Result<T> Failed<T>(T data, DebugInfo debugInfo) => new UnsuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result<T> Succeeded<T>(T data, DebugInfo debugInfo) => new SuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result<T> Panic<T>(T data, DebugInfo debugInfo) => new UnsuccessfulResult<T> {Data = data, DebugInfo = debugInfo};

    public static Result Panic(DebugInfo debugInfo) => new UnsuccessfulResult {DebugInfo = debugInfo};

    public static Result<T> Panic<T>(DebugInfo debugInfo) => new UnsuccessfulResult<T> {DebugInfo = debugInfo};
}