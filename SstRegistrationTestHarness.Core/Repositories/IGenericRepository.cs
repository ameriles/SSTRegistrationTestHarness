using System.Collections.Generic;

namespace SstRegistrationTestHarness.Core.Repositories
{
    public interface IGenericRepository<in TId>
    {
        TEntity GetById<TEntity>(TId id) where TEntity : class;
        void Save<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        ICollection<TEntity> ListAll<TEntity>() where TEntity : class;
    }
}