using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
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

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public IDictionary<string, UserEvent> UserChoices { get; set; }
    }
}
