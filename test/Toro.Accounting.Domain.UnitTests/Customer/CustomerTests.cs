using Shouldly;
using Toro.Accounting.Domain.Entities;

namespace Toro.Accounting.Domain.UnitTests
{
    public class CustomerTests
    {
        private readonly Customer _customer = new Customer(Guid.NewGuid(), "luffy", "Monkey D. Luffy", "87498578964", "283749", 2000);

        [Theory]
        [InlineData(100.5)]
        [InlineData(50.3)]
        [InlineData(25)]
        public void Customer_Deposit_ShouldUpdateAccountBalanceWithAmount(decimal amount)
        {
            var accountStartBalance = _customer.AccountBalance;
            _customer.Deposit(amount);
            _customer.AccountBalance.ShouldBe(accountStartBalance + amount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Customer_NegativeOrZeroDeposit_ShouldThrowException(decimal amount)
        {
            Should.Throw<Exception>(() =>
            {
                _customer.Deposit(amount);
            }).Message
            .ShouldBe("Deposit method should be used just for amounts greather than zero");
        }
    }
}
