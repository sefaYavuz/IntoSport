using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport3.Models;

namespace IntoSport3.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Contact c)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Verzonden";
                return View();
            }
            else
            {
                ViewBag.Message = "Er was een fout";
                return View();
            }
        }

    }
}
