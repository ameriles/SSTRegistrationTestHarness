using System;
using System.Linq;

namespace SstRegistrationTestHarness.Core.Utilities
{
    public class EnumHelper
    {
        public static T GetEnumItemFromValueName<T>(string name)
        {
            var value = Enum.GetValues(typeof(T))
                .Cast<T>()
                .FirstOrDefault(x => x.ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (value != null)
            {
                return value;
            }

            throw new ArgumentException($"No item exists for type {typeof(T)} corresponding to value of {name}");
        }
    }
}
