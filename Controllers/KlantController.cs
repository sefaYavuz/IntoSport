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
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];//.ASPXAUTH
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string email = authTicket.Name;
            Query userQuery = new Query();
            userQuery.Select("id");
            userQuery.From("user");
            userQuery.Where("email = " + "\"" + @email + "\"");
            int id = 0;

            foreach (Dictionary<string, object> user in userQuery.Execute())
            {
                id = (int)user["id"];
            }

            ViewData.Add("orders", Models.Order.GetAllOrders("", id));

            return View();

           
        }


        [Authorize(Roles="klant")]
        [HttpPost]
        public ActionResult Bestellingen(FormCollection collection)
        {
            int ID = Int32.Parse(collection["orderID"]);
            Query q = new Query();
            Order order = new Order(ID);
            order.isVervallen();
            order.UpdateStatus();

            return RedirectToAction("bestellingen");
        }

        
    }

    
}
