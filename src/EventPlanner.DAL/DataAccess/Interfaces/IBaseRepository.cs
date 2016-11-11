using EventPlanner.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.DAL.DataAccess.Interfaces
{
    public interface IBaseRepository<T> where T : IEntity
    {
        Task AddAsync(T entity);

        Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter);

        Task DeleteAsync(ObjectId id);

        Task<T> GetAsync(ObjectId id);

        Task UpdateAsync(ObjectId id, UpdateDefinition<T> updateDefinition);
    }
}
