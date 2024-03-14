using Code.Base;
using ExhaustiveMatching;
using LanguageExt;
using Rewind.SharedData;

namespace Rewind.Behaviours
{
	public partial class PlatformA : IStatusValue
	{
		public Option<float> StatusValue => model.entity.platformAState.value switch
		{
			PlatformAState.Active => 1,
			PlatformAState.NotActive => 0,
			_ => throw ExhaustiveMatch.Failed(model.entity.platformAState.value)
		};
	}
}