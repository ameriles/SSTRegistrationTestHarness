using System;
using SstRegistrationTestHarness.Core.Exceptions;
using SstRegistrationTestHarness.Core.Repositories;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class Contractor
    {
        public Guid Id { get; protected set; }
        public string CspCode { get; set; }
        //public string CspCode => "CSP000011";
        //public string CasCode => "CAS000011";
        public string CasCode { get; set; }
        public string CompanyName { get; set; }

        protected Contractor()
        {
        }

        public Contractor(string companyName)
        {
            Id = Guid.NewGuid();
            CompanyName = companyName?.Trim();
        }

        public void Validate(IContractorRepository contractorRepository)
        {
            if (string.IsNullOrEmpty(CompanyName))
            {
                throw new EntityModelException(nameof(CompanyName), "The company name is mandatory.");
            }

            CasCode = CasCode?.Trim();
            CspCode = CspCode?.Trim();

            if (string.IsNullOrEmpty(CasCode) && string.IsNullOrEmpty(CspCode))
            {
                throw new EntityModelException($"{nameof(CasCode)}+{nameof(CspCode)}", "The CASCode and the CSPCode can not be both empty.");
            }

            if (!string.IsNullOrEmpty(CasCode) && !string.IsNullOrEmpty(CspCode))
            {
                throw new EntityModelException($"{nameof(CasCode)}+{nameof(CspCode)}", "The CASCode and the CSPCode can not have both values.");
            }

            if (!contractorRepository.IsUnique(this))
            {
                throw new EntityModelException($"{nameof(CompanyName)}", "There is another contractor with the same company name.");
            }
        }
    }
}
