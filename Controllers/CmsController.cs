using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport.Controllers
{
    public class CmsController : Controller
    {
        //
        // GET: /Cms/
        [Authorize]

        public ActionResult Index()
        {
            if (User.IsInRole("Beheerder"))
            {
                return RedirectToAction("Beheerder");
            }
            else if (User.IsInRole("Manager"))
            {
                return RedirectToAction("Manager");
            }

            return View();
        }

        [Authorize(Roles = "Beheerder")]
        public ActionResult Beheerder()
        {
            return View();
        }



    }
}
