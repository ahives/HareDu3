namespace HareDu.Core;

using System.Collections.Generic;

public record DebugInfo
{
    public string URL { get; init; }

    public string Request { get; init; }
        
    public string Exception { get; init; }
        
    public string StackTrace { get; init; }
        
    public string Response { get; init; }

    public IReadOnlyList<Error> Errors { get; init; }
}