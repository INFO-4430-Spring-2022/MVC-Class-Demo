using Microsoft.AspNetCore.Mvc;

namespace MVCDemo.Controllers {
    public class SandwichController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
