using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(string username, string password) {
            return RedirectToAction("Index", "Home");
        }


    }
}
