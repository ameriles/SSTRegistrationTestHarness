using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class BusinessInfo : IActionCouItem
    {
        public string DoingBusinessName { get; set; }
        public string SellerPhone { get; set; }
        public string SellerPhoneExtension { get; set; }
        public string StateIncorporated { get; set; }
        public string NaicsCode { get; set; }
        public Address PhysicalAddress { get; set; }
        public Address MailingAddress { get; set; }
        public ContactSellerInfo ContactInfo { get; set; }

        public object MapActionItem()
        {
            return new BulkRegistrationCOUTypeBusinessInfo
            {
                DBAName = DoingBusinessName,
                NAICSCode = NaicsCode,
                PhysicalAddress = PhysicalAddress?.MapAddressType(),
                MailingAddress = MailingAddress?.MapAddressType(),
                SellerPhone = SellerPhone?.Trim(),
                SellerPhoneExt = SellerPhoneExtension,
                StateIncorporated = StateIncorporated,
                SSTPContact = ContactInfo?.MapContactInformation()
            };
        }
    }
}