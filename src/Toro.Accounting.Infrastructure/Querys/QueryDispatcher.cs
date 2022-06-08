using Toro.Accounting.Application.Contracts.Querys;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Infrastructure
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TQueryResult> Dispatch<TQueryResult>(IQuery<TQueryResult> query)
        {
            var type = typeof(IQueryHandler<,>);
            var argTypes = new Type[] { query.GetType(), typeof(TQueryResult) };
            var handlerType = type.MakeGenericType(argTypes);

            dynamic? handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"Could not create a handler for type {handlerType}");
            return await handler.Handle((dynamic)query);
        }
    }
}