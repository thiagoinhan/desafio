using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Contracts.Querys
{
    public interface IQueryDispatcher
    {
        Task<TQueryResult> Dispatch<TQueryResult>(IQuery<TQueryResult> query);
    }
}