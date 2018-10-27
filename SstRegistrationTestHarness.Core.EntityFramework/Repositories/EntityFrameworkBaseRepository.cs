using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;

namespace SstRegistrationTestHarness.Core.EntityFramework.Repositories
{
    public class EntityFrameworkBaseRepository
    {
        protected virtual DbContext CurrentContext
        {
            get
            {
                if (EntityFrameworkDbContextHelper.Instance.Context == null)
                {
                    throw new ArgumentNullException("The Context is not initialized.");
                }

                return EntityFrameworkDbContextHelper.Instance.Context;
            }
        }

        protected IQueryable<TEntity> RetrievePage<TEntity, TKey>(IQueryable<TEntity> queryable, int page, int pageSize, Expression<Func<TEntity, TKey>> orderExpression = null, bool @ascending = true) where TEntity : class
        {
            IOrderedQueryable<TEntity> orderedQueryable;
            if (ascending)
            {
                orderedQueryable = orderExpression != null
                    ? queryable.OrderBy(orderExpression)
                    : OrderBy(queryable, "Id");
            }
            else
            {
                orderedQueryable = orderExpression != null
                    ? queryable.OrderByDescending(orderExpression)
                    : OrderBy(queryable, "Id", false);
            }

            return orderedQueryable.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");

            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }

            return Expression.Lambda(body, param);
        }

        private static IOrderedQueryable<TSource> OrderBy<TSource>(IQueryable<TSource> query, string key, bool ascending = true)
        {
            var lambda = (dynamic)CreateExpression(typeof(TSource), key);

            return ascending
                ? Queryable.OrderBy(query, lambda)
                : Queryable.OrderByDescending(query, lambda);
        }
    }

}
