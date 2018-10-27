using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.EntityFramework.Repositories;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Submission;
using SstRegistrationTestHarness.Core.Transmitter;
using SstRegistrationTestHarness.Core.Utilities;

namespace SstRegistrationTestHarness.Core.Test.Transmission
{
    [TestClass]
    public class BulkOperationNewTest
    {
        private static Contractor _contractor;
        private static IList<string> _sstStates;
        private static IList<string> _nonSstStates;

        private TransmittalEnvironment _mockEnvironment;
        private SstRegistrationTestHarnessSystem _root;
        private SimplifiedElectronicReturnManager _manager;
        private UsaAddress _physicalUsaAddress;
        private UsaAddress _mailUsaAddress;
        private ContactSellerInfo _contactSellerInfo;


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _contractor = new Contractor("SOVOS") { CspCode = "CSP000011" };

            _sstStates = new List<string>
            {
                "VT", "MN", "WY", "UT", "MI", "GA", "ND", "OH", "RI", "KY", "NC", "IA",
                "WV", "IN", "KS", "NJ", "OK", "WA", "AR", "SD", "WI", "NE", "NV", "TN"
            };

            var states = Enum.GetNames(typeof(StateType));
            _nonSstStates = states.Except(_sstStates).ToList();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _physicalUsaAddress = new UsaAddress(StateType.AA.ToString(), "01887", "5th Avenue", "Wilmington");
            _mailUsaAddress = new UsaAddress(StateType.WY.ToString(), "10023", "6th Avenue", "New York");
            _contactSellerInfo = new ContactSellerInfo("Federico", "Navarro", "1234567890", "federico.navarro@sovos.com");

            _mockEnvironment = new TransmittalEnvironment("Mock service", "mock", "sst", "Sovos1234!", "SST2015V01", ETransmissionMode.OnlyValidate);
            _root = new SstRegistrationTestHarnessSystem(Guid.NewGuid());
            var transmitter = new SstRegistrationTransmitter(_contractor, _mockEnvironment);

            var userSubmitter = new User("admin", "Sovos1234!");
            _manager = new SimplifiedElectronicReturnManager(transmitter, userSubmitter, _root, new EntityFrameworkGenericRepository<Guid>());
        }
        
        [TestMethod]
        public void RegisterNewSsnTaxPayerWithInvalidFirstName_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", null, "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "     ", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The First Name is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewSsnTaxPayerWithInvalidLastName_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", null, StateType.TN.ToString(), "1234567890", "Sovos1234");
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "       ", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Last Name is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewSsnTaxPayerWithInvalidMiddleInitialName_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234") { MiddleName = "F" };
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234") { MiddleName = "" };
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234") { MiddleName = "    " };
            var seller4 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234") { MiddleName = "FE" };

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Middle Initial length must be one character. Value: FE.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidBussinesName_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", null, StateType.TN.ToString(), "1234567890", "Sovos1234!");
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "", StateType.TN.ToString(), "1234567890", "Sovos1234!");
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "     ", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.Count);

            var message = string.Join(",", response.Errors.SelectMany(x => x.Messages));
            StringAssert.Contains(message, "The Business Name is mandatory.");
        }

        [TestMethod]
        public void RegisterNewSstTaxPayerWithInvalidStateIncorporated_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", null, "1234567890", "Sovos1234");
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", "", "1234567890", "Sovos1234");
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", "              ", "1234567890", "Sovos1234");
            var seller4 = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", "invalid-state", "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(4, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The State Incorporated is mandatory.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The State 'invalid-state' is not valid.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidStateIncorporated_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller1 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "Fedex", null, "1234567890", "Sovos1234");
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "Fedex", "", "1234567890", "Sovos1234");
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "Fedex", "              ", "1234567890", "Sovos1234");
            var seller4 = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "Fedex", "invalid-state", "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(4, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The State Incorporated is mandatory.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The State 'invalid-state' is not valid.")));
        }

        [TestMethod]
        public void RegisterNewSstTaxPayerWithInvalidNaicsCode_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });

            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "", _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });

            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), "        ", _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });

            var seller1 = new SsnTaxPayer(_contractor, bulkOperation1, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation2, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation3, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The NICS Code is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidNaicsCode_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });

            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "", _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });

            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), "        ", _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });

            var seller1 = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation2, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation3, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The NICS Code is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewSsnTaxPayerWithInvalidPhysicalAddress_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);

            var usaAddress1 = new UsaAddress(_sstStates[0], null, null, null);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), null, usaAddress1, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new SsnTaxPayer(_contractor, bulkOperation1, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress2 = new UsaAddress(_sstStates[0], "", "", "");
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), null, usaAddress2, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation2, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress3 = new UsaAddress(_sstStates[0], "     ", "     ", "     ");
            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), null, usaAddress3, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation3, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress1 = new ForeignAddress(CountryType.AR.ToString(), null, null, null, null);
            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), null, foreignAddress1, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new SsnTaxPayer(_contractor, bulkOperation4, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress2 = new ForeignAddress(CountryType.AR.ToString(), "", "", "", "");
            var bulkOperation5 = new BulkOperationNew(EntityType.SP.ToString(), null, foreignAddress2, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation5.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller5 = new SsnTaxPayer(_contractor, bulkOperation5, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress3 = new ForeignAddress(CountryType.AR.ToString(), "     ", "     ", "     ", "     ");
            var bulkOperation6 = new BulkOperationNew(EntityType.SP.ToString(), null, foreignAddress3, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation6.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller6 = new SsnTaxPayer(_contractor, bulkOperation6, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4, seller5, seller6 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(6, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Zip Code is mandatory."))); ;
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Zip Code is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The State is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidPhysicalAddress_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);

            var usaAddress1 = new UsaAddress(_sstStates[0], null, null, null);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), null, usaAddress1, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress2 = new UsaAddress(_sstStates[0], "", "", "");
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), null, usaAddress2, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation2, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress3 = new UsaAddress(_sstStates[0], "     ", "     ", "     ");
            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), null, usaAddress3, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation3, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress1 = new ForeignAddress(CountryType.AR.ToString(), null, null, null, null);
            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), null, foreignAddress1, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new FeinTaxpayer(_contractor, bulkOperation4, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress2 = new ForeignAddress(CountryType.AR.ToString(), "", "", "", "");
            var bulkOperation5 = new BulkOperationNew(EntityType.SP.ToString(), null, foreignAddress2, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation5.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller5 = new FeinTaxpayer(_contractor, bulkOperation5, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress3 = new ForeignAddress(CountryType.AR.ToString(), "     ", "     ", "     ", "     ");
            var bulkOperation6 = new BulkOperationNew(EntityType.SP.ToString(), null, foreignAddress3, _contactSellerInfo, techModel) { MailingAddress = _mailUsaAddress };
            bulkOperation6.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller6 = new FeinTaxpayer(_contractor, bulkOperation6, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4, seller5, seller6 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(6, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Zip Code is mandatory."))); ;
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Zip Code is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The State is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewSsnTaxpayerWithInvalidMailingAddress_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);

            var usaAddress1 = new UsaAddress(_sstStates[0], null, null, null);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = usaAddress1 };
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new SsnTaxPayer(_contractor, bulkOperation1, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress2 = new UsaAddress(_sstStates[0], "", "", "");
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = usaAddress2 };
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation2, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress3 = new UsaAddress(_sstStates[0], "     ", "     ", "     ");
            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = usaAddress3 };
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation3, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress1 = new ForeignAddress(CountryType.AR.ToString(), null, null, null, null);
            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = foreignAddress1 };
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new SsnTaxPayer(_contractor, bulkOperation4, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress2 = new ForeignAddress(CountryType.AR.ToString(), "", "", "", "");
            var bulkOperation5 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = foreignAddress2 };
            bulkOperation5.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller5 = new SsnTaxPayer(_contractor, bulkOperation5, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress3 = new ForeignAddress(CountryType.AR.ToString(), "     ", "     ", "     ", "     ");
            var bulkOperation6 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = foreignAddress3 };
            bulkOperation6.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller6 = new SsnTaxPayer(_contractor, bulkOperation6, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4, seller5, seller6 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(6, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Zip Code is mandatory."))); ;
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Zip Code is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The State is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidMailingAddress_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);

            var usaAddress1 = new UsaAddress(_sstStates[0], null, null, null);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = usaAddress1 };
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress2 = new UsaAddress(_sstStates[0], "", "", "");
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = usaAddress2 };
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation2, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var usaAddress3 = new UsaAddress(_sstStates[0], "     ", "     ", "     ");
            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = usaAddress3 };
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation3, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress1 = new ForeignAddress(CountryType.AR.ToString(), null, null, null, null);
            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = foreignAddress1 };
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new FeinTaxpayer(_contractor, bulkOperation4, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress2 = new ForeignAddress(CountryType.AR.ToString(), "", "", "", "");
            var bulkOperation5 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = foreignAddress2 };
            bulkOperation5.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller5 = new FeinTaxpayer(_contractor, bulkOperation5, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var foreignAddress3 = new ForeignAddress(CountryType.AR.ToString(), "     ", "     ", "     ", "     ");
            var bulkOperation6 = new BulkOperationNew(EntityType.SP.ToString(), null, _physicalUsaAddress, _contactSellerInfo, techModel) { MailingAddress = foreignAddress3 };
            bulkOperation6.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller6 = new FeinTaxpayer(_contractor, bulkOperation6, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4, seller5, seller6 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(6, response.Errors.Count);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Address 1 is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The City is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Usa Address] The Zip Code is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The Zip Code is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("[Foreign Address] The State is mandatory.")));
        }

        [TestMethod]
        public void RegisterNewSsnTaxPayerWithInvalidContactSellerInfo_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);

            var contactSellerInfo1 = new ContactSellerInfo(null, null, null, null);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo1, techModel);
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo2 = new ContactSellerInfo("", "", "", "");
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo2, techModel);
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation2, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo3 = new ContactSellerInfo("     ", "     ", "     ", "     ");
            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo3, techModel);
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation3, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo4 = new ContactSellerInfo("John", "Doe", "123456789", "john.doe@sovos.com");
            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo4, techModel);
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new FeinTaxpayer(_contractor, bulkOperation4, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo5 = new ContactSellerInfo("John", "Doe", "12345678901", "john.doe@sovos.com");
            var bulkOperation5 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo5, techModel);
            bulkOperation5.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller5 = new FeinTaxpayer(_contractor, bulkOperation5, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo6 = new ContactSellerInfo("John", "Doe", "1234567890", "john.doe@sovos.com") { InitialName = "ab" };
            var bulkOperation6 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo6, techModel);
            bulkOperation6.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller6 = new FeinTaxpayer(_contractor, bulkOperation6, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4, seller5, seller6 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The First Name from Contact is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Last Name from Contact is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Contact Phone from Contact is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Email from Contact is mandatory.")));

            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Phone from Contact must have a length of 10. Value: 123456789.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Phone from Contact must have a length of 10. Value: 12345678901.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The MiddleInitial from Contact should have length 1. Value: ab.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidContactName_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);

            var contactSellerInfo1 = new ContactSellerInfo(null, null, null, null);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo1, techModel);
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new SsnTaxPayer(_contractor, bulkOperation1, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo2 = new ContactSellerInfo("", "", "", "");
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo2, techModel);
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation2, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo3 = new ContactSellerInfo("     ", "     ", "     ", "     ");
            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo3, techModel);
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new SsnTaxPayer(_contractor, bulkOperation3, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo4 = new ContactSellerInfo("John", "Doe", "123456789", "john.doe@sovos.com");
            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo4, techModel);
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new SsnTaxPayer(_contractor, bulkOperation4, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo5 = new ContactSellerInfo("John", "Doe", "12345678901", "john.doe@sovos.com");
            var bulkOperation5 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo5, techModel);
            bulkOperation5.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller5 = new SsnTaxPayer(_contractor, bulkOperation5, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var contactSellerInfo6 = new ContactSellerInfo("John", "Doe", "1234567890", "john.doe@sovos.com") { InitialName = "ab" };
            var bulkOperation6 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, contactSellerInfo6, techModel);
            bulkOperation6.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller6 = new SsnTaxPayer(_contractor, bulkOperation6, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4, seller5, seller6 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The First Name from Contact is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Contact Phone from Contact is mandatory.")));
            Assert.AreEqual(3, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Email from Contact is mandatory.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Phone from Contact must have a length of 10. Value: 123456789.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Phone from Contact must have a length of 10. Value: 12345678901.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The MiddleInitial from Contact should have length 1. Value: ab.")));
        }

        [TestMethod]
        public void RegisterNewSsnTaxPayerWithInvalidTechModel_ShouldReturnErrors()
        {
            var techModelThree = new TechModelThree();
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModelThree);
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new SsnTaxPayer(_contractor, bulkOperation1, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var techModelNone = new TechModelNone();
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModelNone);
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new SsnTaxPayer(_contractor, bulkOperation2, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains($"The Tech Model {ItemChoiceType2.ModelThree} is not available in New Registration.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains($"The Tech Model {ItemChoiceType2.None} is not available in New Registration.")));
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithInvalidTechModel_ShouldReturnErrors()
        {
            var techModelThree = new TechModelThree();
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModelThree);
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var techModelNone = new TechModelNone();
            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModelNone);
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation2, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains($"The Tech Model {ItemChoiceType2.ModelThree} is not available in New Registration.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains($"The Tech Model {ItemChoiceType2.None} is not available in New Registration.")));
        }

        [TestMethod]
        public void RegisterNewSstTaxpayerWithInvalidSellerPhone_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller1 = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), sellerPhone: null, password: "Sovos1234");

            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            bulkOperation2.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller2 = new FeinTaxpayer(_contractor, bulkOperation2, "12345678", "Fedex", StateType.TN.ToString(), sellerPhone: "", password: "Sovos1234");

            var bulkOperation3 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            bulkOperation3.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller3 = new FeinTaxpayer(_contractor, bulkOperation3, "12345678", "Fedex", StateType.TN.ToString(), sellerPhone: "123456789", password: "Sovos1234");

            var bulkOperation4 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            bulkOperation4.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var seller4 = new FeinTaxpayer(_contractor, bulkOperation4, "12345678", "Fedex", StateType.TN.ToString(), sellerPhone: "12345678901", password: "Sovos1234");

            var sellers = new List<Taxpayer> { seller1, seller2, seller3, seller4 };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(2, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The Seller Phone is mandatory.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Phone from Taxpayer must have a length of 10. Value: 123456789.")));
            Assert.IsTrue(response.Errors.SelectMany(x => x.Messages).Any(x => x.Contains("The Phone from Taxpayer must have a length of 10. Value: 12345678901.")));
        }

        [TestMethod]
        public void RegisterNewTaxPayerWithEmptyStateRegistrationIndicator_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            var feinSeller = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var bulkOperation2 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            var ssnSeller = new SsnTaxPayer(_contractor, bulkOperation2, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { feinSeller, ssnSeller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(2, response.Errors.SelectMany(x => x.Messages).Count(x => x.Contains("The State Registration Indicator must have at least one item.")));
        }

        [Ignore("TODO complete UT with documentation")]
        [TestMethod]
        public void RegisterNewTaxPayerWithInvalidStateIndicator_ShouldReturnErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation1 = new BulkOperationNew(EntityType.SP.ToString(), "12345", _physicalUsaAddress, _contactSellerInfo, techModel);
            bulkOperation1.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(_sstStates[0], "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            var feinSeller = new FeinTaxpayer(_contractor, bulkOperation1, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithAllValidValues_ShouldSubmitWithoutErrors()
        {
            var technModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, technModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.AreEqual(0, response.Errors.Count);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.ContentReceived);

            var contentSent = XmlHelper.Deserialize<BulkRegistrationTransmissionType>(response.ContentSent);
            Assert.AreEqual(_mockEnvironment.TransmissionVersion, contentSent.transmissionVersion);

            var header = contentSent.TransmissionHeader;
            Assert.IsNotNull(header);
            Assert.IsTrue(header.TransmissionId.Contains(_contractor.CspCode));
            Assert.IsNotNull(header.Transmitter);
            Assert.AreEqual(_contractor.CspCode, header.Transmitter.ETIN);
            Assert.AreEqual(1, int.Parse(header.DocumentCount));
            Assert.IsTrue(contentSent.BulkRegistrationDocument.Any());
            Assert.AreEqual(TINTypeTypeTIN.FEIN, contentSent.BulkRegistrationDocument[0].BulkRegistrationHeader.TIN.TypeTIN);
        }

        [TestMethod]
        public void RegisterNewSsnTaxPayerWithAllValidValues_ShouldSubmitWithoutErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };
            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", "Y") { RemoteEffectiveDate = DateTime.UtcNow });
            }

            var seller = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.AreEqual(0, response.Errors.Count);
            Assert.IsFalse(response.Errors.Any());
            Assert.IsNotNull(response.ContentReceived);

            var contentSent = XmlHelper.Deserialize<BulkRegistrationTransmissionType>(response.ContentSent);
            Assert.AreEqual(_mockEnvironment.TransmissionVersion, contentSent.transmissionVersion);

            var header = contentSent.TransmissionHeader;
            Assert.IsNotNull(header);
            Assert.IsTrue(header.TransmissionId.Contains(_contractor.CspCode));
            Assert.IsNotNull(header.Transmitter);
            Assert.AreEqual(_contractor.CspCode, header.Transmitter.ETIN);
            Assert.AreEqual(1, int.Parse(header.DocumentCount));
            Assert.IsTrue(contentSent.BulkRegistrationDocument.Any());
            Assert.AreEqual(TINTypeTypeTIN.SSN, contentSent.BulkRegistrationDocument[0].BulkRegistrationHeader.TIN.TypeTIN);
        }

        [TestMethod]
        public void RegisterNewTaxPayerWithRemoteSellerIdAndRemoteEffectiveDate_ShouldSubmitWithoutErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", remoteSellerId: "Y") { RemoteEffectiveDate = DateTime.UtcNow.AddDays(-1) });
            }

            var seller = new FeinTaxpayer(_contractor, bulkOperation, "12345678", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234!");

            var sellers = new List<Taxpayer> { seller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.ContentReceived);

            var contentSent = XmlHelper.Deserialize<BulkRegistrationTransmissionType>(response.ContentSent);
            var bulkRegistrationType = (BulkRegistrationNewType)contentSent.BulkRegistrationDocument[0].Item;
            Assert.AreEqual(BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID.Y, bulkRegistrationType.StateRegistrationIndicator[0].RemoteSellerID);
            Assert.AreEqual(DateTime.UtcNow.Date.AddDays(-1), bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDate);
            Assert.IsTrue(bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDateSpecified);
        }

        [TestMethod]
        public void RegisterNewTaxPayerWithRemoteSellerIdAndWithoutRemoteEffectiveDate_ShouldSubmitWithoutErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };
            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", remoteSellerId: "Y"));
            }

            var seller = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.ContentReceived);

            var contentSent = XmlHelper.Deserialize<BulkRegistrationTransmissionType>(response.ContentSent);
            var bulkRegistrationType = (BulkRegistrationNewType)contentSent.BulkRegistrationDocument[0].Item;
            Assert.AreEqual(BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID.Y, bulkRegistrationType.StateRegistrationIndicator[0].RemoteSellerID);
            Assert.AreEqual(DateTime.MinValue, bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDate);
            Assert.IsFalse(bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDateSpecified);
        }

        [TestMethod]
        public void RegisterNewSstTaxPayerWithoutRemoteSellerIdAndWithoutRemoteEffectiveDate_ShouldSubmitWithoutErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel);

            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", remoteSellerId: "N"));
            }

            var seller = new SsnTaxPayer(_contractor, bulkOperation, "12345678", "Federico", "Navarro", StateType.TN.ToString(), "1234567890", "Sovos1234");
            var sellers = new List<Taxpayer> { seller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.ContentReceived);

            var contentSent = XmlHelper.Deserialize<BulkRegistrationTransmissionType>(response.ContentSent);
            var header = contentSent.BulkRegistrationDocument[0].BulkRegistrationHeader;
            Assert.AreEqual(TINTypeTypeTIN.SSN, header.TIN.TypeTIN);
            Assert.AreEqual("12345678", header.TIN.FedTIN);
            var bulkRegistrationType = (BulkRegistrationNewType)contentSent.BulkRegistrationDocument[0].Item;
            Assert.AreEqual(BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID.N, bulkRegistrationType.StateRegistrationIndicator[0].RemoteSellerID);
            Assert.AreEqual(DateTime.MinValue, bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDate);
            Assert.IsFalse(bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDateSpecified);
        }

        [TestMethod]
        public void RegisterNewFeinTaxPayerWithoutRemoteSellerIdAndWithoutRemoteEffectiveDate_ShouldSubmitWithoutErrors()
        {
            var techModel = new TechModelOne(_contractor.CspCode);
            var bulkOperation = new BulkOperationNew(EntityType.SP.ToString(), "123456", _physicalUsaAddress, _contactSellerInfo, techModel)
            {
                MailingAddress = _mailUsaAddress
            };
            
            foreach (var state in _sstStates)
            {
                bulkOperation.RegistrationIndicatorItems.Add(new BulkNewRegistrationIndicatorOperation(state, "R", DateTime.UtcNow, "Y", remoteSellerId: "N"));
            }

            var seller = new FeinTaxpayer(_contractor, bulkOperation, "100072099", "Fedex", StateType.TN.ToString(), "1234567890", "Sovos1234");

            var sellers = new List<Taxpayer> { seller };

            var response = _manager.SubmitTaxpayerData(sellers);

            Assert.AreEqual(0, response.Errors.Count);
            Assert.IsFalse(response.Errors.Any());
            Assert.IsNotNull(response.ContentReceived);

            var contentSent = XmlHelper.Deserialize<BulkRegistrationTransmissionType>(response.ContentSent);
            var header = contentSent.BulkRegistrationDocument[0].BulkRegistrationHeader;
            Assert.AreEqual(TINTypeTypeTIN.FEIN, header.TIN.TypeTIN);
            Assert.AreEqual(seller.FederalEmployerIdentificationNumber, header.TIN.FedTIN);
            var bulkRegistrationType = (BulkRegistrationNewType)contentSent.BulkRegistrationDocument[0].Item;
            Assert.AreEqual(BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID.N, bulkRegistrationType.StateRegistrationIndicator[0].RemoteSellerID);
            Assert.AreEqual(DateTime.MinValue, bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDate);
            Assert.IsFalse(bulkRegistrationType.StateRegistrationIndicator[0].RemoteEffDateSpecified);
        }
    }
}
