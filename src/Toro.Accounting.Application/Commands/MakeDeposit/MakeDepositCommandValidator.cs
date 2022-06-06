using FluentValidation;
using Toro.Accounting.Application.Contracts.Persistence;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommandValidator : AbstractValidator<MakeDepositCommand>
    {
        private static string TransferEventTypeName = "TRANSFER";
        private readonly ICustomerRepository _customerRepository;

        public MakeDepositCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(p => p.EventType)
                .Equal(TransferEventTypeName)
                    .WithMessage("O evento deve ser uma transferência");

            RuleFor(p => p.OriginCPF)
                .NotEmpty()
                    .WithMessage("O cpf de origem deve ser fornecido");

            RuleFor(p => p.AccountNumber)
                .NotEmpty()
                    .WithMessage("O número da conta de destino deve ser fornecido")
                .MustAsync(AccountNumberExists)
                    .WithMessage("O número da conta de destino fornecido não existe");

            RuleFor(p => p.Amount)                
                .GreaterThan(0)
                    .WithMessage("A quantida do depósito deve ser maior que zero");

            RuleFor(p => p)
                .MustAsync(OriginCPFShouldBeSameOfTarget)
                    .WithMessage("O CPF da conta de origem é diferente do CPF da conta na Toro. Não é possível fazer um depósito entre contas de CPFs diferentes");
        }

        private async Task<bool> AccountNumberExists(string accountNumber, CancellationToken cancellationToken)
        {
            return await _customerRepository.AccountNumberExists(accountNumber);
        }

        private async Task<bool> OriginCPFShouldBeSameOfTarget(MakeDepositCommand command, CancellationToken cancellationToken)
        {
            var customerCPF = await _customerRepository.GetCustomerCPFByAccountNumber(command.AccountNumber);
            return customerCPF == command.OriginCPF;
        }
    }
}
