using Code.Base;
using ExhaustiveMatching;
using LanguageExt;
using Rewind.SharedData;

namespace Rewind.Behaviours {
	public partial class ButtonA : IStatusValue {
		public Option<float> statusValue => model.state switch {
			ButtonAState.Closed => 0,
			ButtonAState.Opened => 1,
			_ => throw ExhaustiveMatch.Failed(model.state)
		};
	}
}