using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.UI.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IEventFacade EventFacade { get; set; }

        public UserController(IEventFacade eventFacade)
        {
            EventFacade = eventFacade;
        }

        // GET: api/user/{userId}/events
        [HttpGet]
        [Route("{userId}/events")]
        public async Task<IEnumerable<EventDTO>> GetEvents(string userId)
        {
            return await EventFacade.GetUserEvents(userId);
        }

        // POST: api/user/{userId}/createEvent
        [HttpPost]
        [Route("{userId}/createEvent")]
        public async Task<EventDTO> CreateEvent(string userId, [FromBody] EventDTO dto)
        {
            return await EventFacade.CreateEvent(dto, userId);
        }
    }
}
