using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.Repositories
{
    public interface IContractorRepository
    {
        bool IsUnique(Contractor contractor);
    }
}