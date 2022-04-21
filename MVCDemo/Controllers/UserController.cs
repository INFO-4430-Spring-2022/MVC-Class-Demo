using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDemo.Models;

namespace MVCDemo.Controllers {
    public class UserController : Controller {
        private const string _UserData = "UserData";
    
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(string username, string password) {

            User usr = fDAL.GetUser(username);
            if (usr != null) {
                // found a user with this username.
                string userSalt = usr.Salt; // get user's salt. 
                string hashedPassword = Tools.Hasher.HashIt(password, userSalt, 100000, 48);
                if (usr.Password == hashedPassword) {
                    // password match
                    // set the cookie to store user's login information. 
                    Response.Cookies.Append(_UserData, Tools.DataEncryptor.Protect(usr.ID.ToString()));
                    return RedirectToAction("LoggedIn");
                } else {
                    /// "nope"
                }
            } else {
                // no user in the system with this username 
            }


            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoggedIn() {
            User logged = GetLoggedInUser(Request);
            return View(logged);
        }

        /// <summary>
        /// Get user that is currently logged in using cookie data.
        /// </summary>
        /// <returns></returns>
        public static User GetLoggedInUser(HttpRequest req) {
            User loggedUser = null;
            string uCookieData = "";
            // get user cookie. Should contain encrypted user ID.
            req.Cookies.TryGetValue(_UserData, out uCookieData);
            if (!String.IsNullOrEmpty(uCookieData)) {
                // have data; get userID.
                int userID;
                string encryptedUserID = Tools.DataEncryptor.Unprotect(uCookieData);
                int.TryParse(encryptedUserID, out userID);
                if (userID > 0) { // valid ID.
                    // get user
                    loggedUser = fDAL.GetUser(userID);
                }
            }

            return loggedUser;
        }

        public ActionResult Logout() {
            // delete user cookie; logs user out.
            Response.Cookies.Delete(_UserData);
            // take back to home page. 
            return RedirectToAction("Index", "Home");
        }


        // GET: User
        public ActionResult Index() {
            return View(fDAL.GetUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int id) {
            return View(fDAL.GetUser(id));
        }


        // GET: User/Create
        public ActionResult Create() {
            SelectList sList = new SelectList(
                fDAL.GetRoles(), "ID", "Name");
            ViewBag.Roles = sList;
            return View(); // new User() { TypeID = 3} ); // Start with an empty object so that it has an ID for the form.
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User usr) {
            if (ModelState.IsValid) { // this validates based off of Data Annotations.
                if (usr.dbAdd() > 0) {
                    // added
                    return RedirectToAction(nameof(Index));
                } else {
                    // no add
                    SelectList sList = new SelectList(
                        fDAL.GetRoles(), "ID", "Name");
                    ViewBag.Roles = sList;
                    return View(usr);
                }
            } else {
                SelectList sList = new SelectList(
                    fDAL.GetRoles(), "ID", "Name");
                ViewBag.Roles = sList;
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id) {
            List<Role> types = fDAL.GetRoles();
            SelectList sList = new SelectList(types, "ID", "Name");
            ViewBag.Roles = sList;
            return View(fDAL.GetUser(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User usr) {
            if (ModelState.IsValid) { // this validates based off of Data Annotations.
                int rowsChanged = usr.dbUpdate(); // 1 on success - 1 on errors ; 0 oops
                if (rowsChanged > 0) {
                    // table updated 
                    return RedirectToAction(nameof(Index));
                } else {
                    // noUser updated 
                    SelectList sList = new SelectList(fDAL.GetRoles(), "ID", "Name");
                    ViewBag.Roles = sList;
                    return View(usr);
                }
            } else {
                SelectList sList = new SelectList(fDAL.GetRoles(), "ID", "Name");
                ViewBag.Roles = sList;
                return View(usr);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id) {
            User thg = fDAL.GetUser(id != null ? (int)id : -1);
            return View(thg);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, string ok) {

            User thg = fDAL.GetUser(id != null ? (int)id : -1);
            if (ok == "submitted") {
                // form was submitted 
                int rowsAffected = thg.dbRemove();
                if (rowsAffected == 1) {
                    // only one row deleted
                    return RedirectToAction("Index");
                } else {
                    // oops someUser went wrong.
                    return View(thg);
                }
            } else {
                // not send from correct view
                return View(thg);
            }
        }

    }
}
