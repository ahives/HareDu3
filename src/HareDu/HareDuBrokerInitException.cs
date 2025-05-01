namespace HareDu;

using System;
using System.Runtime.Serialization;

public class HareDuBrokerInitException :
    Exception
{
    public HareDuBrokerInitException()
    {
    }

    protected HareDuBrokerInitException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public HareDuBrokerInitException(string message)
        : base(message)
    {
    }

    public HareDuBrokerInitException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}