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
    public class Mp3Controller : Controller
    {
        public ActionResult Index(int range)
        {
            int start = range * 12;
           
            using (MyMusicListDB _db = new MyMusicListDB())
            {
                int cnt = _db.Mp3s.ToList().Count;
                var model = _db.Mp3s.Include("Albums").Where(y => y.Number > start).OrderBy(x => x.Number).Take(12).ToList();

                int linksCount = (cnt / 12) + 1;
                List<int> linksArray = new List<int>();

                for (int i = 0; i < linksCount; i++)
                {
                    linksArray.Add(i);
                }

                ViewBag.TotalCount = cnt;
                ViewBag.StartCount = start + 1;
                ViewBag.EndCount = ((start + 12) > cnt) ? cnt : start+12;
                ViewBag.CurrentRange = range;
                ViewBag.Links = linksArray;
                return View(model);
            }

            
        }

    }
}
