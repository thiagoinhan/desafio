using Moq;
using Shouldly;
using System.Net;
using System.Text.Json;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Application.Dtos;
using Toro.Accounting.Commom.UnitTests.Mocks;

namespace Toro.Accounting.EndToEndTests
{
    [Collection("E2E Collection")]
    public class AccountsTests
    {
        private readonly WebApplicationFactoryFixture _fixture;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        public AccountsTests(WebApplicationFactoryFixture fixture)
        {
            _mockCustomerRepository = RepositoryMocks.GetCustomerRepository();
            this._fixture = fixture;
        }

        [Theory]
        [InlineData("9623f6d8-01d7-420f-9264-2143df3557cd")]
        [InlineData("56fe0147-97d1-4b24-87c1-00ff67c40f8f")]
        [InlineData("bef882d7-9359-44ec-9f4b-77c71413592c")]
        public async void Accounts_GetAccount_ShouldReturnAccountDetailsAndOKStatusForValidUsers(string userId)
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(Guid.Parse(userId));
            var client = _fixture.WebApplicationFactory.CreateClient();
            var response = await client.GetAsync($"/accounts/{userId}");

            var content = await response.Content.ReadAsStringAsync();
            var accountDetails = JsonSerializer.Deserialize<AccountDetails>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            accountDetails.userName.ShouldBe(customer.Name);
            accountDetails.accountBalance.ShouldBe(customer.AccountBalance);
            accountDetails.accountNumber.ShouldBe(customer.AccountNumber);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("aaaaaaaa-01d7-420f-9264-2143df3557cd")]
        [InlineData("bbbbbbbb-97d1-4b24-87c1-00ff67c40f8f")]
        public async void Accounts_GetAccount_ShouldReturnNotFoundStatusForInvalidUsers(string userId)
        {
            var client = _fixture.WebApplicationFactory.CreateClient();
            var response = await client.GetAsync($"/accounts/{userId}");
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Accounts_GetAllAccounts_ShouldReturnAllAccountsAndOKStatus()
        {
            var customers = await _mockCustomerRepository.Object.GetAllAsync();
            var client = _fixture.WebApplicationFactory.CreateClient();
            var response = await client.GetAsync($"/accounts");

            var content = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<AccountDetails>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            customers.Count.ShouldBe(accounts.Count);

            var customersIdsFromDatabase = customers.Select(w => w.Name);
            var customersIdsFromHandle = accounts.Select(w => w.userName);

            customersIdsFromDatabase.All(customersIdsFromHandle.Contains).ShouldBeTrue();
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
