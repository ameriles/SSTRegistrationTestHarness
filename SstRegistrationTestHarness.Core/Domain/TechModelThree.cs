using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TechModelThree : TechModel
    {
        public override TechModelType MapTechnologyModel()
        {
            return new TechModelType
            {
                ItemElementName = ItemChoiceType2.ModelThree
            };
        }
    }
}