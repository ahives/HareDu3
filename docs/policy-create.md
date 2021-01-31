# Creating Policies

The Broker API allows you to create a queue on the RabbitMQ broker. To do so is pretty simple with HareDu 2. You can do it yourself or the IoC way.

**Do It Yourself**

```csharp
var result = await new BrokerObjectFactory(config)
                .Object<Policy>()
                .Create(x =>
                {
                    x.Policy("your_policy");
                    x.Configure(p =>
                    {
                        p.UsingPattern("your_regex");
                        p.HasArguments(d =>
                        {
                            ...
                        });
                    });
                    x.Targeting(t => t.VirtualHost("your_vhost"));
                });
```
<br>

**Autofac**

```csharp
var result = await _container.Resolve<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create(x =>
                {
                    x.Policy("your_policy");
                    x.Configure(p =>
                    {
                        p.UsingPattern("your_regex");
                        p.HasArguments(d =>
                        {
                            ...
                        });
                    });
                    x.Targeting(t => t.VirtualHost("your_vhost"));
                });
```
<br>

**.NET Core DI**

```csharp
var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create(x =>
                {
                    x.Policy("your_policy");
                    x.Configure(p =>
                    {
                        p.UsingPattern("your_regex");
                        p.HasArguments(d =>
                        {
                            ...
                        });
                    });
                    x.Targeting(t => t.VirtualHost("your_vhost"));
                });
```
<br>

RabbitMQ supports the concept of [durability](https://www.rabbitmq.com/queues.html), which means that if the broker restarts the queues will survive. To configure a queue to be durable during creation, add the ```IsDurable``` method within ```Configure``` like so...

```csharp
c.IsDurable();
```
<br>

HareDu 2 supports the below RabbitMQ arguments during queue creation.

<br>

| Argument | Method |
| --- | --- |
| [expires](https://www.rabbitmq.com/ttl.html#queue-ttl) | SetQueueExpiration |
| [message-ttl](https://www.rabbitmq.com/ttl.html#message-ttl-using-policy) | SetPerQueuedMessageExpiration |
| [dead-letter-exchange](https://www.rabbitmq.com/dlx.html#using-optional-queue-arguments) | SetDeadLetterExchange |
| [dead-letter-routing-key](https://www.rabbitmq.com/dlx.html#using-optional-queue-arguments) | SetDeadLetterExchangeRoutingKey |
| [alternate-exchange](https://www.rabbitmq.com/ae.html) | SetAlternateExchange |
| [federation-upstream](https://www.rabbitmq.com/parameters.html#policies) | SetFederationUpstream |
| [ha-mode](https://www.rabbitmq.com/parameters.html#policies) | SetHighAvailabilityMode |
| [ha-sync-mode](https://www.rabbitmq.com/ha.html#examples) | SetHighAvailabilitySyncMode |
| [ha-params](https://www.rabbitmq.com/ha.html) | SetHighAvailabilityParams |
| [queue-master-locator](https://www.rabbitmq.com/ha.html#queue-master-location) | SetQueueMasterLocator |
| [max-length-bytes](https://www.rabbitmq.com/parameters.html#operator-policies) | SetMessageMaxSizeInBytes |
| [max-length](https://www.rabbitmq.com/parameters.html#operator-policies) | SetMessageMaxSize |
| [ha-promote-on-shutdown](https://www.rabbitmq.com/ha.html#cluster-shutdown) | SetQueuePromotionOnShutdown |
| [ha-promote-on-failure](https://www.rabbitmq.com/ha.html#promoting-unsynchronised-mirrors) | SetQueuePromotionOnFailure |
| [ha-sync-batch-size](https://www.rabbitmq.com/ha.html#cluster-shutdown) | SetQueuedMessageSyncBatchSize |
| []() |  |
| []() |  |

The addition of the below code in ```HasArguments``` within ```Configure``` will set the above RabbitMQ arguments.

```csharp
p.HasArguments(d =>
{
    d.SetHighAvailabilityMode(HighAvailabilityModes.All);
    d.SetQueueExpiration(1000);
    d.SetAlternateExchange("your_exchange");
    d.SetFederationUpstream("your_value");
    d.SetQueueMode(QueueMode.Default);
    d.SetDeadLetterExchange("your_value");
    d.SetFederationUpstreamSet("your_value");
    d.SetHighAvailabilityMode(HighAvailabilityModes.All);
    d.SetHighAvailabilityParams("your_value");
    d.SetMessageMaxSize(9);
    d.SetDeadLetterRoutingKey("your_value");
    d.SetHighAvailabilitySyncMode(HighAvailabilitySyncModes.Automatic);
    d.SetMessageTimeToLive(8);
    d.SetMessageMaxSizeInBytes(7);
});
```
<br>

A complete example would look something like this...

```csharp
var result = await _container.Resolve<IBrokerObjectFactory>()
                .Object<Policy>()
                .Create(x =>
                {
                    x.Policy("your_policy");
                    x.Configure(y =>
                    {
                        y.UsingPattern("your_regex");
                        y.HasArguments(z =>
                        {
                            ...
                        });
                    });
                    x.Targeting(t => t.VirtualHost("your_vhost"));
                });
```

<br>

*Please note that subsequent calls to any of the above methods will result in overriding the argument.*

<br>

All examples in this document assumes the broker has been configured. If you want to know how then go to the Configuration documentation [here](https://github.com/ahives/HareDu2/blob/master/docs/configuration.md) .

