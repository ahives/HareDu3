namespace HareDu.Extensions
{
    using System;

    public static class EnumConversionExtensions
    {
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