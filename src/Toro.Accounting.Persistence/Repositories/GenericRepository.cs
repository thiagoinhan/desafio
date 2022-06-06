using MongoDB.Driver;
using System.Linq.Expressions;
using Toro.Accounting.Application.Contracts.Persistence;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepositoryAsync<T> where T : IHasKey<Guid>
    {
        protected readonly IMongoCollection<T> collection;
        protected readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public GenericRepository(IMongoDatabase database, string collectionName)
        {
            collection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await collection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, entity.Id);
            await collection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, id);
            await collection.DeleteOneAsync(filter);
        }
    }
}
