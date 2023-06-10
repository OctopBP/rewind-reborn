#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(SerializableGuidValidator))]
public class SerializableGuidValidator : ValueValidator<SerializableGuid>  {
	protected override void Validate(ValidationResult result) {
		var serializableGuid = Value;

		if (serializableGuid.isEmpty) {
			result
				.AddError("SerializableGuid is empty")
				.WithFix("New id", () => serializableGuid = SerializableGuid.create());
		}
	}
}
#endif
