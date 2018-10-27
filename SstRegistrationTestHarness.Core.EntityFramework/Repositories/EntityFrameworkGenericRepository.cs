using System.Collections.Generic;
using System.Linq;
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

        public ICollection<TEntity> ListAll<TEntity>() where TEntity : class
        {
            return CurrentContext.Set<TEntity>().ToList();
        }
    }
}
