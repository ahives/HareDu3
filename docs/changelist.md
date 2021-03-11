# Changelist

| Version | | Description | Type | Breaking Change? |
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

