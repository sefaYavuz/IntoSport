using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;

namespace IntoSport.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public void addToCart(int ID, int Quantity)
        {
            if (Request.Cookies["cart"]== null)
            {
                string cart = new HttpCookie();

            }
        }
    }
}
