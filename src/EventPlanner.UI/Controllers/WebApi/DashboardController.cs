using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EventPlanner.BL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.UI.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        public List<UserEventDTO> Get()
        {
            return
                new List<UserEventDTO>
                {
                    new UserEventDTO
                    {
                        Choices = new Dictionary<PlaceDTO, IList<DateTime>>()
                        {
                            {new PlaceDTO {X = 10, Y = 15}, new List<DateTime> {DateTime.UtcNow, DateTime.UtcNow}}
                        }
                    }
                };
        }
    }
}
