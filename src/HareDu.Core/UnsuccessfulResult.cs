using System;

namespace HareDu.Core;

public record UnsuccessfulResult :
    Result
{
    public UnsuccessfulResult()
    {
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }
}

public record UnsuccessfulResult<T> :
    Result<T>
{
    public UnsuccessfulResult()
    {
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public override bool HasData => Data is not null;
}