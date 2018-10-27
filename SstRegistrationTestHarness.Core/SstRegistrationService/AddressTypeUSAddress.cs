namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class AddressTypeUSAddress : IAddressType
    {
        public CountryType Country => CountryType.US;
        public string State => StateAbbreviationCd.ToString();
        public string ZipCode => ZIPCd;
        public string TypeName => "Usa Address";
    }
}
