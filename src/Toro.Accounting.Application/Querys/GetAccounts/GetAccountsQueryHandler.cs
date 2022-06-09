using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Application.Contracts.Querys;
using Toro.Accounting.Application.Dtos;

namespace Toro.Accounting.Application.Querys
{
    public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, List<AccountDetails>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAccountsQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<AccountDetails>> Handle(GetAccountsQuery request)
        {
            var customers = await _customerRepository.GetAllAsync();

            if (customers == null)
                return null;

            return customers
                .Select(c => new AccountDetails("352", "0001", c.Id, c.Name, c.CPF, c.AccountNumber, c.AccountBalance))
                .ToList();
        }
    }
}
