namespace HareDu
{
    public interface VirtualHostLimitsConfiguration
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