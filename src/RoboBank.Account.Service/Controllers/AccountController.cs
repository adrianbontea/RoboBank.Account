using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using RoboBank.Account.Application;
using RoboBank.Account.Service.Custom;
using RoboBank.Account.Service.Models;

namespace RoboBank.Account.Service.Controllers
{
    [ElmahAccountExceptionHandling]
    public class AccountController : ApiController
    {
        private readonly AccountApplicationService _accountApplicationService;

        public AccountController(AccountApplicationService accountApplicationService)
        {
            _accountApplicationService = accountApplicationService;
        }

        [Route("transfers")]
        [HttpPost]
        [SaveUnitOfWorkChanges]
        public async Task<IHttpActionResult> TransferAsync(TransferModel transferModel)
        {
            var transferInfo = Mapper.Map<TransferModel, TransferInfo>(transferModel);
            await _accountApplicationService.TransferAsync(transferInfo);

            return Ok();
        }

        [Route("accounts/cards/{cardId}/withdrawals")]
        [HttpPost]
        [SaveUnitOfWorkChanges]
        public async Task<IHttpActionResult> WithdrawAsync(string cardId, AmountModel amountModel)
        {
            var amountInfo = Mapper.Map<AmountModel, AmountInfo>(amountModel);
            await _accountApplicationService.WithdrawAsync(cardId, amountInfo);

            return Ok();
        }

        [Route("accounts/cards/{cardId}/balance")]
        [HttpGet]
        public async Task<IHttpActionResult> GetBalanceAsync(string cardId)
        {
            var balanceInfo = await _accountApplicationService.GetBalanceAsync(cardId);
            return Ok(Mapper.Map<AmountInfo, AmountModel>(balanceInfo));
        }

        [Route("customers/{customerId}/accounts")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountsAsync(string customerId)
        {
            var accountsInfo = await _accountApplicationService.GetCustomerAccountsAsync(customerId);
            return Ok(Mapper.Map<IEnumerable<AccountInfo>, IEnumerable<AccountModel>>(accountsInfo));
        }

        [Route("accounts/{accountId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAccountAsync(string accountId)
        {
            var accountInfo = await _accountApplicationService.GetAccountAsync(accountId);
            return Ok(Mapper.Map<AccountInfo, AccountModel>(accountInfo));
        }
    }
}
