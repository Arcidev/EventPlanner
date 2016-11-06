using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EventPlanner.DAL.Entities
{
    public class Event : IEntity
    {
        public ObjectId Id { get; set; }

        public string AuthorId { get; set; }

        public ObjectId [] Users { get; set; }

        public Place [] Places { get; set; }

        public DateTime[] Times { get; set; }

        
    }
}
