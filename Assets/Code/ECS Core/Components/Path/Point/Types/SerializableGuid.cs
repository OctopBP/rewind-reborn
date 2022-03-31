using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SerializableGuid {
	[SerializeField] string _guid;

	public Guid guid {
		get {
			if (Guid.TryParse(_guid, out var id)) {
				return id;
			} else {
				var newId = Guid.NewGuid();
				_guid = newId.ToString();
				return newId;
			}
		}
		set => _guid = value.ToString();
	}

	public SerializableGuid(Guid guid) => this.guid = guid;

	public bool empty => String.IsNullOrEmpty(_guid) || Guid.Empty.ToString() == _guid;

	public Color randomColor() {
		var hash = guid.GetHashCode();
		Random.InitState(hash);
		return Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
	}
	
	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) {
			return false;
		}

		var pathPointType = (SerializableGuid) obj;
		return Equals(pathPointType);
	}

	public bool Equals(SerializableGuid obj) => obj != null && _guid.Equals(obj._guid);

	public override string ToString() => _guid;

	public static implicit operator Guid(SerializableGuid serializableGuid) => serializableGuid.guid;
	public static implicit operator SerializableGuid(Guid guid) => new(guid);

	public static bool operator ==(SerializableGuid lhs, SerializableGuid rhs) => lhs.Equals((object) rhs);
	public static bool operator !=(SerializableGuid lhs, SerializableGuid rhs) => !(lhs == rhs);

	public static bool operator ==(SerializableGuid lhs, Guid rhs) => lhs.guid.Equals(rhs);
	public static bool operator !=(SerializableGuid lhs, Guid rhs) => !(lhs == rhs);
}