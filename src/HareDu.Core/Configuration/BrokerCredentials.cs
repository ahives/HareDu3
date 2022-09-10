namespace HareDu.Core.Configuration;

public record BrokerCredentials
{
    public string Username { get; init; }
        
    public string Password { get; init; }
}