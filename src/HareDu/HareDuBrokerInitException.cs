namespace HareDu;

using System;
using System.Runtime.Serialization;

/// <summary>
/// Represents errors that occur during the initialization of a broker within the HareDu library.
/// </summary>
/// <remarks>
/// This exception is typically thrown when the HareDu library is unable to locate the implementation of a required component,
/// or there are issues associated with the initialization process of a connection to the RabbitMQ broker.
/// </remarks>
/// <seealso cref="System.Exception" />
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