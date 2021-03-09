namespace HareDu.Extensions
{
    using System;

    public static class EnumConversionExtensions
    {
        public static string Convert(this DeleteShovelAfterMode mode)
        {
            switch (mode)
            {
                case DeleteShovelAfterMode.Never:
                    return "never";
                
                case DeleteShovelAfterMode.QueueLength:
                    return "queue-length";
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string Convert(this PolicyAppliedTo appliedTo)
        {
            switch (appliedTo)
            {
                case PolicyAppliedTo.All:
                    return "all";
                
                case PolicyAppliedTo.Queues:
                    return "queues";

                case PolicyAppliedTo.Exchanges:
                    return "exchanges";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(appliedTo), appliedTo, null);
            }
        }

        public static string Convert(this OperatorPolicyAppliedTo appliedTo)
        {
            switch (appliedTo)
            {
                case OperatorPolicyAppliedTo.Queues:
                    return "queues";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(appliedTo), appliedTo, null);
            }
        }

        public static string Convert(this ExchangeRoutingType routingType)
        {
            switch (routingType)
            {
                case ExchangeRoutingType.Fanout:
                    return "fanout";

                case ExchangeRoutingType.Direct:
                    return "direct";

                case ExchangeRoutingType.Topic:
                    return "topic";

                case ExchangeRoutingType.Headers:
                    return "headers";

                case ExchangeRoutingType.Federated:
                    return "federated";

                case ExchangeRoutingType.Match:
                    return "match";

                default:
                    throw new ArgumentOutOfRangeException(nameof(routingType), routingType, null);
            }
        }

        public static string Convert(this ShovelProtocolType shovelProtocolType)
        {
            switch (shovelProtocolType)
            {
                case ShovelProtocolType.Amqp091:
                    return "amqp091";
                
                case ShovelProtocolType.Amqp10:
                    return "amqp10";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(shovelProtocolType), shovelProtocolType, null);
            }
        }

        public static string Convert(this AckMode mode)
        {
            switch (mode)
            {
                case AckMode.OnConfirm:
                    return "on-confirm";
                
                case AckMode.OnPublish:
                    return "on-publish";
                
                case AckMode.NoAck:
                    return "no-ack";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string Convert(this HighAvailabilityModes mode)
        {
            switch (mode)
            {
                case HighAvailabilityModes.All:
                    return "all";
                    
                case HighAvailabilityModes.Exactly:
                    return "exactly";
                    
                case HighAvailabilityModes.Nodes:
                    return "nodes";
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static HighAvailabilityModes Convert(this string mode)
        {
            switch (mode.ToLower())
            {
                case "all":
                    return HighAvailabilityModes.All;
                    
                case "exactly":
                    return HighAvailabilityModes.Exactly;
                    
                case "nodes":
                    return HighAvailabilityModes.Nodes;
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string Convert(this HighAvailabilitySyncMode mode)
        {
            switch (mode)
            {
                case HighAvailabilitySyncMode.Manual:
                    return "manual";
                    
                case HighAvailabilitySyncMode.Automatic:
                    return "automatic";
                    
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string Convert(this QueueMode mode)
        {
            switch (mode)
            {
                case QueueMode.Default:
                    return "default";
                
                case QueueMode.Lazy:
                    return "lazy";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string Convert(this QueuePromotionFailureMode mode)
        {
            switch (mode)
            {
                case QueuePromotionFailureMode.Always:
                    return "always";
                
                case QueuePromotionFailureMode.WhenSynced:
                    return "when-synced";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static string Convert(this QueuePromotionShutdownMode mode)
        {
            switch (mode)
            {
                case QueuePromotionShutdownMode.Always:
                    return "always";
                
                case QueuePromotionShutdownMode.WhenSynced:
                    return "when-synced";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}