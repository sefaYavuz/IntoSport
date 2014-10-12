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
        public ActionResult Index(string ReturnUrl = "")
        {
            if (Request.IsAuthenticated)
            {
                if (ReturnUrl.Length > 0)
                {
                    return Redirect(ReturnUrl);
                }
                return Redirect("Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login user, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid())
                {
                    FormsAuthentication.SetAuthCookie(user.email, true);
                    if (ReturnUrl.Length > 0)
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
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