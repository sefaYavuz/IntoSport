using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using System.Web.Security;
using IntoSport.Helpers;

namespace IntoSport.Controllers
{
    public class KlantController : Controller
    {
        //
        // GET: /Klant/
        [Authorize(Roles = "klant")]
        public ActionResult Index()
        {
            return View();
        }
      
        [Authorize(Roles = "klant")]
        public ActionResult Account(User klant)
        {
            if(ModelState.IsValid)
            {

            }
            else
            {
                klant = Models.KlantGegevens.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));
            }
           
          
            return View(klant);
        }
       [Authorize(Roles ="klant")]
        public ActionResult Bestellingen()
        {
            User user = Models.User.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));

            return View(new BestellingHelper().getMyBestellingen(user));
        }
    }
}
