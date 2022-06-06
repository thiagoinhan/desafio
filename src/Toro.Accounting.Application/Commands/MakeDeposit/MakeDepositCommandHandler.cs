﻿using Toro.Accounting.Application.Contracts;
using Toro.Accounting.Domain.Entities;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommandHandler : ICommandHandler<MakeDepositCommand, MakeDepositCommandResponse>
    {
        public async Task<MakeDepositCommandResponse> Handle(MakeDepositCommand command)
        {
            var makeDepositCommandResponse = new MakeDepositCommandResponse();
            var validator = new MakeDepositCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (validationResult.Errors.Any())
            {
                makeDepositCommandResponse.Success = false;

                foreach (var error in validationResult.Errors)
                {
                    makeDepositCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (makeDepositCommandResponse.Success)
            {
                //Just a mock
                var customer = new Customer(Guid.NewGuid().ToString(), "test", "test", "12345678901", "123456", 0);
                customer.Deposit(command.Amount);
            }

            return makeDepositCommandResponse;
        }
    }
}