using System;
using LanguageExt;
using Sirenix.OdinInspector;
using UnityEngine;
using static LanguageExt.Prelude;

[Serializable]
public partial class MaybePathPoint {
	[SerializeField, PublicAccessor] bool isSome;
	[SerializeField, ShowIf(nameof(isSome))] PathPoint pathPoint;

	public Option<PathPoint> optValue => isSome ? pathPoint : None;
	public static MaybePathPoint none() => new MaybePathPoint();
}

[Serializable]
public struct PathPoint {
	public SerializableGuid pathId;
	public int index;

	public PathPoint(SerializableGuid pathId, int index) {
		this.pathId = pathId;
		this.index = index;
	}

	public PathPoint pathWithIndex(int index) => new PathPoint(pathId, index);

	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) {
			return false;
		}

		var pathPointType = (PathPoint) obj;
		return Equals(pathPointType);
	}

	public bool Equals(PathPoint other) => pathId.Equals(other.pathId) && index == other.index;

	public override string ToString() => $"{index} ({pathId})";

	public static bool operator ==(PathPoint lhs, PathPoint rhs) => lhs.Equals(rhs);
	public static bool operator !=(PathPoint lhs, PathPoint rhs) => !(lhs == rhs);
}