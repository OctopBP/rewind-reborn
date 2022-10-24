using System;
using UnityEditor;

namespace Entitas.VisualDebugging.Unity.Editor
{
	public class PathPointTypeDrawer : ITypeDrawer
	{
		public bool HandlesType(Type type) => type == typeof(PathPointType);

		public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {
			var pathPoint = (PathPointType) value;

			EditorGUILayout.LabelField("id", pathPoint.pathId.ToString());
			pathPoint.index = EditorGUILayout.IntField("index", pathPoint.index);

			return pathPoint;
		}
	}
}
