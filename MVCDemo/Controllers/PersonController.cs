using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class PersonController : Controller {

        //private DAL _DAL;
        private User _CurrentUser = null;

        public PersonController() {
            //_DAL = new DAL();
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);
            _CurrentUser = UserController.GetLoggedInUser(Request);
            ViewBag.CurrentUser = _CurrentUser;
        }

        public IActionResult Index(int page = 1, int count = 5) {
            //ViewBag.Pizza = "Pepperoni";
            //ViewData["IceCream"] = "Chocolate";
            List<Person> peopleToShow = new List<Person>();
            peopleToShow = fDAL.GetPeople().Skip((page-1) * count).Take(count).ToList();
            return View("List", peopleToShow);
        }

        public IActionResult CardList() {
            return View(fDAL.GetPeople());
        }

        public IActionResult Card(int? id) {
            if (id == null) id = 0;
            Person p = fDAL.GetPerson((int)id);
            return PartialView("_Card",p);
        }

        //public List<Person> GetPeople() {
        //    List<Person> lst = new List<Person>();
        //    lst.Add(getBob());
        //    lst.Add(new Person() { FirstName = "Sally", LastName = "Smith" });
        //    lst.Add(new Person() { FirstName = "Jerry", LastName = "Gerison" });
        //    lst.Add(new Person() { FirstName = "Gus", LastName = "Guster" });

        //    return lst;
        //}


        //public Person getBob() {
        //    Person p = new Person();
        //    p.FirstName = "Bob";
        //    p.LastName = "Awesome";
        //    p.Prefix = "Mr.";
        //    p.Postfix = "Esquire";
        //    p.Homepage = "http://www.iamawesome.com";
        //    p.Phone = "5555551234";
        //    p.Email = "bob@iamawesome.com";
        //    return p;
        //}

        public IActionResult Details(int? id) {
            //TempData["Pizza"] = "Meat Lovers";
            //return RedirectToAction("Index");

            //Person bob = getBob();
            //ThingType tType = new ThingType();
            //tType.Name = "thing one";
            Person bob = fDAL.GetPerson(id != null ? (int)id : -1);
            return View("About", bob);

        }
        public IActionResult Create() {
            Person newP = new Person();
            //newP.DateOfBirth = DateTime.Now;
            //return View(newP);
            return View(newP);
        }

        [HttpPost]
        public IActionResult Create(Person per) {
            // data checking
            bool objectDataValid = true;
            // do not need to do these if validation is done in the property setter. See Thing class.
            per.Prefix = per.Prefix == null ? "" : per.Prefix;
            per.Postfix = per.Postfix == null ? "" : per.Postfix;
            per.Email = per.Email == null ? "" : per.Email;
            per.Phone = per.Phone == null ? "" : per.Phone;
            per.Homepage = per.Homepage == null ? "" : per.Homepage;
            DateTime minDate = new DateTime(1900, 1, 1);
            // cannot be born before 1900
            per.DateOfBirth = per.DateOfBirth < minDate ? minDate : per.DateOfBirth;
            // cannot be not be born yet.
            per.DateOfBirth = per.DateOfBirth > DateTime.Now ? DateTime.Now : per.DateOfBirth;

            // data valid
            if (objectDataValid) {
                per.dbAdd();
                if (per.ID > 0) {
                    // successfully added to DB
                    return RedirectToAction("Index");
                } else {
                    // error; send back to form
                    return View(per);
                }
            } else {
                // data was not valid; redirect back to form.
                return View(per);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id) {
            User currentUser = UserController.GetLoggedInUser(Request);
            Role myRole = currentUser != null ? fDAL.GetRole(currentUser.RoleID) : new Role();
            if (myRole.CanEditPerson) {
                Person p;
                if (id != null) {
                    p = fDAL.GetPerson((int)id);
                    return View(p);
                } else {
                    //p = new Person() { FirstName = "John", LastName = "Doe" };
                    // no person requested; go back to list of people.
                    return RedirectToAction("Index");
                }
            } else {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        //public IActionResult Edit(int id,
        //    string firstName, string lastname, bool isManager, string emailaddress) {
        public IActionResult Edit([FromRoute] int? id,
            Person per) {
            //[Bind("FirstName", "LastName", "Prefix", "HomePage")] Person per) {
            // data checking
            User currentUser = UserController.GetLoggedInUser(Request);
            Role myRole = currentUser != null ? fDAL.GetRole(currentUser.RoleID) : new Role();
            if (myRole.CanEditPerson) {
                bool objectDataValid = true;
                // do not need to do these if validation is done in the property setter. See Thing class.
                per.Prefix = per.Prefix == null ? "" : per.Prefix;
                per.Postfix = per.Postfix == null ? "" : per.Postfix;
                per.Email = per.Email == null ? "" : per.Email;
                per.Phone = per.Phone == null ? "" : per.Phone;
                per.Homepage = per.Homepage == null ? "" : per.Homepage;
                DateTime minDate = new DateTime(1900, 1, 1);
                // cannot be born before 1900
                per.DateOfBirth = per.DateOfBirth < minDate ? minDate : per.DateOfBirth;
                // cannot be not be born yet.
                per.DateOfBirth = per.DateOfBirth > DateTime.Now ? DateTime.Now : per.DateOfBirth;

                // data valid
                if (objectDataValid) {
                    int rowsChanged = per.dbUpdate();
                    if (rowsChanged == 1) {
                        // successfully changed one row in DB
                        return RedirectToAction("Index");
                    } else {
                        // error; send back to form
                        return View(per);
                    }
                } else {
                    // data was not valid; redirect back to form.
                    return View(per);
                }
            } else {
                // no perms
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int? id) {

            //Person bob = getBob();
            Person bob = fDAL.GetPerson(id != null ? (int)id : -1);
            return View(bob);
        }

        [HttpPost]
        public IActionResult Delete(int? id,string ok) {

            //Person bob = getBob();
            Person bob = fDAL.GetPerson(id != null ? (int)id : -1);
            if (ok == "submitted") {
                // form was submitted 
                int rowsAffected = bob.dbRemove();
                if (rowsAffected == 1) {
                    // only one row deleted
                    return RedirectToAction("Index");
                } else {
                    // oops something went wrong.
                return View(bob);
                }
            } else {
                // not send from correct view
                return View(bob);
            }
            

        }

    }
}
