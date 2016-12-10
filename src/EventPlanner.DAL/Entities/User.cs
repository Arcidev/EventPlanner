using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace EventPlanner.DAL.Entities
{
    public class User : IEntity
    {
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public IList<ObjectId> CreatedEvents { get; set; }
    }
}
