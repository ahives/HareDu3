namespace HareDu;

using System.Diagnostics.CodeAnalysis;

public interface QueueArgumentConfigurator
{
    /// <summary>
    /// Add a new argument.
    /// </summary>
    /// <param name="arg"></param>
    /// <param name="value"></param>
    /// <typeparam name="T"></typeparam>
    void Set<T>([NotNull] string arg, [NotNull] T value);
        
    /// <summary>
    /// Set 'x-expires' argument.
    /// </summary>
    /// <param name="milliseconds">Number of milliseconds to set queue expiration</param>
    void SetQueueExpiration([NotNull] ulong milliseconds);
        
    /// <summary>
    /// Set 'x-message-ttl' argument.
    /// </summary>
    /// <param name="milliseconds"></param>
    void SetPerQueuedMessageExpiration([NotNull] ulong milliseconds);
        
    /// <summary>
    /// Set 'x-dead-letter-exchange' argument.
    /// </summary>
    /// <param name="exchange"></param>
    void SetDeadLetterExchange([NotNull] string exchange);
        
    /// <summary>
    /// Set 'x-dead-letter-routing-key' argument.
    /// </summary>
    /// <param name="routingKey"></param>
    void SetDeadLetterExchangeRoutingKey([NotNull] string routingKey);
        
    /// <summary>
    /// Set 'alternate-exchange' argument.
    /// </summary>
    /// <param name="exchange"></param>
    void SetAlternateExchange([NotNull] string exchange);
}