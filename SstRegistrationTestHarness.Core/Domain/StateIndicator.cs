using System;
using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class StateIndicator : BulkNewRegistrationIndicatorOperation, IActionCouItem
    {
        public DateTime LastSaleDate { get; set; }
        public string WantRemainRegistered { get; set; }
        public DateTime CspEndDate { get; set; }
        public DateTime CspLastFiling { get; set; }
        public DateTime AccountCloseDate { get; set; }
        public DateTime FirstFilingPeriod { get; set; }
        public DateTime RemoteEndDate { get; set; }

        public StateIndicator(string state, DateTime firstSalesDate, string allowanceIndicator)
            : base(state)
        {
            FirstSalesDate = firstSalesDate;
            AllowanceIndicator = allowanceIndicator;
        }

        public StateIndicator(string state)
            : base(state)
        {
        }

        public object MapActionItem()
        {
            var registrationIndicatorSpecified = Enum.TryParse(Indicator, out BulkRegistrationCOUTypeStateIndicatorsRegistrationIndicator registrationdIndicator);
            var wantRemainRegisteredSpecified = Enum.TryParse(WantRemainRegistered, out BulkRegistrationCOUTypeStateIndicatorsStateAcctInd wantRemainRegistered);
            var allowanceIndicatorSpecified = Enum.TryParse(AllowanceIndicator, out BulkRegistrationCOUTypeStateIndicatorsSSTPAllowanceIndicator allowanceIndicator);
            var remoteSellerIdSpecified = Enum.TryParse(RemoteSellerId, out BulkRegistrationCOUTypeStateIndicatorsRemoteSellerID remoteSellerId);

            return new BulkRegistrationCOUTypeStateIndicators
            {
                State = State,
                RegistrationIndicator = registrationdIndicator,
                RegistrationIndicatorSpecified = registrationIndicatorSpecified,
                RemoteSellerID = remoteSellerId,
                RemoteSellerIDSpecified = remoteSellerIdSpecified,
                FirstSalesDate = FirstSalesDate,
                FirstSalesDateSpecified = FirstSalesDate != DateTime.MinValue,
                LastSaleDate = LastSaleDate,
                LastSaleDateSpecified = LastSaleDate != DateTime.MinValue,
                RemoteEffDate = RemoteEffectiveDate,
                RemoteEffDateSpecified = RemoteEffectiveDate != DateTime.MinValue,
                StateAcctInd = wantRemainRegistered,
                StateAcctIndSpecified = wantRemainRegisteredSpecified,
                CSPEndDate = CspEndDate,
                CSPEndDateSpecified = CspEndDate != DateTime.MinValue,
                CSPLastFilingPd = CspLastFiling != DateTime.MinValue ? CspLastFiling.ToString("yyyy-MM") : null,
                AcctCloseDate = AccountCloseDate,
                AcctCloseDateSpecified = AccountCloseDate != DateTime.MinValue,
                SSTPAllowanceIndicator = allowanceIndicator,
                SSTPAllowanceIndicatorSpecified = allowanceIndicatorSpecified,
                FirstFilingPeriod = FirstFilingPeriod,
                FirstFilingPeriodSpecified = FirstFilingPeriod != DateTime.MinValue,
                RemoteEndDate = RemoteEndDate,
                RemoteEndDateSpecified = RemoteEndDate != DateTime.MinValue
            };
        }
    }
}