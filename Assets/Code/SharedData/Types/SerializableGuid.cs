using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class SerializableGuid
{
	[SerializeField] private string _guid;

	public Guid Guid
    {
		get => Guid.TryParse(_guid, out var id) ? id : Guid.Empty;
		set => _guid = value.ToString();
	}

	public SerializableGuid(Guid guid) => Guid = guid;

	public static SerializableGuid Create() => new(Guid.NewGuid());

	public bool IsEmpty => string.IsNullOrEmpty(_guid) || Guid.Empty.ToString() == _guid;

	public override bool Equals(object obj)
    {
		if (obj == null || GetType() != obj.GetType())
        {
			return false;
		}

		var pathPointType = (SerializableGuid) obj;
		return Equals(pathPointType);
	}

	public override int GetHashCode() => _guid != null ? _guid.GetHashCode() : 0;

	public bool Equals(SerializableGuid obj) =>
		obj != null && _guid != null && obj._guid != null && _guid.Equals(obj._guid);

	public override string ToString() => _guid;

	public static implicit operator Guid(SerializableGuid serializableGuid) => serializableGuid.Guid;
	public static implicit operator SerializableGuid(Guid guid) => new(guid);

	public static bool operator ==(SerializableGuid lhs, SerializableGuid rhs) => lhs.Equals((object) rhs);
	public static bool operator !=(SerializableGuid lhs, SerializableGuid rhs) => !(lhs == rhs);

	public static bool operator ==(SerializableGuid lhs, Guid rhs) => lhs.Guid.Equals(rhs);
	public static bool operator !=(SerializableGuid lhs, Guid rhs) => !(lhs == rhs);
}

public static class SerializableGuidExt
{
	public static bool IsNullOrEmpty(this SerializableGuid serializableGuid) =>
		serializableGuid == null || serializableGuid.IsEmpty;
}