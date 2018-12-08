using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace PS.MAD.D4AS.TicketValidator
{
    public class WorkerRole : RoleEntryPoint
    {
        private IContainer _container;
        private Microsoft.Azure.ServiceBus.QueueClient _client;
        private PS.MAD.D4AS.UseCases.SubmitNewTicket _service;

        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public override void Run()
        {
            this._service = this._container.Resolve<PS.MAD.D4AS.UseCases.SubmitNewTicket>();

            _client.RegisterMessageHandler(async (message, cancelationToken) => {
                var user = new PS.MAD.D4AS.Entities.User()
                {
                    Id = (Guid)message.UserProperties["UserId"]
                };

                dynamic ticketRequest = Newtonsoft.Json.JsonConvert.DeserializeObject(
                    System.Text.Encoding.UTF8.GetString(message.Body));

                var ticket = new PS.MAD.D4AS.Entities.Ticket()
                {
                    Description = ticketRequest.Description
                };

                this._service.ForUser(user, ticket);

                await _client.CompleteAsync(message.SystemProperties.LockToken);
            },
            new Microsoft.Azure.ServiceBus.MessageHandlerOptions((e) => LogMessageHandlerException(e))
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });

            CompletedEvent.WaitOne();
        }

        public override bool OnStart()
        {
            this.ConfigureDependencyInjection();

            ServicePointManager.DefaultConnectionLimit = 12;

            var connectionString = CloudConfigurationManager.GetSetting("ServiceBus.ConnectionString");
            var receivingQueueName = CloudConfigurationManager.GetSetting("ServiceBus.ReceivingQueueName");
            _client = new Microsoft.Azure.ServiceBus.QueueClient(connectionString, receivingQueueName);

            return base.OnStart();
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            _client.CloseAsync();
            CompletedEvent.Set();
            base.OnStop();
        }

        private Task LogMessageHandlerException(Microsoft.Azure.ServiceBus.ExceptionReceivedEventArgs e)
        {
            // Log the error
            return Task.CompletedTask;
        }

        private void ConfigureDependencyInjection()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PS.MAD.D4AS.Storage.AzureStorage>().As<PS.MAD.D4AS.DataAccess.Contracts.IStorage>();
            builder.RegisterType<PS.MAD.D4AS.DataAccess.TicketRepository>().As<PS.MAD.D4AS.UseCases.Contracts.ITicketRepository>();
            builder.RegisterType<PS.MAD.D4AS.UseCases.SubmitNewTicket>();

            _container = builder.Build();
        }
    }
}
