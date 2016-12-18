using System.Threading.Tasks;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application.Ports
{
    public interface IEventService
    {
        Task Publish (AccountEvent evt);
    }
}
