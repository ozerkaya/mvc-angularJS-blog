using RepositoryBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using BlogDAL;
using System.Data.Entity;

namespace RepositoryBL
{
    public class SimpleRepo<T> : ISimpleProcess<T> where T : class
    {
        private DBContext context;

        private DbSet<T> dbset;

        public SimpleRepo(DBContext db)
        {
            context = db;
            dbset = context.Set<T>();
        }

        public T findById(object id, string include = "NoN")
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> find(Expression<Func<T, bool>> Filter = null, string include = "NoN")
        {
            if (include == "NoN")
            {
                return dbset.Where(Filter);
            }
            else
            {
                return dbset.Include(include).Where(Filter);
            }

        }

        public bool deleteById(object id)
        {
            try
            {
                T entity = dbset.Find(id);
                dbset.Attach(entity);
                dbset.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool delete(Expression<Func<T, bool>> Filter = null)
        {
            try
            {
                T entity = dbset.FirstOrDefault(Filter);
                dbset.Attach(entity);
                dbset.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        T ISimpleProcess<T>.deleteById(object id)
        {
            throw new NotImplementedException();
        }

        public bool update(T entity)
        {
            try
            {
                dbset.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(Expression<Func<T, bool>> Filter = null)
        {
            throw new NotImplementedException();
        }

        public bool insert(T entity)
        {
            try
            {
                dbset.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<T> list(string include = "NoN")
        {
            if (include == "NoN")
            {
                return dbset.ToList();
            }
            else
            {
                return dbset.Include(include).ToList();
            }

        }      

        public IList<T> listByWhere(Expression<Func<T, bool>> Filter = null)
        {
            return dbset.Where(Filter).ToList();
        }

        public int countByWhere(Expression<Func<T, bool>> Filter = null)
        {
            return dbset.Count(Filter);
        }

        public T getFirstOrDefault()
        {
            return dbset.FirstOrDefault();
        }

        public bool removeRange(Expression<Func<T, bool>> Filter = null)
        {
            try
            {
                dbset.RemoveRange(dbset.Where(Filter));
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}