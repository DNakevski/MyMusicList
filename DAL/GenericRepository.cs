using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyMusicList.Core;

namespace MyMusicList.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal MyMusicListDB context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(MyMusicListDB context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

    }
}