using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace EventPlanner.BL.DTO
{
    public class EventCreateDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<string> SenderList { get; set; }

        public IList<PlaceDTO> Places { get; set; }

        public IList<DateTime> Times { get; set; }

        public IDictionary<string, UserEventDTO> UserChoices { get; set; }
    }
}
