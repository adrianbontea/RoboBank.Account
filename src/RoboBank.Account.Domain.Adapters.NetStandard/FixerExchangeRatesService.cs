using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;

namespace RoboBank.Account.Domain.Adapters.NetStandard
{
    public class FixerExchangeRatesService : FundsTransferService.IExchangeRatesService
    {
        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            if(fromCurrency == toCurrency)
            {
                return 1;
            }

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://api.fixer.io/latest?base={fromCurrency}");

            var responseModel = JsonConvert.DeserializeObject<ExchangeRatesResultModel>(await response.Content.ReadAsStringAsync());

            if (responseModel.Rates.Properties.ContainsKey(toCurrency))
            {
                return decimal.Parse(responseModel.Rates.Properties[toCurrency].ToString());
            }

            throw new ArgumentException("Invalid target currency");
        }
    }
}
