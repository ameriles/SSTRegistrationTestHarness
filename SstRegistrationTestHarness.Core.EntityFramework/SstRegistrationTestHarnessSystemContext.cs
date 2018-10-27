using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.EntityFramework.Conventions;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;

namespace SstRegistrationTestHarness.Core.EntityFramework
{
    public class SstRegistrationTestHarnessSystemContext : DbContext
    {
        private readonly IFluentConfigurer _fluentConfigurer;
        public virtual DbSet<SstRegistrationTestHarnessSystem> SstRegistrationTestHarnessSystem { get; set; }

        public SstRegistrationTestHarnessSystemContext(IFluentConfigurer fluentConfigurer, DbConnection connection)
            : base(connection, true)
        {
            _fluentConfigurer = fluentConfigurer;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
            modelBuilder.Configurations.AddFromAssembly(_fluentConfigurer.ContextType.Assembly);
            
            var initializer = _fluentConfigurer.CreateInitializer<SstRegistrationTestHarnessSystemContext>(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}
