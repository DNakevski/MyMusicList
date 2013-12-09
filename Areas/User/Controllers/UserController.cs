using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicList.Filters;
using MyMusicList.Core;
using MyMusicList.Areas.User.Models;
using System.Web.Configuration;

namespace MyMusicList.Areas.User.Controllers
{
    [AuthorizeUser]
    public class UserController : Controller
    {
        public ActionResult EditProfile(string msg)
        {
            ViewBag.Msg = msg;

            Models.User user = (Models.User)Session["User"];
            return View("Profile", user);
        }

        [HttpPost]
        public ActionResult EditProfile(Models.User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                using (var _db = new MyMusicListDB())
                {
                    var checkUser = _db.Users.Where(x => x.Email == user.Email && x.ID != user.ID).FirstOrDefault();
                    //if such user exists that the email is already in use
                    if (checkUser != null)
                    {
                        ModelState.AddModelError("email", "The email is already in use by another User.");
                        return View("Profile", user);
                    }

                    //If the user has uploaded image
                    if (file != null)
                    {
                        string[] allowed = { ".jpg", ".jpeg", ".png", ".gif" };
                        string extension = System.IO.Path.GetExtension(file.FileName);

                        if (allowed.Contains(extension.ToLower()))
                        {
                            string ProfilePath = WebConfigurationManager.OpenWebConfiguration("/Views/Web.config").AppSettings.Settings["ProfilePath"].Value;
                            string NewLocation = Server.MapPath("~") + ProfilePath + "Image_" + user.ID + extension;

                            try
                            {
                                if (user.ImgUrl != "" && user.ImgUrl != null)
                                    System.IO.File.Delete(Server.MapPath("~") + user.ImgUrl);

                                file.SaveAs(NewLocation);

                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("image", "There was an error while uploading image.");
                                return View("Profile", user);
                            }

                            user.ImgUrl = ProfilePath + "Image_" + user.ID + extension;

                        }
                        else
                        {
                            ModelState.AddModelError("image", "Only .jpg .jpeg .png .gif file types are allowed!");
                            return View("Profile", user);
                        }
                    }

                    MyMusicList.Areas.User.Models.User tempUser = _db.Users.AsNoTracking().Where(x => x.ID == user.ID).FirstOrDefault();

                    user.Password = tempUser.Password;
                    user.Status = tempUser.Status;
                    _db.Entry(user).State = System.Data.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("EditProfile", "User", new { msg = "profileUpdated" });
                }
            }
            else
            {
                return View("Profile", user);
            }
            
        }

    }
}
