using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.UI.Controllers.Web
{
    [Route("/[action]")]
    public class PlannerController : Controller
    {
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/event/{eventId}/")]
        public IActionResult EventDetail(string eventId)
        {
            return View();
        }

        [HttpGet]
        [Route("/event/{eventId}/edit")]
        public IActionResult EventEdit(string eventId)
        {
            return View();
        }

        [HttpGet]
        [Route("/event/{eventId}/edit")]
        public IActionResult MyEvents()
        {
            return View();
        }
    }
}
