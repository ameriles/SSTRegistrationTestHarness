using SstRegistrationTestHarness.Core.SstRegistrationService;

namespace SstRegistrationTestHarness.Core.Domain
{
    public class TechModelOne : TechModel
    {
        private readonly string _cspCode;

        public TechModelOne(string cspCode)
        {
            _cspCode = cspCode;
        }

        public override TechModelType MapTechnologyModel()
        {
            return new TechModelType
            {
                ItemElementName = ItemChoiceType2.ModelOne,
                Item = new ModelOneType { CSPCode = _cspCode }
            };
        }
    }
}