using System;
using System.Globalization;
using System.Linq;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Validators
{
    public class NewSstValidator : SstValidator
    {
        protected override void LoadErrors()
        {
            ValidateTaxPayerIdentificationNumber(Content.BulkRegistrationHeader.TIN);
            
            var bulkRegistrationNew = (BulkRegistrationNewType)Content.Item;
            TinValidator.ValidateTaxPayerName(bulkRegistrationNew.Item);

            if (string.IsNullOrWhiteSpace(bulkRegistrationNew.StateIncorporated))
            {
                Content.ErrorMessages.Add("The State Incorporated is mandatory.");
            }
            else
            {
                ValidateState(bulkRegistrationNew.StateIncorporated);
            }

            ValidateEmptyString(bulkRegistrationNew.NAICSCode, NAICS_CODE_ERROR_MSG);
            
            ValidateAddress((IAddressType)bulkRegistrationNew.PhysicalAddress?.Item);

            if (bulkRegistrationNew.MailingAddress != null)
            {
                ValidateAddress((IAddressType)bulkRegistrationNew.MailingAddress?.Item);
            }

            if (bulkRegistrationNew.SSTPContact == null)
            {
                Content.ErrorMessages.Add("The Contact Seller Info is mandatory.");
            }
            else
            {
                ValidateContact(bulkRegistrationNew.SSTPContact);
            }

            if (bulkRegistrationNew.TechnologyModel == null)
            {
                Content.ErrorMessages.Add("The Technology Model is mandatory.");
            }
            else
            {
                ValidateTechnologyModel(bulkRegistrationNew.TechnologyModel);
            }

            ValidateEmptyString(bulkRegistrationNew.SellerPhone, SELLER_PHONE_ERROR_MSG);
            ValidatePhone(bulkRegistrationNew.SellerPhone, "Taxpayer");
            
            if (!bulkRegistrationNew.StateRegistrationIndicator.Any())
            {
                Content.ErrorMessages.Add("The State Registration Indicator must have at least one item.");
            }

            var firstFilingPeriod = DateTime.ParseExact(bulkRegistrationNew.FirstFilingPeriod, "yyyy-MM", CultureInfo.InvariantCulture);
            foreach (var registrationIndicator in bulkRegistrationNew.StateRegistrationIndicator)
            {
                ValidateRegistrationIndicator(registrationIndicator, firstFilingPeriod);
            }
            
            //if (!Regex.IsMatch(bulkRegistrationNew.NewPass, @"^(?=.*[\d])(?=.*[\D])([\w]{8,10}\\+)$"))
            //{
            //    ErrorMessages.Add("The New Password must be a minimun of 8 characters, maximun of 10 characters and must contain at least 1 alphacharacter and at least 1 number.");
            //}
        }
        
        private void ValidateRegistrationIndicator(BulkRegistrationNewTypeStateRegistrationIndicator registrationIndicator, DateTime firstFilingPeriod)
        {
            if (firstFilingPeriod.Month != registrationIndicator.FirstSalesDate.Month || firstFilingPeriod.Year != registrationIndicator.FirstSalesDate.Year)
            {
                Content.ErrorMessages.Add($"First filling period {firstFilingPeriod:MM/yyyy} must be the same year and month as first sales date {registrationIndicator.FirstSalesDate:MM/yyyy}.");
            }

            if (registrationIndicator.FirstSalesDate.Date < DateTime.UtcNow.Date || registrationIndicator.FirstSalesDate.Date > DateTime.UtcNow.Date.AddDays(60))
            {
                Content.ErrorMessages.Add($"The First Sales Date must be the current date or no more than 60 days in the future. Value: {(registrationIndicator.FirstSalesDate != DateTime.MinValue ? registrationIndicator.FirstSalesDate.ToString("MM/dd/yyyy") : "Empty")}.");
            }

            if (SstStates.Any(x => x == registrationIndicator.State))
            {
                var validRegistrationIndicator = new[] { BulkRegistrationNewTypeStateRegistrationIndicatorRegistrationIndicator.R, BulkRegistrationNewTypeStateRegistrationIndicatorRegistrationIndicator.A };

                if (validRegistrationIndicator.All(x => x != registrationIndicator.RegistrationIndicator))
                {
                    Content.ErrorMessages.Add($"For SST States the Registration Indicator can only be R or A. Value: {registrationIndicator.RegistrationIndicator}.");
                }
            }
            else if (NonSstStates.Any(x => x == registrationIndicator.State))
            {
                if (registrationIndicator.RegistrationIndicator == BulkRegistrationNewTypeStateRegistrationIndicatorRegistrationIndicator.N)
                {
                    Content.ErrorMessages.Add("For Non SST States the Registration Indicator can not be N.");
                }
            }
            else
            {
                Content.ErrorMessages.Add($"The State does not exists. Value: {registrationIndicator.State}.");
            }

            if (!registrationIndicator.SSTPAllowanceIndicatorSpecified)
            {
                Content.ErrorMessages.Add("The Allowance Indicator is mandatory.");
            }
        }

        private void ValidateTaxPayerIdentificationNumber(TINType tinType)
        {
            foreach (var invalidPrefixDigit in TinValidator.InvalidPrefixes)
            {
                if (tinType.FedTIN.StartsWith(invalidPrefixDigit))
                {
                    Content.ErrorMessages.Add($"{TinValidator.InvalidPrefixesMessage} {string.Join(", ", invalidPrefixDigit)}.");
                }
            }

            CheckWithReservedCodes(tinType.FedTIN);
        }

        private void CheckWithReservedCodes(string code)
        {
            var invalidCodes = new[] { "123456789", "111111111", "222222222", "333333333", "444444444", "555555555", "666666666", "777777777", "888888888", "999999999", "000000000", "987654321" };

            foreach (var invalidCode in invalidCodes)
            {
                if (code.StartsWith(invalidCode))
                {
                    Content.ErrorMessages.Add($"Do not allow SSN or FEIN to equal {string.Join(", ", invalidCode)}.");
                }
            }
        }
    }
}