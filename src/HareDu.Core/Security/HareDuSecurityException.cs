namespace HareDu.Core.Security;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Represents errors that occur when there is a security-related issue in HareDu operations.
/// </summary>
public class HareDuSecurityException :
    Exception
{
    public HareDuSecurityException()
    {
    }

    protected HareDuSecurityException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public HareDuSecurityException(string message)
        : base(message)
    {
    }

    public HareDuSecurityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}