using Code.Base;
using ExhaustiveMatching;
using LanguageExt;
using Rewind.SharedData;

namespace Rewind.Behaviours
{
	public partial class DoorA : IStatusValue
	{
		public Option<float> StatusValue => model.entity.doorAState.value switch
		{
			DoorAState.Opened => 1,
			DoorAState.Closed => 0,
			_ => throw ExhaustiveMatch.Failed(model.entity.doorAState.value)
		};
	}
}