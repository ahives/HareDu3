namespace HareDu;

public interface UserLimitConfigurator
{
    /// <summary>
    /// Set the RabbitMQ user limit value.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="value"></param>
    void SetLimit(UserLimit limit, ulong value);
}