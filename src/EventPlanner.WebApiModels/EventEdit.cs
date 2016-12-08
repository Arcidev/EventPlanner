using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.WebApiModels
{
    public class EventEdit
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string[] People { get; set; }
        public string[] Dates { get; set; }
    }
}
