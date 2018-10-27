using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.Domain
{
    public abstract class BulkOperationType
    {
        public SstValidator Validator { get; protected set; }
        public abstract BulkRegistrationDocumentTypeDocumentType DocumentType { get; }
        public abstract BulkRegistrationHeaderTypeFilingType FilingType { get; }
        public abstract object CreateBulkRegistrationType(Taxpayer taxpayer);
    }
}
