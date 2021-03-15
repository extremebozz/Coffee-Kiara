using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffee_Kiara.Models
{
    public class MenuData
    {
        public int ID { get; set; }
        public string NamaMenu { get; set; }
        public decimal HargaMenu { get; set; }
        public int KategoriId { get; set; }
    }
}