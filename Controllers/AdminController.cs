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

        [Authorize(Roles = "beheerder, manager, allebei")]

        public ActionResult Index()
        {
            if(base.User.IsInRole("beheerder"))
            {
                return Redirect("admin/beheerder");
            }

            return Redirect("admin/manager");
        }

        [Authorize(Roles = "beheerder, allebei")]
        public ActionResult Beheerder() 
        {
            return View();
        }

        [Authorize(Roles = "manager, allebei")]
        public ActionResult Manager()
        {
            return View();
        }
    }
}
