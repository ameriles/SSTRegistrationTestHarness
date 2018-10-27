using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TechModelNone : TechModel
    {
        public override TechModelType MapTechnologyModel()
        {
            return new TechModelType
            {
                ItemElementName = ItemChoiceType2.None
            };
        }
    }
}