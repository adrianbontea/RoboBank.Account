using System.Threading.Tasks;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application.Ports
{
    public interface ICardRepository
    {
        Task<Card> GetByIdAsync(string id);
    }
}
