using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class UserController : Controller
    {
        private const string _UserData = "UserData";
        public IActionResult Index()
        {
            return View();
        }

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
                    Response.Cookies.Append(_UserData,Tools.DataEncryptor.Protect(usr.ID.ToString()));
                    return RedirectToAction("LoggedIn");
                } else {
                    /// "nope"
                }
            } else { 
                // no user in the system with this username 
            }

            
            return RedirectToAction("Index", "Home");
        }

        private IActionResult LoggedIn() {
            User logged = GetLoggedInUser();

            return View(logged);
        }

        /// <summary>
        /// Get user that is currently logged in using cookie data.
        /// </summary>
        /// <returns></returns>
        public User GetLoggedInUser() {
            User loggedUser = null;
            string uCookieData = "";
            // get user cookie. Should contain encrypted user ID.
            Request.Cookies.TryGetValue(_UserData, out uCookieData);
            if (String.IsNullOrEmpty(uCookieData)) {
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

    }
}
