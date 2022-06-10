using Moq;
using Shouldly;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Application.Querys;
using Toro.Accounting.Commom.UnitTests.Mocks;

namespace Toro.Accounting.IntegrationTests.Querys
{
    public class GetAccountDetailsQueryTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;

        public GetAccountDetailsQueryTests()
        {
            _mockCustomerRepository = RepositoryMocks.GetCustomerRepository();
        }

        [Theory]
        [InlineData("9623f6d8-01d7-420f-9264-2143df3557cd")]
        [InlineData("56fe0147-97d1-4b24-87c1-00ff67c40f8f")]
        [InlineData("bef882d7-9359-44ec-9f4b-77c71413592c")]
        public async Task Handle_GetAccountDetailsQuery_ShouldReturnAccountDetailsForValidUsersIds(string userId)
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(Guid.Parse(userId));
            var command = new GetAccountDetailsQuery(userId);
            var handler = new GetAccountDetailsQueryHandler(_mockCustomerRepository.Object);
            var accountDetails = await handler.Handle(command);

            accountDetails.userName.ShouldBe(customer.Name);
            accountDetails.accountBalance.ShouldBe(customer.AccountBalance);
            accountDetails.accountNumber.ShouldBe(customer.AccountNumber);
        }

        [Theory]
        [InlineData("aaaaaaaa-01d7-420f-9264-2143df3557aa")]
        [InlineData("bbbbbbbb-97d1-4b24-87c1-00ff67c40faa")]
        public async Task Handle_GetAccountDetailsQuery_ShouldReturnNullForInvalidUsersIds(string userId)
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(Guid.Parse(userId));
            var command = new GetAccountDetailsQuery(userId);
            var handler = new GetAccountDetailsQueryHandler(_mockCustomerRepository.Object);

            await Should.NotThrowAsync(async () =>
            {
                var accountDetails = await handler.Handle(command);
                accountDetails.ShouldBeNull();                
            });
        }
    }
}
