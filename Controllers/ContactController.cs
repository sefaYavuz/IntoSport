using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntoSport3.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View("Account");
        }


        public ActionResult NoAccount()
        {
            return View("NoAccount");
        }

        public ActionResult Account()
        {
            return View("Account");
        }

    }
}
