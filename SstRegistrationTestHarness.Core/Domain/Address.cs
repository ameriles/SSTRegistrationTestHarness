using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public abstract class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }

        protected Address(string addressLine1, string city)
        {
            AddressLine1 = addressLine1;
            City = city;
        }

        public AddressType MapAddressType()
        {
            return new AddressType { Item = CreateDtoAddress() };
        }

        protected abstract object CreateDtoAddress();
    }
}
