using System.Data.Entity;
using SQLite.CodeFirst;
using SstRegistrationTestHarness.Core.EntityFramework;

namespace SstRegistrationTestHarness.Core.Test
{
    public class SstRegistrationTestHernessSystemDbInitializer : SqliteDropCreateDatabaseAlways<SstRegistrationTestHarnessSystemContext>
    {
        public SstRegistrationTestHernessSystemDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder)
        {
        }
    }
}
