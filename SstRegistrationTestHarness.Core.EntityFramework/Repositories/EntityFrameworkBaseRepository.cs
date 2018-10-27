using System;
using System.Data.Entity;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;

namespace SstRegistrationTestHarness.Core.EntityFramework.Repositories
{
    public class EntityFrameworkBaseRepository
    {
        protected virtual DbContext CurrentContext
        {
            get
            {
                if (EntityFrameworkDbContextHelper.Instance.Context == null)
                {
                    throw new ArgumentNullException("The Context is not initialized.");
                }

                return EntityFrameworkDbContextHelper.Instance.Context;
            }
        }
    }
}
