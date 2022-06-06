using Toro.Accounting.Application.Contracts;
using Toro.Accounting.Application.Contracts.Persistence;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommandHandler : ICommandHandler<MakeDepositCommand, MakeDepositCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public MakeDepositCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<MakeDepositCommandResponse> Handle(MakeDepositCommand command)
        {
            var makeDepositCommandResponse = new MakeDepositCommandResponse();
            var validator = new MakeDepositCommandValidator(_customerRepository);
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
                var customer = await _customerRepository.GetCustomerByAccountNumber(command.AccountNumber);
                customer.Deposit(command.Amount);
                await _customerRepository.UpdateAsync(customer);
            }

            return makeDepositCommandResponse;
        }
    }
}