using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.EntityFramework.Configurations
{
    public class TransmittalLogItemConfiguration : EntityTypeConfiguration<TransmittalLogItem>
    {
        public TransmittalLogItemConfiguration()
        {
            HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DocumentId).IsRequired();
            Property(x => x.SstpId);
            Property(x => x.DocumentStatus).IsRequired();
            Property(x => x.Errors);
        }
    }
}