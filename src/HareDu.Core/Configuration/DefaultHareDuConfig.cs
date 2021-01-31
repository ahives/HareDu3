namespace HareDu.Core.Configuration
{
    public record DefaultHareDuConfig :
        HareDuConfig
    {
        public DefaultHareDuConfig()
        {
            Diagnostics = new DiagnosticsConfig()
            {
                Probes = new ProbesConfig()
                {
                    SocketUsageThresholdCoefficient = 0.50M,
                    MessageRedeliveryThresholdCoefficient = 1M,
                    HighConnectionClosureRateThreshold = 100,
                    HighConnectionCreationRateThreshold = 100,
                    RuntimeProcessUsageThresholdCoefficient = 0.7M,
                    FileDescriptorUsageThresholdCoefficient = 0.7M,
                    ConsumerUtilizationThreshold = 0.50M,
                    QueueLowFlowThreshold = 20,
                    QueueHighFlowThreshold = 100
                }
            };
            Broker = new BrokerConfig()
            {
                Url = "http://localhost:15672",
                Credentials = new BrokerCredentials{Username = "guest", Password = "guest"}
            };
        }
    }
}