using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntoSport.Models;
using IntoSport.DatabaseControllers;

namespace IntoSport.Controllers
{
    public class KlantController : Controller
    {
        //
        // GET: /Klant/


        public ActionResult Register()
        {
            Console.WriteLine("blabla");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterKlantModel RKM)
        {
            Console.WriteLine("blabla");
            if (ModelState.IsValid)
            {
                try
                {
                    KlantDBController KC = new KlantDBController();

                    KC.AddKlant(RKM);
                    ModelState.Clear();
                    RKM = null;
                    ViewBag.Message = "Account aangemaakt";
                }
                catch (Exception)
                {
                    ViewBag.Message = "er was een fout";
                }
            }
            return View(RKM);
        }

    }
}
