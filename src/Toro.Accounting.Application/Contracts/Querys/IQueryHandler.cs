using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Contracts.Querys
{
    public interface IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> Handle(TQuery query);
    }
}