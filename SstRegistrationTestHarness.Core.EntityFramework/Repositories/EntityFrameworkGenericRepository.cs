using System;
using System.Linq;
using System.Linq.Expressions;
using SstRegistrationTestHarness.Core.Repositories;

namespace SstRegistrationTestHarness.Core.EntityFramework.Repositories
{
    public class EntityFrameworkGenericRepository<TId> : EntityFrameworkBaseRepository, IGenericRepository<TId>
    {
        public TEntity GetById<TEntity>(TId id) where TEntity : class
        {
            return CurrentContext.Set<TEntity>().Find(id);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            CurrentContext.Set<TEntity>().Add(entity);
            CurrentContext.SaveChanges();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            CurrentContext.Set<TEntity>().Remove(entity);
            CurrentContext.SaveChanges();
        }

        public int Count<TEntity>() where TEntity : class
        {
            return CurrentContext.Set<TEntity>().Count();
        }

        public IQueryable<TEntity> ListAll<TEntity>() where TEntity : class
        {
            return CurrentContext.Set<TEntity>();
        }

        public IQueryable<TEntity> ListByQuery<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            return CurrentContext.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> ListAllPaged<TEntity>(int page, int pageSize) where TEntity : class
        {
            var queryable = CurrentContext.Set<TEntity>();

            return RetrievePage<TEntity, TId>(queryable, page, pageSize);
        }

        public IQueryable<TEntity> ListAllPaged<TEntity, TKey>(int page, int pageSize, Expression<Func<TEntity, TKey>> orderExpression, bool ascending = true) where TEntity : class
        {
            var queryable = CurrentContext.Set<TEntity>();

            return RetrievePage(queryable, page, pageSize, orderExpression, ascending);
        }

        public IQueryable<TEntity> ListByQueryPaged<TEntity>(Expression<Func<TEntity, bool>> expression, int page, int pageSize) where TEntity : class
        {
            var queryable = CurrentContext.Set<TEntity>()
                .Where(expression);

            return RetrievePage<TEntity, TId>(queryable, page, pageSize);
        }

        public IQueryable<TEntity> ListByQueryPaged<TEntity, TKey>(Expression<Func<TEntity, bool>> expression, int page, int pageSize, Expression<Func<TEntity, TKey>> orderExpression, bool ascending = true) where TEntity : class
        {
            var queryable = CurrentContext.Set<TEntity>()
                .Where(expression);

            return RetrievePage(queryable, page, pageSize, orderExpression, ascending);
        }
    }
}
