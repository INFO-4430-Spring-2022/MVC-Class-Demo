using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers {
    public class DrinkController : Controller {
        public IActionResult Index() {
            Models.ThingType tType = new Models.ThingType();
            tType.ID = 45;

            return View();
        }

        public IActionResult RootBeer() {
            return View();
        }

    }
}
