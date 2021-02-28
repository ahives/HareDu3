namespace HareDu.Snapshotting.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Testing;
    using HareDu.Model;

    public class FakeQueueObject :
        Queue,
        HareDuTestingFake
    {
        public async Task<ResultList<QueueInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            var channel = new QueueInfo
            {
                TotalMessages = 7823668,
                UnacknowledgedMessages = 7273020,
                ReadyMessages = 9293093,
                TotalReductions = 992039,
                Name = "Queue 1",
                MessageBytesPagedOut = 239939803,
                TotalMessagesPagedOut = 90290398,
                MessageBytesInRAM = 992390933,
                TotalBytesOfMessagesDeliveredButUnacknowledged = 82830892,
                TotalBytesOfMessagesReadyForDelivery = 892839823,
                TotalBytesOfAllMessages = 82938938723,
                UnacknowledgedMessagesInRAM = 82938982323,
                MessagesReadyForDeliveryInRAM = 8892388929,
                MessagesInRAM = 9883892938,
                Consumers = 773709938,
                ConsumerUtilization = 0.50M,
                Memory = 92990390,
                MessageStats = new ()
                {
                    TotalMessagesPublished = 763928923,
                    TotalMessageGets = 82938820903,
                    TotalMessageGetsWithoutAck = 23997979383,
                    TotalMessagesDelivered = 238847970,
                    TotalMessageDeliveredWithoutAck = 48898693232,
                    TotalMessageDeliveryGets = 50092830929,
                    TotalMessagesRedelivered = 488983002934,
                    TotalMessagesAcknowledged = 92303949398
                }
            };

            return new SuccessfulResultList<QueueInfo>{Data = new List<QueueInfo> {channel}, DebugInfo = null};
        }

        public async Task<Result> Create(string queue, string vhost, string node, Action<QueueConfigurator> configuration,
            CancellationToken cancellationToken = default) =>
            throw new NotImplementedException();

        public async Task<Result> Delete(string queue, string vhost, Action<QueueDeletionConfigurator> configurator = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public async Task<Result> Empty(string queue, string vhost, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}