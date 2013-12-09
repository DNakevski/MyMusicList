using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMusicList.Areas.User.Models
{
    public class Mp3
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public List<Album> Albums { get; set; }
    }
}