namespace HareDu.Core;

using System.Collections.Generic;

public static class Responses
{
    public static Results<T> Failed<T>(DebugInfo debugInfo) => new UnsuccessfulResults<T> {DebugInfo = debugInfo};

    public static Results<T> Succeeded<T>(IReadOnlyList<T> data, DebugInfo debugInfo) => new SuccessfulResults<T> {Data = data, DebugInfo = debugInfo};

    public static Results<T> Faulted<T>(DebugInfo debugInfo) => new FaultedResults<T> {DebugInfo = debugInfo};

    public static Results<T> Panic<T>(DebugInfo debugInfo) => new UnsuccessfulResults<T> {DebugInfo = debugInfo};
}