using System;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TransmittalLogItem
    {
        public Guid Id { get; protected set; }
        public string DocumentId { get; protected set; }
        public string SstpId { get; set; }
        public ETransmittalStatus DocumentStatus { get; protected set; }
        public string Errors { get; set; }

        protected TransmittalLogItem()
        {
        }

        public TransmittalLogItem(string documentId, ETransmittalStatus documentStatus)
        {
            Id = Guid.NewGuid();
            DocumentId = documentId;
            DocumentStatus = documentStatus;
        }
    }
}