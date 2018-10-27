using System.Collections.Generic;

namespace SstRegistrationTestHarness.Core.Validators
{
    public abstract class TinTypeValidator
    {
        public IList<string> ErrorMessages { get; }

        public abstract IList<string> InvalidPrefixes { get; }
        public abstract string InvalidPrefixesMessage { get; }

        protected TinTypeValidator(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public abstract void ValidateTaxPayerName(object taxPayerName);
    }
}