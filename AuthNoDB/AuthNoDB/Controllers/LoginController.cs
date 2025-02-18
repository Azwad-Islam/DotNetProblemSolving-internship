using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthNoDB.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                Session["Authenticated"] = true;
                return RedirectToAction("SecuredPage", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid credentials!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["Authenticated"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
