using MongoDB.Bson;

namespace EventPlanner.DAL.Entities
{
    public interface IEntity
    {
        ObjectId _id { get; set; }
    }
}
