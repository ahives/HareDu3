# HareDu 

![Join the chat at https://gitter.im/HareDu2/Lobby](https://img.shields.io/gitter/room/haredu2/HareDu2?style=flat)
![NuGet downloads](https://img.shields.io/nuget/dt/haredu?style=flat)

HareDu is a .NET library for managing and monitoring RabbitMQ clusters.

HareDu is Apache 2.0 licensed.

### HareDu 3 NuGet Packages

| Package Name |  | .NET Runtime |
|---| --- | --- |
| **API** |  |  |
| [HareDu.Core](https://www.nuget.org/packages/HareDu.Core/) | Configuration API | 5.0 |
| [HareDu](https://www.nuget.org/packages/HareDu/) | Broker API | 5.0 |
| [HareDu.Snapshotting](https://www.nuget.org/packages/HareDu.Snapshotting/) | Snapshot API | 5.0 |
| [HareDu.Diagnostics](https://www.nuget.org/packages/HareDu.Diagnostics/) | Diagnostics API | 5.0 |
| **Dependency Injection Containers** | | |
| [HareDu.AutofacIntegration](https://www.nuget.org/packages/HareDu.AutofacIntegration/) | Autofac Integration API | 5.0 |
| [HareDu.MicrosoftIntegration](https://www.nuget.org/packages/HareDu.MicrosoftIntegration/) | Microsoft Dependency Injection Integration API| 5.0 |


# Why HareDu 3?

If you are familiar with HareDu, you should know that HareDu 3 introduces some really cool new functionality. HareDu 3 came about from feedback of production deployments and because the original API was lacking in some key areas. In particular, HareDu 3 introduces the following enhancements:
1. Increased test coverage
2. Improved low level administrative API (i.e. Broker API)
3. .NET 5 support

HareDu 3 was rewritten with C# 9.0 and .NET 5 in mind using such features as record classes for API immutability and the built in Json parser.

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
Install-Package -Version 3.1.0 HareDu
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
.NET 5 or above


# Debugging

If you find that making an API call is failing for reasons unknown, HareDu 3 introduces a way to return a text representation of the serialized JSON of the returned ```Result``` or ```Result<T>``` monads. Here is an example,

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

|   | Version |
|---| --- |
| Operating System | macOS Catalina 10.15.3 |
| RabbitMQ | 3.8.2, 3.8.9 |
| Erlang OTP | 22.0.4 (x64), 23.2 (x64) |
| .NET Runtime | .NET 5 |


# Known Issues

Broker API throws a json parsing exception when attempting to access socket_opts (SystemOverviewInfo.Listeners.SocketOptions) on RabbitMQ API version 3.8.2 in situations when this value is an array. The issue is with the RabbitMQ HTTP API v3.8.2 returning inconsistent object types for this property. This issue has been reported to RabbitMQ maintainers and a workaround has been added to the HareDu 3.1 release.


# Changelist
| Version | | Description | Type | Is Breaking Change? |
| --- | --- | --- | --- | --- |
| **3.0.0** | 1 | Removed JSON.NET dependency and replaced with System.Text.Json parser introduced in C# 8 | Enhancement | No |
| | 2 | Introduced new object in Broker API called Shovel for creating, viewing, and deleting dynamic shovels that are used to move messages between exchanges and queues | Enhancement | No |
| | 3 | Changed method signatures for Broker API objects to make them less verbose | Enhancement | Yes |
| | 4 | Introduced extension methods for Broker API objects to call methods directly off of IBrokerObjectFactory | Enhancement | No |
| | 5 | Changed name of HareDu.CoreIntegration package to HareDu.MicrosoftIntegration | Enhancement | Yes |
| | 6 | Removed support for YAML for configuring HareDu APIs to JSON | Enhancement | Yes |
| | 7 | Added support for configuring HareDu APIs in JSON | Enhancement | No |
| | 8 | Added support for configuring HareDu APIs using code via Dependency Injection APIs | Enhancement | No |
| | | | | |
| **3.0.1** | 1 | Fixed issue with Snapshot API not being registered in HareDu.MicrosoftIntegration and HareDu.AutofacIntegration packages | Bug Fix | No |
| | | | | |
| **3.1.0** | 1 | Changed SystemOverview object in Broker API to BrokerSystem | Enhancement | Yes |
| | 2 | Changed method signature for the Create method in Policy | Enhancement | Yes |
| | 3 | Fixed issue with inconsistent values being returned from socket_opts in api/overview | Bug Fix | No |
| | 4 | Introduced new object in Broker API called OperatorPolicy | New | No |

