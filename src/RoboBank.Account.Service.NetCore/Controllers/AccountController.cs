using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RoboBank.Account.Application;
using RoboBank.Account.Service.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using RoboBank.Account.Service.NetCore.Custom;

namespace RoboBank.Account.Service.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountApplicationService _accountApplicationService;

        public AccountController(AccountApplicationService accountApplicationService)
        {
            _accountApplicationService = accountApplicationService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return Redirect("/swagger/ui");
        }

        [HttpPost("/transfers")]
        [ServiceFilter(typeof(SaveUnitOfWorkChanges))]
        public async Task<IActionResult> TransferAsync([FromBody]TransferModel transferModel)
        {
            var transferInfo = Mapper.Map<TransferModel, TransferInfo>(transferModel);
            await _accountApplicationService.TransferAsync(transferInfo);

            return Ok();
        }


        [HttpPost("/accounts/cards/{cardId}/withdrawals")]
        [ServiceFilter(typeof(SaveUnitOfWorkChanges))]
        public async Task<IActionResult> WithdrawAsync(string cardId, [FromBody]AmountModel amountModel)
        {
            var amountInfo = Mapper.Map<AmountModel, AmountInfo>(amountModel);
            await _accountApplicationService.WithdrawAsync(cardId, amountInfo);

            return Ok();
        }

        [HttpGet("/accounts/cards/{cardId}/balance")]
        public async Task<IActionResult> GetBalanceAsync(string cardId)
        {
            var balanceInfo = await _accountApplicationService.GetBalanceAsync(cardId);
            return Ok(Mapper.Map<AmountInfo, AmountModel>(balanceInfo));
        }

        [HttpGet("/customers/{customerId}/accounts")]
        public async Task<IActionResult> GetAccountsAsync(string customerId)
        {
            var accountsInfo = await _accountApplicationService.GetCustomerAccountsAsync(customerId);
            return Ok(Mapper.Map<IEnumerable<AccountInfo>, IEnumerable<AccountModel>>(accountsInfo));
        }

        [HttpGet("/accounts/{accountId}")]
        public async Task<IActionResult> GetAccountAsync(string accountId)
        {
            var accountInfo = await _accountApplicationService.GetAccountAsync(accountId);
            return Ok(Mapper.Map<AccountInfo, AccountModel>(accountInfo));
        }
    }
}
