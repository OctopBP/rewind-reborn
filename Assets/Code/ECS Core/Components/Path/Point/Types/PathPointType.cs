using System;

[Serializable]
public struct PathPointType {
	public SerializableGuid pathId;
	public int index;

	public PathPointType(SerializableGuid pathId, int index) {
		this.pathId = pathId;
		this.index = index;
	}

	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) {
			return false;
		}

		var pathPointType = (PathPointType) obj;
		return Equals(pathPointType);
	}

	public bool Equals(PathPointType other) => pathId.Equals(other.pathId) && index == other.index;

	public override string ToString() => $"{pathId}.{index}";

	public static bool operator ==(PathPointType lhs, PathPointType rhs) => lhs.Equals(rhs);
	public static bool operator !=(PathPointType lhs, PathPointType rhs) => !(lhs == rhs);
}