using System;

namespace SstRegistrationTestHarness.Core.Exceptions
{
    public class EntityModelException : Exception
    {
        public string PropertyName { get; }

        public EntityModelException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
