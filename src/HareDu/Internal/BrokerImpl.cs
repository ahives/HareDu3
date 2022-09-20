namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;

class BrokerImpl :
    BaseBrokerObject,
    Broker
{
    public BrokerImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Result<BrokerOverviewInfo>> GetOverview(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
            
        return await GetRequest<BrokerOverviewInfo>("api/overview", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> RebalanceAllQueues(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();
            
        return await PostEmptyRequest("api/rebalance/queues", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<AlarmState>> IsAlarmsInEffect(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        string url = "api/health/checks/alarms";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<AlarmState>
                {Data = AlarmState.InEffect, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<AlarmState>
                {Data = AlarmState.NotInEffect, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<AlarmState>
            {
                Data = AlarmState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether an alarm is in effect or not.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<BrokerState>> IsBrokerAlive(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        string url = $"api/aliveness-test/{vhost.ToSanitizedName()}";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<BrokerState>
                {Data = BrokerState.Alive, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<BrokerState>
                {Data = BrokerState.NotAlive, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<BrokerState>
            {
                Data = BrokerState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether an alarm is in effect or not.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }

    public async Task<Result<VirtualHostState>> IsVirtualHostsRunning(CancellationToken cancellationToken = default)
    {
        cancellationToken.RequestCanceled();

        string url = "api/health/checks/virtual-hosts";
        var result = await GetRequest(url, cancellationToken).ConfigureAwait(false);

        return result switch
        {
            SuccessfulResult => new SuccessfulResult<VirtualHostState>
                {Data = VirtualHostState.Running, DebugInfo = result.DebugInfo},
            FaultedResult => new UnsuccessfulResult<VirtualHostState>
                {Data = VirtualHostState.NotRunning, DebugInfo = result.DebugInfo},
            _ => new FaultedResult<VirtualHostState>
            {
                Data = VirtualHostState.NotRecognized,
                DebugInfo = new()
                {
                    URL = url,
                    Errors = new List<Error>
                    {
                        new()
                        {
                            Reason = "Not able to determine whether an alarm is in effect or not.",
                            Timestamp = DateTimeOffset.UtcNow
                        }
                    }
                }
            }
        };
    }
}