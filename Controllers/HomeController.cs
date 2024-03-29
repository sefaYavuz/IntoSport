﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using IntoSport.Helpers;

namespace IntoSport.Controllers
{
    public class HomeController : Controller
    {
        
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewData.Add("getAllProducts", Product.getAllProducts("", "8", "INNER", "product_categorie AS pc ON product.id = pc.product_id", "product.voorraad != 0"));
            ViewData.Add("kortingProducten",new ProductHelper().getKortingen());
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Voorwaarden()
        {
            return View();
        }
    
    }
}
