using System.Linq.Expressions;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Contracts.Persistence
{
    public interface IGenericRepositoryAsync<T> where T : IHasKey<Guid>
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}
