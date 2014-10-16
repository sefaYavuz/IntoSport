using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IntoSport.Models;

namespace IntoSport.Controllers
{
    public class AccountController : MainController
    {
        [Authorize]
        public ActionResult Index()
        {
            User u = Models.User.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));
            //ViewData.Add("orders", u.getPlacedOrders());
           // ViewData.Add("orderTotal", u.getTotalOrderAmount());
            //ViewData.Add("config", Config.getConfig());
            return View(u);
        }
        /*
        [Authorize]
        [HttpPost]
        public ActionResult Index(Account u)
        {
            User user = Models.User.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));
            ViewData.Add("orders", user.getPlacedOrders());
            ViewData.Add("orderTotal", user.getTotalOrderAmount());
            ViewData.Add("config", Config.getConfig());
            u.id = user.id;
            u.email = HttpUtility.HtmlEncode(User.Identity.Name);
            u.role = user.role;
            if (ModelState.IsValid && u.Save())
            {
                ViewData.Add("msg", "Uw gegevens zijn succesvol opgeslagen!");
            }
            return View(user);
        }

        [Authorize]
        public ActionResult DeleteOrder(int OrderID)
        {
            User user = Models.User.GetUser(HttpUtility.HtmlEncode(User.Identity.Name));
            Order.DeleteOrder(user, OrderID);
            return RedirectToAction("index", "account");
        }
         */
    }
}