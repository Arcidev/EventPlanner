using EventPlanner.BL.DTO;
using EventPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.BL.Facades.Interfaces
{
    public interface IEventFacade
    {
        Task<IDictionary<string, IList<Tuple<string, bool>>>> GetEventUsersTimes(string eventId, PlaceDTO place);

        Task<IList<PlaceDTO>> GetEventPlaces(string eventId);

        Task<IEnumerable<EventDTO>> GetUserEvents(string userId);

        Task<EventDTO> CreateEvent(EventCreateDTO e, string userId);

        Task SignUpForEvent(string eventId, string userId, UserEventDTO choices);

        Task<IEnumerable<UserDTO>> GetUsersForEvent(string eventId);
    }
}
