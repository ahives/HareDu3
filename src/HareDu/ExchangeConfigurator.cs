namespace HareDu;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Provides methods to configure an exchange in a messaging system.
/// </summary>
public interface ExchangeConfigurator
{
    /// <summary>
    /// Specifies the routing type of the exchange.
    /// </summary>
    /// <param name="routingType">The routing type to be applied to the exchange.</param>
    void WithRoutingType([NotNull] RoutingType routingType);

    /// <summary>
    /// Specify that the exchange is durable.
    /// </summary>
    void IsDurable();

    /// <summary>
    /// Specify that the exchange is for internal use only.
    /// </summary>
    void IsForInternalUse();

    /// <summary>
    /// Specify one or more arguments for the exchange configuration.
    /// </summary>
    /// <param name="arguments">Callback action to define the exchange arguments.</param>
    void HasArguments([NotNull] Action<ExchangeArgumentConfigurator> arguments);

    /// <summary>
    /// Specify that the exchange will be deleted when there are no consumers.
    /// </summary>
    void AutoDeleteWhenNotInUse();
}