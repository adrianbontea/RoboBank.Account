using Xunit;

namespace RoboBank.Account.Domain.Tests
{

    public class AccountTests
    {
        [Fact]
        public void CanDebit_ShouldReturnTrue_WhenBallanceIsGreaterThanAmount()
        {
            // Arrange
            var account = new Account {Balance = 100};

            // Act & Assert
            Assert.True(account.CanDebit(99));
        }

        [Fact]
        public void CanDebit_ShouldReturnTrue_WhenBallanceIsEqualToAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act & Assert
            Assert.True(account.CanDebit(100));
        }

        [Fact]
        public void CanDebit_ShouldReturnFalse_WhenBallanceIsSmallerThanAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act & Assert
            Assert.False(account.CanDebit(101));
        }

        [Fact]
        public void Debit_ShouldDecreaseBallance_WhenBallanceIsGreaterThanAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Debit(99);

            // Assert
            Assert.Equal(1, account.Balance);
        }

        [Fact]
        public void Debit_ShouldDecreaseBallance_WhenBallanceIsEqualToAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Debit(100);

            // Assert
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void Debit_ShouldNotDecreaseBallance_WhenBallanceIsSmallerThanAmount()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Debit(101);

            // Assert
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Credit_ShouldIncreaseBallance()
        {
            // Arrange
            var account = new Account { Balance = 100 };

            // Act
            account.Credit(50);

            // Assert
            Assert.Equal(150, account.Balance);
        }
    }
}
