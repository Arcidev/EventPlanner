using EventPlanner.BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.BL.Facades.Interfaces
{
    public interface IEventFacade
    {
        Task<IList<PlaceDTO>> GetEventPlaces(string eventId);

        Task<IEnumerable<EventDTO>> GetUserEvents(string userId);

        Task<EventDTO> CreateEvent(EventCreateDTO e, string userId);

        Task SignUpForEvent(string eventId, string userId, UserEventDTO choices);

        //Task<IEnumerable<UserDTO>> GetUsersForEvent(string eventId);

        Task<EventDTO> GetEvent(string id);

        Task<IEnumerable<UserDTO>> GetUsersForEvent(string eventId);

        Task<IEnumerable<EventDTO>> GetInvitedToEvents(string userId);
    }
}
