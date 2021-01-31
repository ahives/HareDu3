namespace HareDu
{
    public interface QueuePeekConfiguration
    {
        /// <summary>
        /// Specify how many messages to take from the queue.
        /// </summary>
        /// <param name="count"></param>
        void Take(uint count);

        /// <summary>
        /// Specify how to encode messages when requeued. 
        /// </summary>
        /// <param name="encoding"></param>
        void Encoding(MessageEncoding encoding);

        /// <summary>
        /// Specify the size of messages in bytes that are acceptable before having to truncate.
        /// </summary>
        /// <param name="bytes"></param>
        void TruncateIfAbove(uint bytes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        void AckMode(RequeueMode mode);
    }
}