#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(PathPointValidator))]
public class PathPointValidator : ValueValidator<PathPoint>
{
	protected override void Validate(ValidationResult result)
	{
		var pathPoint = this.Value;

		if (pathPoint.pathId.isNullOrEmpty())
		{
			result.AddError("Path is not selected");
		}

		Debug.Log($"pathPoint.pathId {pathPoint.pathId}");

		if (pathPoint.index < 0)
		{
			result.AddError("Path index less than 0");
		}
	}
}
#endif
