using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthNoDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult NotSecuredPage()
        {
            return View();
        }

        public ActionResult SecuredPage()
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}

