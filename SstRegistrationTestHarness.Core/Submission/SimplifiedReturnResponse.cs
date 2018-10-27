using System.Collections.Generic;
using System.Linq;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Submission
{
    public class SimplifiedReturnResponse
    {
        public IList<SstError> Errors { get; }
        public string ContentSent { get; set; }
        public string ContentReceived { get; set; }
        public BulkRegistrationAcknowledgementType OriginalResponse { get; set; }
        public bool HasErrors => Errors.Any();

        public SimplifiedReturnResponse()
        {
            OriginalResponse = new BulkRegistrationAcknowledgementType { BulkRegAcknowledgement = new BulkRegAcknowledgementType[] { } };
            Errors = new List<SstError>();
        }

        public SimplifiedReturnResponse(string contentSent, string contentReceived, BulkRegistrationAcknowledgementType originalResponse)
        {
            ContentSent = contentSent;
            ContentReceived = contentReceived;
            OriginalResponse = originalResponse;
            Errors = new List<SstError>();
        }

        public ETransmittalStatus DefineTransmittalStatus(IList<BulkRegistrationDocumentType> documents)
        {
            if (documents.All(doc => Errors.Any(error => error.DocumentId.Equals(doc.DocumentId))))
            {
                return ETransmittalStatus.Rejected;
            }

            return !Errors.Any() ? ETransmittalStatus.Approved : ETransmittalStatus.ApprovedWithErrors;
        }

        public ETransmittalStatus DefineDocumentStatus(string documentId)
        {
            return OriginalResponse.BulkRegAcknowledgement.Any(x => x.DocumentId.Equals(documentId) && x.DocumentStatus == StatusType.A) ? ETransmittalStatus.Approved : ETransmittalStatus.Rejected;
        }

        public BulkRegAcknowledgementType GetDocumentAcknowledgementByDocumentId(string documentId)
        {
            return OriginalResponse.BulkRegAcknowledgement.FirstOrDefault(x => x.DocumentId.Equals(documentId));
        }
    }
}
