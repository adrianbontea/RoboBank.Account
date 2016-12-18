using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RoboBank.Account.Application.Ports;

namespace RoboBank.Account.Application.Adapters
{
    public class AccountRepository : IAccountRepository
    {
        private const string OwnCustomerId = "RoboBank";

        private readonly UnitOfWork _unitOfWork;

        public AccountRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Domain.Account>> GetByCustomerIdAsync(string customerId)
        {
            return await _unitOfWork.Accounts.Where(acc => acc.CustomerId == customerId).ToArrayAsync();
        }

        public async Task<Domain.Account> GetByIdAsync(string id)
        {
            return await _unitOfWork.Accounts.FirstOrDefaultAsync(acc => acc.Id == id);
        }

        public async Task<Domain.Account> GetOwnAccountByCurrencyAsync(string currency)
        {
            return await _unitOfWork.Accounts.FirstOrDefaultAsync(acc => acc.CustomerId == OwnCustomerId && acc.Currency == currency);
        }
    }
}
