namespace RoboBank.Account.Domain
{
    public class AccountEvent
    {
        public string AccountId { get; set; }

        public string CustomerId { get; set; }

        public AccountEventType Type { get; set; }

        public decimal Amount { get; set; }
    }
}
