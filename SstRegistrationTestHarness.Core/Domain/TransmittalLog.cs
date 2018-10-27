using System;
using System.Collections.Generic;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TransmittalLog
    {
        public Guid Id { get; protected set; }
        public DateTime LogDate { get; protected set; }
        public string EndpointUrl { get; protected set; }
        public ETransmissionMode TransmissionMode { get; set; }
        public string ContentSent { get; protected set; }
        public string ContentReceived { get; protected set; }
        public string TransmissionId { get; protected set; }
        public ETransmittalStatus TransmissionStatus { get; protected set; }
        public User Submitter { get; protected set; }
        public IList<TransmittalLogItem> LogItems { get; protected set; }
        public SstRegistrationTestHarnessSystem SstRegistrationTestHarnessSystem { get; set; }

        protected TransmittalLog()
        {
        }

        public TransmittalLog(TransmittalEnvironment transmittalEnvironment, User submitter, ETransmittalStatus transmissionStatus, string contentSent, string contentReceived, string transmissionId)
        {
            Id = Guid.NewGuid();
            EndpointUrl = transmittalEnvironment.EndpointUrl;
            TransmissionMode = transmittalEnvironment.TransmissionMode;
            Submitter = submitter;
            LogDate = DateTime.UtcNow;
            TransmissionStatus = transmissionStatus;
            ContentSent = contentSent;
            ContentReceived = contentReceived;
            TransmissionId = transmissionId;
            LogItems = new List<TransmittalLogItem>();
        }
    }
}
