using System;
using System.Globalization;
using System.Linq;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Validators
{
    public class StateIndicatorValidator
    {
        private readonly SstValidator _baseValidator;

        public StateIndicatorValidator(SstValidator baseValidator)
        {
            _baseValidator = baseValidator;
        }

        public void ValidateUpdate(BulkRegistrationCOUTypeStateIndicators stateIndicator)
        {
            if (stateIndicator.RegistrationIndicatorSpecified)
            {
                if (_baseValidator.SstStates.Any(x => x == stateIndicator.State))
                {
                    var validRegistrationIndicator = new[] { BulkRegistrationCOUTypeStateIndicatorsRegistrationIndicator.A, BulkRegistrationCOUTypeStateIndicatorsRegistrationIndicator.R };

                    if (validRegistrationIndicator.All(x => x != stateIndicator.RegistrationIndicator))
                    {
                        _baseValidator.Content.ErrorMessages.Add($"For SST States the Registration Indicator can only be R or A. Value: {stateIndicator.RegistrationIndicator}.");
                    }
                }
                else if (_baseValidator.NonSstStates.Any(x => x == stateIndicator.State))
                {
                    if (stateIndicator.RegistrationIndicator != BulkRegistrationCOUTypeStateIndicatorsRegistrationIndicator.U)
                    {
                        _baseValidator.Content.ErrorMessages.Add("For Non SST States the Registration Indicator must be U.");
                    }
                    else if (!stateIndicator.LastSaleDateSpecified || stateIndicator.LastSaleDate < DateTime.UtcNow.AddDays(-60) || stateIndicator.LastSaleDate > DateTime.UtcNow)
                    {
                        _baseValidator.Content.ErrorMessages.Add("For Registration Indicator U, the Last Sale Date is mandatory and cannot be more than 60 days in the past and no future date is allowed.");
                    }
                }
                else
                {
                    _baseValidator.Content.ErrorMessages.Add($"For State Indicator U The State does not exists. Value: {stateIndicator.State}.");
                }
            }

            if (stateIndicator.FirstSalesDateSpecified && stateIndicator.FirstFilingPeriodSpecified)
            {
                var date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

                //if (stateIndicator.FirstSalesDate.Date != DateTime.UtcNow.Date && stateIndicator.FirstSalesDate.Date != date.Date)
                //{
                //    _baseValidator.ErrorMessages.Add("First Sales Date must be the current date or the first day of the current month.");
                //}

                if (stateIndicator.FirstFilingPeriod.Month != DateTime.UtcNow.Month || stateIndicator.FirstFilingPeriod.Year != DateTime.UtcNow.Year)
                {
                    _baseValidator.Content.ErrorMessages.Add("The First Filing Period must be the current month and year.");
                }
            }
            //TODO seems that this validation is not necessary any more
            //else
            //{
            //    _baseValidator.ErrorMessages.Add("First Sales Date and First Filing Period are mandatory for State Indicator update.");
            //}
            
            if (stateIndicator.StateAcctIndSpecified
                //|| stateIndicator.RemoteSellerIDSpecified
                //|| stateIndicator.RemoteEffDateSpecified
                //|| stateIndicator.RemoteEndDateSpecified
                || stateIndicator.CSPEndDateSpecified
                || !string.IsNullOrEmpty(stateIndicator.CSPLastFilingPd)
                || stateIndicator.AcctCloseDateSpecified)
            {
                _baseValidator.Content.ErrorMessages.Add("State Account Indicator, CSP End Date, CSP Last Filling and Account Clase Date are not allowed for State Indcators Updates.");
            }


            if (stateIndicator.RemoteSellerIDSpecified)
            {
                switch (stateIndicator.RemoteSellerID)
                {
                    case BulkRegistrationCOUTypeStateIndicatorsRemoteSellerID.Y:
                        if (!stateIndicator.RemoteEffDateSpecified)
                        {
                            _baseValidator.Content.ErrorMessages.Add("Remote Effective Date is mandatory when Remote Seller ID is Y.");
                        }
                        break;

                    case BulkRegistrationCOUTypeStateIndicatorsRemoteSellerID.N:
                        if (!stateIndicator.RemoteEndDateSpecified)
                        {
                            _baseValidator.Content.ErrorMessages.Add("Remote End Data is mandatory when Remote Seller ID is N.");
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (!stateIndicator.SSTPAllowanceIndicatorSpecified || stateIndicator.SSTPAllowanceIndicator == BulkRegistrationCOUTypeStateIndicatorsSSTPAllowanceIndicator.U)
            {
                _baseValidator.Content.ErrorMessages.Add($"SSTP Allowance Indicator is mandatory and cannot be U. Value: {(stateIndicator.SSTPAllowanceIndicatorSpecified ? stateIndicator.SSTPAllowanceIndicator.ToString() : "Empty")}.");
            }
        }

        public void ValidateEndManagingRegistration(BulkRegistrationCOUTypeStateIndicators stateIndicator)
        {
            if (stateIndicator.CSPEndDateSpecified && !string.IsNullOrEmpty(stateIndicator.CSPLastFilingPd))
            {
                var lastDayPriorMonth = new DateTime(stateIndicator.CSPEndDate.Year, stateIndicator.CSPEndDate.Month, 1).AddDays(-1);
                if (stateIndicator.CSPEndDate < lastDayPriorMonth || stateIndicator.CSPEndDate > DateTime.UtcNow.AddDays(60))
                {
                    _baseValidator.Content.ErrorMessages.Add("The CSP End Date should always be the last day of month no more that 60 dyas in the future to the current month or the last day of the prior month.");
                }
                else
                {
                    var lastDayCurrentMonth = new DateTime(stateIndicator.CSPEndDate.Year, stateIndicator.CSPEndDate.Month, 1).AddMonths(1).AddDays(-1);
                    var lastDayNextMonth = new DateTime(stateIndicator.CSPEndDate.Year, stateIndicator.CSPEndDate.Month, 1).AddMonths(2).AddDays(-1);
                    if (stateIndicator.CSPEndDate != lastDayPriorMonth && stateIndicator.CSPEndDate != lastDayCurrentMonth && stateIndicator.CSPEndDate != lastDayNextMonth)
                    {
                        _baseValidator.Content.ErrorMessages.Add("The CSP End Date should always be the last day of month no more that 60 dyas in the future to the current month or the last day of the prior month.");
                    }
                }

                DateTime.TryParseExact(stateIndicator.CSPLastFilingPd + "-01", "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var cspLastFilingPeriod);
                if (cspLastFilingPeriod.Year != stateIndicator.CSPEndDate.Year || cspLastFilingPeriod.Month != stateIndicator.CSPEndDate.Month)
                {
                    _baseValidator.Content.ErrorMessages.Add("The CSP End Date month and year must the same for CSP Last Filing Period.");
                }
            }
            else
            {
                _baseValidator.Content.ErrorMessages.Add("The CSP End Date and CSP Last Filing Period are mandatory for end managing registration.");
            }
            
            if (stateIndicator.StateAcctIndSpecified
                || stateIndicator.RemoteSellerIDSpecified
                || stateIndicator.RemoteEffDateSpecified
                || stateIndicator.RemoteEndDateSpecified
                || stateIndicator.RegistrationIndicatorSpecified
                || stateIndicator.LastSaleDateSpecified
                || stateIndicator.AcctCloseDateSpecified
                || stateIndicator.FirstSalesDateSpecified
                || stateIndicator.SSTPAllowanceIndicatorSpecified
                || stateIndicator.FirstFilingPeriodSpecified)
            {
                _baseValidator.Content.ErrorMessages.Add("State Account Indicator, Remote Seller Id, Remote Effective Date, Remote End Date, Registration Indicator, Last Sale Date, Account Close Date, " +
                                                 "First Sales Date, SSTP Allowance Indicator and First Filling Period are not allowed for State Indcators Updates.");
            }
        }

        public void ValidateOutOfBusiness(BulkRegistrationCOUTypeStateIndicators stateIndicator)
        {
            if (stateIndicator.AcctCloseDateSpecified)
            {
                var lastDayPriorMonth= new DateTime(stateIndicator.AcctCloseDate.Year, stateIndicator.AcctCloseDate.Month, 1).AddDays(-1);
                var lastDayCurrentMonth = new DateTime(stateIndicator.AcctCloseDate.Year, stateIndicator.AcctCloseDate.Month, 1).AddMonths(1).AddDays(-1);

                if (stateIndicator.AcctCloseDate != lastDayPriorMonth && stateIndicator.AcctCloseDate != lastDayCurrentMonth)
                {
                    _baseValidator.Content.ErrorMessages.Add("The Account Close Date should be the last day of the current month or the last day of the prior month.");
                }
            }
            else
            {
                _baseValidator.Content.ErrorMessages.Add("The Account Close Date is mandatory for Out Of Business.");
            }

            if (stateIndicator.StateAcctIndSpecified && stateIndicator.StateAcctInd != BulkRegistrationCOUTypeStateIndicatorsStateAcctInd.N)
            {
                _baseValidator.Content.ErrorMessages.Add("If the State Account Indicator is specified, must be N for Out Of Business.");
            }

            if (stateIndicator.RemoteSellerIDSpecified
                || stateIndicator.RemoteEffDateSpecified
                || stateIndicator.RemoteEndDateSpecified
                || stateIndicator.RegistrationIndicatorSpecified
                || stateIndicator.LastSaleDateSpecified
                || stateIndicator.CSPEndDateSpecified
                || !string.IsNullOrEmpty(stateIndicator.CSPLastFilingPd)
                || stateIndicator.FirstSalesDateSpecified
                || stateIndicator.SSTPAllowanceIndicatorSpecified
                || stateIndicator.FirstFilingPeriodSpecified)
            {
                _baseValidator.Content.ErrorMessages.Add("Remote Seller Id, Remote Effective Date, Remote End Date, Registration Indicator, CSP End Date" +
                                                 "CSP Last Filind Period, First Sales Date, SSTP Allowance Indicator and First Filling Period are not allowed for Out Of Business.");
            }
        }

        public void ValidateUnvolunteerUnregister(BulkRegistrationCOUTypeStateIndicators stateIndicator)
        {
            if (stateIndicator.AcctCloseDateSpecified)
            {
                var lastDayPriorMonth = new DateTime(stateIndicator.AcctCloseDate.Year, stateIndicator.AcctCloseDate.Month, 1).AddDays(-1);
                var lastDayCurrentMonth = new DateTime(stateIndicator.AcctCloseDate.Year, stateIndicator.AcctCloseDate.Month, 1).AddMonths(1).AddDays(-1);

                if (stateIndicator.AcctCloseDate != lastDayPriorMonth && stateIndicator.AcctCloseDate != lastDayCurrentMonth)
                {
                    _baseValidator.Content.ErrorMessages.Add("The Account Close Date should be the last day of the current month or the last day of the prior month.");
                }
            }
            else
            {
                _baseValidator.Content.ErrorMessages.Add("The Account Close Date is mandatory for Unvolunteer/Unregister.");
            }
            
            if (stateIndicator.RemoteSellerIDSpecified
                || stateIndicator.RemoteEffDateSpecified
                || stateIndicator.RemoteEndDateSpecified
                || stateIndicator.RegistrationIndicatorSpecified
                || stateIndicator.LastSaleDateSpecified
                || stateIndicator.CSPEndDateSpecified
                || !string.IsNullOrEmpty(stateIndicator.CSPLastFilingPd)
                || stateIndicator.FirstSalesDateSpecified
                || stateIndicator.SSTPAllowanceIndicatorSpecified
                || stateIndicator.FirstFilingPeriodSpecified)
            {
                _baseValidator.Content.ErrorMessages.Add("Remote Seller Id, Remote Effective Date, Remote End Date, Registration Indicator, CSP End Date" +
                                                 "CSP Last Filind Period, First Sales Date, SSTP Allowance Indicator and First Filling Period are not allowed for Unvolunteer/Unregister.");
            }
        }
    }
}