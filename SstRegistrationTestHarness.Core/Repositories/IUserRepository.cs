using SstRegistrationTestHarness.Core.Domain;

namespace SstRegistrationTestHarness.Core.Repositories
{
    public interface IUserRepository
    {
        bool IsUnique(User user);
    }
}