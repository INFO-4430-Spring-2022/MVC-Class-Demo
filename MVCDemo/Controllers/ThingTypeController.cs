using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class ThingTypeController : Controller {

        int a;


        public IActionResult Index() {
            return View();
        }

        public IActionResult About(string value, 
            int? number) {
            
            if (value != null) {
                ViewData["Stuff"] = value;
            } else {
                ViewData["Stuff"] = "Nothing";
            }

            return View();
        }

        [HttpPost]
        public IActionResult About(string value,
    int? number, bool? yesNo) {
            if (value != null) {
                ViewData["Stuff"] = value;
            } else {
                ViewData["Stuff"] = "Nothing";
            }

            return View();
        }

        public IActionResult Create() {
            ThingType tType = new ThingType();
            tType.ID = 1234;
            tType.Name = "Doo Hickey";

            return View(tType);
        }

        [HttpPost]
        public IActionResult Create(
            //int id,
            //string Name,
            //bool CanShare
            ThingType tType
            ) {

            return View();

        }


    }
}
