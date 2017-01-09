using System.Threading.Tasks;
using Moq;
using RoboBank.Account.Domain.Ports;
using Xunit;
using Ploeh.AutoFixture.Xunit2;

namespace RoboBank.Account.Domain.Tests
{
    public class FundsTransferServiceTests
    {

        [Theory]
        [AutoMoqData]
        public void TransferAsync_ThrowsException_WhenTargetAccountCurrencyIsInvalid(FundsTransferService sut)
        {
            // Arrange

            // Act
            var ex = Record.ExceptionAsync(() => sut.TransferAsync(new Account(), new Account {Currency = "GBP"}, new Account(),  10, "USD")).Result;

            // Assert
            Assert.IsType<AccountException>(ex);
        }

        [Theory]
        [AutoMoqData]
        public void TransferAsync_ThrowsException_WhenSourceAccountDoesNotHaveEnoughFunds([Frozen]Mock<IExchangeRatesService> exchangeRateServiceMock, FundsTransferService sut)
        {
            // Arrange
            exchangeRateServiceMock.Setup(m => m.GetExchangeRateAsync("USD", "EUR"))
                .Returns(Task.FromResult<decimal>(14));

            // Act
            var ex = Record.ExceptionAsync(() => sut.TransferAsync(new Account {Currency = "EUR", Balance = 2},
                new Account {Currency = "USD"}, new Account(), 10, "USD")).Result;

            // Assert
            Assert.IsType<AccountException>(ex);
        }

        [Theory]
        [AutoMoqData]
        public void TransferAsync_ShouldCorrectlyUpdateAccounts_WhenEverythingIsOK([Frozen]Mock<IExchangeRatesService> exchangeRateServiceMock, FundsTransferService sut)
        {
            // Arrange
            var sourceAccount = new Account {Currency = "EUR", Balance = 200};
            var targetAccount = new Account {Currency = "USD", Balance = 100};
            var ownAccount = new Account { Currency = "EUR", Balance = 1000 };

            exchangeRateServiceMock.Setup(m => m.GetExchangeRateAsync("USD", "EUR"))
                .Returns(Task.FromResult<decimal>(1));

            // Act
            sut.TransferAsync(sourceAccount, targetAccount, ownAccount, 10, "USD").Wait();

            // Assert
            Assert.Equal(110, targetAccount.Balance);
            Assert.Equal((decimal)1000.2, ownAccount.Balance);
            Assert.Equal((decimal)189.8, sourceAccount.Balance);
        }
    }
}
