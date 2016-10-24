using EventPlanner.DAL.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.DAL.DataAccess
{
    public class EventService : BaseService<Event>
    {
        public EventService() : base("events") { }

        public async Task<IList<Event>> GetByAuthor(string authorId)
        {
            var filter = Builders<Event>.Filter.Eq("AuthorId", authorId);
            return await FindAsync(filter);
        }
    }
}
