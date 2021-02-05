namespace HareDu.Core.Configuration
{
    using System;
    using System.Threading;
    using Extensions;

    public class HareDuConfigProvider :
        IHareDuConfigProvider
    {
        public HareDuConfig Configure(Action<HareDuConfigurator> configurator)
        {
            if (configurator == null)
                return ConfigCache.Default;
            
            var impl = new HareDuConfiguratorImpl();
            configurator(impl);

            HareDuConfig config = impl.Settings.Value;

            return Validate(config) ? config : ConfigCache.Default;
        }

        bool Validate(HareDuConfig config) => Validate(config.Broker) && Validate(config.Diagnostics);

        static bool Validate(DiagnosticsConfig config) =>
            config.IsNotNull()
            && config.Probes.IsNotNull()
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
            => config.IsNotNull() &&
                config.Credentials.IsNotNull() &&
                !string.IsNullOrWhiteSpace(config.Credentials.Username) &&
                !string.IsNullOrWhiteSpace(config.Credentials.Password) &&
                !string.IsNullOrWhiteSpace(config.Url);

        
        class HareDuConfiguratorImpl :
            HareDuConfigurator
        {
            DiagnosticsConfig _diagnosticsSettings;
            BrokerConfig _brokerConfig;

            public Lazy<HareDuConfig> Settings { get; }

            public HareDuConfiguratorImpl()
            {
                Settings =
                    new Lazy<HareDuConfig>(() => new HareDuConfig{Broker = _brokerConfig, Diagnostics = _diagnosticsSettings}, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Diagnostics(Action<DiagnosticsConfigurator> configurator)
            {
                if (configurator == null)
                    _diagnosticsSettings = ConfigCache.Default.Diagnostics;

                var impl = new DiagnosticsConfiguratorImpl();
                configurator(impl);

                DiagnosticsConfig config = impl.Settings.Value;

                _diagnosticsSettings = Validate(config) ? config : ConfigCache.Default.Diagnostics;
            }

            public void Broker(Action<BrokerConfigurator> configurator)
            {
                if (configurator == null)
                    _brokerConfig = ConfigCache.Default.Broker;

                var impl = new BrokerConfiguratorImpl();
                configurator(impl);

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

                public Lazy<BrokerConfig> Settings { get; }

                public BrokerConfiguratorImpl()
                {
                    Settings = new Lazy<BrokerConfig>(
                        () => new BrokerConfig
                        {
                            Url = _url,
                            Timeout = _timeout,
                            Credentials = !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password)
                                ? new() {Username = _username, Password = _password}
                                : default
                        }, LazyThreadSafetyMode.PublicationOnly);
                }

                public void ConnectTo(string url) => _url = url;

                public void TimeoutAfter(TimeSpan timeout) => _timeout = timeout;

                public void UsingCredentials(string username, string password)
                {
                    _username = username;
                    _password = password;
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

                public void SetHighConnectionClosureRateThreshold(uint value) => _highClosureRateWarningThreshold = value;

                public void SetHighConnectionCreationRateThreshold(uint value) => _highCreationRateWarningThreshold = value;

                public void SetQueueHighFlowThreshold(uint value) => _queueHighFlowThreshold = value;

                public void SetQueueLowFlowThreshold(uint value) => _queueLowFlowThreshold = value;

                public void SetMessageRedeliveryThresholdCoefficient(decimal value) =>
                    _messageRedeliveryCoefficient = value;

                public void SetSocketUsageThresholdCoefficient(decimal value) => _socketUsageCoefficient = value;

                public void SetRuntimeProcessUsageThresholdCoefficient(decimal value) =>
                    _runtimeProcessUsageCoefficient = value;

                public void SetFileDescriptorUsageThresholdCoefficient(decimal value) =>
                    _fileDescriptorUsageWarningCoefficient = value;

                public void SetConsumerUtilizationThreshold(decimal value) =>
                    _consumerUtilizationWarningCoefficient = value;
            }

            
            class DiagnosticsConfiguratorImpl :
                DiagnosticsConfigurator
            {
                ProbesConfig _probes;
                
                public Lazy<DiagnosticsConfig> Settings { get; }

                public DiagnosticsConfiguratorImpl()
                {
                    Settings = new Lazy<DiagnosticsConfig>(
                        () => new DiagnosticsConfig{Probes = _probes}, LazyThreadSafetyMode.PublicationOnly);
                }

                public void Probes(Action<DiagnosticProbesConfigurator> configurator)
                {
                    var impl = new DiagnosticProbesConfiguratorImpl();
                    configurator(impl);

                    _probes = impl.Settings.Value;
                }
            }
        }
    }
}