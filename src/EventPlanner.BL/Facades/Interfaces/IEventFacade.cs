using EventPlanner.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.BL.Facades.Interfaces
{
    public interface IEventFacade
    {
        Task<IEnumerable<EventDTO>> GetUserEvents(string userId);

        Task<EventDTO> CreateEvent(EventCreateDTO e, string userId);

        Task SignUpForEvent(string eventId, string userId, UserEventDTO choices);

        Task<IEnumerable<UserDTO>> GetUsersForEvent(string eventId);
    }
}
