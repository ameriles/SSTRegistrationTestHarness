using SstRegistrationTestHarness.Core.Validators;

namespace SstRegistrationTestHarness.Core.SstRegistrationService
{
    public partial class BulkRegistrationCOUTypeBusinessInfo : ICouItemValidable
    {
        public void Validate(BulkRegistrationCOUTypeActionCode actionCode, SstValidator sstValidator)
        {
            if (actionCode != BulkRegistrationCOUTypeActionCode.C)
            {
                sstValidator.Content.ErrorMessages.Add($"The Action Code for Bussiness Info must be C. Value: {actionCode}.");
            }

            if (!string.IsNullOrWhiteSpace(SellerPhone))
            {
                sstValidator.ValidatePhone(SellerPhone, "Business Info");
            }

            if (!string.IsNullOrWhiteSpace(StateIncorporated))
            {
                sstValidator.ValidateStateIncorporated(StateIncorporated);
            }

            if (PhysicalAddress != null)
            {
                sstValidator.ValidateAddress((IAddressType)PhysicalAddress.Item);
            }

            if (MailingAddress != null)
            {
                sstValidator.ValidateAddress((IAddressType)MailingAddress.Item);
            }

            if (SSTPContact != null)
            {
                sstValidator.ValidateContact(SSTPContact);
            }

            //var businessValidator = new BusinessInfoValidator(sstValidator);
            //businessValidator.Validate(this);
        }
    }
}
