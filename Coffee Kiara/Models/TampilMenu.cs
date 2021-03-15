using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffee_Kiara.Models
{
    public class TampilMenu
    {
        public int KategoriId { get; set; }
        public string KategoriNama { get; set; }
        public List<MenuData> Menu { get; set; }
    }
}