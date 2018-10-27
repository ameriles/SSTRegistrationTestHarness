using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SstRegistrationTestHarness.Core.Domain;
using SstRegistrationTestHarness.Core.EntityFramework.Persistence;
using SstRegistrationTestHarness.Core.EntityFramework.Repositories;
using SstRegistrationTestHarness.Core.Exceptions;
using SstRegistrationTestHarness.Core.Repositories;

namespace SstRegistrationTestHarness.Core.Test.Domain
{
    [TestClass]
    public class ContractorTest
    {
        private SstRegistrationTestHarnessSystem _root;
        private IGenericRepository<Guid> _genericRepository;
        private IContractorRepository _contractorRepository;

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

            _contractorRepository = new ContractorRepository();
            _genericRepository = new EntityFrameworkGenericRepository<Guid>();

            _root = new SstRegistrationTestHarnessSystem(Guid.NewGuid());
            _genericRepository.Save(_root);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _genericRepository.Delete(_root);
            EntityFrameworkDbContextHelper.Instance.RollbackTransaction();
        }

        [TestMethod]
        public void CreateContractorWithValidData_ShouldPersist()
        {
            var contractor = new Contractor("Sovos") { CspCode = "CSP000011" };
            _root.AddContractor(contractor, _genericRepository, _contractorRepository);

            var persistedContractor = _genericRepository.GetById<Contractor>(contractor.Id);
            Assert.AreEqual("Sovos", persistedContractor.CompanyName);
        }

        [TestMethod]
        public void CreateContractorWithoutCompanyName_ShouldThrowException()
        {
            var contractor1 = new Contractor(null) { CspCode = "CSP000011" };
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor1, _genericRepository, _contractorRepository), "The company name is mandatory.");
            Assert.AreEqual(nameof(Contractor.CompanyName), e1.PropertyName);

            var contractor2 = new Contractor("") { CspCode = "CSP000011" };
            var e2 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor2, _genericRepository, _contractorRepository), "The company name is mandatory.");
            Assert.AreEqual(nameof(Contractor.CompanyName), e2.PropertyName);

            var contractor3 = new Contractor("       ") { CspCode = "CSP000011" };
            var e3 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor3, _genericRepository, _contractorRepository), "The company name is mandatory.");
            Assert.AreEqual(nameof(Contractor.CompanyName), e3.PropertyName);
        }
        
        [TestMethod]
        public void CreateContractorWithoutCspCodeAndCasCode_ShouldThrowException()
        {
            var contractor1 = new Contractor("Sovos");
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor1, _genericRepository, _contractorRepository), "The CASCode and the CSPCode can not be both empty.");
            Assert.AreEqual($"{nameof(Contractor.CasCode)}+{nameof(Contractor.CspCode)}", e1.PropertyName);

            var contractor2 = new Contractor("Sovos") { CasCode = "", CspCode = "" };
            var e2 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor2, _genericRepository, _contractorRepository), "The CASCode and the CSPCode can not be both empty.");
            Assert.AreEqual($"{nameof(Contractor.CasCode)}+{nameof(Contractor.CspCode)}", e2.PropertyName);

            var contractor3 = new Contractor("Sovos") { CasCode = "    ", CspCode = "    " };
            var e3 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor3, _genericRepository, _contractorRepository), "The CASCode and the CSPCode can not be both empty.");
            Assert.AreEqual($"{nameof(Contractor.CasCode)}+{nameof(Contractor.CspCode)}", e3.PropertyName);
        }

        [TestMethod]
        public void CreateContractorWithBothCspCodeAndCasCode_ShouldThrowException()
        {
            var contractor = new Contractor("Sovos") { CasCode = "CAS000011", CspCode = "CSP000011" };
            var e = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor, _genericRepository, _contractorRepository), "The CASCode and the CSPCode can not have both values.");
            Assert.AreEqual($"{nameof(Contractor.CasCode)}+{nameof(Contractor.CspCode)}", e.PropertyName);
        }

        [TestMethod]
        public void CreateContractorWithSameUniqueCriteria_ShouldThrowException()
        {
            var contractor1 = new Contractor("Sovos") { CspCode = "CSP000011" };
            _root.AddContractor(contractor1, _genericRepository, _contractorRepository);

            var contractor2 = new Contractor("Sovos") { CspCode = "CSP000011" };
            var e1 = Assert.ThrowsException<EntityModelException>(() => _root.AddContractor(contractor2, _genericRepository, _contractorRepository), "There is another contractor with the same company name.");
            Assert.AreEqual($"{nameof(Contractor.CompanyName)}", e1.PropertyName);

            var contractor3 = new Contractor("Sovos1") { CspCode = "CSP000011" };
            _root.AddContractor(contractor3, _genericRepository, _contractorRepository);
            
            var contractor4 = _genericRepository.GetById<Contractor>(contractor3.Id);
            contractor4.CompanyName = "Sovos";
            var e2 = Assert.ThrowsException<EntityModelException>(() => contractor4.Validate(_contractorRepository), "There is another contractor with the same company name.");
            Assert.AreEqual($"{nameof(Contractor.CompanyName)}", e2.PropertyName);
        }
    }
}

