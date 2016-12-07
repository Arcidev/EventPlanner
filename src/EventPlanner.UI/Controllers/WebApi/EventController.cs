using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.WebApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.UI.Controllers.WebApi
{
    [Route("api/[controller]/{eventId}")]
    public class EventController : Controller
    {
        private IEventFacade EventFacade { get; set; }

        public EventController(IEventFacade eventFacade)
        {
            EventFacade = eventFacade;
        }

        // GET: api/event/{eventId}/users
        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<UserDTO>> GetUsers(string eventId)
        {
            return await EventFacade.GetUsersForEvent(eventId);
        }

        // GET: api/event/{eventId}/places
        [HttpGet]
        [Route("places")]
        public async Task<IEnumerable<PlaceDTO>> GetPlaces(string eventId)
        {
            return await EventFacade.GetEventPlaces(eventId);
        }

        // GET: api/event/{eventId}/signUp
        [HttpPost]
        [Route("signUp")]
        public async Task SignUpForEvent(string eventId, [FromBody] SignUpForEventModel signUpModel)
        {
            await EventFacade.SignUpForEvent(eventId, signUpModel.UserId, signUpModel.Choices);
        }
    }
}
