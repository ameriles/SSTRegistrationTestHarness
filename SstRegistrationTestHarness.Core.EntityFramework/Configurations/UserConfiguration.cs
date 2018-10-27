using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.EntityFramework.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Username).IsRequired();
            Property(x => x.Password).IsRequired();
        }
    }
}