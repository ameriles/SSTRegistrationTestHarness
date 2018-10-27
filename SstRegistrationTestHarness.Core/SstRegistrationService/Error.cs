namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class Error
    {
        public override string ToString()
        {
            return $"{ItemElementName}: {Item}. Error message: {ErrorMessage}. Data value: {DataValue}.";
        }
    }
}
