using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace OzzaimAhsap.Models
{
    public class Geridonusler
    {
        public int id { get; set; }

        public string ad_soyad { get; set; }

        public string telefon { get; set; }

        public string email { get; set; }

        public string mesaj { get; set; }
    }
}