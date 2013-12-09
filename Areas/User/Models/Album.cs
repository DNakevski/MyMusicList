using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusicList.Areas.User.Models
{
    public class Album
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Please insert Album Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        public int ArtistID { get; set; }
        public int ? Mp3ID { get; set; }

        public virtual Mp3 Mp3 { get; set; }
        public virtual Artist Artist { get; set; }
    }
}