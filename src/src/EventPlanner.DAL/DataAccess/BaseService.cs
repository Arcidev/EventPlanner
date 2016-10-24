using MongoDB.Driver;
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

        public async Task Add(T entity)
        {
            var collection = GetCollection();
            await collection.InsertOneAsync(entity);
        }

        protected IMongoCollection<T> GetCollection()
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("EventPlanner");
            return db.GetCollection<T>(collectionName);
        }
    }
}
