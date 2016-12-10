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
            var e = new EventCreateDTO()
            {
                Name = "Event in London and Paris",
                Description = "This is event only for testing purposes with Markers",
                SenderList = new[] { "rluks@seznam.cz", "luksromanluks@gmail.com" },
                Times = new[] { System.DateTime.UtcNow, System.DateTime.UtcNow.Date },
                Places = new[] { new PlaceDTO() { X = 51.5074, Y = 0.1278, Title = "London" }, new PlaceDTO() { X = 48.8566, Y = 2.3522, Title = "Paris" } }
            };
            var eventDtoTest = await eventFacade.CreateEvent(e, "5835b855b7fc292064f6f6da");
            System.Diagnostics.Debug.WriteLine("eventDtoTest.Id:" + eventDtoTest.Id);
            */
            

            var data = new EventEdit
            {
                Name = eventDto.Name,
                Desc = eventDto.Description,
                People = eventDto.SenderList.ToArray(),
                Dates = eventDto.Times.Select(x => x.ToString("yyyy-MM-ddTHH:mm")).ToArray(),
                Markers = new MarkerVM[eventDto.Places.Count]
            };

            var places = eventDto.Places.OrderBy(x => x.Title).ToList();
            for (int i = 0; i < places.Count; i++)
            {
                var place = places.ElementAt(i);
                data.Markers[i] = new MarkerVM()
                {
                    // i => index of the place
                    Key = i,
                    Position = new PositionVM()
                    {
                        Lat = place.X,
                        Lng = place.Y
                    },
                    Title = place.Title
                };
            }

            return new ObjectResult(data);
        }

        [HttpGet]
        [Route("event/{eventId}/get")]
        public async Task<IActionResult> GetEventData(string eventId)
        {
            // Get event
            var eventDto = await eventFacade.GetEvent(eventId);

            // Get all users on this event
            // var users = await eventFacade.GetUsersForEvent(eventId);

            var page = new EventPageVM();
            page.SelectedPlaceId = 0;

            var id = ObjectId.Parse(eventId);
            page.Tables = new TableVM[eventDto.Places.Count];
            page.Markers = new MarkerVM[eventDto.Places.Count];

            var places = eventDto.Places.OrderBy(x => x.Title).ToList();
            for (int i = 0; i < places.Count; i++)
            {
                // Users that have some choices for this place
                // i => index of the place
                var placeUsers = eventDto.UserChoices.Where(x => x.Value.Choices.ContainsKey(i));

                // Get place we're working on
                var place = places.ElementAt(i);
                page.Tables[i] = new TableVM()
                {
                    // i => index of the place
                    Key = i,
                    Header = new HeaderVM()
                    {
                        // Group datetimes by date and then assign to every date time
                        Dates = eventDto.Times.OrderBy(x => x).GroupBy(y => y.Date).Select(z => new DateVM()
                        {
                            Value = z.Key.ToString("dd:MM:yyyy"),
                            Hours = z.Select(a => a.TimeOfDay.ToString("hh:mm")).ToArray()
                        }).ToArray()
                    },
                };

                // Add users with choices
                var userRows = eventDto.UserChoices.Keys.Select(user =>
                {
                    int[] choices;
                    if (!eventDto.UserChoices[user].Choices.TryGetValue(i, out choices))
                    {
                        // user without choices
                        choices = page.Tables[i].Header.Dates.SelectMany(x => x.Hours).Select(x => -1).ToArray();
                    }
                    return new UserRowVM()
                    {
                        UserName = user,
                        Choices = choices
                    };
                });
                page.Tables[i].UserRows = userRows.ToArray();

                page.Markers[i] = new MarkerVM()
                {
                    // i => index of the place
                    Key = i,
                    Position = new PositionVM()
                    {
                        Lat = place.X,
                        Lng = place.Y
                    },
                    Title = place.Title
                };
            }

            return new ObjectResult(page);
        }

        [HttpPost]
        [Route("event/{eventId}/save-choices")]
        public async Task SaveUserChoices(string eventId, [FromBody]UserEditRowVM editRow)
        {
            var user = await userFacade.CreateOrGetUser(editRow.UserName);
            var e = await eventFacade.GetEvent(eventId);

            UserEventDTO userEvent;
            if (e.UserChoices == null || !e.UserChoices.TryGetValue(user.Email, out userEvent))
                userEvent = new UserEventDTO();

            userEvent.Choices[editRow.TableKey] = editRow.Hours;
            await eventFacade.SignUpForEvent(eventId, user.Id, userEvent);
        }
    }
}
