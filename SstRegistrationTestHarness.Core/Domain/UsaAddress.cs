using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Utilities;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class UsaAddress : Address
    {
        public string State { get; set; }
        public string ZipCode { get; set; }

        public UsaAddress(string state, string zipCode, string addressLine1, string city)
            : base(addressLine1, city)
        {
            State = state;
            ZipCode = zipCode;
        }

        protected override object CreateDtoAddress()
        {
            var state = EnumHelper.GetEnumItemFromValueName<StateType>(State);

            return new AddressTypeUSAddress
            {
                AddressLine1Txt = AddressLine1,
                AddressLine2Txt = AddressLine2,
                CityNm = City,
                StateAbbreviationCd = state,
                ZIPCd = ZipCode
            };
        }
    }
}