using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.EntityFramework.Configurations
{
    public class ContractorConfiguration : EntityTypeConfiguration<Contractor>
    {
        public ContractorConfiguration()
        {
            HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompanyName).IsRequired();
            Property(x => x.CasCode);
            Property(x => x.CspCode);
        }
    }
}
