#if UNITY_EDITOR
using LanguageExt;
using Rewind.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore
{
	public partial class WalkPath
    {
		public int Length__Editor => points.Count;
		public Option<PointData> at_EDITOR(int i) =>
			pointModels != null ? pointModels.At(i).Map(_ => _.PointData) : points.At(i);
		public void setWorldPosition_EDITOR(int i, Vector2 position) =>
			points[i].localPosition = position - transform.position.XY();
		
		public Vector2 GetWorldPositionOrThrow(int i) =>
			GetMaybeWorldPosition(i).GetOrThrow($"Can't get position for point {i} in path {pathId}");

		[Button]
		private void PositionToPathCenter()
        {
			if (Length__Editor == 0) return;

			Undo.RecordObject(transform, "Position to center");
			
			var positionStart = GetWorldPositionOrThrow(0);
			var positionEnd = GetWorldPositionOrThrow(Length__Editor - 1);

			var center = (positionStart + positionEnd) / 2;
			var offset = transform.position.XY() - center;

			transform.position = center.WithZ(0);

			for (var i = 0; i < Length__Editor; i++)
            {
				points[i].localPosition += offset;
			}
		}
		
		[Button]
		private void DistributePoints()
        {
			if (Length__Editor <= 2) return;

			Undo.RecordObject(transform, "Distribute points");
			var firstPoint = points[0].localPosition;
			var lastPoint = points[Length__Editor - 1].localPosition;
			var step = (lastPoint - firstPoint) / (Length__Editor - 1);

			for (var i = 0; i < Length__Editor; i++) {
				points[i].localPosition = firstPoint + step * i;
			}
		}
		
		[Button]
		private void DistributePoints08()
		{
			if (Length__Editor <= 2) return;

			Undo.RecordObject(transform, "Distribute points 0.8");
			var firstPoint = points[0].localPosition;
			var lastPoint = firstPoint + Vector2.right * (Length__Editor - 1) * 0.8f;
			var step = (lastPoint - firstPoint) / (Length__Editor - 1);

			for (var i = 0; i < Length__Editor; i++) {
				points[i].localPosition = firstPoint + step * i;
			}
		}
	}
}
#endif