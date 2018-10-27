using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.EntityFramework.Configurations
{
    public class SstRegistrationTestHarnessSystemConfiguration : EntityTypeConfiguration<SstRegistrationTestHarnessSystem>
    {
        public SstRegistrationTestHarnessSystemConfiguration()
        {
            HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).IsRequired();
            
            HasMany(SstRegistrationTestHarnessSystem.UsersAccessor).WithRequired();
            HasMany(SstRegistrationTestHarnessSystem.ContractorAccessor).WithRequired();
        }
    }
}