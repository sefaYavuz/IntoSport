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
            String[] cart = CartHelper.getItems(Request.Cookies["cart"].Values["cart"]);
            ViewData["cart"] = cart;
            return View();
        }

        public ActionResult addToCart(int ID, int Quantity)
        {
            if (Request.Cookies["cart"]== null)
            {
                HttpCookie cart = new HttpCookie();
                cart.Value = CartHelper.addItem(ID, Quantity);
                Response.Cookies.Add(cart);

            }
            else
            {
                Response.Cookies["cart"].Value = CartHelper.addItem(Request.Cookies["cart"].Value, ID, Quantity);
                Response.Cookies["cart"].Expires = DateTime.Now.AddDays(1);
            }

            return View();
        }
    }
}
