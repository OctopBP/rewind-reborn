using System;
using UnityEngine;

[Serializable]
public class SerializableGuid {
	[SerializeField] string _guid;

	public Guid guid {
		get => Guid.TryParse(_guid, out var id) ? id : Guid.Empty;
		set => _guid = value.ToString();
	}

	public SerializableGuid(Guid guid) => this.guid = guid;

	public static SerializableGuid create() => new(Guid.NewGuid());

	public bool isEmpty => string.IsNullOrEmpty(_guid) || Guid.Empty.ToString() == _guid;

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

public static class SerializableGuidExt {
	public static bool isNullOrEmpty(this SerializableGuid serializableGuid) =>
		serializableGuid == null || serializableGuid.isEmpty;
}