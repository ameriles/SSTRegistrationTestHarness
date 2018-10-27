using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Utilities;

namespace SstRegistrationTestHarness.Core.Transmitter
{
    public class ApiServiceClientMock : ClientBase<IApiService>, IApiService
    {
        private readonly ServiceMockParameters _setupParameters;

        public ApiServiceClientMock(ServiceMockParameters setupParameters)
        {
            _setupParameters = setupParameters ?? new ServiceMockParameters();
        }

        public BulkRegistrationAcknowledgementType BulkRegistration(BulkRegistrationTransmissionType bulkRegistrationTransmission)
        {
            if (_setupParameters.ThrowEndpointNotFoundException)
            {
                throw new EndpointNotFoundException("Endpoint not found exception.");
            }

            var header = new AcknowledgementHeaderType
            {
                AcknowledgementTimestamp = DateTime.UtcNow,
                AcknowledgementCount = bulkRegistrationTransmission.BulkRegistrationDocument.Length.ToString()
            };

            var transmissionAcknowledgement = new TransmissionAcknowledgementType
            {
                TransmissionStatus = StatusType.A,
                TransmissionId = bulkRegistrationTransmission.TransmissionHeader.TransmissionId,
                TransmissionTimestamp = DateTime.UtcNow
            };

            var bulkRegistrationAcknowledgement = new List<BulkRegAcknowledgementType>();
            foreach (var item in bulkRegistrationTransmission.BulkRegistrationDocument)
            {
                var registrationAcknowledgement = new BulkRegAcknowledgementType
                {
                    DocumentId = item.DocumentId,
                    DocumentType = EnumHelper.GetEnumItemFromValueName<BulkRegAcknowledgementTypeDocumentType>(item.BulkRegistrationHeader.FilingType.ToString()),
                };

                if (item.ErrorMessages.Any())
                {
                    registrationAcknowledgement.DocumentStatus = StatusType.R;
                }
                else
                {
                    registrationAcknowledgement.SSTPID = item.DocumentType == BulkRegistrationDocumentTypeDocumentType.BulkRegistrationCOU
                        ? ((BulkRegistrationCOUType)item.Item).SSTPID
                        : Guid.NewGuid().ToString();
                }

                var errors = item.ErrorMessages.Select(errorMessage => new Error
                {
                    errorId = Guid.NewGuid().ToString(),
                    ErrorMessage = errorMessage,
                    AdditionalErrorMessage = errorMessage,
                    ItemElementName = ItemChoiceType.XPath
                }).ToList();

                if (errors.Any())
                {
                    registrationAcknowledgement.Errors = new Errors
                    {
                        errorCount = errors.Count.ToString(),
                        Error = errors.ToArray()
                    };
                }

                bulkRegistrationAcknowledgement.Add(registrationAcknowledgement);
            }

            return new BulkRegistrationAcknowledgementType
            {
                AcknowledgementHeader = header,
                TransmissionAcknowledgement = transmissionAcknowledgement,
                BulkRegAcknowledgement = bulkRegistrationAcknowledgement.ToArray()
            };
        }

        public Task<BulkRegistrationAcknowledgementType> BulkRegistrationAsync(BulkRegistrationTransmissionType bulkRegistrationTransmission)
        {
            return null;
        }

        public SSTRegistrationTransmissionType GetDocuments(string AcknowledgementStatus)
        {
            return new SSTRegistrationTransmissionType();
        }

        public Task<SSTRegistrationTransmissionType> GetDocumentsAsync(string AcknowledgementStatus)
        {
            return null;
        }

        public SSTRegistrationTransmissionType GetTransmission(string TransmissionId)
        {
            return new SSTRegistrationTransmissionType();
        }

        public Task<SSTRegistrationTransmissionType> GetTransmissionAsync(string TransmissionId)
        {
            return null;
        }

        public SSTReceiptType AcknowledgeTransmission(SSTPAcknowledgementType acknowledgement)
        {
            return new SSTReceiptType();
        }

        public Task<SSTReceiptType> AcknowledgeTransmissionAsync(SSTPAcknowledgementType acknowledgement)
        {
            return null;
        }
    }
}