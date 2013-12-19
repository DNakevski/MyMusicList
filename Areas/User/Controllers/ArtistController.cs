using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusicList.Filters;
using MyMusicList.Areas.User.Models;
using MyMusicList.Core;

namespace MyMusicList.Areas.User.Controllers
{
    [AuthorizeUser]
    public class ArtistController : Controller
    {
        public ActionResult Index(string search, string status)
        {
            if (search == null)
                search = "";

            ViewBag.Status = status;

            using(MyMusicListDB _db = new MyMusicListDB())
            {
                List<Artist> artists = new List<Artist>();

                if (search != "")
                {
                    artists = _db.Artists.Include("Genre").Include("Albums").OrderBy(x => x.Name).Where(x => x.Name.ToUpper().Contains(search.ToUpper()) ||
                        x.Description.ToUpper().Contains(search.ToUpper()) || x.Genre.Name.ToUpper().Contains(search.ToUpper())).ToList<Artist>();
                }
                else
                {
                    artists = _db.Artists.Include("Genre").Include("Albums").OrderBy(x => x.Name).ToList<Artist>();
                }

                ViewBag.Search = search;
                return View(artists);
            }

        }

        public ActionResult Insert()
        {
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                List<Genre> genres = new List<Genre>();
                genres = _db.Genres.ToList<Genre>();
                ViewBag.Genres = genres;

                return View();
            }
        }

        [HttpPost]
        public ActionResult Insert(Artist artist)
        {

            using (MyMusicListDB _db = new MyMusicListDB())
            {
                List<Genre> genres = new List<Genre>();
                genres = _db.Genres.ToList<Genre>();
                ViewBag.Genres = genres;


                if (ViewData.ModelState.IsValid)
                {
                    _db.Artists.Add(artist);
                    _db.SaveChanges();
                    return RedirectToAction("Index", new { status = "insertSuccess"}); 
                        
                }
                else
                {
                    return View(artist);
                }

            }

        }

        public ActionResult Edit(int id)
        {
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                Artist artist = _db.Artists.Where(a => a.ID == id).FirstOrDefault();

                List<Genre> genres = new List<Genre>();
                genres = _db.Genres.ToList<Genre>();
                ViewBag.Genres = genres;

                if (artist == null)
                    RedirectToAction("Index");

                return View(artist);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                using (MyMusicListDB _db = new MyMusicListDB())
                {
                    _db.Entry(artist).State = System.Data.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index", new { status = "editSuccess" });
                }
            }
            else
            {
                ViewBag.ErrorMessage = "The form is not properly completed!";
                return View(artist);
            }
        }

        public ActionResult Delete(int id)
        {
            using(MyMusicListDB _db = new MyMusicListDB())
            {
                Artist artist = _db.Artists.Where(x => x.ID == id).FirstOrDefault();
                _db.Artists.Remove(artist);
                _db.SaveChanges();
                return RedirectToAction("Index", new { status = "deleteSuccess" });
            }
        }

    }
}
