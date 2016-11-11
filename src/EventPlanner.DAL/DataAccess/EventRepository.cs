using EventPlanner.DAL.DataAccess.Interfaces;
using EventPlanner.DAL.Entities;

namespace EventPlanner.DAL.DataAccess
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository() : base("events") { }
    }
}
