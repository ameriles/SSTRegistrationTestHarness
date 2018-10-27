using System.Collections.Generic;
using System.Linq;

namespace SstRegistrationTestHarness.Core.Submission
{
    public class SstError
    {
        public string DocumentId { get; }
        public IList<string> Messages { get; }

        public SstError(string documentId, IEnumerable<string> messages)
        {
            DocumentId = documentId;
            Messages = messages.ToList();
        }
    }
}
