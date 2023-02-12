namespace HareDu;

using System;
using System.Runtime.Serialization;

public class HareDuBrokerApiInitException :
    Exception
{
    public HareDuBrokerApiInitException()
    {
    }

    protected HareDuBrokerApiInitException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public HareDuBrokerApiInitException(string message)
        : base(message)
    {
    }

    public HareDuBrokerApiInitException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}