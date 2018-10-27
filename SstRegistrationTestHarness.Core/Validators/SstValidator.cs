using System;
using System.Collections.Generic;
using System.Linq;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Validators
{
    public abstract class SstValidator
    {
        public const string NAICS_CODE_ERROR_MSG = "The NICS Code is mandatory.";
        public const string SELLER_PHONE_ERROR_MSG = "The Seller Phone is mandatory.";

        public IList<string> SstStates { get; }
        public IList<string> NonSstStates { get; }
        public BulkRegistrationDocumentType Content { get; private set; }
        protected TinTypeValidator TinValidator { get; private set; }

        protected SstValidator()
        {
            SstStates = new List<string>
            {
                "VT", "MN", "WY", "UT", "MI", "GA", "ND", "OH", "RI", "KY", "NC", "IA",
                "WV", "IN", "KS", "NJ", "OK", "WA", "AR", "SD", "WI", "NE", "NV", "TN"
            };

            var states = Enum.GetNames(typeof(StateType));
            NonSstStates = states.Except(SstStates).ToList();
        }

        public void VerifyContent(BulkRegistrationDocumentType content)
        {
            Content = content;
            TinValidator = CreateTinValidator(content.BulkRegistrationHeader.TIN.TypeTIN);
            LoadErrors();
        }

        protected abstract void LoadErrors();

        private TinTypeValidator CreateTinValidator(TINTypeTypeTIN type)
        {
            switch (type)
            {
                case TINTypeTypeTIN.SSN:
                    return new SsnValidator(Content.ErrorMessages);

                case TINTypeTypeTIN.FEIN:
                    return new FeinValidator(Content.ErrorMessages);

                default:
                    throw new ArgumentOutOfRangeException($"Can not create Tin validator for {type}.");
            }
        }
        
        public void ValidateEmptyString(string value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Content.ErrorMessages.Add(errorMessage);
            }
        }

        public void ValidatePhone(string value, string entityName)
        {
            if (value?.Length != 10)
            {
                Content.ErrorMessages.Add($"The Phone from {entityName} must have a length of 10. Value: {value}.");
            }
        }

        public void ValidateState(string state)
        {
            if (string.IsNullOrEmpty(state) || !Enum.IsDefined(typeof(StateType), state))
            {
                Content.ErrorMessages.Add($"The State '{state}' is not valid.");
            }
        }

        public void ValidateStateIncorporated(string stateIncorporated)
        {
            if (SstStates.All(x => x != stateIncorporated))
            {
                Content.ErrorMessages.Add($"The State '{stateIncorporated}' is not a SST State.");
            }
        }

        public void ValidateAddress(IAddressType address)
        {
            if (address == null)
            {
                Content.ErrorMessages.Add("The Physical Address is mandatory.");
                return;
            }

            if (string.IsNullOrWhiteSpace(address.AddressLine1Txt))
            {
                Content.ErrorMessages.Add($"[{address.TypeName}] The Address 1 is mandatory.");
            }

            if (string.IsNullOrWhiteSpace(address.CityNm))
            {
                Content.ErrorMessages.Add($"[{address.TypeName}] The City is mandatory.");
            }

            if (string.IsNullOrWhiteSpace(address.ZipCode))
            {
                Content.ErrorMessages.Add($"[{address.TypeName}] The Zip Code is mandatory.");
            }

            if (string.IsNullOrWhiteSpace(address.State))
            {
                Content.ErrorMessages.Add($"[{address.TypeName}] The State is mandatory.");
            }
        }

        public void ValidateContact(ContactType contact)
        {
            var individualName = contact.ContactName;

            if (string.IsNullOrWhiteSpace(individualName.FirstName))
            {
                Content.ErrorMessages.Add("The First Name from Contact is mandatory.");
            }

            if (string.IsNullOrWhiteSpace(individualName.LastName))
            {
                Content.ErrorMessages.Add("The Last Name from Contact is mandatory.");
            }

            if (!string.IsNullOrWhiteSpace(individualName.MiddleInitial) && individualName.MiddleInitial.Length != 1)
            {
                Content.ErrorMessages.Add($"The MiddleInitial from Contact should have length 1. Value: {individualName.MiddleInitial}.");
            }

            if (string.IsNullOrWhiteSpace(contact.ContactPhone))
            {
                Content.ErrorMessages.Add("The Contact Phone from Contact is mandatory.");
            }
            else
            {
                ValidatePhone(contact.ContactPhone, "Contact");
            }

            if (string.IsNullOrWhiteSpace(contact.ContactEmail))
            {
                Content.ErrorMessages.Add("The Email from Contact is mandatory.");
            }
        }

        public void ValidateTechnologyModel(TechModelType techModel)
        {
            var validTechnologyModels = new[] { ItemChoiceType2.ModelOne, ItemChoiceType2.ModelTwo };
            if (validTechnologyModels.All(x => techModel.ItemElementName != x))
            {
                Content.ErrorMessages.Add($"The Tech Model {techModel.ItemElementName} is not available in New Registration.");
                return;
            }

            var code = techModel.Item.ToString();
            if (string.IsNullOrEmpty(code))
            {
                Content.ErrorMessages.Add($"The CSP/CAS code for {techModel.ItemElementName} is mandatory.");
            }
        }
    }
}
