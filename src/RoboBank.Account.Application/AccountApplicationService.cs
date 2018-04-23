using System.Collections.Generic;
using System.Threading.Tasks;
using RoboBank.Account.Domain;

namespace RoboBank.Account.Application
{
    public class AccountApplicationService
    {
        private readonly FundsTransferService _fundsTransferService;
        private readonly IEventService _eventService;
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public AccountApplicationService(FundsTransferService fundsTransferService, IEventService eventService, IAccountRepository accountRepository, IMapper mapper, ICardRepository cardRepository)
        {
            _fundsTransferService = fundsTransferService;
            _eventService = eventService;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _cardRepository = cardRepository;
        }

        public async Task TransferAsync(TransferInfo transferInfo)
        {
            var sourceAccount = await _accountRepository.GetByIdAsync(transferInfo.SourceAccountId);

            if (sourceAccount == null)
            {
                throw new AccountException("Source account not found");
            }

            var targetAccount = await _accountRepository.GetByIdAsync(transferInfo.TargetAccountId);

            if (targetAccount == null)
            {
                throw new AccountException("Target account not found");
            }

            var ownAccount = await _accountRepository.GetOwnAccountByCurrencyAsync(sourceAccount.Currency);

            var result = await _fundsTransferService.TransferAsync(sourceAccount, targetAccount, ownAccount, transferInfo.Amount, transferInfo.Currency);

            await _eventService.Publish(new AccountEvent
            {
                AccountId = transferInfo.SourceAccountId,
                Amount = result.Source.Amount,
                CustomerId = result.Source.CustomerId,
                Type = AccountEventType.Debit
            });

            await _eventService.Publish(new AccountEvent
            {
                AccountId = transferInfo.TargetAccountId,
                Amount = result.Target.Amount,
                CustomerId = result.Target.CustomerId,
                Type = AccountEventType.Credit
            });
        }

        public async Task WithdrawAsync(string cardId, AmountInfo amountInfo)
        {
            var card = await _cardRepository.GetByIdAsync(cardId);

            if (card == null)
            {
                throw new AccountException("Card not found");
            }

            card.Account.Debit(amountInfo.Amount);

            await _eventService.Publish(new AccountEvent
            {
                AccountId = card.Account.Id,
                Amount = amountInfo.Amount,
                CustomerId = card.Account.CustomerId,
                Type = AccountEventType.Debit
            });
        }

        public async Task<AmountInfo> GetBalanceAsync(string cardId)
        {
            var card = await _cardRepository.GetByIdAsync(cardId);

            if (card == null)
            {
                throw new AccountException("Card not found");
            }

            return new AmountInfo
            {
                Amount = card.Account.Balance,
                Currency = card.Account.Currency
            };
        }

        public async Task<IEnumerable<AccountInfo>> GetCustomerAccountsAsync(string customerId)
        {
            var accounts = await _accountRepository.GetByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<Domain.Account>, IEnumerable<AccountInfo>>(accounts);
        }

        public async Task<AccountInfo> GetAccountAsync(string accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            return _mapper.Map<Domain.Account, AccountInfo>(account);
        }

        #region Ports
        public interface IAccountRepository
        {
            Task<IEnumerable<Domain.Account>> GetByCustomerIdAsync(string customerId);

            Task<Domain.Account> GetByIdAsync(string id);

            Task<Domain.Account> GetOwnAccountByCurrencyAsync(string currency);
        }

        public interface ICardRepository
        {
            Task<Card> GetByIdAsync(string id);
        }

        public interface IEventService
        {
            Task Publish(AccountEvent evt);
        }

        public interface IMapper
        {
            TDestination Map<TSource, TDestination>(TSource source);
        }
        #endregion
    }
}
