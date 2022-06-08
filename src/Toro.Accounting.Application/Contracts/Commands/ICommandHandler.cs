using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Contracts.Commands
{
    public interface ICommandHandler<TCommand, TCommandResult> where TCommand : ICommand<TCommandResult>
    {
        Task<TCommandResult> Handle(TCommand command);
    }
}