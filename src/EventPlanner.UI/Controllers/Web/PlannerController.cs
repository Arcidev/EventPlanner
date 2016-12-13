using System.Collections.Generic;
using EventPlanner.WebApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventPlanner.BL.Facades.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using EventPlanner.BL.DTO;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.UI.Controllers.Web
{
    [Authorize, Route("/[action]")]
    public class PlannerController : Controller
    {
        private IUserFacade userFacade;
        private IEventFacade eventFacade;

        public PlannerController(IUserFacade userFacade, IEventFacade eventFacade)
        {
            this.userFacade = userFacade;
            this.eventFacade = eventFacade;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        [Route("/event/{eventId}")]
        public IActionResult EventDetail(string eventId)
        {
            return View();
        }

        [HttpGet]
        [Route("/event/new")]
        public async Task<IActionResult> NewEvent()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await userFacade.CreateOrGetUser(email);

            var newEvent = await eventFacade.CreateEvent(new EventCreateDTO(), user.Id);
            return RedirectToAction(nameof(EventEdit), new { eventId = newEvent.Id });
        }

        [HttpGet]
        [Route("/event/{eventId}/edit")]
        public IActionResult EventEdit(string eventId)
        {
            return View();
        }

        [HttpGet]
        [Route("/events")]
        public async Task<IActionResult> MyEvents()
        {
            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = await userFacade.CreateOrGetUser(email);
            var eventsCreated = await eventFacade.GetUserEvents(user.Id);
            var invitedTo = await eventFacade.GetInvitedToEvents(user.Id);

            var model = new MyEventsPage()
            {
                InvitedTo = invitedTo.Select(e=> new EventListItemVM()
                {
                    CanEdit = false,
                    Name = e.Name,
                    EventId = e.Id,
                    IdTag = "my"
                }).ToList(),
                Created = eventsCreated.Select(e => new EventListItemVM()
                {
                    CanEdit = true,
                    EventId = e.Id,
                    Name = e.Name,
                    IdTag = "inv"
                }).ToList()
            };
            return View(model);
        }
    }
}
