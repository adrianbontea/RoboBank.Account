using Newtonsoft.Json;
using System;

namespace RoboBank.Account.Domain.Adapters.NetStandard
{
    public class ExchangeRatesResultModel
    {
        [JsonProperty(PropertyName = "base")]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "rates")]
        public RatesModel Rates { get; set; }
    }
}
