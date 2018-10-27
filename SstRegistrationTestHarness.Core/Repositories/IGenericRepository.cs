using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SstRegistrationTestHarness.Core.Repositories
{
    public interface IGenericRepository<in TId>
    {
        TEntity GetById<TEntity>(TId id) where TEntity : class;
        void Save<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        int Count<TEntity>() where TEntity : class;
        IQueryable<TEntity> ListAll<TEntity>() where TEntity : class;
        IQueryable<TEntity> ListByQuery<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        IQueryable<TEntity> ListAllPaged<TEntity>(int page, int pageSize) where TEntity : class;
        IQueryable<TEntity> ListAllPaged<TEntity, TKey>(int page, int pageSize, Expression<Func<TEntity, TKey>> orderExpression, bool ascending = true) where TEntity : class;
        IQueryable<TEntity> ListByQueryPaged<TEntity>(Expression<Func<TEntity, bool>> expression, int page, int pageSize) where TEntity : class;
        IQueryable<TEntity> ListByQueryPaged<TEntity, TKey>(Expression<Func<TEntity, bool>> expression, int page, int pageSize, Expression<Func<TEntity, TKey>> orderExpression, bool ascending = true) where TEntity : class;
    }
}