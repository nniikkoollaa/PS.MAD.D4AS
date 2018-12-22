using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace PS.MAD.D4AS.GuidProvider.Worker
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class Worker : StatefulService
    {
        private object _service;
        private object _container;

        private Microsoft.Azure.ServiceBus.QueueClient _client;

        public Worker(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            IReliableDictionary<Guid, Guid> generatedGuids = await StateManager.GetOrAddAsync<IReliableDictionary<Guid, Guid>>("guids");

            _client.RegisterMessageHandler(async (message, cancelationToken) => {
                var newGuid = Guid.NewGuid();

                using (ITransaction tx = StateManager.CreateTransaction())
                {
                    while (await generatedGuids.ContainsKeyAsync(tx, newGuid))
                    {
                        newGuid = Guid.NewGuid();
                    }

                    await generatedGuids.AddAsync(tx, newGuid, newGuid);
                    await tx.CommitAsync();
                }

                await _client.CompleteAsync(message.SystemProperties.LockToken);
            },
            new Microsoft.Azure.ServiceBus.MessageHandlerOptions((e) => LogMessageHandlerException(e))
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
        }

        private Task LogMessageHandlerException(Microsoft.Azure.ServiceBus.ExceptionReceivedEventArgs e)
        {
            // Log the error
            return Task.CompletedTask;
        }
    }
}
