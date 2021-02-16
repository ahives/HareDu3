namespace HareDu
{
    using System;

    public interface DeleteExchangeCriteria
    {
        /// <summary>
        /// Specify the conditions for which the exchange can be deleted.
        /// </summary>
        /// <param name="condition"></param>
        void When(Action<DeleteExchangeCondition> condition);
    }
}