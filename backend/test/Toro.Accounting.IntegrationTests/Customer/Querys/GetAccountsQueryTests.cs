using Moq;
using Shouldly;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Application.Querys;
using Toro.Accounting.Commom.UnitTests.Mocks;

namespace Toro.Accounting.IntegrationTests.Querys
{
    public class GetAccountsQueryTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;

        public GetAccountsQueryTests()
        {
            _mockCustomerRepository = RepositoryMocks.GetCustomerRepository();
        }

        [Fact]
        public async Task Handle_GetAccountsQuery_ShouldReturnAllAccounts()
        {
            var customers = await _mockCustomerRepository.Object.GetAllAsync();
            var command = new GetAccountsQuery();
            var handler = new GetAccountsQueryHandler(_mockCustomerRepository.Object);
            var accounts = await handler.Handle(command);

            customers.Count.ShouldBe(accounts.Count);

            var customersIdsFromDatabase = customers.Select(w => w.Name);
            var customersIdsFromHandle = accounts.Select(w => w.userName);

            customersIdsFromDatabase.All(customersIdsFromHandle.Contains).ShouldBeTrue();
        }
    }
}
