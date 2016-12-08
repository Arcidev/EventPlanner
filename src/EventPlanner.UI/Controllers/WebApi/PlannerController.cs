using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EventPlanner.BL.DTO;
using EventPlanner.WebApiModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EventPlanner.UI.Controllers.WebApi
{
    public class PlannerController : Controller
    {
        [HttpGet]
        [Route("event/{eventId}/edit/get")]
        public IActionResult GetEventEditData(string eventId)
        {
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
                Name = "Pavlova Oslava Narozek"
            };

            return new ObjectResult(data);
        }

        [HttpGet]
        [Route("event/{eventId}/get")]
        public IActionResult GetEventData(string eventId)
        {
            //0 -no, 1-yes, 2-maybe
            var page = new EventPageVM
            {
                SelectedPlaceId = 1,
                Markers = new[] {
                     new MarkerVM {
                    Title = "Toulouse",
                        Key = 1,
                        Position = new PositionVM
                        {
                            Lat = 43.604363,
                            Lng = 1.443363,
                        }
                    },
                    new MarkerVM {
                    Title = "Zero",
                        Key = 2,
                        Position = new PositionVM
                        {
                            Lat = 0,
                            Lng = 0,
                        }
                    }
                },
                Tables = new[] {
                    new TableVM {
                        Key = 1,
                        UserRows = new UserRowVM [] {
                            new UserRowVM {
                                UserName = "Nick",
                                Choices = new int [] { 1, 0, 2, 1, 2 }
                            },
                            new UserRowVM {
                                UserName = "Judy",
                                Choices = new int [] { 0, 0, 1, 1, 2 }
                            },
                        },
                        Header = new HeaderVM
                        {
                            Dates = new[] {
                                new DateVM {
                                    Value = "2. 3. 2016",
                                    Hours = new string [] { "8:00", "9:00", "10:00" }
                                },
                                new DateVM {
                                    Value=  "2. 4. 2019",
                                    Hours = new string [] { "10:00", "11:00" }
                                }
                            }
                        }
                    },
                    new TableVM {
                        Key = 2,
                        UserRows = new[] {
                            new UserRowVM {
                                UserName = "Tom",
                                Choices = new int [] { 1, 0, 1, 1}
                            },
                        },
                        Header = new HeaderVM
                        {
                            Dates = new DateVM [] {
                                new DateVM {
                                    Value = "3. 4. 2016",
                                    Hours = new string [] { "8:00", "9:00" }
                                },
                                new DateVM {
                                    Value=  "2. 4. 2019",
                                    Hours = new string [] { "10:00", "11:00" }
                                }
                            }
                        }
                    }
                }
            };

            return new ObjectResult(page);
        }

     

        [HttpPost]
        [Route("event/{eventId}/save-choices")]
        public void SaveUserChoices(string eventId, [FromBody]UserEditRowVM editRow)
        {
            //TODO save here
        }
    }
}
