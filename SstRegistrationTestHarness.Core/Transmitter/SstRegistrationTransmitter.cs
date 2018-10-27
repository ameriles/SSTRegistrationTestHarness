using System;
using System.Collections.Generic;
using System.Linq;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Submission;
using SstRegistrationTestHarness.Core.Utilities;

namespace SstRegistrationTestHarness.Core.Transmitter
{
    public class SstRegistrationTransmitter
    {
        public TransmittalEnvironment TransmittalEnvironment { get; }
        public Contractor Contractor { get; }
        public bool ForceTransmission { get; set; }

        public SstRegistrationTransmitter(Contractor contractor, TransmittalEnvironment transmittalEnvironment)
        {
            Contractor = contractor;
            TransmittalEnvironment = transmittalEnvironment;
        }

        public SimplifiedReturnResponse BulkRegistration(BulkRegistrationTransmissionType request)
        {
            var serviceBuilder = TransmittalEnvironment.CrateServiceBuilder();
            var serviceClient = serviceBuilder.Build();
            var contentSent = XmlHelper.Serialize(request);

            BulkRegistrationAcknowledgementType serviceResponse;

            try
            {
                serviceResponse = serviceClient.BulkRegistration(request);
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                var response = new SimplifiedReturnResponse { ContentSent = contentSent };
                foreach (var item in request.BulkRegistrationDocument)
                {
                    response.Errors.Add(new SstError(item.DocumentId, errors));
                }

                return response;
            }

            var contentReceived = XmlHelper.Serialize(serviceResponse);
            var simplifiedResponse = new SimplifiedReturnResponse(contentSent, contentReceived, serviceResponse);

            if (serviceResponse.TransmissionAcknowledgement.TransmissionStatus == StatusType.R)
            {
                simplifiedResponse.Errors.Add(new SstError(serviceResponse.TransmissionAcknowledgement.TransmissionId, serviceResponse.TransmissionAcknowledgement.Errors.Error.Select(x => x.ToString()).ToList()));
                return simplifiedResponse;
            }

            foreach (var status in serviceResponse.BulkRegAcknowledgement.Where(x => x.DocumentStatus == StatusType.R))
            {
                var error = new SstError(status.DocumentId, status.Errors.Error.Select(x => x.ToString()));
                simplifiedResponse.Errors.Add(error);
            }

            return simplifiedResponse;
        }
    }
}