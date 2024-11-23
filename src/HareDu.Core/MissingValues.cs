namespace HareDu.Core;

using System.Collections.Generic;

internal record MissingValues<T> :
    Results<T>
{
    public override bool HasData => false;
    public override bool HasFaulted => false;
    public override IReadOnlyList<T> Data => throw new ResultEmptyException("The value is empty.");
}