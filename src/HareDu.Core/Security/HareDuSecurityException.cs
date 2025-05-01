namespace HareDu.Core.Security;

using System;
using System.Runtime.Serialization;

public class HareDuSecurityException :
    Exception
{
    public HareDuSecurityException()
    {
    }

    protected HareDuSecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public HareDuSecurityException(string message) : base(message)
    {
    }

    public HareDuSecurityException(string message, Exception innerException) : base(message, innerException)
    {
    }
}