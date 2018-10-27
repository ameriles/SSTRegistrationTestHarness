using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.Repositories;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Transmitter;

namespace SstRegistrationTestHarness.Core.Submission
{
    public class SimplifiedElectronicReturnManager
    {
        private readonly SstRegistrationTransmitter _transmitter;
        private readonly User _userSubmitter;
        private readonly SstRegistrationTestHarnessSystem _rootSstRegistrationTestHarnessSystem;
        private readonly IGenericRepository<Guid> _genericRepository;

        public SimplifiedElectronicReturnManager(SstRegistrationTransmitter transmitter, User userSubmitter, SstRegistrationTestHarnessSystem rootSstRegistrationTestHarnessSystem, IGenericRepository<Guid> genericRepository)
        {
            _transmitter = transmitter;
            _userSubmitter = userSubmitter;
            _rootSstRegistrationTestHarnessSystem = rootSstRegistrationTestHarnessSystem;
            _genericRepository = genericRepository;
        }

        public SimplifiedReturnResponse SubmitTaxpayerData(IList<Taxpayer> sellers)
        {
            var documents = new List<BulkRegistrationDocumentType>();

            foreach (var taxpayer in sellers)
            {
                var item = taxpayer.CreateOperationRecord();
                item.DocumentId = CreateUniqueId();

                var validator = taxpayer.BulkOperation.Validator;
                validator.VerifyContent(item);

                documents.Add(item);
            }

            var request = CreateRequest(documents);
            var response = _transmitter.BulkRegistration(request);
            CreateTransmissionLog(request, response);

            return response;
        }

        private void CreateTransmissionLog(BulkRegistrationTransmissionType request, SimplifiedReturnResponse response)
        {
            var status = response.DefineTransmittalStatus(request.BulkRegistrationDocument);
            var transmittalLog = new TransmittalLog(_transmitter.TransmittalEnvironment, _userSubmitter, status, response.ContentSent, response.ContentReceived, request.TransmissionHeader.TransmissionId);

            foreach (var document in request.BulkRegistrationDocument)
            {
                var documentStatus = response.DefineDocumentStatus(document.DocumentId);
                var documentAcknowledgement = response.GetDocumentAcknowledgementByDocumentId(document.DocumentId);
                var newLog = new TransmittalLogItem(document.DocumentId, documentStatus)
                {
                    SstpId = documentAcknowledgement?.SSTPID
                };

                if (documentAcknowledgement?.Errors != null)
                {
                    //Log the errors from the service response
                    var messages = new StringBuilder();
                    foreach (var error in documentAcknowledgement.Errors.Error)
                    {
                        messages.AppendLine(error.ToString());
                    }

                    newLog.Errors = messages.ToString();
                }
                else
                {
                    //Log the errors from local validations if we receive an invalid response
                    foreach (var sstError in response.Errors.Where(x => x.DocumentId.Equals(document.DocumentId)))
                    {
                        newLog.Errors += string.Join(", ", sstError.Messages);
                    }
                }

                transmittalLog.LogItems.Add(newLog);
            }

            _rootSstRegistrationTestHarnessSystem.AddTransmittalLog(transmittalLog, _genericRepository);
        }

        private string CreateUniqueId()
        {
            Thread.Sleep(100);
            var ticks = DateTime.UtcNow.Ticks;
            return $"{_transmitter.Contractor.CspCode}{DateTime.UtcNow:yy}{DateTime.Today.DayOfYear.ToString().PadLeft(3, '0')}{ticks.ToString().Substring(12, 6)}";
        }

        private BulkRegistrationTransmissionType CreateRequest(ICollection<BulkRegistrationDocumentType> documents)
        {
            var header = new TransmissionHeaderType
            {
                TransmissionId = CreateUniqueId(),
                Transmitter = new TransmissionHeaderTypeTransmitter
                {
                    ETIN = _transmitter.Contractor.CspCode
                },
                DocumentCount = documents.Count.ToString()
            };

            var request = new BulkRegistrationTransmissionType
            {
                BulkRegistrationDocument = documents.ToArray(),
                TransmissionHeader = header,
                transmissionVersion = _transmitter.TransmittalEnvironment.TransmissionVersion
            };

            return request;
        }
    }
}
