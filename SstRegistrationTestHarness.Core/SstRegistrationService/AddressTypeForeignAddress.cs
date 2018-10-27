namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class AddressTypeForeignAddress : IAddressType
    {
        public CountryType Country => CountryCd;
        public string State => ProvinceOrStateNm;
        public string ZipCode => ForeignPostalCd;
        public string TypeName => "Foreign Address";
    }
}