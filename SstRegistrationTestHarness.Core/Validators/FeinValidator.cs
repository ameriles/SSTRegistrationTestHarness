using System.Collections.Generic;

namespace SstRegistrationTestHarness.Core.Validators
{
    public class FeinValidator : TinTypeValidator
    {
        public FeinValidator(IList<string> errorMessages)
            : base(errorMessages)
        {
        }

        public override IList<string> InvalidPrefixes => new[] { "00", "07", "08", "09", "17", "18", "19", "28", "29", "49", "69", "70", "78", "79", "89", "96", "97" };
        public override string InvalidPrefixesMessage => "Do not allow FEIN with the first two digits of";

        public override void ValidateTaxPayerName(object taxPayerName)
        {
            if (string.IsNullOrWhiteSpace((string)taxPayerName))
            {
                ErrorMessages.Add("The Business Name is mandatory.");
            }
        }
    }
}