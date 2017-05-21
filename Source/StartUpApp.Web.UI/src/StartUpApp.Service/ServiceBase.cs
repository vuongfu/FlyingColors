using StartUpApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using StartUpApp.Repository;

namespace StartUpApp.Service
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        protected IRepository<TEntity> Repository;

        public TEntity Add(TEntity entity)
        {
            var result = Repository.Add(entity);
            return result;
        }

        public IEnumerable<TEntity> AllIncludeding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = Repository.AllIncluding(includeProperties);
            return result;
        }

        public int Count()
        {
            var result = Repository.Count();
            return result;
        }

        public TEntity Delete(TEntity entity)
        {
            var result = Repository.Delete(entity);
            return result;
        }

        public IEnumerable<TEntity> DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var result = Repository.DeleteWhere(predicate);
            return result;
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            var result = Repository.FindBy(predicate);
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {        
            var result = Repository.GetAll();
            return result;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            var result = Repository.GetSingle(predicate);
            return result;
        }

        public TEntity GetSingle(int id)
        {
            var result = Repository.GetSingle(id);
            return result;
        }

        public TEntity Getsingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = Repository.GetSingle(predicate,includeProperties);
            return result;
        }

        public TEntity SaveOrUpdate(TEntity entity)
        {
            if (entity.IsNewEntity)
            {
                var saveResult = Repository.Add(entity);
                return saveResult;
            }
            else
            {
                var updateResult = Repository.Update(entity);
                return updateResult;
            }
        }

        public TEntity Update(TEntity entity)
        {
            var result = Repository.Update(entity);
            return result;
        }
    }
}
