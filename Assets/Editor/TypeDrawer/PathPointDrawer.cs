using System;
using UnityEditor;

namespace Entitas.VisualDebugging.Unity.Editor
{
	public class PathPointDrawer : ITypeDrawer
	{
		public bool HandlesType(Type type) => type == typeof(PathPoint);

		public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
		{
			var pathPoint = (PathPoint) value;

			EditorGUILayout.LabelField("id", pathPoint.pathId.ToString());
			pathPoint.index = EditorGUILayout.IntField("index", pathPoint.index);

			return pathPoint;
		}
	}
}
