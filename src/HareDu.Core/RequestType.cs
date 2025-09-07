namespace HareDu.Core;

/// <summary>
/// Represents the various types of errors that can occur during the execution of a process or operation.
/// </summary>
public enum RequestType
{
    /// <summary>
    /// Represents a request type associated with the validation of inputs or configurations.
    /// Typically used to specify errors or issues detected during the validation process.
    /// </summary>
    Validation,

    /// <summary>
    /// Represents a request type related to interactions with a web server.
    /// Typically used to identify operations associated with the web server configuration or status retrieval.
    /// </summary>
    WebServer,

    /// <summary>
    /// Represents a request type associated with the broker in the context of RabbitMQ operations.
    /// Typically used for performing actions or retrieving information directly related to the broker's functionality.
    /// </summary>
    Broker,

    /// <summary>
    /// Represents a request type associated with operations or actions performed on message queues.
    /// Typically used for managing and monitoring queue-specific behaviors or states.
    /// </summary>
    Queue,

    /// <summary>
    /// Represents a request type associated with operations or interactions concerning exchanges.
    /// Typically used to specify activities related to exchange creation, deletion, or retrieval.
    /// </summary>
    Exchange,

    /// <summary>
    /// Represents a request type associated with the management of bindings in a messaging system.
    /// Typically used to perform operations or retrieve information related to bindings between queues and exchanges.
    /// </summary>
    Binding,

    /// <summary>
    /// Represents a request type related to the management and monitoring of connections.
    /// Typically used to query or handle operations involving client-to-broker connections.
    /// </summary>
    Connection,

    /// <summary>
    /// Represents a request type associated with operations or interactions
    /// related to channels within the context of messaging or brokerage systems.
    /// Typically used to specify operations such as channel management or diagnostics.
    /// </summary>
    Channel,

    /// <summary>
    /// Represents a request type related to the operations or activities involving consumers.
    /// Typically used to specify tasks such as monitoring or managing consumer behavior in messaging systems.
    /// </summary>
    Consumer,

    /// <summary>
    /// Represents a request type associated with message-related operations or processing.
    /// Typically used in scenarios involving actions, management, or events pertaining to messages.
    /// </summary>
    Message,

    /// <summary>
    /// Represents a request type associated with virtual host operations in a messaging broker.
    /// Typically used to retrieve or modify data related to virtual hosts within the system.
    /// </summary>
    VirtualHost,

    /// <summary>
    /// Represents a request type related to operations or configurations involving RabbitMQ shovels.
    /// Typically used for managing or querying shovel settings, movements, or states.
    /// </summary>
    Shovel,

    /// <summary>
    /// Represents a request type associated with operations or actions related to a user entity.
    /// Typically used to manage or interact with user-specific configurations or credentials.
    /// </summary>
    User,

    /// <summary>
    /// Represents a request type associated with defining or retrieving permissions for topics.
    /// Typically used in operations where access to specific topics needs to be managed or verified.
    /// </summary>
    TopicPermissions,

    /// <summary>
    /// Represents a request type associated with managing or interacting with parameters
    /// within a specific scope, such as system-wide or entity-specific configurations.
    /// Typically used for operations involving scoped configuration parameters.
    /// </summary>
    ScopeParameter,

    /// <summary>
    /// Represents a request type associated with the management or retrieval of policies.
    /// Typically used in scenarios involving policy definitions, updates, or deletions.
    /// </summary>
    Policy,

    /// <summary>
    /// Represents a request type associated with the policies enforced by operators.
    /// Typically used to manage or define operator-specific configurations and rules.
    /// </summary>
    OperatorPolicy,

    /// <summary>
    /// Represents a request type associated with operations or inquiries concerning a node.
    /// Typically used to handle or fetch data related to a specific node in the system.
    /// </summary>
    Node,

    /// <summary>
    /// Represents a request type related to the management or retrieval of global parameters.
    /// Typically utilized in operations involving globally scoped configurations or settings.
    /// </summary>
    GlobalParameter,

    /// <summary>
    /// Represents a request type related to user authentication or authorization processes.
    /// Typically used for identifying requests that handle login, credential verification,
    /// or the enforcement of access control policies.
    /// </summary>
    Authentication
}