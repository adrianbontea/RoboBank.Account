namespace RoboBank.Account.Domain
{
    public class BankCharge
    {
        private readonly decimal _amount;

        public BankCharge(decimal amount)
        {
            _amount = amount;
        }

        public decimal Value => (_amount * 2)/100;
    }
}
