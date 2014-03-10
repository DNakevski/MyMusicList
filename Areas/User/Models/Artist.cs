using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusicList.Areas.User.Models
{
    public class Artist
    {
        public int ID { get; set; }

        [Required (ErrorMessage="Please insert Artist name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? GenreID { get; set; }
        public Genre Genre { get; set; }
        public List<Album> Albums { get; set; }

        public Artist()
        {
            Albums = new List<Album>();
        }
    }
}