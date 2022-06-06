using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Toro.Accounting.Application.Contracts;

namespace Toro.Accounting.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.TryAddScoped<ICommandDispatcher, CommandDispatcher>();

            // INFO: Using https://www.nuget.org/packages/Scrutor for registering all Query and Command handlers by convention
            services.Scan(selector =>
            {
                selector.FromAssembliesOf(typeof(ICommandHandler<,>))
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });

            return services;
        }
    }
}
