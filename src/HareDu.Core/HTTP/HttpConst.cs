namespace HareDu.Core.HTTP;

public record HttpConst
{
    public string Value { get; }

    private HttpConst(string value)
    {
        Value = value;
    }

    public static HttpConst BrokerClient = new("broker");
}