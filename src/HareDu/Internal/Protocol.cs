namespace HareDu.Internal;

public record Protocol
{
    public string Value { get; }

    private Protocol(string value)
    {
        Value = value;
    }
    
    public static Protocol AMQP091 = new("amqp091");
    public static Protocol AMQP10 = new("amqp10");
    public static Protocol MQTT = new("mqtt");
    public static Protocol STOMP = new("stomp");
    public static Protocol WEBMQTT = new("web-mqtt");
    public static Protocol WEBSTOMP = new("web-stomp");
}