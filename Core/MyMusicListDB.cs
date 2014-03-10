using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyMusicList.Areas.User.Models;
using MyMusicList.Areas.Admin.Models;

namespace MyMusicList.Core
{
    public class MyMusicListDB : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Mp3> Mp3s { get; set; }

    }

}