namespace HareDu.Core;

using System;
using System.Runtime.Serialization;

public class ResultEmptyException : Exception
{
    public ResultEmptyException()
    {
    }

    protected ResultEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ResultEmptyException(string message) : base(message)
    {
    }

    public ResultEmptyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}