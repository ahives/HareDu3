namespace HareDu
{
    public interface VirtualHostLimitsConfigurator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void SetMaxConnectionLimit(ulong value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void SetMaxQueueLimit(ulong value);
    }
}