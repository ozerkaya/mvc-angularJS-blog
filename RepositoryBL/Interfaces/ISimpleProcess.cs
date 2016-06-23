using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryBL.Interfaces
{
    public interface ISimpleProcess<T> where T : class
    {
        T findById(object id, string include = "NoN");

        IEnumerable<T> find(Expression<Func<T, bool>> Filter = null, string include = "NoN");

        List<T> list(string include = "NoN");

        IList<T> listByWhere(Expression<Func<T, bool>> Filter = null);

        T deleteById(object id);

        bool delete(Expression<Func<T, bool>> Filter = null);

        bool update(Expression<Func<T, bool>> Filter = null);

        bool insert(T Entity);

        T getFirstOrDefault();

        bool removeRange(Expression<Func<T, bool>> Filter = null);
    }
}
