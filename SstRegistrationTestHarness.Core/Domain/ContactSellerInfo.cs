using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class ContactSellerInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string InitialName { get; set; }
        public string Phone { get; set; }
        public string PhoneExtention { get; set; }
        public string Email { get; set; }

        public ContactSellerInfo(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        public ContactType MapContactInformation()
        {
            return new ContactType
            {
                ContactName = new IndividualNameType
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    MiddleInitial = InitialName
                },
                ContactPhone = Phone,
                ContactPhoneExt = PhoneExtention,
                ContactEmail = Email
            };
        }
    }
}