#if UNITY_EDITOR
using Rewind.Behaviours;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(PuzzleGroupValidator))]
public class PuzzleGroupValidator : ValueValidator<PuzzleGroup>  {
	protected override void Validate(ValidationResult result) {
		var puzzleGroup = Value;

		if (puzzleGroup._conditionGroup.isEmpty) {
			result.AddError("Puzzle Group conditions is empty");
		}

		if (puzzleGroup._puzzleValueReceivers.isEmpty()) {
			result.AddError("Puzzle Group value receivers is empty");
		}
	}
}
#endif
