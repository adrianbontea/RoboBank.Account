using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using RoboBank.Account.Domain.Ports;

namespace RoboBank.Account.Domain.Tests
{
    [TestClass]
    public class FundsTransferServiceTests
    {

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TransferAsync_ThrowsException_WhenTargetAccountCurrencyIsInvalid()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var fundsTransferService = fixture.Create<FundsTransferService>();

            // Act
            fundsTransferService.TransferAsync(new Account(), new Account {Currency = "GBP"}, new Account(),  10, "USD").Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TransferAsync_ThrowsException_WhenSourceAccountDoesNotHaveEnoughFunds()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var exchangeRateServiceMock = fixture.Freeze<Mock<IExchangeRatesService>>();
            exchangeRateServiceMock.Setup(m => m.GetExchangeRateAsync("USD", "EUR"))
                .Returns(Task.FromResult<decimal>(14));

            var fundsTransferService = fixture.Create<FundsTransferService>();

            // Act
            fundsTransferService.TransferAsync(new Account {Currency = "EUR", Balance = 2},
                new Account {Currency = "USD"}, new Account(), 10, "USD").Wait();
        }

        [TestMethod]
        public void TransferAsync_ShouldCorrectlyUpdateAccounts_WhenEverythingIsOK()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var exchangeRateServiceMock = fixture.Freeze<Mock<IExchangeRatesService>>();

            var sourceAccount = new Account {Currency = "EUR", Balance = 200};
            var targetAccount = new Account {Currency = "USD", Balance = 100};
            var ownAccount = new Account { Currency = "EUR", Balance = 1000 };

            exchangeRateServiceMock.Setup(m => m.GetExchangeRateAsync("USD", "EUR"))
                .Returns(Task.FromResult<decimal>(1));

            var fundsTransferService = fixture.Create<FundsTransferService>();

            // Act
            fundsTransferService.TransferAsync(sourceAccount, targetAccount, ownAccount, 10, "USD").Wait();

            // Assert
            Assert.AreEqual(110, targetAccount.Balance);
            Assert.AreEqual((decimal)1000.2, ownAccount.Balance);
            Assert.AreEqual((decimal)189.8, sourceAccount.Balance);
        }
    }
}
