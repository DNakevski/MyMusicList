using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMusicList.Areas.Admin.Models
{
    public class Administrator
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}