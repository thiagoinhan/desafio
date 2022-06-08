using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;
using Toro.Accounting.Api.Dtos;
using Toro.Accounting.Application.Commands;

namespace Toro.Accounting.EndToEndTests.sbp.events
{
    [Collection("E2E Collection")]
    public class MakeDepositTests
    {
        private readonly WebApplicationFactoryFixture _fixture;
        public MakeDepositTests(WebApplicationFactoryFixture fixture) => this._fixture = fixture;   

        [Fact]
        public async void Sbp_MakeDeposit_ShouldReturnNoContentResponse() 
        {
            var depositEventDetails = new DepositEventDetails(
                "TRANSFER",
                new TargetAccountDetails("352", "0001", "283749"),
                new OriginAccountDetails("033", "03312", "87498578964"),
                100
            );

            var client = _fixture.WebApplicationFactory.CreateClient();
            var response = await client.PostAsync("/spb/events", new StringContent(JsonSerializer.Serialize(depositEventDetails), Encoding.UTF8, "application/json"));

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void Sbp_MakeDepositWithDifferentCPF_ShouldReturnBadRequestResponse()
        {
            var depositEventDetails = new DepositEventDetails(
                "TRANSFER",
                new TargetAccountDetails("352", "0001", "283749"),
                new OriginAccountDetails("033", "03312", "11111111111"),
                100
            );

            var client = _fixture.WebApplicationFactory.CreateClient();
            var response = await client.PostAsync("/spb/events", new StringContent(JsonSerializer.Serialize(depositEventDetails), Encoding.UTF8, "application/json"));

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<MakeDepositCommandResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            result.ValidationErrors.Any(error => error.Equals("O CPF da conta de origem é diferente do CPF da conta na Toro. Não é possível fazer um depósito entre contas de CPFs diferentes")).ShouldBeTrue();            
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
