﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View(new Thing()); // Start with an empty object so that it has an ID for the form.
        }

        // POST: Thing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Thing tng) {
            if (ModelState.IsValid) { 
                if (tng.dbAdd() > 0 ) {
                    // added
                    return RedirectToAction(nameof(Index));
                } else {
                    // no add
                    return View(tng);
                }
            } else {
                return View();
            }
        }

        // GET: Thing/Edit/5
        public ActionResult Edit(int id) {
            return View(fDAL.GetThing(id));
        }

        // POST: Thing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Thing tng) {
            try {

                int rowsChanged  = tng.dbUpdate(); // 1 on success - 1 on errors ; 0 oops
                if (rowsChanged > 0) {
                    // table updated 
                    return RedirectToAction(nameof(Index));
                } else {
                    // nothing updated 
                    return View(tng);
                }
            } catch {
                return View();
            }
        }

        // GET: Thing/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Thing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Thing tng) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}