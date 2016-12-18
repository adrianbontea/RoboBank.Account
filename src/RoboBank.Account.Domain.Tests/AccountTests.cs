using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoboBank.Account.Domain.Tests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void CanDebit_ShouldReturnTrue_WhenBallanceIsGreaterThanAmount()
        {
            // Arrange
            var account = new Account {Balance = 100};

            // Act & Assert
            Assert.IsTrue(account.CanDebit(99));
        }

        [TestMethod]
        public void CanDebit_ShouldReturnTrue_WhenBallanceIsEqualToAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act & Assert
            Assert.IsTrue(account.CanDebit(100));
        }

        [TestMethod]
        public void CanDebit_ShouldReturnFalse_WhenBallanceIsSmallerThanAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act & Assert
            Assert.IsFalse(account.CanDebit(101));
        }

        [TestMethod]
        public void Debit_ShouldDecreaseBallance_WhenBallanceIsGreaterThanAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Debit(99);

            // Assert
            Assert.AreEqual(1, account.Balance);
        }

        [TestMethod]
        public void Debit_ShouldDecreaseBallance_WhenBallanceIsEqualToAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Debit(100);

            // Assert
            Assert.AreEqual(0, account.Balance);
        }

        [TestMethod]
        public void Debit_ShouldNotDecreaseBallance_WhenBallanceIsSmallerThanAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Debit(101);

            // Assert
            Assert.AreEqual(100, account.Balance);
        }

        [TestMethod]
        public void Credit_ShouldIncreaseBallance()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Credit(50);

            // Assert
            Assert.AreEqual(150, account.Balance);
        }
    }
}
