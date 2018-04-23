using System.Threading.Tasks;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application.Adapters.NetStandard
{
    public class ServiceBusEventService : AccountApplicationService.IEventService
    {

        public async Task Publish(AccountEvent evt)
        {
            // TODO: Find a ServiceBus/AMQP client library compatible with .net standard library v1.6.1.
            // WindowsAzure.ServiceBus and AMQPNetLite are not yet...
        }
    }
}
