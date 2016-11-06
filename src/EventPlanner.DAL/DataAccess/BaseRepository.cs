using EventPlanner.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.DAL.DataAccess
{
    public abstract class BaseRepository<T> where T : IEntity
    {
        private string collectionName;

        protected BaseRepository(string collectionName)
        {
            this.collectionName = collectionName;
        }

        public async Task AddAsync(T entity)
        {
            var collection = GetCollection();
            await collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(FilterDefinition<T> filter)
        {
            var collection = GetCollection();
            return await collection.Find(filter).ToListAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var collection = GetCollection();
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<T> GetAsync(ObjectId id)
        {
            var collection = GetCollection();
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ObjectId id, UpdateDefinition<T> updateDefinition)
        {
            var collection = GetCollection();
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.UpdateOneAsync(filter, updateDefinition);
        }

        protected IMongoCollection<T> GetCollection()
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("EventPlanner");
            return db.GetCollection<T>(collectionName);
        }
    }
}
