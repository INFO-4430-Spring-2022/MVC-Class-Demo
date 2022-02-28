using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class PersonController : Controller {
        public IActionResult Index() {
            //ViewBag.Pizza = "Pepperoni";
            //ViewData["IceCream"] = "Chocolate";
            return View();
        }

        public IActionResult Details() {
            //TempData["Pizza"] = "Meat Lovers";
            //return RedirectToAction("Index");
            Person p = new Person();
            p.FirstName = "Bob";
            p.LastName = "Awesome";
            ThingType tType = new ThingType();
            tType.Name = "thing one";
            return View(p);
        }

    }
}
