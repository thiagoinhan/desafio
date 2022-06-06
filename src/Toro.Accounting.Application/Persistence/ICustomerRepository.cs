using Toro.Accounting.Domain.Entities;

namespace Toro.Accounting.Application.Contracts.Persistence
{
    public interface ICustomerRepository : IGenericRepositoryAsync<Customer>
    {
        public Task<Customer> GetCustomerByAccountNumber(string accountNumber);
        public Task<string> GetCustomerCPFByAccountNumber(string accountNumber);
        public Task<bool> AccountNumberExists(string accountNumber);
    }
}
