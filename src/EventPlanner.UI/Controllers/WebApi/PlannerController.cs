using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.WebApiModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.UI.Controllers.WebApi
{
    public class PlannerController : Controller
    {
        private readonly IEventFacade eventFacade;
        private readonly IUserFacade userFacade;

        public PlannerController(IEventFacade eventFacade, IUserFacade userFacade)
        {
            this.eventFacade = eventFacade;
            this.userFacade = userFacade;
        }

        [HttpGet]
        [Route("event/{eventId}/edit/get")]
        public async Task<IActionResult> GetEventEditData(string eventId)
        {
            var eventDto = await eventFacade.GetEvent(eventId);
            /*
            var eventData = {
            name : "Pavlova oslava narozek",
            desc: "Po roce se zase shledame, dame neco dobryho k jidlu a piti a poprejeme Pavlovi k jeho 25. narozkam.",
            people: ["john.smith77@gmail.com", "teri899@yahoo.com"],
            dates: ["2016-12-30T20:40:00", "2016-12-30T21:40:00", "2016-12-31T17:00:00"],
            places: [{ lat: 59.938043, lng: 30.337157 }, { lat: 59.938, lng: 30.33 }]
            */

            var data = new EventEdit
            {
                Name = eventDto.Name,
                Desc = eventDto.Description,
                People = eventDto.SenderList.ToArray(),
                Dates = eventDto.Times.Select(x => x.ToString()).ToArray()
            };

            return new ObjectResult(data);
        }

        [HttpGet]
        [Route("event/{eventId}/get")]
        public async Task<IActionResult> GetEventData(string eventId)
        {
            var eventDto = await eventFacade.GetEvent(eventId);
            var users = await eventFacade.GetUsersForEvent(eventId);

            var page = new EventPageVM();
            var index = 1;
            page.SelectedPlaceId = index;

            var id = ObjectId.Parse(eventId);
            page.Tables = eventDto.Places.Select(place =>
            {
                var placeUsers = users.Where(x => x.UserEvents[id].Choices.ContainsKey(index));
                var rest = users.Where(x => !placeUsers.Any(y => x.Id == y.Id));
                var userRows = placeUsers.Select(user => new UserRowVM()
                {
                    UserName = user.Email,
                    Choices = user.UserEvents[id].Choices[index++]
                }).ToList();
                userRows.AddRange(rest.Select(user => new UserRowVM()
                {
                    UserName = user.Email
                }));

                return new TableVM()
                {
                    Key = index,
                    Header = new HeaderVM()
                    {
                        Dates = eventDto.Times.OrderBy(x => x).GroupBy(y => y.Date).Select(z => new DateVM
                        {
                            Value = z.Key.ToString("dd:MM:yyyy"),
                            Hours = z.Select(a => a.TimeOfDay.ToString("hh:mm")).ToArray()
                        }).ToArray()
                    },
                    UserRows = userRows.ToArray()
                };
            }).ToArray();

            return new ObjectResult(page);
        }

     

        [HttpPost]
        [Route("event/{eventId}/save-choices")]
        public async Task SaveUserChoices(string eventId, [FromBody]UserEditRowVM editRow)
        {
            var user = await userFacade.CreateOrGetUser(editRow.UserName);

            UserEventDTO userEvent;
            if (!user.UserEvents.TryGetValue(ObjectId.Parse(eventId), out userEvent))
                userEvent = new UserEventDTO();

            userEvent.Choices[editRow.TableKey] = editRow.Hours;
            await eventFacade.SignUpForEvent(eventId, user.Id, userEvent);
        }
    }
}
