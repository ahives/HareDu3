namespace HareDu.Core.Configuration;

using System;
using System.Threading;

/// <summary>
/// Provides methods for configuring an instance of the HareDu configuration.
/// </summary>
/// <remarks>
/// This class implements <see cref="IHareDuConfigProvider"/> to manage the configuration process
/// using a supplied configurator, validate it, and return the final <see cref="HareDuConfig"/>.
/// </remarks>
public class HareDuConfigProvider :
    IHareDuConfigProvider
{
    public HareDuConfig Configure(Action<HareDuConfigurator> configurator)
    {
        if (configurator is null)
            throw new HareDuConfigurationException($"{nameof(configurator)} is null.");
            
        var impl = new HareDuConfiguratorImpl();
        configurator(impl);

        HareDuConfig config = impl.Settings.Value;

        return Validate(config) ? config : throw new HareDuConfigurationException("Invalid configuration.");
    }

    bool Validate(HareDuConfig config) => Validate(config.Broker) && Validate(config.Diagnostics);

    static bool Validate(DiagnosticsConfig config) =>
        config?.Probes != null 
        && config.Probes.ConsumerUtilizationThreshold > 0 
        && config.Probes.HighConnectionClosureRateThreshold > 0 
        && config.Probes.HighConnectionCreationRateThreshold > 0 
        && config.Probes.MessageRedeliveryThresholdCoefficient > 0 
        && config.Probes.QueueHighFlowThreshold > 0 
        && config.Probes.QueueLowFlowThreshold > 0 
        && config.Probes.SocketUsageThresholdCoefficient > 0 
        && config.Probes.FileDescriptorUsageThresholdCoefficient > 0 
        && config.Probes.RuntimeProcessUsageThresholdCoefficient > 0;

    static bool Validate(BrokerConfig config)
           => !string.IsNullOrWhiteSpace(config.Url) &&
           config?.Behavior.MaxConcurrentRequests >= 1 &&
           config?.Behavior.RequestReplenishmentInterval >= 1 &&
           config?.Behavior.RequestsPerReplenishment >= 1;

        
    class HareDuConfiguratorImpl :
        HareDuConfigurator
    {
        DiagnosticsConfig _diagnosticsSettings;
        BrokerConfig _brokerConfig;

        public Lazy<HareDuConfig> Settings { get; }

        public HareDuConfiguratorImpl()
        {
            Settings = new Lazy<HareDuConfig>(() => new HareDuConfig{Broker = _brokerConfig, Diagnostics = _diagnosticsSettings}, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Diagnostics(Action<DiagnosticsConfigurator> configurator)
        {
            if (configurator is null)
                _diagnosticsSettings = ConfigCache.Default.Diagnostics;

            var impl = new DiagnosticsConfiguratorImpl();
            configurator?.Invoke(impl);

            DiagnosticsConfig config = impl.Settings.Value;

            _diagnosticsSettings = Validate(config) ? config : ConfigCache.Default.Diagnostics;
        }

        public void Broker(Action<BrokerConfigurator> configurator)
        {
            if (configurator is null)
                _brokerConfig = ConfigCache.Default.Broker;

            var impl = new BrokerConfiguratorImpl();
            configurator?.Invoke(impl);

            BrokerConfig config = impl.Settings.Value;

            _brokerConfig = Validate(config) ? config : ConfigCache.Default.Broker;
        }


        class BrokerConfiguratorImpl :
            BrokerConfigurator
        {
            string _url;
            TimeSpan _timeout;
            string _username;
            string _password;
            HareDuBehaviorConfig _behavior;

            public Lazy<BrokerConfig> Settings { get; }

            public BrokerConfiguratorImpl()
            {
                Settings = new Lazy<BrokerConfig>(
                    () => new()
                    {
                        Url = _url,
                        Timeout = _timeout,
                        Behavior = _behavior
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void ConnectTo(string url) => _url = url;

            public void TimeoutAfter(TimeSpan timeout) => _timeout = timeout;

            public void WithBehavior(Action<BehaviorConfigurator> configurator)
            {
                var impl = new BehaviorConfiguratorImpl();
                configurator?.Invoke(impl);

                _behavior = impl.Behavior.Value;
            }

            class BehaviorConfiguratorImpl :
                BehaviorConfigurator
            {
                int _maxConcurrentRequests;
                int _requestReplenishmentPeriod;
                int _requestsPerReplenishment;

                public Lazy<HareDuBehaviorConfig> Behavior { get; }

                public BehaviorConfiguratorImpl()
                {
                    Behavior = new Lazy<HareDuBehaviorConfig>(
                        () => new HareDuBehaviorConfig
                        {
                            MaxConcurrentRequests = _maxConcurrentRequests,
                            RequestReplenishmentInterval = _requestReplenishmentPeriod,
                            RequestsPerReplenishment = _requestsPerReplenishment
                        }, LazyThreadSafetyMode.PublicationOnly);
                }

                public void LimitRequests(int maxConcurrentRequests = 100, int requestsPerReplenishment = 100, int replenishInterval = 1)
                {
                    _maxConcurrentRequests = maxConcurrentRequests;
                    _requestReplenishmentPeriod = replenishInterval;
                    _requestsPerReplenishment = requestsPerReplenishment;
                }
            }
        }


        class DiagnosticProbesConfiguratorImpl :
            DiagnosticProbesConfigurator
        {
            uint _highClosureRateWarningThreshold;
            uint _highCreationRateWarningThreshold;
            uint _queueHighFlowThreshold;
            uint _queueLowFlowThreshold;
            decimal _messageRedeliveryCoefficient;
            decimal _socketUsageCoefficient;
            decimal _runtimeProcessUsageCoefficient;
            decimal _fileDescriptorUsageWarningCoefficient;
            decimal _consumerUtilizationWarningCoefficient;

            public Lazy<ProbesConfig> Settings { get; }

            public DiagnosticProbesConfiguratorImpl()
            {
                Settings = new Lazy<ProbesConfig>(
                    () => new ProbesConfig()
                    {
                        HighConnectionClosureRateThreshold = _highClosureRateWarningThreshold,
                        HighConnectionCreationRateThreshold = _highCreationRateWarningThreshold,
                        QueueHighFlowThreshold = _queueHighFlowThreshold,
                        QueueLowFlowThreshold = _queueLowFlowThreshold,
                        MessageRedeliveryThresholdCoefficient = _messageRedeliveryCoefficient,
                        SocketUsageThresholdCoefficient = _socketUsageCoefficient,
                        RuntimeProcessUsageThresholdCoefficient = _runtimeProcessUsageCoefficient,
                        FileDescriptorUsageThresholdCoefficient = _fileDescriptorUsageWarningCoefficient,
                        ConsumerUtilizationThreshold = _consumerUtilizationWarningCoefficient
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void SetHighConnectionClosureRateThreshold(uint threshold) => _highClosureRateWarningThreshold = threshold;

            public void SetHighConnectionCreationRateThreshold(uint threshold) => _highCreationRateWarningThreshold = threshold;

            public void SetQueueHighFlowThreshold(uint threshold) => _queueHighFlowThreshold = threshold;

            public void SetQueueLowFlowThreshold(uint threshold) => _queueLowFlowThreshold = threshold;

            public void SetMessageRedeliveryThresholdCoefficient(decimal coefficient) => _messageRedeliveryCoefficient = coefficient;

            public void SetSocketUsageThresholdCoefficient(decimal coefficient) => _socketUsageCoefficient = coefficient;

            public void SetRuntimeProcessUsageThresholdCoefficient(decimal coefficient) => _runtimeProcessUsageCoefficient = coefficient;

            public void SetFileDescriptorUsageThresholdCoefficient(decimal coefficient) => _fileDescriptorUsageWarningCoefficient = coefficient;

            public void SetConsumerUtilizationThreshold(decimal threshold) => _consumerUtilizationWarningCoefficient = threshold;
        }

            
        class DiagnosticsConfiguratorImpl :
            DiagnosticsConfigurator
        {
            ProbesConfig _probes;
                
            public Lazy<DiagnosticsConfig> Settings { get; }

            public DiagnosticsConfiguratorImpl()
            {
                Settings = new Lazy<DiagnosticsConfig>(() => new DiagnosticsConfig{Probes = _probes}, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Probes(Action<DiagnosticProbesConfigurator> configurator)
            {
                var impl = new DiagnosticProbesConfiguratorImpl();
                configurator?.Invoke(impl);

                _probes = impl.Settings.Value;
            }
        }
    }
}