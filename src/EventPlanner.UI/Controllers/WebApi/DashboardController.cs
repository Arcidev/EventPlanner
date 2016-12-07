using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EventPlanner.BL.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.UI.Controllers.WebApi
{
    public class EventPageVM
    {
        public int SelectedPlaceId { get; set; }
        public MarkerVM[] Markers { get; set; }
        public TableVM[] Tables { get; set; }
    }

    public class TableVM
    {
        public int Key { get; set; }
        public HeaderVM Header { get; set; }
        public UserRowVM[] UserRows { get; set; }

    }

    public class HeaderVM
    {
        public DateVM[] Dates { get; set; }
    }

    public class DateVM
    {
        public string Value { get; set; }
        public string[] Hours { get; set; }
    }

    public class UserRowVM
    {
        public string UserName { get; set; }
        public int[] Choices { get; set; }
    }

    public class MarkerVM
    {
        public string Title { get; set; }
        public int Key { get; set; }
        public PositionVM Position { get; set; }
    }

    public class PositionVM
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class UserEditRowVM
    {
        public string UserName { get; set; }

        [Required]
        public int[] Hours { get; set; } = new int[] { };
    }

    public class DashboardController : Controller
    {
        [HttpGet]
        [Route("events/{eventId}/get")]
        public IActionResult GetEventData(string eventId)
        {
            var page = new EventPageVM
            {
                SelectedPlaceId = 1,
                Markers = new MarkerVM[] {
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
                Tables = new TableVM[] {
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
                            Dates = new DateVM [] {
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
                        UserRows = new UserRowVM [] {
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
        [Route("events/{eventId}/save-choices")]
        public void SaveUserChoices(string eventId, [FromBody]UserEditRowVM editRow)
        {
            //TODO save here
        }
    }
}
