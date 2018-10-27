using System;
using System.Collections.Generic;
using System.Linq;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class BulkOperationCou : BulkOperationType
    {
        public string ActionCode { get; set; }
        public IList<IActionCouItem> ActionItems { get; }
        public override BulkRegistrationDocumentTypeDocumentType DocumentType => BulkRegistrationDocumentTypeDocumentType.BulkRegistrationCOU;
        public override BulkRegistrationHeaderTypeFilingType FilingType => BulkRegistrationHeaderTypeFilingType.BulkRegCOU;

        public BulkOperationCou(string actionCode)
        {
            ActionCode = actionCode?.Trim();
            ActionItems = new List<IActionCouItem>();
            Validator = new CouSstValidator();
        }

        public override object CreateBulkRegistrationType(Taxpayer taxpayer)
        {
            var actionsItems = ActionItems.Select(x => x.MapActionItem());

            if (!Enum.TryParse(ActionCode, true, out BulkRegistrationCOUTypeActionCode actionCode))
            {
                throw new ArgumentOutOfRangeException($"Can not parse the value Action Code. Value: {(string.IsNullOrEmpty(ActionCode) ? "Empty" : ActionCode)}.");
            }

            var record = new BulkRegistrationCOUType
            {
                ActionCode = actionCode,
                EffectiveDate = DateTime.UtcNow,
                SSTPID = taxpayer.SstpId,
                Items = actionsItems.ToArray()
            };

            return record;
        }
    }
}