namespace HareDu.Core;

internal record MissingValue<T> :
    Result<T>
{
    public override bool HasData => false;
    public override bool HasFaulted => false;
    public override T Data => throw new ResultEmptyException("The value is empty.");
}