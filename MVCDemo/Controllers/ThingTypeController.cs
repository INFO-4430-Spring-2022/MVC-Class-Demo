using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class ThingTypeController : Controller {

        //int a;

        public IActionResult Index() {
            return View(fDAL.GetThingTypes());
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
        public IActionResult About(string value, int? number, bool? yesNo) {
            if (value != null) {
                ViewData["Stuff"] = value;
            } else {
                ViewData["Stuff"] = "Nothing";
            }

            return View();
        }

        public IActionResult Create() {
            ThingType tType = new ThingType();
            //tType.ID = 1234;
            //tType.Name = "Doo Hickey";
            return View(tType);
        }

        [HttpPost]
        public IActionResult Create(
            //int id,
            //string Name,
            //bool CanShare
            ThingType tType
            ) {

            // data checking
            bool objectDataValid = true;

            // data valid
            if (objectDataValid) {
                tType.dbAdd();
                if (tType.ID > 0) {
                    // successfully added to DB
                    return RedirectToAction("Index");
                } else {
                    // error; send back to form
                    return View(tType);
                }
            } else {
                // data was not valid; redirect back to form.
                return View(tType);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id) {
            ThingType tType;
            if (id != null) {
                tType = fDAL.GetThingType((int)id);
            } else {
                // no person requested; go back to list of people.
                return RedirectToAction("Index");
            }
            return View(tType);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id,
            ThingType tType) {
            // data checking
            bool objectDataValid = true;

            // data valid
            if (objectDataValid) {
                int rowsChanged = tType.dbUpdate();
                if (rowsChanged == 1) {
                    // successfully changed one row in DB
                    return RedirectToAction("Index");
                } else {
                    // error; send back to form
                    return View(tType);
                }
            } else {
                // data was not valid; redirect back to form.
                return View(tType);
            }
        }

        public IActionResult Delete(int? id) {
            ThingType tType = fDAL.GetThingType(id != null ? (int)id : -1);
            return View(tType);
        }

        [HttpPost]
        public IActionResult Delete(int? id, string ok) {
            ThingType tType = fDAL.GetThingType(id != null ? (int)id : -1);
            if (ok == "submitted") {
                // form was submitted 
                int rowsAffected = tType.dbRemove();
                if (rowsAffected == 1) {
                    // only one row deleted
                    return RedirectToAction("Index");
                } else {
                    // oops something went wrong.
                    return View(tType);
                }
            } else {
                // not send from correct view
                return View(tType);
            }


        }


    }
}
