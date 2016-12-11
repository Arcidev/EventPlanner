using EventPlanner.BL.DTO;
using EventPlanner.BL.Facades.Interfaces;
using EventPlanner.WebApiModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        [Route("event/{eventId}/edit/save")]
        public async Task SaveEventEditData(string eventId, [FromBody]EventEdit changedEvent)
        {
            System.Diagnostics.Debug.WriteLine(changedEvent.ToJson());
            List<PlaceDTO> places = changedEvent.Markers.Select(marker => {
                return new PlaceDTO()
                {
                    Title = marker.Title,
                    X = marker.Position.Lat,
                    Y = marker.Position.Lng
                };
            }).ToList();
            var dates = new List<DateTime>() { };
            foreach (var date in changedEvent.Dates) {
                DateTime dateValue;
                if (DateTime.TryParse(date, out dateValue))
                {
                    dates.Add(dateValue);
                }
            }
            EventDTO eventDto = new EventDTO()
            {
                Name = changedEvent.Name,
                Description = changedEvent.Desc,
                SenderList = changedEvent.People,
                Times = dates,
                Places = places
            };
            await eventFacade.EditEvent(eventId, eventDto);
        }

        [HttpGet]
        [Route("event/{eventId}/edit/get")]
        public async Task<IActionResult> GetEventEditData(string eventId)
        {
            var eventDto = await eventFacade.GetEvent(eventId);
           
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
