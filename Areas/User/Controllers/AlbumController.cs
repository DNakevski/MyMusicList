using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MyMusicList.Core;
using MyMusicList.Filters;
using MyMusicList.Areas.User.Models;
using System.Web.Configuration;

namespace MyMusicList.Areas.User.Controllers
{
    [AuthorizeUser]
    public class AlbumController : Controller
    {
        public ActionResult Index(string x, string search)
        {
            
            int id;

            if (search == null)
                search = "";

            using(MyMusicListDB _db  = new MyMusicListDB())
            {
                List<Album> albums = new List<Album>();
                if (Int32.TryParse(x, out id))
                {
                    if (search == "")
                        albums = _db.Albums.Include("Artist").Where(a => a.ArtistID == id).ToList<Album>();
                    else
                        albums = _db.Albums.Include("Artist").Where(a => a.ArtistID == id && (a.Name.ToUpper().Contains(search.ToUpper()))).ToList<Album>();
                    
                    ViewBag.SearchBy = "Artist";
                    ViewBag.Artist = _db.Artists.Where(art => art.ID == id).FirstOrDefault();
                    ViewBag.Search = search;

                    if (albums == null)
                        return RedirectToAction("Index", "Home");

                    return View(albums);

                }
                else
                {

                    if (search == "")
                        albums = _db.Albums.Include("Artist").ToList<Album>();
                    else
                        albums = _db.Albums.Include("Artist").Where(a => a.Name.ToUpper().Contains(search.ToUpper())).ToList<Album>();

                    ViewBag.SearchBy = "All";
                    ViewBag.Search = search;

                    return View(albums);
                }

            }
        }

        public ActionResult Edit(int id)
        {
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                Album album = _db.Albums.Include("Artist").Where(x => x.ID == id).FirstOrDefault();

                if (album == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                return View(album);
            }
        }

        [HttpPost]
        public ActionResult Edit(Album album, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                using (MyMusicListDB _db = new MyMusicListDB())
                {
                    if (file != null)
                    {
                        string[] allowed = { ".jpg", ".jpeg", ".png", ".gif"};
                        string extension = System.IO.Path.GetExtension(file.FileName);

                        if (allowed.Contains(extension.ToLower()))
                        {
                            string CoverPath = WebConfigurationManager.OpenWebConfiguration("/Views/Web.config").AppSettings.Settings["CoverPath"].Value;
                            string NewLocation = Server.MapPath("~") + CoverPath + "Cover_" + album.ID + extension;

                            try
                            {
                                if(album.ImgUrl != "" && album.ImgUrl != null)
                                    System.IO.File.Delete(Server.MapPath("~") + album.ImgUrl);

                                file.SaveAs(NewLocation);

                            }
                            catch (Exception ex)
                            {
                                ViewBag.error = "Error uploading image.";

                                return View(album);

                            }

                            album.ImgUrl = CoverPath + "Cover_" + album.ID + extension;

                        }
                        else
                        {
                            ViewBag.error = "Only .jpg .jpeg .png .gif file types are allowed!";
                            return View(album);
                        }
                    }

                    try
                    {
                        _db.Entry(album).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.error = "Error while modifying the Album.";

                        return View(album);
                    }

                    return RedirectToAction("Index", new { x = album.ArtistID });

                }
            }
            else
            {
                ViewBag.ErrorMessage = "The form is not valid!";
                return View(album);
            }
        }

        public ActionResult Insert(int id)
        {
            if(id == null)
                return RedirectToAction("Index", "Home");

            using(MyMusicListDB _db = new MyMusicListDB())
            {
                Artist artist = _db.Artists.Where(x => x.ID == id).FirstOrDefault();
                if(artist == null)
                    return RedirectToAction("Index", "Home");

                Album album = new Album();
                album.ArtistID = artist.ID;
                album.Artist = artist;

                return View(album);
            }

        }

        [HttpPost]
        public ActionResult Insert(Album album, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                using (MyMusicListDB _db = new MyMusicListDB())
                {
                    if (file != null)
                    {
                        string[] allowed = { ".jpg", ".jpeg", ".png", ".gif" };
                        string extension = System.IO.Path.GetExtension(file.FileName);

                        if (allowed.Contains(extension.ToLower()))
                        {
                            string CoverPath = WebConfigurationManager.OpenWebConfiguration("/Views/Web.config").AppSettings.Settings["CoverPath"].Value;
                            string NewLocation = Server.MapPath("~") + CoverPath + "Cover_" + album.ID + extension;

                            try
                            {
                                file.SaveAs(NewLocation);
                            }
                            catch (Exception ex)
                            {
                                ViewBag.error = "Error uploading image.";

                                return View(album);

                            }

                            album.ImgUrl = CoverPath + "Cover_" + album.ID + extension;

                        }
                        else
                        {
                            ViewBag.error = "Only .jpg .jpeg .png .gif file types are allowed!";
                            return View(album);
                        }

                    }

                    try
                    {
                        _db.Albums.Add(album);
                        _db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.error = "Error while modifying the Album.";

                        return View(album);
                    }

                    return RedirectToAction("Index", new { x = album.ArtistID });
                }
            }
            else
            {
                ViewBag.ErrorMessage = "The form is not valid!";
                return View(album);
            }
        }

        public ActionResult Delete(int id)
        {
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                Album album = _db.Albums.Where(x => x.ID == id).FirstOrDefault();
                if (album != null)
                {
                    _db.Albums.Remove(album);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index", new { x = "all" });
        }

        public JsonResult SearchAlbums(string term, int artistId)
        {
            term = term.ToLower();
            List<Album> albums = new List<Album>();
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                if (artistId == 0)
                {
                    albums = _db.Albums.Where(x => x.Name.ToLower().Contains(term)).ToList();
                }
                else 
                {
                    albums = _db.Albums.Where(x => x.ArtistID == artistId && x.Name.ToLower().Contains(term)).ToList();
                }
            }

            return Json(albums.Select(x => new { 
                label = x.Name,
                value = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
