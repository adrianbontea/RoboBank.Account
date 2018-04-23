using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboBank.Account.Application.Adapters.NetStandard
{
    public class StubAccountRepository : AccountApplicationService.IAccountRepository
    {
        private const string OwnCustomerId = "RoboBank";
        private readonly IEnumerable<Domain.Account> _cache = new List<Domain.Account>
        {
            new Domain.Account
            {
                Id = "account1",
                CustomerId ="customer1",
                Currency = "USD",
                Balance = 1000
            } ,
            new Domain.Account
            {
                Id = "account2",
                CustomerId = "customer1",
                Currency = "EUR",
                Balance = 1000
            }
        };

        public async Task<IEnumerable<Domain.Account>> GetByCustomerIdAsync(string customerId)
        {
            return _cache.Where(acc => acc.CustomerId == customerId).ToArray();
        }

        public async Task<Domain.Account> GetByIdAsync(string id)
        {
            return _cache.FirstOrDefault(acc => acc.Id == id);
        }

        public async Task<Domain.Account> GetOwnAccountByCurrencyAsync(string currency)
        {
            return _cache.FirstOrDefault(acc => acc.CustomerId == OwnCustomerId && acc.Currency == currency);
        }
    }
}
