using LanguageExt;

namespace Code.Base
{
	public interface IStatusValue
    {
		Option<float> StatusValue { get; }
	}
}