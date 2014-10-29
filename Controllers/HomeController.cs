using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;

namespace IntoSport.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewData.Add("getAllProducts", Product.getAllProducts("", "8"));
            return View();
        }

    }
}
