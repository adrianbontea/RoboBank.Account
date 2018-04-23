using System.Data.Entity;
using System.Threading.Tasks;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application.Adapters
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
