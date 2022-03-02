using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class PersonController : Controller {
        public IActionResult Index() {
            //ViewBag.Pizza = "Pepperoni";
            //ViewData["IceCream"] = "Chocolate";
            return View("List", GetPeople());
        }

        public List<Person> GetPeople() {
            List<Person> lst = new List<Person>();
            lst.Add(getBob());
            lst.Add(new Person() { FirstName = "Sally", LastName = "Smith" });
            lst.Add(new Person() { FirstName = "Jerry", LastName = "Gerison" });
            lst.Add(new Person() { FirstName = "Gus", LastName = "Guster" });

            return lst;
        }


        public Person getBob() {
            Person p = new Person();
            p.FirstName = "Bob";
            p.LastName = "Awesome";
            p.Prefix = "Mr.";
            p.Postfix = "Esquire";
            p.Homepage = "http://www.iamawesome.com";
            p.Phone = "5555551234";
            p.Email = "bob@iamawesome.com";
            return p;
        }

        public IActionResult Details() {
            //TempData["Pizza"] = "Meat Lovers";
            //return RedirectToAction("Index");

            Person bob = getBob();
            ThingType tType = new ThingType();
            tType.Name = "thing one";
            return View("About", bob);

        }

        public IActionResult Delete() {

            Person bob = getBob();
            return View(bob);
        }

        [HttpGet]
        public IActionResult Edit() {
            return View(getBob());
        }

        [HttpPost]
        public IActionResult Edit(int id,
            string firstName) {
            return View(getBob());
        }

        public IActionResult Create() {
            Person newP = new Person();
            newP.DateOfBirth = DateTime.Now;
            return View(newP);
        }


    }
}
