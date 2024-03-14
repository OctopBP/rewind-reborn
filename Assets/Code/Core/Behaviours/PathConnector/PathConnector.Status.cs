using Code.Base;
using ExhaustiveMatching;
using LanguageExt;
using Rewind.SharedData;

namespace Rewind.Behaviours
{
	public partial class PathConnector : IStatusValue
	{
		public Option<float> StatusValue => model.state.Value switch
		{
			ConnectorState.Closed => 0,
			ConnectorState.Opened => 1,
			_ => throw ExhaustiveMatch.Failed(model.state.Value)
		};
	}
}