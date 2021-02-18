namespace HareDu
{
    using System;

    public interface DeleteQueueConfigurator
    {
        /// <summary>
        /// Specify acceptable conditions for which the queue can be deleted.
        /// </summary>
        /// <param name="condition"></param>
        void When(Action<QueueDeleteCondition> condition);
    }
}