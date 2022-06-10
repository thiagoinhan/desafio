using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Moq;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Commom.UnitTests.Mocks;
using Toro.Accounting.Domain.Entities;

namespace Toro.Accounting.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var mockCustomerRepository = RepositoryMocks.GetCustomerRepository();

            var mockMongoDatabase = new Mock<IMongoDatabase>();
            mockMongoDatabase.Setup(db => db.GetCollection<Customer>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Verifiable();

            builder.UseEnvironment("test");

            builder.ConfigureServices(services =>
            {
                var descriptorToAdd = new ServiceDescriptor(
                    typeof(ICustomerRepository), (p) =>
                    {
                        return mockCustomerRepository.Object;
                    },
                    ServiceLifetime.Scoped);

                services.Add(descriptorToAdd);

                var descriptorToAdd2 =
                new ServiceDescriptor(
                    typeof(IMongoDatabase), (p) =>
                    {
                        return mockMongoDatabase.Object;
                    },
                    ServiceLifetime.Singleton);

                services.Add(descriptorToAdd2);
            });
        }
    }
}
