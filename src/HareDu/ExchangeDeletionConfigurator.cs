namespace HareDu;

/// <summary>
/// Configures the conditions under which an exchange can be deleted in RabbitMQ.
/// </summary>
public interface ExchangeDeletionConfigurator
{
    /// <summary>
    /// Specifies that the exchange should only be deleted if it is unused.
    /// </summary>
    void WhenUnused();
}