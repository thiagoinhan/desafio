using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Commom.UnitTests.Mocks;
using Toro.Accounting.Persistence.Repositories;

namespace Toro.Accounting.EndToEndTests
{
    public class WebApplicationFactoryFixture : IDisposable
    {
        public WebApplicationFactory<Program> WebApplicationFactory { get; }

        public WebApplicationFactoryFixture()
        {
            var mockCustomerRepository = RepositoryMocks.GetCustomerRepository();

            this.WebApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {

                builder.ConfigureServices(services =>
                {
                    var descriptorToRemove =
                        new ServiceDescriptor(
                            typeof(ICustomerRepository),
                            typeof(CustomerRepository),
                            ServiceLifetime.Scoped);

                    var descriptorToAdd =
                        new ServiceDescriptor(
                            typeof(ICustomerRepository), (p) => {
                                return mockCustomerRepository.Object;
                            },
                            ServiceLifetime.Scoped);

                    services.Remove(descriptorToRemove);
                    services.Add(descriptorToAdd);
                });
            });
        }

        public void Dispose() => WebApplicationFactory?.Dispose();
    }
}
