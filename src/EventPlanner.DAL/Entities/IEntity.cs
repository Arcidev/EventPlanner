using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventPlanner.DAL.Entities
{
    public interface IEntity
    {

        [BsonElement("_id")]
        ObjectId Id { get; set; }
    }
}
