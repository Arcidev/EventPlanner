using EventPlanner.DAL.Entities;

namespace EventPlanner.DAL.DataAccess
{
    public class EventRepository : BaseRepository<Event>
    {
        public EventRepository() : base("events") { }
    }
}
