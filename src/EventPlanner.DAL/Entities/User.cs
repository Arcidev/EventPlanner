using System;
using MongoDB.Bson;
using System.Collections.Generic;

namespace EventPlanner.DAL.Entities
{
    public class User : IEntity
    {
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public IDictionary<Place, IList<DateTime>> Choices { get; set; }

        public IList<ObjectId> CreatedEvents { get; set; }
    }
}
