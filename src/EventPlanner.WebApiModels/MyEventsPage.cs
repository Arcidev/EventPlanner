using System.Collections.Generic;

namespace EventPlanner.WebApiModels
{
    public class MyEventsPage
    {
        public List<EventListItemVM> Created { get; set; } = new List<EventListItemVM>();
        public List<EventListItemVM> InvitedTo { get; set; } = new List<EventListItemVM>();
    }
}