using System;
using SstRegistrationTestHarness.Core.SstRegistrationService;
using SstRegistrationTestHarness.Core.Utilities;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class BulkNewRegistrationIndicatorOperation
    {
        public string State { get; }
        public string Indicator { get; }
        public DateTime FirstSalesDate { get; set; }
        public DateTime RemoteEffectiveDate { get; set; }
        public string AllowanceIndicator { get; set; }
        public string RemoteSellerId { get; set; }

        public BulkNewRegistrationIndicatorOperation(string state, string indicator, DateTime firstSalesDate, string allowanceIndicator, string remoteSellerId)
        {
            State = state;
            Indicator = indicator;
            FirstSalesDate = firstSalesDate;
            AllowanceIndicator = allowanceIndicator;
            RemoteSellerId = remoteSellerId;
        }

        protected BulkNewRegistrationIndicatorOperation(string state)
        {
            State = state;
        }

        public BulkRegistrationNewTypeStateRegistrationIndicator MapStateRegistrationIndicator()
        {
            var allowanceIndicator = Enum.TryParse(AllowanceIndicator, out BulkRegistrationNewTypeStateRegistrationIndicatorSSTPAllowanceIndicator enumValue);

            return new BulkRegistrationNewTypeStateRegistrationIndicator
            {
                State = State,
                RegistrationIndicator = EnumHelper.GetEnumItemFromValueName<BulkRegistrationNewTypeStateRegistrationIndicatorRegistrationIndicator>(Indicator),
                RemoteSellerID = EnumHelper.GetEnumItemFromValueName<BulkRegistrationNewTypeStateRegistrationIndicatorRemoteSellerID>(RemoteSellerId),
                FirstSalesDate = FirstSalesDate,
                RemoteEffDate = RemoteEffectiveDate,
                RemoteEffDateSpecified = RemoteEffectiveDate != DateTime.MinValue,
                SSTPAllowanceIndicator = enumValue,
                SSTPAllowanceIndicatorSpecified = allowanceIndicator
            };
        }
    }
}