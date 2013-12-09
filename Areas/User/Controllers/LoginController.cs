using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicList.Core;
using MyMusicList.Areas.User.Models;

namespace MyMusicList.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, int ? remember)
        {
            Models.User user = new Models.User();

            using (MyMusicListDB _db = new MyMusicListDB())
            {
                user = _db.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            }

            if (user == null)
            {
                ViewBag.ErrorMessage = "There is no such User!";
                return View();
            }
            else
            {
                Session["User"] = user;
                return RedirectToAction("Index", "Home");
            }
            //return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}
