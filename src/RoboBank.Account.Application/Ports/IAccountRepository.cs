using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoboBank.Account.Application.Ports
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Domain.Account>> GetByCustomerIdAsync(string customerId);

        Task<Domain.Account> GetByIdAsync(string id);

        Task<Domain.Account> GetOwnAccountByCurrencyAsync(string currency);
    }
}
