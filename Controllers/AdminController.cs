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
        //
        // GET: /Admin/

        [Authorize(Roles = "beheerder, manager")]

        public ActionResult Index()
        {
            if(base.User.IsInRole("beheerder"))
            {
                return Redirect("admin/beheerder");
            }

            return Redirect("admin/manager");
        }
        [Authorize(Roles = "manager")]
        public ActionResult Producten(String type)
        {
            if(type == "meest verkochte")
            {
            return View();
            }else
            {
                return null;
            }
        }
    
        [Authorize(Roles = "beheerder")]
        public ActionResult Beheerder() 
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        public ActionResult Manager()
        {
            return View();
        }

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
    }
}
