namespace HareDu;

public interface UnbindingConfigurator
{
    /// <summary>
    /// Source binding of the exchange/queue.
    /// </summary>
    /// <param name="source"></param>
    void Source(string source);

    /// <summary>
    /// Destination binding of the exchange/queue.
    /// </summary>
    /// <param name="destination"></param>
    void Destination(string destination);
}