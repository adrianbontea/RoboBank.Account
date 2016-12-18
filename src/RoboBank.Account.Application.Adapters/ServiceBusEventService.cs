using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using RoboBank.Account.Application.Ports;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application.Adapters
{
    public class ServiceBusEventService : IEventService
    {
        private readonly QueueClient _queueClient;

        public ServiceBusEventService(QueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task Publish(AccountEvent evt)
        {
            var message = new BrokeredMessage();

            message.Properties["AccountId"] = evt.AccountId;
            message.Properties["CustomerId"] = evt.CustomerId;
            message.Properties["Type"] = evt.Type.ToString();
            message.Properties["Amount"] = evt.Amount;

            await _queueClient.SendAsync(message);
        }
    }
}
