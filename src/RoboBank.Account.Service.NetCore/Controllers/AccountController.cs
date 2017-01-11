using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RoboBank.Account.Application;
using RoboBank.Account.Service.Custom;
using RoboBank.Account.Service.NetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace RoboBank.Account.Service.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountApplicationService _accountApplicationService;

        public AccountController(AccountApplicationService accountApplicationService)
        {
            _accountApplicationService = accountApplicationService;
        }

        [Route("transfers")]
        [HttpPost]
        [ServiceFilter(typeof(SaveUnitOfWorkChanges))]
        public async Task<IActionResult> TransferAsync(TransferModel transferModel)
        {
            var transferInfo = Mapper.Map<TransferModel, TransferInfo>(transferModel);
            await _accountApplicationService.TransferAsync(transferInfo);

            return Ok();
        }

        [Route("accounts/cards/{cardId}/withdrawals")]
        [HttpPost]
        [ServiceFilter(typeof(SaveUnitOfWorkChanges))]
        public async Task<IActionResult> WithdrawAsync(string cardId, AmountModel amountModel)
        {
            var amountInfo = Mapper.Map<AmountModel, AmountInfo>(amountModel);
            await _accountApplicationService.WithdrawAsync(cardId, amountInfo);

            return Ok();
        }

        [Route("accounts/cards/{cardId}/balance")]
        [HttpGet]
        public async Task<IActionResult> GetBalanceAsync(string cardId)
        {
            var balanceInfo = await _accountApplicationService.GetBalanceAsync(cardId);
            return Ok(Mapper.Map<AmountInfo, AmountModel>(balanceInfo));
        }

        [Route("customers/{customerId}/accounts")]
        [HttpGet]
        public async Task<IActionResult> GetAccountsAsync(string customerId)
        {
            var accountsInfo = await _accountApplicationService.GetCustomerAccountsAsync(customerId);
            return Ok(Mapper.Map<IEnumerable<AccountInfo>, IEnumerable<AccountModel>>(accountsInfo));
        }

        [Route("accounts/{accountId}")]
        [HttpGet]
        public async Task<IActionResult> GetAccountAsync(string accountId)
        {
            var accountInfo = await _accountApplicationService.GetAccountAsync(accountId);
            return Ok(Mapper.Map<AccountInfo, AccountModel>(accountInfo));
        }
    }
}
