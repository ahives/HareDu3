namespace HareDu.Shovel;

using Core;

internal static class Response
{
    public static Result Panic(DebugInfo debugInfo) => new UnsuccessfulResult {DebugInfo = debugInfo};

    public static Result<T> Panic<T>(DebugInfo debugInfo) => new UnsuccessfulResult<T> {DebugInfo = debugInfo};
}