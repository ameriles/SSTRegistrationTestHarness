using System.Linq;
using System.Text.RegularExpressions;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Validators
{
    public class CouSstValidator : SstValidator
    {
        protected override void LoadErrors()
        {
            var bulkRegistrationCou = (BulkRegistrationCOUType)Content.Item;
            
            if (string.IsNullOrWhiteSpace(bulkRegistrationCou.SSTPID) || !Regex.IsMatch(bulkRegistrationCou.SSTPID, @"^S\d{8}$"))
            {
                Content.ErrorMessages.Add($"The SSTPID must start with an S followed by 8 digits. Value: {(string.IsNullOrWhiteSpace(bulkRegistrationCou.SSTPID) ? "Empty" : bulkRegistrationCou.SSTPID)}.");
            }

            foreach (var item in bulkRegistrationCou.Items.Cast<ICouItemValidable>())
            {
                item.Validate(bulkRegistrationCou.ActionCode, this);
            }
        }
    }
}