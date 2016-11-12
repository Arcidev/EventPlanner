﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;

namespace EventPlanner.DAL.Entities
{
    public class UserEvent
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public IDictionary<Place, IList<DateTime>> Choices { get; set; }
    }
}
