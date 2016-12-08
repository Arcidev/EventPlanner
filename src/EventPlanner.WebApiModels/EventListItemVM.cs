using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.WebApiModels
{
    public class EventListItemVM
    {
        public string Name { get; set; }
        public bool CanEdit { get; set; }
        public string EventId { get; set; }
    }
}
