using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicList.Core;
using MyMusicList.Filters;
using MyMusicList.Areas.User.Models;

namespace MyMusicList.Areas.User.Controllers
{

    [AuthorizeUser]
    public class HomeController : Controller
    {
        //
        // GET: /User/Home/

        public ActionResult Index()
        {
            //MyMusicListDB _db = new MyMusicListDB();
            //var users = _db.Users.ToList();
            ViewBag.Message = "Welcome to the User Area!!!";
            ViewBag.User = ((Models.User)Session["User"]).Name;

            return View();
        }

    }
}
