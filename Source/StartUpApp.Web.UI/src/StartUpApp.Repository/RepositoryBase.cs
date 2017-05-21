using StartUpApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using StartUpApp.DataAccess;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace StartUpApp.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        private StartUpAppDbContext _dbContext;

        public RepositoryBase(StartUpAppDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        public virtual  T Add(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            _dbContext.Set<T>().Add(entity);
            Commit();
            return entity;
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach(var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public virtual void Commit()
        {
            _dbContext.SaveChanges();
        }

        public virtual int Count()
        {
            return _dbContext.Set<T>().Count(); 
        }

        public virtual T Delete(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
            Commit();
            return entity;
        }

        public IEnumerable<T> DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _dbContext.Set<T>().Where(predicate);

            foreach(var entity in entities)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Deleted;
            }

            Commit();
            return entities;
        }

        public void Dispose()
        {
            if (_dbContext == null) return;
            _dbContext.Dispose();
            _dbContext = null;
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            //for database pagging
            // .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return _dbContext.Set<T>().AsEnumerable().OrderBy(c => c.Id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public T GetSingle(int id)
        {
            return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach(var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual T Update(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;

            Commit();
            return entity;
        }
    }
}
