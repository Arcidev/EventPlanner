using System;
using System.Collections.Generic;

namespace EventPlanner.BL.DTO
{
    public class UserEventDTO
    {
        public IDictionary<PlaceDTO, IList<DateTime>> Choices { get; set; }
    }
}
