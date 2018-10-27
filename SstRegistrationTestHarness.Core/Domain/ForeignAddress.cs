using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Utilities;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class ForeignAddress : Address
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

        public ForeignAddress(string country, string province, string postalCode, string addressLine1, string city)
            : base(addressLine1, city)
        {
            Country = country;
            Province = province;
            PostalCode = postalCode;
        }

        protected override object CreateDtoAddress()
        {
            var country = EnumHelper.GetEnumItemFromValueName<CountryType>(Country);

            return new AddressTypeForeignAddress
            {
                AddressLine1Txt = AddressLine1,
                AddressLine2Txt = AddressLine2,
                CityNm = City,
                CountryCd = country,
                ForeignPostalCd = PostalCode,
                ProvinceOrStateNm = Province
            };
        }
    }
}