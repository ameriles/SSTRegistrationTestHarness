using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class SsnTaxPayer : Taxpayer
    {
        public string SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        protected override TINType MapTaxpayerIdentificationNumber()
        {
            return new TINType
            {
                TypeTIN = TINTypeTypeTIN.SSN,
                FedTIN = SocialSecurityNumber
            };
        }

        public override object MapTaxpayerName()
        {
            return new IndividualNameType
            {
                FirstName = FirstName,
                LastName = LastName,
                MiddleInitial = MiddleName
            };
        }

        public SsnTaxPayer(Contractor contractor, BulkOperationNew bulkOperationNew, string socialSecurityNumber, string firstName, string lastName, string stateIncorporated, string sellerPhone, string password)
            : base(contractor, bulkOperationNew, stateIncorporated, sellerPhone, password)
        {
            SocialSecurityNumber = socialSecurityNumber;
            FirstName = firstName;
            LastName = lastName;
        }

        public SsnTaxPayer(Contractor contractor, BulkOperationCou bulkOperationCou, string sstpId, string socialSecurityNumber)
            : base(contractor, bulkOperationCou)
        {
            SstpId = sstpId;
            SocialSecurityNumber = socialSecurityNumber;
        }
    }
}