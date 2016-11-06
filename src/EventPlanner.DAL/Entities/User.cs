using System;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EventPlanner.DAL.Entities
{
    public class User : IEntity
    {
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public Dictionary<Place, DateTime []> Choices { get; set; }

        public ObjectId [] CreatedEvents { get; set; }
    }
}
