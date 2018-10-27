using System;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public abstract class Taxpayer
    {
        public Guid Id { get; protected set; }
        public Contractor Contractor { get; }
        public string SstpId { get; set; }
        public string StateIncorporated { get; set; }
        public string SellerPhoneExtension { get; set; }
        public string SellerPhone { get; set; }
        public string DoingBusinessName { get; set; }
        public string Password { get; set; }
        public BulkOperationType BulkOperation { get; }
        public SstRegistrationTestHarnessSystem SstRegistrationTestHarnessSystem { get; set; }

        protected Taxpayer()
        {
        }

        protected Taxpayer(Contractor contractor, BulkOperationType bulkOperation, string stateIncorporated, string sellerPhone, string password)
        {
            Contractor = contractor;
            BulkOperation = bulkOperation;
            StateIncorporated = stateIncorporated;
            SellerPhone = sellerPhone;
            Password = password;
        }

        protected Taxpayer(Contractor contractor, BulkOperationType bulkOperation)
        {
            Contractor = contractor;
            BulkOperation = bulkOperation;
        }

        protected abstract TINType MapTaxpayerIdentificationNumber();
        public abstract object MapTaxpayerName();

        public BulkRegistrationDocumentType CreateOperationRecord()
        {
            var taxpayerIdentificationNumber = MapTaxpayerIdentificationNumber();
            var bulkRegistrationHeader = new BulkRegistrationHeaderType
            {
                ElectronicPostmark = new BulkRegistrationHeaderTypeElectronicPostmark
                {
                    CSPID = Contractor.CspCode,
                    Value = DateTime.UtcNow
                },
                FilingType = BulkOperation.FilingType,
                TIN = taxpayerIdentificationNumber
            };

            var bulkRegistrationType = BulkOperation.CreateBulkRegistrationType(this);
            var document = new BulkRegistrationDocumentType
            {
                BulkRegistrationHeader = bulkRegistrationHeader,
                DocumentType = BulkOperation.DocumentType,
                Item = bulkRegistrationType
            };

            return document;
        }
    }
}
