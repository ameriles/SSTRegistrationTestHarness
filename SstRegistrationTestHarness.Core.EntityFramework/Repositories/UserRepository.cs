using System.Linq;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.Repositories;

namespace SstRegistrationTestHarness.Core.EntityFramework.Repositories
{
    public class UserRepository : EntityFrameworkBaseRepository, IUserRepository
    {
        public bool IsUnique(User user)
        {
            return CurrentContext.Set<User>().Count(x => x.Username == user.Username && x.Id != user.Id) == 0;
        }
    }
}
