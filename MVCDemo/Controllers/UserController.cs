using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(string username, string password) {

            User usr = fDAL.GetUser(username);
            if (usr != null) {
                string hashedPassword = Tools.Hasher.HashIt(password, "aaaa", 100000, 48);
               if (usr.Password == hashedPassword) {
                    // password match
                    Response.Cookies.Append("User",Tools.DataEncryptor.Protect(usr.ID.ToString())); 
                } else {
                    /// "nope"
                }
            } else { 
                // no user in the system with thsi username 
            }

            
            return RedirectToAction("Index", "Home");
        }


    }
}
