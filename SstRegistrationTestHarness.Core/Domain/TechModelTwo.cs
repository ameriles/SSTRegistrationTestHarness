using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TechModelTwo : TechModel
    {
        private readonly string _casCode;

        public TechModelTwo(string casCode)
        {
            _casCode = casCode;
        }

        public override TechModelType MapTechnologyModel()
        {
            return new TechModelType
            {
                ItemElementName = ItemChoiceType2.ModelTwo,
                Item = new ModelTwoType { CASCode = _casCode }
            };
        }
    }
}