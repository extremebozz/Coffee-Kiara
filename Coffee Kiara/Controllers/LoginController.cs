using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Kiara.Controllers
{
    public class LoginController : Controller
    {
        MySqlConnection con;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CekUser(Coffee_Kiara.Models.UserData userData)
        {            
            using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {
                con.Open();
                var userDetail = new MySqlCommand(string.Format("SELECT * FROM user_data WHERE username = '{0}' AND password = '{1}'",
                userData.Username, userData.Password), con).ExecuteNonQuery().ToString();

                if (userDetail == null)
                {
                    userData.ErrorMessage = "Username Atau Password Salah!";
                    return View("Index", userData);
                }
                else
                {
                    Session["userID"] = userDetail[0];
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}