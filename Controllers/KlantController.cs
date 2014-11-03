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
        public ActionResult Save(FormCollection collection)
        {

           
            IntoSport.Models.User user = IntoSport.Models.User.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));
           
                user.email = Request.Form["email"];
                user.voornaam = Request.Form["voornaam"];
                user.achternaam = Request.Form["achternaam"];
                user.huisnr = Request.Form["huisnr"];
                user.plaats = Request.Form["plaats"];
                user.tel = Request.Form["telefoon"];
                user.wachtwoord = Request.Form["wachtwoord"];
                user.postcode = Request.Form["postcode"];


                new IntoSport.Helpers.KlantGegevensHelper().SaveKlant(user);
            
            

            return Redirect("/Klant/Account");
        }
        [Authorize(Roles = "klant")]
        public ActionResult Account(IntoSport.Models.User klant)
        {
          
         
                klant = Models.KlantGegevens.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));
            
            ViewData.Add("klant", klant);
          
            return View();
        }
       [Authorize(Roles ="klant")]
        public ActionResult Bestellingen()
        {
            User user = Models.User.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));

            return View(new BestellingHelper().getMyBestellingen(user));
        }
    }
}
