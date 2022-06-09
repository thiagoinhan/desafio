using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Contracts.Commands
{
    public interface ICommandDispatcher
    {
        Task<TCommandResult> Dispatch<TCommandResult>(ICommand<TCommandResult> command);
    }
}