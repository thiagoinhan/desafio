using Moq;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Domain.Entities;

namespace Toro.Accounting.Commom.UnitTests.Mocks
{
    public static class RepositoryMocks
    {
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer(Guid.Parse("9623f6d8-01d7-420f-9264-2143df3557cd"), "luffy", "Monkey D. Luffy", "87498578964", "283749", 2000),
                new Customer(Guid.Parse("56fe0147-97d1-4b24-87c1-00ff67c40f8f"), "tanjiro", "Tanjiro Kamado", "74894803977", "839456", 0),
                new Customer(Guid.Parse("bef882d7-9359-44ec-9f4b-77c71413592c"), "eren", "Eren Yeager", "17384975822", "139029", 1000),
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);
            mockCustomerRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(
                (Guid guid) =>
                {
                    return customers.Find(c => c.Id == guid);
                });

            mockCustomerRepository.Setup(repo => repo.GetCustomerByAccountNumber(It.IsAny<string>())).ReturnsAsync(
               (string accountNumber) =>
               {
                   return customers.Find(c => c.AccountNumber == accountNumber);
               });

            mockCustomerRepository.Setup(repo => repo.AccountNumberExists(It.IsAny<string>())).ReturnsAsync(
               (string accountNumber) =>
               {
                   return customers.Any(c => c.AccountNumber == accountNumber);
               });

            mockCustomerRepository.Setup(repo => repo.GetCustomerCPFByAccountNumber(It.IsAny<string>())).ReturnsAsync(
               (string accountNumber) =>
               {
                   return customers.Find(c => c.AccountNumber == accountNumber)?.CPF;
               });

            mockCustomerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Customer>())).Returns(
                (Customer customer) =>
                {
                    var index = customers.FindIndex(c => c.Id == customer.Id);

                    if (index != -1)
                    {
                        customers[index] = customer;
                    }

                    return Task.CompletedTask;
                });

            mockCustomerRepository.Setup(repo => repo.CreateAsync(It.IsAny<Customer>())).Returns(
                (Customer customer) =>
                {
                    customers.Add(customer);
                    return Task.CompletedTask;
                });

            return mockCustomerRepository;
        }
    }
}
