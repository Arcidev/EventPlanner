using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.DAL.DataAccess
{
    public abstract class BaseService<T> where T : class
    {
        private string collectionName;

        public BaseService(string collectionName)
        {
            this.collectionName = collectionName;
        }

        public async Task AddAsync(T entity)
        {
            var collection = GetCollection();
            await collection.InsertOneAsync(entity);
        }

        public async Task<IList<T>> FindAsync(FilterDefinition<T> filter)
        {
            var collection = GetCollection();
            return await (await collection.FindAsync(filter)).ToListAsync();
        }

        protected IMongoCollection<T> GetCollection()
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("EventPlanner");
            return db.GetCollection<T>(collectionName);
        }
    }
}
