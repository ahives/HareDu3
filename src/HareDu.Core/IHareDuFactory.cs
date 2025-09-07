namespace HareDu.Core;

using System;
using System.Diagnostics.CodeAnalysis;
using Security;

/// <summary>
/// Represents a factory interface for creating objects that provide access to RabbitMQ server resources
/// via its REST API. The objects created by this interface expose various RabbitMQ resources like
/// Virtual Hosts, Exchanges, Queues, Users, etc.
/// </summary>
public interface IHareDuFactory
{
    /// <summary>
    /// Creates an instance of an object implementing the specified type <typeparamref name="T"/>, which provides access to a group of resources
    /// (e.g., Virtual Hosts, Exchanges, Queues, Users, etc.) exposed by the RabbitMQ server via its REST API.
    /// </summary>
    /// <typeparam name="T">The interface that derives from the base interface <see cref="HareDuAPI"/>.</typeparam>
    /// <param name="credentials">A configuration action used to supply the credentials required to access the RabbitMQ server.</param>
    /// <returns>An instance of the specified type <typeparamref name="T"/> providing access to the RabbitMQ server resources.</returns>
    /// <exception cref="HareDuInitException">Thrown if HareDu could not find the implementation associated with a channel.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    T API<T>([NotNull] Action<HareDuCredentialProvider> credentials)
        where T : HareDuAPI;
}