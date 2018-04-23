using System.Threading.Tasks;
using RoboBank.Account.Domain;
using Microsoft.EntityFrameworkCore;

namespace RoboBank.Account.Application.Adapters.NetStandard
{
    public class CardRepository : AccountApplicationService.ICardRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public CardRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Card> GetByIdAsync(string id)
        {
            return await _unitOfWork.Cards.Include(card => card.Account).FirstOrDefaultAsync(card => card.Id == id);
        }
    }
}
