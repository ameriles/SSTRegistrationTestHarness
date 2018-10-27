using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;
using SstRegistrationTestHarness.Core.EntityFramework.Repositories;
using SstRegistrationTestHarness.Core.Repositories;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Submission;
using SstRegistrationTestHarness.Core.Transmitter;

namespace SstRegistrationTestHarness.Core.Test.Transmission
{
    [TestClass]
    public class TransmissionTest
    {
        private SstRegistrationTestHarnessSystem _root;
        private IGenericRepository<Guid> _genericRepository;
        private Contractor _sovosContractor;
        private IList<string> _sstStates;
        private IList<string> _nonSstStates;
        private SimplifiedElectronicReturnManager _manager;
        private UsaAddress _physicalUsaAddress;
        private UsaAddress _mailUsaAddress;
        private ContactSellerInfo _contactSellerInfo;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            EntityFrameworkDbContextHelper.Instance.InitializeDbContextFactory(new TestFluentConfigurer());
            EntityFrameworkDbContextHelper.Instance.Context.Database.Initialize(true);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            EntityFrameworkDbContextHelper.Instance.BeginTransaction();

            _genericRepository = new EntityFrameworkGenericRepository<Guid>();

            _root = new SstRegistrationTestHarnessSystem(Guid.NewGuid());
            _genericRepository.Save(_root);

            var adminUser = new User("admin", "Sovos1234!");
            _root.AddUser(adminUser, _genericRepository, new UserRepository());

            _sovosContractor = new Contractor("Sovos") { CspCode = "CSP000011" };
            _root.AddContractor(_sovosContractor, _genericRepository, new ContractorRepository());

            _sstStates = new List<string>
            {
                "VT", "MN", "WY", "UT", "MI", "GA", "ND", "OH", "RI", "KY", "NC", "IA",
                "WV", "IN", "KS", "NJ", "OK", "WA", "AR", "SD", "WI", "NE", "NV", "TN"
            };

            var states = Enum.GetNames(typeof(StateType));
            _nonSstStates = states.Except(_sstStates).ToList();

            _physicalUsaAddress = new UsaAddress(StateType.AA.ToString(), "01887", "5th Avenue", "Wilmington");
            _mailUsaAddress = new UsaAddress(StateType.WY.ToString(), "10023", "6th Avenue", "New York");
            _contactSellerInfo = new ContactSellerInfo("Federico", "Navarro", "1234567890", "federico.navarro@sovos.com");

            var mockEnvironment = new TransmittalEnvironment("Mock service", "dummy-endpoint", "sst", "Sovos1234!", "SST2015V01", ETransmissionMode.OnlyValidate);
            var transmitter = new SstRegistrationTransmitter(_sovosContractor, mockEnvironment);

            _manager = new SimplifiedElectronicReturnManager(transmitter, adminUser, _root, new EntityFrameworkGenericRepository<Guid>());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _genericRepository.Delete(_root);
            EntityFrameworkDbContextHelper.Instance.RollbackTransaction();
        }

        [TestMethod]
        public void ThrowEndPointNotFoundExceptionWithValidBulkregistrationRequest_ShouldLogFailedTransmission()
        {
            var techModel = new TechModelOne(_sovosContractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.LL.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }
            var seller = new FeinTaxpayer(_sovosContractor, bulkOperation, "12345678", "Walmart", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var user = new User("regulatory-analisys", "Sovos1234!");
            _root.AddUser(user, _genericRepository, new UserRepository());

            var mockEnvironment = new TransmittalEnvironment("Mock service", "dummy-endpoint", "mock", "Sovos1234!", "v1", ETransmissionMode.OnlyValidate)
            {
                SetupMockParameters = new ServiceMockParameters { ThrowEndpointNotFoundException = true }
            };
            var transmitter = new SstRegistrationTransmitter(_sovosContractor, mockEnvironment);
            var manager = new SimplifiedElectronicReturnManager(transmitter, user, _root, _genericRepository);
            
            var response = manager.SubmitTaxpayerData(new List<Taxpayer> { seller });
            Assert.IsTrue(response.HasErrors);
            Assert.IsNotNull(response.ContentSent);
            Assert.IsNull(response.ContentReceived);
            Assert.IsNotNull(response.OriginalResponse);

            var logs = _genericRepository.ListAll<TransmittalLog>();
            Assert.AreEqual(1, _genericRepository.Count<TransmittalLog>());

            var log = logs.ElementAt(0);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.TransmissionStatus);
            Assert.IsNotNull(log.ContentSent);

            foreach (var item in log.LogItems)
            {
                Assert.AreEqual(ETransmittalStatus.Rejected, item.DocumentStatus);
                Assert.AreEqual("Endpoint not found exception.", item.Errors);
            }
        }

        [TestMethod]
        public void SubmitValidNewBulkRegistrationRequest_ShouldLogSuccessfulyTransmission()
        {
            var techModel = new TechModelOne(_sovosContractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.LL.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new FeinTaxpayer(_sovosContractor, bulkOperation, "12345678", "Walmart", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var seller2 = new FeinTaxpayer(_sovosContractor, bulkOperation, "12345678", "Sears", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var seller3 = new FeinTaxpayer(_sovosContractor, bulkOperation, "12345678", "Starbucks", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var response = _manager.SubmitTaxpayerData(new List<Taxpayer> { seller1, seller2, seller3 });

            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.ContentSent);
            Assert.IsNotNull(response.ContentReceived);
            Assert.IsNotNull(response.OriginalResponse);

            var logs = _genericRepository.ListAll<TransmittalLog>();
            Assert.AreEqual(1, logs.Count<TransmittalLog>());

            var log = logs.ElementAt(0);
            Assert.AreEqual(response.ContentSent, log.ContentSent);
            Assert.AreEqual(response.ContentReceived, log.ContentReceived);
            Assert.AreEqual(ETransmittalStatus.Approved, log.TransmissionStatus);
            Assert.AreEqual(response.OriginalResponse.TransmissionAcknowledgement.TransmissionId, log.TransmissionId);
            Assert.AreEqual("admin", log.Submitter.Username);
            Assert.AreEqual(ETransmissionMode.OnlyValidate, log.TransmissionMode);
            Assert.AreEqual("dummy-endpoint", log.EndpointUrl);

            Assert.AreEqual(3, log.LogItems.Count);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].DocumentId, log.LogItems.ElementAt(0).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Approved, log.LogItems.ElementAt(0).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].SSTPID, log.LogItems.ElementAt(0).SstpId);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].Errors, log.LogItems.ElementAt(0).Errors);
            Assert.IsNull(log.LogItems.ElementAt(0).Errors);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].DocumentId, log.LogItems.ElementAt(1).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Approved, log.LogItems.ElementAt(1).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].SSTPID, log.LogItems.ElementAt(1).SstpId);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].Errors, log.LogItems.ElementAt(1).Errors);
            Assert.IsNull(log.LogItems.ElementAt(1).Errors);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].DocumentId, log.LogItems.ElementAt(2).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Approved, log.LogItems.ElementAt(2).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].SSTPID, log.LogItems.ElementAt(2).SstpId);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].Errors, log.LogItems.ElementAt(2).Errors);
            Assert.IsNull(log.LogItems.ElementAt(2).Errors);

            var seller4 = new SsnTaxPayer(_sovosContractor, bulkOperation, "87654321", "Lionel", "Messi", StateType.FL.ToString(), "0987654321", "Sovos1234");
            var seller5 = new SsnTaxPayer(_sovosContractor, bulkOperation, "87654321", "Roger", "Federer", StateType.FL.ToString(), "0987654321", "Sovos1234");
            response = _manager.SubmitTaxpayerData(new List<Taxpayer> { seller4, seller5 });

            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.ContentSent);
            Assert.IsNotNull(response.ContentReceived);
            Assert.IsNotNull(response.OriginalResponse);

            logs = _genericRepository.ListAll<TransmittalLog>();
            Assert.AreEqual(2, _genericRepository.Count<TransmittalLog>());

            log = logs.ElementAt(1);
            Assert.AreEqual(response.ContentSent, log.ContentSent);
            Assert.AreEqual(response.ContentReceived, log.ContentReceived);
            Assert.AreEqual(ETransmittalStatus.Approved, log.TransmissionStatus);
            Assert.AreEqual(response.OriginalResponse.TransmissionAcknowledgement.TransmissionId, log.TransmissionId);
            Assert.AreEqual("admin", log.Submitter.Username);
            Assert.AreEqual(ETransmissionMode.OnlyValidate, log.TransmissionMode);
            Assert.AreEqual("dummy-endpoint", log.EndpointUrl);

            Assert.AreEqual(2, log.LogItems.Count);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].DocumentId, log.LogItems.ElementAt(0).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Approved, log.LogItems.ElementAt(0).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].SSTPID, log.LogItems.ElementAt(0).SstpId);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].Errors, log.LogItems.ElementAt(0).Errors);
            Assert.IsNull(log.LogItems.ElementAt(0).Errors);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].DocumentId, log.LogItems.ElementAt(1).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Approved, log.LogItems.ElementAt(1).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].SSTPID, log.LogItems.ElementAt(1).SstpId);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].Errors, log.LogItems.ElementAt(1).Errors);
            Assert.IsNull(log.LogItems.ElementAt(1).Errors);
        }

        [TestMethod]
        public void Submit1ValidRequestAnd2InvalidRequestForNewRegistration_ShouldLogApproveWithErrorTransmission()
        {
            var techModel = new TechModelOne(_sovosContractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.LL.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel);
            foreach (var state in _sstStates)
            {
                bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }
            var validSeller = new FeinTaxpayer(_sovosContractor, bulkOperation1, "12345678", "Walmart", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var bulkOperation2 = new BulkOperationNew(EntityType.LL.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel);
            foreach (var state in _sstStates)
            {
                bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.MinValue, "Y", "Y"));
            }
            var invalidSeller1 = new FeinTaxpayer(_sovosContractor, bulkOperation2, "12345678", string.Empty, StateType.TN.ToString(), "1234567890", string.Empty);

            var bulkOperation3 = new BulkOperationNew(EntityType.LL.ToString(), "123456", null, null, null);
            foreach (var state in _sstStates)
            {
                bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.MinValue, "Y", "Y"));
            }
            var invalidSeller2 = new FeinTaxpayer(_sovosContractor, bulkOperation3, "12345678", "Starbucks", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var response = _manager.SubmitTaxpayerData(new List<Taxpayer> { validSeller, invalidSeller1, invalidSeller2 });

            Assert.IsTrue(response.HasErrors);
            Assert.IsNotNull(response.ContentSent);
            Assert.IsNotNull(response.ContentReceived);
            Assert.IsNotNull(response.OriginalResponse);

            var logs = _genericRepository.ListAll<TransmittalLog>();
            Assert.AreEqual(1, _genericRepository.Count<TransmittalLog>());

            var log = logs.ElementAt(0);
            Assert.AreEqual(response.ContentSent, log.ContentSent);
            Assert.AreEqual(response.ContentReceived, log.ContentReceived);
            Assert.AreEqual(ETransmittalStatus.ApprovedWithErrors, log.TransmissionStatus);
            Assert.AreEqual(response.OriginalResponse.TransmissionAcknowledgement.TransmissionId, log.TransmissionId);
            Assert.AreEqual("admin", log.Submitter.Username);
            Assert.AreEqual(ETransmissionMode.OnlyValidate, log.TransmissionMode);
            Assert.AreEqual("dummy-endpoint", log.EndpointUrl);

            Assert.AreEqual(3, log.LogItems.Count);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].DocumentId, log.LogItems.ElementAt(0).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Approved, log.LogItems.ElementAt(0).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].SSTPID, log.LogItems.ElementAt(0).SstpId);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].Errors, log.LogItems.ElementAt(0).Errors);
            Assert.IsNull(log.LogItems.ElementAt(0).Errors);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].DocumentId, log.LogItems.ElementAt(1).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.LogItems.ElementAt(1).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].SSTPID, log.LogItems.ElementAt(1).SstpId);
            //Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].Errors, log.LogItems.ElementAt(1).Errors);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].DocumentId, log.LogItems.ElementAt(2).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.LogItems.ElementAt(2).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].SSTPID, log.LogItems.ElementAt(2).SstpId);
        }

        [TestMethod]
        public void SubmitAllInvalidRequestForNewRegistration_ShouldLogRejectedWithErrorTransmission()
        {
            var techModel = new TechModelOne(_sovosContractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.LL.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel);
            foreach (var state in _nonSstStates)
            {
                bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "N", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.MinValue });
            }
            var invalidSeller1 = new FeinTaxpayer(_sovosContractor, bulkOperation1, "12345678", "Walmart", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var bulkOperation2 = new BulkOperationNew(EntityType.LL.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel);
            foreach (var state in _sstStates)
            {
                bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.MinValue, "Y", "Y"));
            }
            var invalidSeller2 = new FeinTaxpayer(_sovosContractor, bulkOperation2, "12345678", string.Empty, StateType.TN.ToString(), "1234567890", string.Empty);

            var bulkOperation3 = new BulkOperationNew(EntityType.LL.ToString(), "123456", null, null, null);
            foreach (var state in _sstStates)
            {
                bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.MinValue, "Y", "Y"));
            }
            var invalidSeller3 = new FeinTaxpayer(_sovosContractor, bulkOperation3, "12345678", "Starbucks", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var response = _manager.SubmitTaxpayerData(new List<Taxpayer> { invalidSeller1, invalidSeller2, invalidSeller3 });

            Assert.IsTrue(response.HasErrors);
            Assert.IsNotNull(response.ContentSent);
            Assert.IsNotNull(response.ContentReceived);
            Assert.IsNotNull(response.OriginalResponse);

            var logs = _genericRepository.ListAll<TransmittalLog>();
            Assert.AreEqual(1, _genericRepository.Count<TransmittalLog>());

            var log = logs.ElementAt(0);
            Assert.AreEqual(response.ContentSent, log.ContentSent);
            Assert.AreEqual(response.ContentReceived, log.ContentReceived);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.TransmissionStatus);
            Assert.AreEqual(response.OriginalResponse.TransmissionAcknowledgement.TransmissionId, log.TransmissionId);
            Assert.AreEqual("admin", log.Submitter.Username);
            Assert.AreEqual(ETransmissionMode.OnlyValidate, log.TransmissionMode);
            Assert.AreEqual("dummy-endpoint", log.EndpointUrl);

            Assert.AreEqual(3, log.LogItems.Count);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].DocumentId, log.LogItems.ElementAt(0).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.LogItems.ElementAt(0).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[0].SSTPID, log.LogItems.ElementAt(0).SstpId);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].DocumentId, log.LogItems.ElementAt(1).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.LogItems.ElementAt(1).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[1].SSTPID, log.LogItems.ElementAt(1).SstpId);

            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].DocumentId, log.LogItems.ElementAt(2).DocumentId);
            Assert.AreEqual(ETransmittalStatus.Rejected, log.LogItems.ElementAt(2).DocumentStatus);
            Assert.AreEqual(response.OriginalResponse.BulkRegAcknowledgement[2].SSTPID, log.LogItems.ElementAt(2).SstpId);
        }
    }
}
