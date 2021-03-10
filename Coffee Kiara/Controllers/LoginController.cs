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
           // try
            {
                using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
                    con.Open();

               // if (Session["role"] != null)
                    switch (Session["role"])
                    {
                        case "admin":
                            return RedirectToAction("Index", "Admin");

                        case "user":
                            return RedirectToAction("Index", "Home");
                    }

                return View();
            }
           // catch
            {
                return Content("Pastikan Database Anda Terhubung!");
            }
        }

        [HttpPost]
        public ActionResult CekUser(Models.UserData UserData)
        {
            var userDetail = new Models.UserData { };

            using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM user_data WHERE username = '{0}' AND password = '{1}'",
                UserData.Username, UserData.Password), con);

                using (MySqlDataReader read = cmd.ExecuteReader())
                    if (read.HasRows)
                    {
                        read.Read();
                        userDetail = new Models.UserData { UserID = Convert.ToInt32(read["id"]), Username = read["username"].ToString(), 
                            Password = read["password"].ToString(), Role = read["role"].ToString()
                        };
                    }

                if (userDetail != null)
                {                    
                    Session["userID"] = userDetail.UserID;
                    Session["user"] = userDetail.Username;
                    Session["role"] = userDetail.Role;
                    switch (userDetail.Role)
                    {
                        case "admin":
                            return RedirectToAction("Index", "Admin");

                        case "user":
                            return RedirectToAction("Index", "Home");
                    }
                }

                UserData.ErrorMessage = "Username Atau Password Salah!";
                return View("Index", UserData);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}