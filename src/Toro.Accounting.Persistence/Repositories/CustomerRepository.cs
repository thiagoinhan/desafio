using MongoDB.Driver;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Domain.Entities;

namespace Toro.Accounting.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IMongoDatabase database) : base(database, Customer.TableName)
        {
        }

        public async Task<bool> AccountNumberExists(string accountNumber)
        {
            return await collection.Find(w => w.AccountNumber == accountNumber).AnyAsync();
        }

        public async Task<Customer> GetCustomerByAccountNumber(string accountNumber)
        {
            var customer = await collection.Find(w => w.AccountNumber == accountNumber).FirstAsync();
            return customer;
        }

        public async Task<string> GetCustomerCPFByAccountNumber(string accountNumber)
        {
            var customerCPF = await collection.Find(w => w.AccountNumber == accountNumber)
                .Project(c => c.CPF)
                .FirstAsync();

            return customerCPF;
        }
    }
}
