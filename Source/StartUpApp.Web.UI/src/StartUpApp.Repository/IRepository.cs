using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StartUpApp.Repository
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetAll();

        int Count();

        T GetSingle(int id);

        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        T Update(T entity);

        T Delete(T entity);

        IEnumerable<T> DeleteWhere(Expression<Func<T, bool>> predicate);

        void Commit();
    }
}
