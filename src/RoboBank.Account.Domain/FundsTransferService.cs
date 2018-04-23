using System.Threading.Tasks;

namespace RoboBank.Account.Domain
{
    public class FundsTransferService
    {
        private readonly IExchangeRatesService _exchangeRatesService;

        public FundsTransferService(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        public async Task<AccountsResult> TransferAsync(Account sourceAccount, Account targetAccount, Account ownAccount, decimal amount, string currency)
        {
            if (targetAccount.Currency != currency)
            {
                throw new AccountException("Invalid target currency");
            }

            var exchangeRate = await _exchangeRatesService.GetExchangeRateAsync(currency, sourceAccount.Currency);

            var amountToDebitForTransfer = amount * exchangeRate;
            var amountToCharge = new BankCharge(amount).Value * exchangeRate;

            var totalAmountToDebit = amountToDebitForTransfer + amountToCharge;

            if (!sourceAccount.CanDebit(totalAmountToDebit))
            {
                throw new AccountException("Source account does not have enough funds.");
            }

            targetAccount.Credit(amount);
            sourceAccount.Debit(totalAmountToDebit);
            ownAccount.Credit(amountToCharge);

            return new AccountsResult
            {
                Source =
                    new AccountResult
                    {
                        CustomerId = sourceAccount.CustomerId,
                        AccountId = sourceAccount.Id,
                        Amount = totalAmountToDebit
                    },
                Target =
                    new AccountResult
                    {
                        CustomerId = targetAccount.CustomerId,
                        AccountId = targetAccount.Id,
                        Amount = amount
                    }
            };
        }

        #region Ports
        public interface IExchangeRatesService
        {
            Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency);
        }
        #endregion
    }
}
