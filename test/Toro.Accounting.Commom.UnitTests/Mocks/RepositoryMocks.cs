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
                new Customer(Guid.NewGuid(), "luffy", "Monkey D. Luffy", "87498578964", "283749", 2000),
                new Customer(Guid.NewGuid(), "tanjiro", "Tanjiro Kamado", "74894803977", "839456", 0),
                new Customer(Guid.NewGuid(), "eren", "Eren Yeager", "17384975822", "139029", 1000),
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            mockCustomerRepository.Setup(repo => repo.CreateAsync(It.IsAny<Customer>())).Returns(
                (Customer customer) =>
                {
                    customers.Add(customer);
                    return customer;
                });

            return mockCustomerRepository;
        }
    }
}
