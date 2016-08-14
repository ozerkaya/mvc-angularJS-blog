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
        private SimpleRepo<User> _userRepository;
        private SimpleRepo<LabelTypes> _labelTypesRepository;
        private SimpleRepo<Labels> _labelsRepository;
        private SimpleRepo<SocialContacts> _socialContactsRepository;
        private SimpleRepo<Comments> _commentsRepository;

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

        public SimpleRepo<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new SimpleRepo<User>(context);
                }
                return _userRepository;
            }
        }

        public SimpleRepo<LabelTypes> LabelTypesRepository
        {
            get
            {
                if (_labelTypesRepository == null)
                {
                    _labelTypesRepository = new SimpleRepo<LabelTypes>(context);
                }
                return _labelTypesRepository;
            }
        }

        public SimpleRepo<Labels> LabelsRepository
        {
            get
            {
                if (_labelsRepository == null)
                {
                    _labelsRepository = new SimpleRepo<Labels>(context);
                }
                return _labelsRepository;
            }
        }

        public SimpleRepo<SocialContacts> SocialContactsRepository
        {
            get
            {
                if (_socialContactsRepository == null)
                {
                    _socialContactsRepository = new SimpleRepo<SocialContacts>(context);
                }
                return _socialContactsRepository;
            }
        }

        public SimpleRepo<Comments> CommentRepository
        {
            get
            {
                if (_commentsRepository == null)
                {
                    _commentsRepository = new SimpleRepo<Comments>(context);
                }
                return _commentsRepository;
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