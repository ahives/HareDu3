namespace HareDu.Model;

/// <summary>
/// Represents a communication protocol configuration. Each protocol specifies
/// a particular method of communication for messaging systems.
/// </summary>
public record Protocol
{
    public string Value { get; }

    private Protocol(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Represents the AMQP 0.9.1 protocol configuration. The AMQP (Advanced Message Queuing Protocol)
    /// is an open standard application layer protocol for message-oriented middleware. This specific
    /// configuration denotes the 0.9.1 version of the protocol, commonly used in RabbitMQ messaging systems.
    /// </summary>
    public static Protocol AMQP091 = new("amqp091");

    /// <summary>
    /// Represents the AMQP 1.0 protocol configuration. The AMQP (Advanced Message Queuing Protocol) 1.0
    /// is an open standard protocol that provides message-oriented middleware solutions known for
    /// interoperability, reliability, and flexibility in distributed systems.
    /// </summary>
    public static Protocol AMQP10 = new("amqp10");

    /// <summary>
    /// Represents the MQTT protocol configuration. MQTT (Message Queuing Telemetry Transport)
    /// is a lightweight messaging protocol designed for constrained devices and low-bandwidth,
    /// high-latency, or unreliable networks, often used in IoT (Internet of Things) applications.
    /// </summary>
    public static Protocol MQTT = new("mqtt");

    /// <summary>
    /// Represents the STOMP protocol configuration. STOMP (Simple/Streaming Text Oriented Messaging Protocol)
    /// is a lightweight, text-based protocol designed for interoperability between messaging clients
    /// and message brokers. It is commonly used for asynchronous messaging and provides a simple way
    /// to communicate over a variety of messaging systems.
    /// </summary>
    public static Protocol STOMP = new("stomp");

    /// <summary>
    /// Represents the Web MQTT protocol configuration. This protocol is an extension that enables
    /// MQTT (Message Queuing Telemetry Transport) communication over WebSocket, allowing it to be
    /// utilized in environments such as web browsers for web-based messaging systems.
    /// </summary>
    public static Protocol WEBMQTT = new("web-mqtt");

    /// <summary>
    /// Represents the WebSTOMP protocol configuration. WebSTOMP, or WebSocket STOMP, extends the STOMP protocol
    /// by enabling communication over WebSocket, providing a lightweight and efficient way to implement real-time
    /// communication in messaging systems. This configuration is commonly used in RabbitMQ for WebSocket-based clients.
    /// </summary>
    public static Protocol WEBSTOMP = new("web-stomp");
}