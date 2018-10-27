namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public interface IAddressType
    {
        string AddressLine1Txt { get; set; }
        string AddressLine2Txt { get; set; }
        string CityNm { get; set; }
        CountryType Country { get; }
        string State { get; }
        string ZipCode { get; }
        string TypeName { get; }
    }
}