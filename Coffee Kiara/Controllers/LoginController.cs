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
        public ActionResult CekUser(Coffee_Kiara.Models.UserData UserData)
        {
            List<Models.UserData> userData = new List<Models.UserData>();

            using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(string.Format("SELECT * FROM user_data WHERE username = '{0}' AND password = '{1}'",
                UserData.Username, UserData.Password), con);

                using (MySqlDataReader read = cmd.ExecuteReader())
                    if (read.HasRows)
                    {
                        read.Read();
                        userData.Add(new Models.UserData()
                        {
                            UserID = Convert.ToInt32(read["id"]),
                            Username = read["username"].ToString(),
                            Password = read["password"].ToString()
                        });
                    }

                if (userData.Count == 0)
                {
                    UserData.ErrorMessage = "Username Atau Password Salah!";
                    return View("Index", UserData);
                }
                else
                {
                    Session["userID"] = userData[0].UserID;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}