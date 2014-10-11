using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using IntoSport.Models;
using IntoSport.DatabaseControllers;

namespace IntoSport.Controllers
{
    public class AccountController : Controller
    {

        private AuthDBController authDBController = new AuthDBController();

        public ViewResult LogOn()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return View();
        }

        [HttpPost]
        public ActionResult PostLogin(LogOnViewModel viewModel, String returnUrl)
        {
          
            if (ModelState.IsValid)
            {
                bool auth = authDBController.isAuthorized(viewModel.Email, viewModel.Password);

                if (auth)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Cms");
                    }
                  
                }
                else
                {
                    ModelState.AddModelError("loginfout", "Uw e-mailadres en wachtwoord combinatie zijn onjuist.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NieuweKlantMetValidatie(RegisterKlantModel RKM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                }
            }
        }

    }

}
