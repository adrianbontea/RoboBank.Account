namespace RoboBank.Account.Service.Models
{
    public class AccountModel
    {
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string Currency { get; set; }

        public decimal Balance { get; set; }
    }
}
