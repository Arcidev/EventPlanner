using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using System.Threading.Tasks;

namespace EventPlanner.UI.Controllers.Web
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public IActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult("Google", new AuthenticationProperties { RedirectUri = returnUrl });
        }

        // GET: /Account/Login
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
