namespace HareDu.Model;

public record UserAccessTag
{
    public string Value { get; }

    private UserAccessTag(string value)
    {
        Value = value;
    }

    public static UserAccessTag Administrator = new("administrator");
    public static UserAccessTag Monitoring = new("monitoring");
    public static UserAccessTag Management = new("management");
    public static UserAccessTag PolicyMaker = new("policymaker");
    public static UserAccessTag Impersonator = new("impersonator");
    public static UserAccessTag None = new(string.Empty);
}