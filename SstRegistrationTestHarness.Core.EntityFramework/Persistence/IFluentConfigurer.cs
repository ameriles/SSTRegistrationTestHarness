using System;
using System.Data;
using System.Data.Entity;

namespace SstRegistrationTestHarness.Core.EntityFramework.Persistence
{
    public interface IFluentConfigurer
    {
        Action<string> LogAction { get; }
        Type ContextType { get; }
        IDbConnection CreateConnection();
        IDatabaseInitializer<TDbContext> CreateInitializer<TDbContext>(DbModelBuilder modelBuilder) where TDbContext : DbContext;
    }
}
