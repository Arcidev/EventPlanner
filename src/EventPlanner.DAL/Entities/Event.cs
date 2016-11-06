using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EventPlanner.DAL.Entities
{
    public class Event : IEntity
    {
        public ObjectId Id { get; set; }

        public List<string> SenderList { get; set; }

        public List<Place> Places { get; set; }

        public List<DateTime> Times { get; set; }

        
    }
}
