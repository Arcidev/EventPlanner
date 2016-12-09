using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace EventPlanner.DAL.Entities
{
    public class Event : IEntity
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<string> SenderList { get; set; }

        public IList<Place> Places { get; set; }

        public IList<DateTime> Times { get; set; }
    }
}
