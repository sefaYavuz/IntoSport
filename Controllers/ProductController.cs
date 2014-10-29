using IntoSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using IntoSport.Helpers;

namespace IntoSport.Controllers
{
    public class ProductController : Controller
    {
        ProductHelper productHelper = new ProductHelper();

        public ActionResult Index(int categorie)
        {
            ViewData.Add("product", productHelper.getProduct(categorie));
            return View();

        }
    
        
        public ActionResult Detail(int id)
        {
            ViewData.Add("product", new Product(id));
            ViewData.Add("details", productHelper.getDetails(id));
            return View();
        }


     


    }
}
