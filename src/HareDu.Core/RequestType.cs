namespace HareDu.Core;

/// <summary>
/// Represents the various types of errors that can occur during the execution of a process or operation.
/// </summary>
public enum RequestType
{
    Validation,
    WebServer,
    Broker,
    Queue,
    Exchange,
    Binding,
    Connection,
    Channel,
    Consumer,
    Message,
    VirtualHost,
    Shovel,
    User,
    TopicPermissions,
    ScopeParameter,
    Policy,
    OperatorPolicy,
    Node,
    GlobalParameter,
    Authentication
}