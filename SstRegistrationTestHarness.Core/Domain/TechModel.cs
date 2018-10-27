using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public abstract class TechModel : IActionCouItem
    {
        public abstract TechModelType MapTechnologyModel();

        public object MapActionItem()
        {
            return MapTechnologyModel();
        }
    }
}
