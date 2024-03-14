using Code.Base;
using ExhaustiveMatching;
using LanguageExt;
using Rewind.SharedData;

namespace Rewind.Behaviours
{
	public partial class GearTypeA : IStatusValue
    {
		public Option<float> StatusValue => model.entity.gearTypeAState.value switch
        {
			GearTypeAState.Closed => 0,
			GearTypeAState.Closing => 0.4f,
			GearTypeAState.Opening => 0.6f,
			GearTypeAState.Opened => 1,
			_ => throw ExhaustiveMatch.Failed(model.entity.gearTypeAState.value)
		};
	}
}