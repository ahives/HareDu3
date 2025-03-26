# HareDu 

![Join the chat at https://gitter.im/HareDu2/Lobby](https://img.shields.io/gitter/room/haredu2/HareDu2?style=flat)
![NuGet downloads](https://img.shields.io/nuget/dt/haredu?style=flat)

HareDu is a fluent .NET library for managing and monitoring RabbitMQ clusters.

HareDu is Apache 2.0 licensed.

### HareDu 4 NuGet Packages

| Package Name |  | .NET Runtime |
|---| --- | --- |
| **API** |  |  |
| [HareDu.Core](https://www.nuget.org/packages/HareDu.Core/) | Configuration API | 8.0, 9.0 |
| [HareDu](https://www.nuget.org/packages/HareDu/) | Broker API | 8.0, 9.0 |
| [HareDu.Snapshotting](https://www.nuget.org/packages/HareDu.Snapshotting/) | Snapshot API | 8.0, 9.0 |
| [HareDu.Diagnostics](https://www.nuget.org/packages/HareDu.Diagnostics/) | Diagnostics API | 8.0, 9.0 |
| **Dependency Injection Containers** | | |
| [HareDu.MicrosoftIntegration](https://www.nuget.org/packages/HareDu.MicrosoftIntegration/) | Microsoft Dependency Injection Integration API| 8.0, 9.0 |


# Why HareDu 4?

If you are familiar with HareDu, you should know that HareDu 4 introduces some really cool new functionality. HareDu 4 came about from feedback of production deployments and because the original API was lacking in some key areas. In particular, HareDu 4 introduces the following enhancements:
1. Increased test coverage
2. Improved low level administrative API (i.e. Broker API)
3. .NET 9 support

HareDu 4 was rewritten with C# on .NET 9 in mind using such features as record classes for API immutability and the built in Json parser.

## Get It
From the Package Manager Console in Visual Studio you can run the following PowerShell script to get the latest version of HareDu...

```
Install-Package HareDu
```

or if you want a specific version of HareDu you can do the following...

```
Install-Package -Version <version> HareDu
```

ex:

```
Install-Package -Version 4.0.0 HareDu
```

The above applies for any NuGet package you wish to install.

## Using HareDu with RabbitMQ

1. Ensure that your RabbitMQ broker has the proper plugins enabled by following the [RabbitMQ documentation](https://www.rabbitmq.com/management.html#clustering).
2. Create a RabbitMQ Virtual Host
   Note: This can be done by either using the Broker API or by logging in to the RabbitMQ UI and creating a vhost
3. Create an appsettings.json file and make sure your newly created virtual host name matches
4. Create an exchange and queue, then bind them together
   Note: This can be done by either using the Broker API or by logging in to the RabbitMQ UI and creating a vhost
5. Publish and consume messages to and from the queue

**Enough with the talking, go check out the docs [here](https://github.com/ahives/HareDu3/blob/master/docs/README.md)**


# Dependencies
.NET 8 or above


# Debugging

If you find that making an API call is failing for reasons unknown, HareDu 4 introduces a way to return a text representation of the serialized JSON of the returned ```Result``` or ```Result<T>``` monads. Here is an example,

```c#
string debugText = result.ToJsonString();
```

That's it. So, the resulting output of calling the ```ToJsonString``` extension method might look something like this,

```json
{
  "Timestamp": "2021-03-01T14:06:43.569396+00:00",
  "DebugInfo": {
    "URL": "api/queues/HareDu/",
    "Request": "{\n  \u0022node\u0022: \u0022Node1\u0022,\n  \u0022durable\u0022: true,\n  \u0022auto_delete\u0022: true,\n  \u0022arguments\u0022: {\n    \u0022x-expires\u0022: 1000,\n    \u0022x-message-ttl\u0022: 2000\n  }\n}",
    "Exception": null,
    "StackTrace": null,
    "Response": null,
    "Errors": [
      {
        "Reason": "The name of the queue is missing.",
        "Timestamp": "0001-01-01T00:00:00+00:00"
      }
    ]
  },
  "HasFaulted": true
}
```


# Tested

|   | Version              |
|---|----------------------|
| Operating System | macOS Sequoia 15.3.2 |
| RabbitMQ | 4.0.3                |
| Erlang OTP | 26.2.5.4 (x64)       |
| .NET Runtime | .NET 9               |


# Changelist
You can find the changelist [here](https://github.com/ahives/HareDu3/blob/master/docs/changelist.md)
