using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MyMusicList.Core;
using MyMusicList.Areas.User.Models;
using MyMusicList.Filters;

namespace MyMusicList.Areas.User.Controllers
{
    [AuthorizeUser]
    public class GenreController : Controller
    {
        public ActionResult Index(string status)
        {
            ViewBag.Status = status;

            using (MyMusicListDB _db = new MyMusicListDB())
            {
                var model = _db.Genres.ToList<Genre>();
                return View(model);
            }
           
        }


        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                using (MyMusicListDB _db = new MyMusicListDB())
                {
                    _db.Genres.Add(genre);
                    _db.SaveChanges();

                    return RedirectToAction("Index", new { status = "insertSuccess" });
                }
            }
            else
            {
                ViewBag.ErrorMsg = "The form is not valid!";
                return View(genre);
            }
        }

        public ActionResult Edit(int id)
        {
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                Genre genre = _db.Genres.Where(x => x.ID == id).FirstOrDefault();

                if (genre == null)
                    return RedirectToAction("Index");

                return View(genre);
            }
        }

        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                using (MyMusicListDB _db = new MyMusicListDB())
                {
                    _db.Entry(genre).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index", new { status = "editSuccess" });
                }
            }
            else
            {
                ViewBag.ErrorMsg = "The form is not valid!";
                return View(genre);
            }
        }


        public ActionResult Delete(int id)
        {
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                Genre genre = _db.Genres.Where(x => x.ID == id).FirstOrDefault();
                
                if (genre == null)
                    return RedirectToAction("Index");

                _db.Genres.Remove(genre);
                _db.SaveChanges();

                return RedirectToAction("Index", new { status = "deleteSuccess" });
            }
        }

    }


}
