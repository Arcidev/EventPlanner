using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.UI.Controllers.Web
{
    [Route("/[action]")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("/Event/Edit")]
        public IActionResult EventEdit()
        {
            return View();
        }
    }
}
