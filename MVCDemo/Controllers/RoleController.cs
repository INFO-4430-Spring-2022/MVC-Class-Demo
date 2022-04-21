using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class RoleController : Controller {
        // GET: Role
        public ActionResult Index() {
            return View(fDAL.GetRoles());
        }

        // GET: Role/Details/5
        public ActionResult Details(int id) {
            return View(fDAL.GetRole(id));
        }


        // GET: Role/Create
        public ActionResult Create() {
            return View(); // new Role() { TypeID = 3} ); // Start with an empty object so that it has an ID for the form.
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role usr) {
            if (ModelState.IsValid) { // this validates based off of Data Annotations.
                if (usr.dbAdd() > 0) {
                    // added
                    return RedirectToAction(nameof(Index));
                } else {
                    // no add
                    return View(usr);
                }
            } else {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id) {
            return View(fDAL.GetRole(id));
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Role usr) {
            if (ModelState.IsValid) { // this validates based off of Data Annotations.
                int rowsChanged = usr.dbUpdate(); // 1 on success - 1 on errors ; 0 oops
                if (rowsChanged > 0) {
                    // table updated 
                    return RedirectToAction(nameof(Index));
                } else {
                    // noRole updated 
                    return View(usr);
                }
            } else {
                return View(usr);
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int? id) {
            Role thg = fDAL.GetRole(id != null ? (int)id : -1);
            return View(thg);
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, string ok) {

            Role thg = fDAL.GetRole(id != null ? (int)id : -1);
            if (ok == "submitted") {
                // form was submitted 
                int rowsAffected = thg.dbRemove();
                if (rowsAffected == 1) {
                    // only one row deleted
                    return RedirectToAction("Index");
                } else {
                    // oops someRole went wrong.
                    return View(thg);
                }
            } else {
                // not send from correct view
                return View(thg);
            }
        }
    }
}
