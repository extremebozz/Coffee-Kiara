using Coffee_Kiara.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coffee_Kiara.Controllers
{
    public class AdminController : Controller
    {
        MySqlConnection con;
        //TampilMenu dataTampilMenu;

        // GET: Admin
        public ActionResult Index()
        {
            List<TampilMenu> dataTampilMenu = new List<TampilMenu>();
            dataTampilMenu = DaftarTampilMenu();

            //Hapus comment dibawah untuk mengetest tampilan
            return View(dataTampilMenu);

            //Function mengecek status login
            if (Session["userID"] != null)
            {
                return View(dataTampilMenu); //Masuk ketampilan bila sudah login
            }
            else
            {
                return RedirectToAction("Index", "Login"); //Kembali ke page login jika belum login
            }            
        }

        public ActionResult Profile()
        {
            return View();
        }

        private List<TampilMenu> DaftarTampilMenu()
        {
            List<TampilMenu> daftarTampilMenu = new List<TampilMenu>();
            List<KategoriData> daftarKategori = new List<KategoriData>();

            using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM menu_kategori ORDER BY id", con);
                using (MySqlDataReader read = cmd.ExecuteReader())
                    while (read.Read())
                        daftarKategori.Add(new KategoriData { KategoriId = Convert.ToInt32(read["id"]), NamaKategori = read["nama"].ToString() });                

                for (int i = 0; i < daftarKategori.Count; i++)
                {
                    List<MenuData> daftarMenu = new List<MenuData>();
                    cmd = new MySqlCommand(string.Format("SELECT * FROM menu_data WHERE id_kategori = '{0}'", daftarKategori[i].KategoriId), con);

                    using (MySqlDataReader read = cmd.ExecuteReader())
                        while (read.Read())
                            daftarMenu.Add(new MenuData
                            {
                                ID = Convert.ToInt32(read["id"]),
                                KategoriId = Convert.ToInt32(read["id_kategori"]),
                                NamaMenu = read["nama"].ToString(),
                                HargaMenu = Convert.ToInt64(read["harga"])
                            });

                    daftarTampilMenu.Add(new TampilMenu
                    {
                        KategoriId = daftarKategori[i].KategoriId,
                        KategoriNama = daftarKategori[i].NamaKategori,
                        Menu = daftarMenu
                    });
                }
            }
            return daftarTampilMenu;
        }

        //private List<MenuData> DaftarMenu()
        //{
        //    List<MenuData> daftarMenu = new List<MenuData>();

        //    using(con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
        //    {
        //        con.Open();
        //        MySqlCommand cmd = new MySqlCommand("SELECT * FROM menu_data", con);
        //        using (MySqlDataReader read = cmd.ExecuteReader())
        //            while (read.Read())
        //                daftarMenu.Add(new MenuData { ID = Convert.ToInt32(read["id"]), KategoriId = Convert.ToInt32(read["id_kategori"]), NamaMenu = read["nama"].ToString(), HargaMenu = Convert.ToInt64(read["harga"])});
        //    }

        //    return daftarMenu;
        //}

        //private List<KategoriData> DaftarKategori()
        //{
        //    List<KategoriData> daftarKategori = new List<KategoriData>();

        //    using (con = new MySqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString))
        //    {
        //        con.Open();
        //        MySqlCommand cmd = new MySqlCommand("SELECT * FROM menu_kategori ORDER BY id", con);

        //        using (MySqlDataReader read = cmd.ExecuteReader())
        //            while (read.Read())
        //                daftarKategori.Add(new KategoriData { KategoriId = Convert.ToInt32(read["id"]), NamaKategori = read["nama"].ToString() });
        //    }

        //    return daftarKategori;
        //}
    }
}