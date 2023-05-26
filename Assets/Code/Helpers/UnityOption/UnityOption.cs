using System;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using UnityEngine;
using static LanguageExt.Prelude;

[Serializable]
public class UnityOption<T> {
	[SerializeField] bool _isSome;
	[SerializeField] T _value;

	public bool hasValue => _isSome && _value != null;
	public bool isSome => _isSome;
	public Option<T> value => hasValue ? Some(_value) : None;

	protected UnityOption() {
		_isSome = false;
		_value = default;
	}

	UnityOption(bool isSome, Option<T> maybeValue) {
		_isSome = isSome;
		_value = maybeValue.IsSome ? maybeValue.ValueUnsafe() : default;
	}

	public static UnityOption<T> fromOption(Option<T> maybeValue) => new(true, maybeValue);
	public static UnityOption<T> none => new(false, default);
}