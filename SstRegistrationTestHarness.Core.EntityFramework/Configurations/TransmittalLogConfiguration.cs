using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.EntityFramework.Configurations
{
    public class TransmittalLogConfiguration : EntityTypeConfiguration<TransmittalLog>
    {
        public TransmittalLogConfiguration()
        {
            HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LogDate).IsRequired();
            Property(x => x.EndpointUrl).IsRequired();
            Property(x => x.TransmissionMode).IsRequired();
            Property(x => x.ContentSent).IsRequired();
            Property(x => x.ContentReceived);
            Property(x => x.TransmissionId).IsRequired();
            Property(x => x.TransmissionStatus).IsRequired();
            HasMany(x => x.LogItems).WithRequired();
            HasRequired(x => x.Submitter);
            HasRequired(x => x.SstRegistrationTestHarnessSystem);
        }
    }
}
