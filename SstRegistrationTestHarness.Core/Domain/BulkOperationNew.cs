using System;
using System.Collections.Generic;
using System.Linq;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Utilities;
using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class BulkOperationNew : BulkOperationType
    {
        public string NaicsCode { get; set; }
        public Address PhysicalAddress { get; set; }
        public Address MailingAddress { get; set; }
        public ContactSellerInfo ContactInfo { get; set; }
        public TechModel TechnologyModel { get; set; }
        public string EntityType { get; }
        public IList<BulkNewRegistrationIndicatorOperation> RegistrationIndicatorItems { get; }
        public override BulkRegistrationDocumentTypeDocumentType DocumentType => BulkRegistrationDocumentTypeDocumentType.BulkRegistrationNew;
        public override BulkRegistrationHeaderTypeFilingType FilingType => BulkRegistrationHeaderTypeFilingType.BulkRegNew;

        public BulkOperationNew(string entityType, string naicsCode, Address physicalAddress, ContactSellerInfo contactInfo, TechModel technologyModel)
        {
            NaicsCode = naicsCode;
            PhysicalAddress = physicalAddress;
            ContactInfo = contactInfo;
            EntityType = entityType;
            TechnologyModel = technologyModel;
            RegistrationIndicatorItems = new List<BulkNewRegistrationIndicatorOperation>();
            Validator = new NewSstValidator();
        }

        public override object CreateBulkRegistrationType(Taxpayer taxpayer)
        {
            var items = RegistrationIndicatorItems.Select(x => x.MapStateRegistrationIndicator());

            var physicalAddress = PhysicalAddress?.MapAddressType();
            var mailingAddress = MailingAddress?.MapAddressType();
            var contactInfo = ContactInfo?.MapContactInformation();
            var technologyModel = TechnologyModel?.MapTechnologyModel();

            var record = new BulkRegistrationNewType
            {
                ActionCode = BulkRegistrationNewTypeActionCode.N,
                RegistrationEntity = EnumHelper.GetEnumItemFromValueName<EntityType>(EntityType),
                NAICSCode = NaicsCode,
                PhysicalAddress = physicalAddress,
                MailingAddress = mailingAddress ?? physicalAddress,
                SSTPContact = contactInfo,
                TechnologyModel = technologyModel,
                Item = taxpayer.MapTaxpayerName(),
                SellerPhone = taxpayer.SellerPhone,
                SellerPhoneExt = taxpayer.SellerPhoneExtension,
                StateIncorporated = taxpayer.StateIncorporated,
                EffectiveDate = DateTime.UtcNow,
                FirstFilingPeriod = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow:MM}",
                NewPass = taxpayer.Password,
                StateRegistrationIndicator = items.ToArray(),
                DBAName = taxpayer.DoingBusinessName
            };

            return record;
        }
    }
}