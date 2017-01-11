using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RoboBank.Account.Domain.Adapters.NetStandard
{
    public class RatesModel
    {
        [JsonExtensionData]
        public IDictionary<string, JValue> Properties { get; set; }
    }
}
