namespace RoboBank.Account.Domain
{
    public class Account
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string Currency { get; set; }

        public decimal Balance { get; set; }

        public void Debit(decimal amount)
        {
            if (CanDebit(amount))
            {
                Balance -= amount;
            }
        }

        public void Credit(decimal amount)
        {
            Balance += amount;
        }

        public bool CanDebit(decimal amount)
        {
            return Balance >= amount;
        }
    }
}
