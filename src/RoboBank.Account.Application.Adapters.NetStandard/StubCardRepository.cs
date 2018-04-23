using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application.Adapters.NetStandard
{
    public class StubCardRepository: AccountApplicationService.ICardRepository
    {
        private readonly IEnumerable<Card> _cache = new List<Card>
        {
            new Card
            {
                Id = "card1",
                Name = "John Doe",
                ExpirationDate = new DateTime(2020, 4, 10),
                CVV = 123,
                Account = new Domain.Account
                {
                   Id = "account1",
                   CustomerId ="customer1",
                   Currency = "USD",
                   Balance = 1000
                }
            }
        };
        public async Task<Card> GetByIdAsync(string id)
        {
            return _cache.FirstOrDefault(card => card.Id == id);
        }
    }
}
