namespace HareDu.Core;

using System;

public record UnsuccessfulResult<T> :
    Result<T>
{
    public UnsuccessfulResult()
    {
        Data = default;
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public override bool HasData => Data is not null;
}

public record UnsuccessfulResult :
    Result
{
    public UnsuccessfulResult()
    {
        HasFaulted = false;
        Timestamp = DateTimeOffset.UtcNow;
    }
}