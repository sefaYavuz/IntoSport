using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using System.IO;
using IntoSport.Helpers;

namespace IntoSport.Controllers
{
    public class AdminController : MainController
    {
        ProductController productController = new ProductController();
        OmzetHelper omzetHelper = new OmzetHelper();
      
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
                List<Omzet> Omzet = omzetHelper.MeestVerkochteProducten();

             return View(Omzet);
            }
            else if (type == "minst verkochte")
            {
                List<Omzet> Omzet = omzetHelper.MinstVerkochteProducten();
                return View(Omzet);
            }
            else{

                return View();
            }
        }



        /* ORDERS START */

        [Authorize(Roles = "beheerder")]
        public ActionResult Orders()
        {
            ViewData.Add("orders", Models.Order.GetAllOrders());
            ViewData.Add("search", "");
            return View();
        }
     

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Orders(FormCollection collection)
        {

           int id = int.Parse(collection["id"]);
           string status = collection["status"];

           Order order = new Order(id);
           switch(status)
           {
                case "in_behandeling":
                    order.inBehandeling();
                    break;
                case "betaald":
                    order.isBetaald();
                    break;
                case "vervallen":
                    order.isVervallen();
                    break;
                case "verstuurd":
                    order.isVerstuurd();
                    break;
            }

            order.UpdateStatus();

            ViewData.Add("orders", Models.Order.GetAllOrders());           
            return View();
        }

        /* ORDERS END */

        /* KLANT START */

        [Authorize(Roles = "beheerder")]
        public ActionResult Customers()
        {
            ViewData.Add("customers", Models.User.GetAllCustomers());
            ViewData.Add("search", "");
            return View();
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Customers(string search = "")
        {
            ViewData.Add("customers", Models.User.GetAllCustomers(search));
            ViewData.Add("search", search);
            return View();
        }

        [Authorize(Roles = "beheerder")]
        public ActionResult Customer_orders(int customerID)
        {
            ViewData.Add("customerOrders", Models.Order.GetAllOrders("", customerID));
            return View();
        }

        /* KLANT END */

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
        public ActionResult Product(int ProductID = 0)
        {
            Product product = new Product(ProductID);
            ViewData.Add("product", product);
            ViewData.Add("details", Models.Detail.getAllDetails());
            ViewData.Add("getCategories", Models.Category.getAllCategories());
            return View("Product");
        }

        [Authorize(Roles = "beheerder")]
        [HttpPost]
        public ActionResult Product(Product product, FormCollection collection, HttpPostedFileBase thumbnail, HttpPostedFileBase afbeelding)
        {

            Product p = new Product();

            if (ModelState.IsValid)
            {
                if ((thumbnail != null && thumbnail.ContentLength > 0) && (afbeelding != null && afbeelding.ContentLength > 0))
                {
                    //Pak de naam van het bestand
                    var thumbName = Path.GetFileName(thumbnail.FileName);
                    var imgName = Path.GetFileName(afbeelding.FileName);

                    // Afbeeldingen opslaan in de bijbehorende folders
                    var thumbPath = Path.Combine(Server.MapPath("~/Template/images/products/thumbnail"), "" + Models.Product.GetLastProductID() + ".png");
                    var imgPath = Path.Combine(Server.MapPath("~/Template/images/products"), "" + Models.Product.GetLastProductID() + ".png");

                    thumbnail.SaveAs(thumbPath);
                    afbeelding.SaveAs(imgPath);

                    p.afbeelding = "Template/images/products/" + Models.Product.GetLastProductID() + ".png";
                    p.thumbnail = "Template/images/products/thumbnail/" + Models.Product.GetLastProductID() + ".png";
                }

                string[] categories = new string[0];
                if (collection.AllKeys.Contains("subcat"))
                {
                    categories = collection["subcat"].Split(',');
                }

                string[] details = new string[0];
                if (collection.AllKeys.Contains("detail"))
                {
                    details = collection["detail"].Split(',');
                }

                p.id = int.Parse(collection["id"]);
                p.naam = collection["naam"];
                p.beschrijving = collection["beschrijving"];
                p.prijs = double.Parse(collection["prijs"]);
                p.korting = (collection["korting"] != null ? int.Parse(collection["korting"]) : 0);
                p.voorraad = (collection["voorraad"] != null ? int.Parse(collection["voorraad"]) : 0);
                if (thumbnail == null)
                {
                    p.thumbnail = "";
                }
                if (afbeelding == null)
                {
                    p.afbeelding = "";
                }

                if(p.id != 0)
                {
                    p.Update();

                    p.UpdateCategorie(categories);
                    p.UpdateDetail(details);
                }
                else 
                {
                    p.id = p.Insert();

                    p.InsertCategorie(categories);
                    p.InsertDetail(details);
                }


                ViewData.Add("msg", "De wijzigingen zijn succesvol opgeslagen.");

                Product p2 = new Product(p.id);
                ViewData.Add("product", p2);
                ViewData.Add("getCategories", Models.Category.getAllCategories());
                ViewData.Add("details", Models.Detail.getAllDetails());
                return View("Product");

            }


            ViewData.Add("product", p);
            ViewData.Add("details", Models.Detail.getAllDetails());
            ViewData.Add("getCategories", Models.Category.getAllCategories());

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
            if (ViewData.ModelState.IsValid)
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
            return View();
        }

        /* CATEGORIEËN END */
    }
}
