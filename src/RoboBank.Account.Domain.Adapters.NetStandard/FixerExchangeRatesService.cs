using System.Threading.Tasks;
using RoboBank.Account.Domain.Ports;
using System.Net.Http;
using Newtonsoft.Json;
using System;

namespace RoboBank.Account.Domain.Adapters.NetStandard
{
    public class FixerExchangeRatesService : IExchangeRatesService
    {
        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://api.fixer.io/latest?base={fromCurrency}");

            var responseModel = JsonConvert.DeserializeObject<ExchangeRatesResultModel>(await response.Content.ReadAsStringAsync());

            if (responseModel.Rates.Properties.ContainsKey(toCurrency))
            {
                return decimal.Parse(responseModel.Rates.Properties[toCurrency].Value.ToString());
            }

            throw new ArgumentException("Invalid target currency");
        }
    }
}
