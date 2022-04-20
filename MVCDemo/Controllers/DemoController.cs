using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers {
    public class DemoController : Controller {

        // Route: Demo/Index or Demo/
        public IActionResult Index() {
            return View("../Home/FromDemo");
        }

        public IActionResult GetStuff() {
            return PartialView("../Sandwich/_Included");
        }


    }
}
