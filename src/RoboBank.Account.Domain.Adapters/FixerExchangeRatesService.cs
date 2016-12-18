using System.Threading.Tasks;
using FixerSharp;
using RoboBank.Account.Domain.Ports;

namespace RoboBank.Account.Domain.Adapters
{
    public class FixerExchangeRatesService : IExchangeRatesService
    {
        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var exchangeRate = await Task.Run(() => Fixer.Rate(fromCurrency, toCurrency));
            return (decimal) exchangeRate.Rate;
        }
    }
}
