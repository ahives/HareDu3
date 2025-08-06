namespace HareDu;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides configuration options for unbinding resources such as exchanges or queues in the broker.
/// </summary>
public interface UnbindingConfigurator
{
    /// <summary>
    /// Source binding of the exchange/queue.
    /// </summary>
    /// <param name="source">The source exchange or queue to unbind.</param>
    void Source([NotNull] string source);

    /// <summary>
    /// Destination binding of the exchange/queue.
    /// </summary>
    /// <param name="destination">The name of the destination where the binding will be applied.</param>
    void Destination([NotNull] string destination);
}