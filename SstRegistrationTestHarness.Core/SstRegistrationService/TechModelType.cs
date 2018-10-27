using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class TechModelType : ICouItemValidable
    {
        public void Validate(BulkRegistrationCOUTypeActionCode actionCode, SstValidator sstValidator)
        {
            if (actionCode != BulkRegistrationCOUTypeActionCode.C)
            {
                sstValidator.Content.ErrorMessages.Add($"The Action Code for Tech Model must be C. Value: {actionCode}.");
            }

            sstValidator.ValidateTechnologyModel(this);
        }
    }
}
