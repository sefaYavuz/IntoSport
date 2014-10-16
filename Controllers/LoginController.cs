using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using System.Web.Security;

namespace IntoSport.Controllers
{
    public class LoginController : MainController
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                if (base.User.IsInRole("beheerder, manager"))
                {
                    return Redirect("admin");
                }

                return Redirect("home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                if (login.IsValid())
                {
                    FormsAuthentication.SetAuthCookie(login.email, true);

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("loginError", "Gebruikersnaam en/of wachtwoord komen niet overeen.. probeer het opnieuw!");
                }
            }
            return View();
        }

        public ActionResult Logout(string ReturnUrl = "")
        {
            FormsAuthentication.SignOut();
            if (ReturnUrl.Length > 0)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "home");
        }
    }
}