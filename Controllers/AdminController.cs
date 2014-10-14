using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
