using System;
using MongoDB.Bson;
using System.Collections.Generic;

namespace EventPlanner.DAL.Entities
{
    public class User : IEntity
    {
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public Dictionary<Place, List<DateTime>> Choices { get; set; }

        public List<ObjectId> CreatedEvents { get; set; }
    }
}
