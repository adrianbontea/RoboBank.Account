using System;

namespace RoboBank.Account.Domain
{
    public class Card
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int CVV { get; set; }

        public Account Account { get; set; }
    }
}
