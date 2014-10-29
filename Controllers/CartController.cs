using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using IntoSport.Helpers;
using IntoSport.ViewModels;
namespace IntoSport.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
			if (Request.Cookies ["cart"] != null) {
				String[] cart = CartHelper.getItems (Request.Cookies ["cart"].Values["cart"]);
				return View();
			} else {
				return View();

			}
            return View();
        }

		[HttpPost]
		public ActionResult Index()
		{
			if(Request.Cookies[FormsAuthentication.FormsCookieName] != null){
				HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];//.ASPXAUTH
				FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
				string id = authTicket.Name;
				OrderDBController ODBC = new OrderDBController();
			}
			else
			{
				return Redirect("Register");
			}
		}

        public ActionResult addToCart(int ID, int Quantity)
        {
            if (Request.Cookies["cart"]== null)
            {
                HttpCookie cart = new HttpCookie("cart");
                cart.Value = CartHelper.addItem(ID, Quantity);
                Response.Cookies.Add(cart);
				return RedirectToAction("Index");

            }
            else
            {
                Response.Cookies["cart"].Value = CartHelper.addItem(Request.Cookies["cart"].Value, ID, Quantity);
                Response.Cookies["cart"].Expires = DateTime.Now.AddDays(1);
				return RedirectToAction("Index");
            }

            return View();
        }
    }
}
