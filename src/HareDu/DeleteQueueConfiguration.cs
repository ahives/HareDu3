namespace HareDu
{
    using System;

    public interface DeleteQueueConfiguration
    {
        /// <summary>
        /// Specify acceptable conditions for which the queue can be deleted.
        /// </summary>
        /// <param name="condition"></param>
        void When(Action<QueueDeleteCondition> condition);
    }
}