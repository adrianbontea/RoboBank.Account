using System.Threading.Tasks;
using FixerSharp;

namespace RoboBank.Account.Domain.Adapters
{
    public class FixerExchangeRatesService : FundsTransferService.IExchangeRatesService
    {
        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var exchangeRate = await Task.Run(() => Fixer.Rate(fromCurrency, toCurrency));
            return (decimal) exchangeRate.Rate;
        }
    }
}
