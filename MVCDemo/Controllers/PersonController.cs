using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers {
    public class PersonController : Controller {
        public IActionResult Index() {
            //ViewBag.Pizza = "Pepperoni";
            //ViewData["IceCream"] = "Chocolate";
            return View();
        }

        public IActionResult Details() {
            TempData["Pizza"] = "Meat Lovers";
            return RedirectToAction("Index");
        }

    }
}
