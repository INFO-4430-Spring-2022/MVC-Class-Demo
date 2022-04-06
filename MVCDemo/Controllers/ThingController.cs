using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class ThingController : Controller {
        // GET: Thing
        public ActionResult Index() {
            return View(fDAL.GetThings());
        }

        // GET: Thing/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: Thing/Create
        public ActionResult Create() {
            SelectList sList = new SelectList(
                fDAL.GetThingTypes(),"ID", "Name");
            ViewBag.ThingTypes = sList;
            return View(); // new Thing() { TypeID = 3} ); // Start with an empty object so that it has an ID for the form.
        }

        // POST: Thing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Thing tng) {
            if (ModelState.IsValid) { // this validates based off of Data Annotations.
                if (tng.dbAdd() > 0 ) {
                    // added
                    return RedirectToAction(nameof(Index));
                } else {
                    // no add
                    SelectList sList = new SelectList(
                        fDAL.GetThingTypes(), "ID", "Name");
                    ViewBag.ThingTypes = sList;
                    return View(tng);
                }
            } else {
                SelectList sList = new SelectList(
                    fDAL.GetThingTypes(), "ID", "Name");
                ViewBag.ThingTypes = sList;
                return View();
            }
        }

        // GET: Thing/Edit/5
        public ActionResult Edit(int id) {
            List<ThingType> types = fDAL.GetThingTypes();
            SelectList sList = new SelectList(types,"ID", "Name");
            ViewBag.ThingTypes = sList;
            return View(fDAL.GetThing(id));
        }

        // POST: Thing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Thing tng) {
            if (ModelState.IsValid) { // this validates based off of Data Annotations.
                int rowsChanged  = tng.dbUpdate(); // 1 on success - 1 on errors ; 0 oops
                if (rowsChanged > 0) {
                    // table updated 
                    return RedirectToAction(nameof(Index));
                } else {
                    // nothing updated 
                    SelectList sList = new SelectList(
    fDAL.GetThingTypes(), "ID", "Name");
                    ViewBag.ThingTypes = sList;
                    return View(tng);
                }
            } else {
                SelectList sList = new SelectList(
    fDAL.GetThingTypes(), "ID", "Name");
                ViewBag.ThingTypes = sList;
                return View(tng);
            }
        }

        // GET: Thing/Delete/5
        public ActionResult Delete(int id) {
            Thing thg = fDAL.GetThing(id != null ? (int)id : -1);
            return View(thg);
        }

        // POST: Thing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string ok) {
            
            Thing thg = fDAL.GetThing(id != null ? (int)id : -1);
            if (ok == "submitted") {
                // form was submitted 
                int rowsAffected = thg.dbRemove();
                if (rowsAffected == 1) {
                    // only one row deleted
                    return RedirectToAction("Index");
                } else {
                    // oops something went wrong.
                    return View(thg);
                }
            } else {
                // not send from correct view
                return View(thg);
            }
        }
    }
}
