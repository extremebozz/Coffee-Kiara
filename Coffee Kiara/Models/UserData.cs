using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coffee_Kiara.Models
{
    public class UserData
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "User Name Tidak Boleh Kosong!")]
        [DisplayName("User Name")]
        public string Username { get; set; }        
        [Required(ErrorMessage = "Password Tidak Boleh Kosong!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }
    }
}