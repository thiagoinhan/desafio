using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Application.Contracts.Querys;
using Toro.Accounting.Application.Dtos;

namespace Toro.Accounting.Application.Querys.GetAccountDetails
{
    public class GetAccountDetailsHandler : IQueryHandler<GetAccountDetailsQuery, AccountDetails>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAccountDetailsHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<AccountDetails> Handle(GetAccountDetailsQuery request)
        {
            var customer = await _customerRepository.GetAsync(Guid.Parse(request.UserId));

            if (customer == null)
                return null;

            return new AccountDetails("352", "0001", customer.Name, customer.AccountNumber, customer.AccountBalance);
        }
    }
}
