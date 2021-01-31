namespace HareDu.Core.Configuration
{
    using System;

    public record BrokerConfig
    {
        public string Url { get; init; }
        
        public TimeSpan Timeout { get; init; }
        
        public BrokerCredentials Credentials { get; init; }
    }
}