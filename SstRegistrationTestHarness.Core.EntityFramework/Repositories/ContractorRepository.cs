using System.Linq;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.Repositories;

namespace SstRegistrationTestHarness.Core.EntityFramework.Repositories
{
    public class ContractorRepository : EntityFrameworkBaseRepository, IContractorRepository
    {
        public bool IsUnique(Contractor contractor)
        {
            return CurrentContext.Set<Contractor>().Count(x => x.CompanyName == contractor.CompanyName && x.Id != contractor.Id) == 0;
        }
    }
}
