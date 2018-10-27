using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public interface ICouItemValidable
    {
        void Validate(BulkRegistrationCOUTypeActionCode actionCode, SstValidator sstValidator);
    }
}