using System.Threading.Tasks;

namespace RoboBank.Account.Domain.Ports
{
    public interface IExchangeRatesService
    {
        Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);
    }
}
