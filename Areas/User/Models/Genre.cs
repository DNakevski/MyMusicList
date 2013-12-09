using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusicList.Areas.User.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [Required (ErrorMessage="Please insert Genre Name")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}