namespace HareDu.Shovel;

using Core;

internal static class Responses
{
    public static Results<T> Panic<T>(DebugInfo debugInfo) => new UnsuccessfulResults<T> {DebugInfo = debugInfo};
}