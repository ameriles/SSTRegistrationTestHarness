using System.Collections.Generic;

namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class BulkRegistrationDocumentType
    {
        public IList<string> ErrorMessages { get; }

        public BulkRegistrationDocumentType()
        {
            ErrorMessages = new List<string>();
        }
    }
}
