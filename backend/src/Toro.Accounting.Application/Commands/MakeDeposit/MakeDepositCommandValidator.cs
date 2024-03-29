﻿using FluentValidation;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommandValidator : AbstractValidator<MakeDepositCommand>
    {
        public static string TransferEventTypeName = "TRANSFER";
        private readonly ICustomerRepository _customerRepository;

        public MakeDepositCommandValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;

            RuleFor(p => p.EventType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("O tipo de evento deve ser fornecido")
                .Equal(TransferEventTypeName)
                    .WithMessage("O tipo de evento deve ser uma transferência");

            RuleFor(p => p.OriginCPF)
                .NotEmpty()
                    .WithMessage("O cpf de origem deve ser fornecido");

            RuleFor(p => p.AccountNumber)
                .NotEmpty()
                    .WithMessage("O número da conta de destino deve ser fornecido");

            RuleFor(p => p.Amount)                
                .GreaterThan(0)
                    .WithMessage("A quantia do depósito deve ser maior que zero");

            RuleFor(p => p)
                .MustAsync(AccountNumberExists)
                    .When(p => p.AccountNumber.IsNotNullOrEmpty())
                        .WithMessage("O número da conta de destino fornecido não existe");

            RuleFor(p => p)
                .MustAsync(OriginCPFShouldBeSameOfTarget)
                    .When(p => p.OriginCPF.IsNotNullOrEmpty())
                    .WhenAsync(AccountNumberExists)
                        .WithMessage("O CPF da conta de origem é diferente do CPF da conta na Toro. Não é possível fazer um depósito entre contas de CPFs diferentes");
        }

        private async Task<bool> AccountNumberExists(MakeDepositCommand command, CancellationToken cancellationToken)
        {
            return await _customerRepository.AccountNumberExists(command.AccountNumber);
        }

        private async Task<bool> OriginCPFShouldBeSameOfTarget(MakeDepositCommand command, CancellationToken cancellationToken)
        {
            var customerCPF = await _customerRepository.GetCustomerCPFByAccountNumber(command.AccountNumber);
            return customerCPF == command.OriginCPF;
        }
    }
}
