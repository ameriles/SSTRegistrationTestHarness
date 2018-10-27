using System;
using System.Data.Entity;

namespace SstRegistrationTestHarness.Core.EntityFramework.Persistence
{
    public class EntityFrameworkDbContextHelper
    {
        private static EntityFrameworkDbContextHelper _instance;
        public static EntityFrameworkDbContextHelper Instance => _instance ?? (_instance = new EntityFrameworkDbContextHelper());
        public DbContext Context { get; private set; }

        public void InitializeDbContextFactory(IFluentConfigurer configurer)
        {
            var connection = configurer.CreateConnection();
            Context = (DbContext)Activator.CreateInstance(configurer.ContextType, configurer, connection);
            Context.Database.Log = configurer.LogAction;
        }

        public DbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            Context.Database.CurrentTransaction.Rollback();
        }
    }
}
