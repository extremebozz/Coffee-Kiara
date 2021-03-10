using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Kiara.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
          //  if (Session["userID"] != null)
            {
                return View();
            }
          //  else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}