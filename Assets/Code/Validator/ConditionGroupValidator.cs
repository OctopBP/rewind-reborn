#if UNITY_EDITOR
using Rewind.LogicBuilder;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(ConditionGroupValidator))]
public class ConditionGroupValidator : ValueValidator<ConditionGroup>
{
	protected override void Validate(ValidationResult result)
	{
		var conditionGroup = Value;
		if (conditionGroup.isEmpty)
		{
			result.AddWarning("ConditionGroup is empty");
		}
	}
}
#endif
