namespace RoboBank.Account.Application
{
    public class TransferInfo
    {
        public string SourceAccountId { get; set; }

        public string TargetAccountId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
