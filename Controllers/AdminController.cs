using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;

namespace IntoSport.Controllers
{
    public class AdminController : MainController
    {
        ProductController productController = new ProductController();
      
        //
        // GET: /Admin/

        [Authorize(Roles = "beheerder, manager")]

        public ActionResult Index()
        {   
            return View();
        }


        [Authorize(Roles = "manager")]
        //GET admin/omzet
        public ActionResult Omzet(String type)
        {
            if(type == "meest verkochte")
            {
                List<Omzet> Omzet = productController.MeestVerkochteProducten();
             return View(Omzet);
            }
            else if (type == "minst verkochte")
            {
                List<Omzet> Omzet = productController.MinstVerkochteProducten();
                return View(Omzet);
            }
            else
            {
                return View();
            }
            
        }

        /* PRODUCTEN START*/

        [Authorize(Roles = "beheerder")]
        public ActionResult Products()
        {
            ViewData.Add("products", Models.Product.getAllProducts());
            ViewData.Add("search", "");
            return View();
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Products(string search = "")
        {
            ViewData.Add("products", Models.Product.getAllProducts(search));
            ViewData.Add("search", search);
            return View();
        }

        [Authorize(Roles = "beheerder")]
        public ActionResult Product(int productID = 0)
        {
            Product product = new Product(productID);
            ViewData.Add("product", product);
            return View();
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Product(FormCollection collection)
        {
            Models.Product.Insert(collection);
         
            ViewData.Add("msg", "De wijzigingen zijn succesvol opgeslagen.");

            return View();
        }

        /* PRODUCTEN END */

        /* CATEGORIEËN START */

        [Authorize(Roles = "beheerder")]
        public ActionResult Categories()
        {
            ViewData.Add("categoriesp", Models.Category.getAllCategories());
            ViewData.Add("search", "");
            return View();
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Categories(String search = "")
        {
            ViewData.Add("categoriesp", Models.Category.getAllCategories(search));
            ViewData.Add("search", search);
            return View();
        }

        [Authorize(Roles = "beheerder")]
        public ActionResult Category(int CategoryID = 0)
        {
            Category category = new Category(CategoryID);
            ViewData.Add("categorie", category);
            return View("Category");
        }

        [Authorize(Roles = "beheerder")]
        public ActionResult DeleteCategory(int CategoryID)
        {
            Query q = new Query();
            q.Delete("categorie");
            q.Where("id =" + CategoryID);
            q.Execute();
            return RedirectToAction("Categories", "Admin");
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Category(FormCollection collection)
        {
            if (!collection["naam"].Equals(""))
            {
                Category c = new Category();
                c.id = int.Parse(collection["id"]);
                c.naam = collection["naam"];
                c.parent = new Category(int.Parse(collection["parent.id"]));

                if (c.id != 0)
                {
                    c.Update();
                }
                else
                {
                    c.id = c.Insert();
                }
                ViewData.Add("msg", "De wijzigingen zijn succesvol opgeslagen.");
                Category category = new Category(c.id);
                ViewData.Add("categorie", category);
                return View("Category");
            }
            return View("categorie?categoryID=" + collection["id"] + "&failed=1");
        }

        /* CATEGORIEËN END */
    }
}
