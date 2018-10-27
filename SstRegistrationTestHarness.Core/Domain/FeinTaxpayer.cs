using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class FeinTaxpayer : Taxpayer
    {
        public string FederalEmployerIdentificationNumber { get; set; }
        public string CompanyName { get; set; }

        protected override TINType MapTaxpayerIdentificationNumber()
        {
            return new TINType
            {
                TypeTIN = TINTypeTypeTIN.FEIN,
                FedTIN = FederalEmployerIdentificationNumber
            };
        }

        public override object MapTaxpayerName()
        {
            return CompanyName;
        }

        public FeinTaxpayer(Contractor contractor, BulkOperationNew bulkOperationNew, string federalEmployerIdentificationNumber, string companyName, string stateIncorporated, string sellerPhone, string password)
            : base(contractor, bulkOperationNew, stateIncorporated, sellerPhone, password)
        {
            FederalEmployerIdentificationNumber = federalEmployerIdentificationNumber;
            CompanyName = companyName;
        }

        public FeinTaxpayer(Contractor contractor, BulkOperationCou bulkOperationCou, string sstpId, string federalEmployerIdentificationNumber)
            : base(contractor, bulkOperationCou)
        {
            SstpId = sstpId;
            FederalEmployerIdentificationNumber = federalEmployerIdentificationNumber;
        }
    }
}