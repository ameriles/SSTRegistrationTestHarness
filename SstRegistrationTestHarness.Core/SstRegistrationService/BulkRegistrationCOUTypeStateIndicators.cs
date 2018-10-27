using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class BulkRegistrationCOUTypeStateIndicators : ICouItemValidable
    {
        public void Validate(BulkRegistrationCOUTypeActionCode actionCode, SstValidator sstValidator)
        {
            var stateIndicatorValidator = new StateIndicatorValidator(sstValidator);

            switch (actionCode)
            {
                case BulkRegistrationCOUTypeActionCode.C:
                    if (CSPEndDateSpecified)
                    {
                        stateIndicatorValidator.ValidateEndManagingRegistration(this);
                    }
                    else
                    {
                        stateIndicatorValidator.ValidateUpdate(this);
                    }
                    break;

                case BulkRegistrationCOUTypeActionCode.O:
                    stateIndicatorValidator.ValidateOutOfBusiness(this);
                    break;

                case BulkRegistrationCOUTypeActionCode.U:
                    stateIndicatorValidator.ValidateUnvolunteerUnregister(this);
                    break;
            }
        }
    }
}
