using BlogDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogDAL;
using BlogDAL.DAL;
using System.Data;
using System.Transactions;

namespace RepositoryBL.Interfaces
{
    public class UnitOfWork : IOperations
    {
        private DBContext context = new BlogDAL.DBContext();
        private SimpleRepo<ThemeOptions> _themeOptionsRepository;
        private SimpleRepo<Posts> _postsRepository;
        private bool _disposed = false;


        public SimpleRepo<ThemeOptions> ThemeOptionsRepository
        {
            get
            {
                if (_themeOptionsRepository == null)
                {
                    _themeOptionsRepository = new SimpleRepo<ThemeOptions>(context);
                }
                return _themeOptionsRepository;
            }
        }

        public SimpleRepo<Posts> PostsRepository
        {
            get
            {
                if (_postsRepository == null)
                {
                    _postsRepository = new SimpleRepo<Posts>(context);
                }
                return _postsRepository;
            }
        }

        public void Save()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                context.SaveChanges();
                scope.Complete();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}