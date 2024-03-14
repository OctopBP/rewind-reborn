using System;
using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using UnityEngine;
using static LanguageExt.Prelude;

[Serializable]
public class UnityOption<T>
{
	[SerializeField] private bool _isSome;
	[SerializeField] private T _value;

	public bool HasValue => _isSome && _value != null;
	public bool IsSome => _isSome;
	public Option<T> Value => HasValue ? Some(_value) : Prelude.None;

	protected UnityOption()
    {
		_isSome = false;
		_value = default;
	}

	private UnityOption(bool isSome, Option<T> maybeValue)
    {
		_isSome = isSome;
		_value = maybeValue.IsSome ? maybeValue.ValueUnsafe() : default;
	}

	public static UnityOption<T> FromOption(Option<T> maybeValue) => new(true, maybeValue);
	public static UnityOption<T> None => new(false, default);
}