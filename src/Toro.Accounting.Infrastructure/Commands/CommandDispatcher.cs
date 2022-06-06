using Toro.Accounting.Application.Contracts;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<TCommandResult> Dispatch<TCommandResult>(ICommand<TCommandResult> command)
        {
            var type = typeof(ICommandHandler<,>);
            var argTypes = new Type[] { command.GetType(), typeof(TCommandResult) };
            var handlerType = type.MakeGenericType(argTypes);

            dynamic? handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"Could not create a handler for type {handlerType}");
            return await handler.Handle((dynamic)command);
        }
    }
}