using Moq;
using Shouldly;
using Toro.Accounting.Application.Commands;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Commom.UnitTests.Mocks;

namespace Toro.Accounting.IntegrationTests.Customer.Commands
{
    public class MakeDepositCommandTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Guid CustomerOneId = Guid.Parse("9623f6d8-01d7-420f-9264-2143df3557cd");

        public MakeDepositCommandTests() 
        {
            _mockCustomerRepository = RepositoryMocks.GetCustomerRepository();
        }

        [Theory]
        [InlineData(100.5)]
        [InlineData(50.3)]
        [InlineData(25)]
        public async Task Handle_MakeDepositCommandWithPositiveAmount_ShouldUpdateAccountBalanceWithAmount(decimal amount)
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            var accountStartBalance = customer.AccountBalance;
            var command = new MakeDepositCommand(MakeDepositCommandValidator.TransferEventTypeName, customer.AccountNumber, customer.CPF, amount);
            var handler = new MakeDepositCommandHandler(_mockCustomerRepository.Object);
            var response = await handler.Handle(command);

            customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            customer.AccountBalance.ShouldBe(accountStartBalance + amount);
            response.Success.ShouldBeTrue();
            response.ValidationErrors.Count.ShouldBe(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public async Task Handle_MakeDepositCommandWithNegativeOrZeroAmount_ShouldUpdateAccountBalanceWithAmount(decimal amount)
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            var accountStartBalance = customer.AccountBalance;
            var command = new MakeDepositCommand(MakeDepositCommandValidator.TransferEventTypeName, customer.AccountNumber, customer.CPF, amount);
            var handler = new MakeDepositCommandHandler(_mockCustomerRepository.Object);
            var response = await handler.Handle(command);

            customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            customer.AccountBalance.ShouldBe(accountStartBalance);
            response.Success.ShouldBeFalse();
            response.ValidationErrors.Count.ShouldBe(1);
            response.ValidationErrors.Any(error => error.Equals("A quantia do depósito deve ser maior que zero")).ShouldBeTrue();
        }

        [Fact]
        public async Task Handle_MakeDepositCommand_ShouldValidateEmptyFields()
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            var accountStartBalance = customer.AccountBalance;
            var command = new MakeDepositCommand(string.Empty, string.Empty, string.Empty, 100);
            var handler = new MakeDepositCommandHandler(_mockCustomerRepository.Object);
            var response = await handler.Handle(command);

            customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            customer.AccountBalance.ShouldBe(accountStartBalance);
            response.Success.ShouldBeFalse();
            response.ValidationErrors.Count.ShouldBe(3);
            response.ValidationErrors.Any(error => error.Equals("O tipo de evento deve ser fornecido")).ShouldBeTrue();
            response.ValidationErrors.Any(error => error.Equals("O cpf de origem deve ser fornecido")).ShouldBeTrue();
            response.ValidationErrors.Any(error => error.Equals("O número da conta de destino deve ser fornecido")).ShouldBeTrue();
        }

        [Fact]
        public async Task Handle_MakeDepositCommand_ShouldValidateEventType()
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            var accountStartBalance = customer.AccountBalance;
            var command = new MakeDepositCommand("WITHDRAW", customer.AccountNumber, customer.CPF, 100);
            var handler = new MakeDepositCommandHandler(_mockCustomerRepository.Object);
            var response = await handler.Handle(command);

            customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            customer.AccountBalance.ShouldBe(accountStartBalance);
            response.Success.ShouldBeFalse();
            response.ValidationErrors.Count.ShouldBe(1);
            response.ValidationErrors.Any(error => error.Equals("O tipo de evento deve ser uma transferência")).ShouldBeTrue();
        }

        [Fact]
        public async Task Handle_MakeDepositCommandWithInvalidAccountNumber_ShouldReturnError()
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            var accountStartBalance = customer.AccountBalance;
            var command = new MakeDepositCommand(MakeDepositCommandValidator.TransferEventTypeName, "000000", customer.CPF, 100);
            var handler = new MakeDepositCommandHandler(_mockCustomerRepository.Object);
            var response = await handler.Handle(command);

            customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            customer.AccountBalance.ShouldBe(accountStartBalance);
            response.Success.ShouldBeFalse();
            response.ValidationErrors.Count.ShouldBe(1);
            response.ValidationErrors.Any(error => error.Equals("O número da conta de destino fornecido não existe")).ShouldBeTrue();
        }

        [Fact]
        public async Task Handle_MakeDepositCommandWithOriginCPFDifferentFromTarget_ShouldReturnError()
        {
            var customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            var accountStartBalance = customer.AccountBalance;
            var command = new MakeDepositCommand(MakeDepositCommandValidator.TransferEventTypeName, customer.AccountNumber, "11111111111", 100);
            var handler = new MakeDepositCommandHandler(_mockCustomerRepository.Object);
            var response = await handler.Handle(command);

            customer = await _mockCustomerRepository.Object.GetAsync(CustomerOneId);
            customer.AccountBalance.ShouldBe(accountStartBalance);
            response.Success.ShouldBeFalse();
            response.ValidationErrors.Count.ShouldBe(1);
            response.ValidationErrors.Any(error => error.Equals("O CPF da conta de origem é diferente do CPF da conta na Toro. Não é possível fazer um depósito entre contas de CPFs diferentes")).ShouldBeTrue();
        }
    }
}
