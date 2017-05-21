using StartUpApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StartUpApp.Service
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        IEnumerable<TEntity> AllIncludeding(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAll();
        int Count();
        TEntity GetSingle(int id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        TEntity Getsingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity SaveOrUpdate(TEntity entity);
        TEntity Delete(TEntity entity);
        IEnumerable<TEntity> DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
