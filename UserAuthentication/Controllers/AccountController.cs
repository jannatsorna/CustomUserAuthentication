using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace UserAuthentication.Models
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        //Login
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Membership model)
        {
            using (var context = new OfficeEntities1())
            {
                bool isValid = context.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            
        }

        // Signup
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var context = new OfficeEntities1())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }



        // Signout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}