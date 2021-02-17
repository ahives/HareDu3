namespace HareDu.Extensions
{
    using System;

    public static class EnumConversionExtensions
    {
        public static string Convert(this PolicyAppliedTo appliedTo)
        {
            switch (appliedTo)
            {
                case PolicyAppliedTo.All:
                    return "all";
                
                case PolicyAppliedTo.Queues:
                    return "queues";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(appliedTo), appliedTo, null);
            }
        }
        
        public static string Convert(this MessageEncoding encoding)
        {
            switch (encoding)
            {
                case MessageEncoding.Auto:
                    return "auto";

                case MessageEncoding.Base64:
                    return "base64";

                default:
                    throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null);
            }
        }
        
        public static string Convert(this RequeueMode mode)
        {
            switch (mode)
            {
                case RequeueMode.DoNotAckRequeue:
                    return "ack_requeue_false";

                case RequeueMode.RejectRequeue:
                    return "reject_requeue_true";

                case RequeueMode.DoNotRejectRequeue:
                    return "reject_requeue_false";

                default:
                    return "ack_requeue_true";
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
        
        public static string Convert(this Protocol protocol)
        {
            switch (protocol)
            {
                case Protocol.Amqp091:
                    return "amqp091";
                
                case Protocol.Amqp10:
                    return "amqp10";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(protocol), protocol, null);
            }
        }

        public static string Convert(this AcknowledgeMode mode)
        {
            switch (mode)
            {
                case AcknowledgeMode.OnConfirm:
                    return "on-confirm";
                
                case AcknowledgeMode.OnPublish:
                    return "on-publish";
                
                case AcknowledgeMode.NoAcknowledgement:
                    return "no-ack";
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}