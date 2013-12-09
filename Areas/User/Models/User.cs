using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMusicList.Areas.User.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please insert Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please insert Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        [Display(Name="Image")]
        public string ImgUrl { get; set; }
    }
}