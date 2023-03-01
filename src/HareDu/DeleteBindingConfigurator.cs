namespace HareDu;

public interface DeleteBindingConfigurator
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

    /// <summary>
    /// Type of binding either exchange or queue.
    /// </summary>
    /// <param name="bindingType"></param>
    void BindingType(BindingType bindingType);

    /// <summary>
    /// Combination of routing key and hash of its arguments.
    /// </summary>
    /// <param name="propertiesKey"></param>
    void PropertiesKey(string propertiesKey);
}