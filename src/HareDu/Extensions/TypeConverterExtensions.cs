namespace HareDu.Extensions
{
    public static class TypeConverterExtensions
    {
        public static ulong ToLong(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ulong.MaxValue;
            
            if (value.Equals("infinity"))
                return ulong.MaxValue;

            return ulong.TryParse(value, out ulong result) ? result : ulong.MaxValue;
        }
        
        public static string ToRequeueModeString(this RequeueMode mode)
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
    }
}