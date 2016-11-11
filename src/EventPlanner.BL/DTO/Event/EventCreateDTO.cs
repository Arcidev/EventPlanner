using System;
using System.Collections.Generic;

namespace EventPlanner.BL.DTO
{
    public class EventCreateDTO
    {
        public IList<string> SenderList { get; set; }

        public IList<PlaceDTO> Places { get; set; }

        public IList<DateTime> Times { get; set; }
    }
}
