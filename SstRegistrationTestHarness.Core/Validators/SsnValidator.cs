using System.Collections.Generic;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Validators
{
    public class SsnValidator : TinTypeValidator
    {
        public SsnValidator(IList<string> errorMessages)
            : base(errorMessages)
        {
        }

        public override IList<string> InvalidPrefixes => new[] { "000", "666", "999" };
        public override string InvalidPrefixesMessage => "Do not allow SSN with the first three digits of";

        public override void ValidateTaxPayerName(object taxPayerName)
        {
            var individualName = (IndividualNameType)taxPayerName;
            if (string.IsNullOrWhiteSpace(individualName.FirstName))
            {
                ErrorMessages.Add("The First Name is mandatory.");
            }

            if (string.IsNullOrWhiteSpace(individualName.LastName))
            {
                ErrorMessages.Add("The Last Name is mandatory.");
            }

            if (!string.IsNullOrWhiteSpace(individualName.MiddleInitial) && individualName.MiddleInitial?.Length != 1)
            {
                ErrorMessages.Add($"The Middle Initial length must be one character. Value: {individualName.MiddleInitial}.");
            }
        }
    }
}