using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCDemo.Models;
using System.Diagnostics;

namespace MVCDemo.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
            private User _CurrentUser = null;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
        _CurrentUser = UserController.GetLoggedInUser(Request);
            ViewBag.CurrentUser = _CurrentUser;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Hotdog() {
            return View();
        }

        public IActionResult Frank() {
            return View("HotDog");
        }

        public IActionResult Types() {
            return View("ListofTypes");
        }

        public IActionResult MakeMe() {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}