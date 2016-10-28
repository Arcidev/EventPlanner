using MongoDB.Bson;

namespace EventPlanner.DAL.Entities
{
    public class Event : IEntity
    {
        public ObjectId _id { get; set; }

        public string AuthorId { get; set; }
    }
}
