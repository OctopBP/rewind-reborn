#if UNITY_EDITOR
using System.Linq;
using Rewind.ECSCore;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;

[assembly: RegisterValidator(typeof(PathPointValidator))]
public class PathPointValidator : ValueValidator<PathPoint>
{
	protected override void Validate(ValidationResult result)
	{
		var pathPoint = Value;

		if (pathPoint.index < 0)
        {
			result.AddError("Path index less than 0");
		}

		if (pathPoint.pathId.IsNullOrEmpty())
        {
			result.AddError("Path is not selected");
		}
		else
        {
			var paths = Object.FindObjectsOfType<WalkPath>().Select(_ => _._pathId);
			if (!paths.Contains(pathPoint.pathId))
            {
				result.AddError("Wrong path selected");
			}
		}
	}
}
#endif
