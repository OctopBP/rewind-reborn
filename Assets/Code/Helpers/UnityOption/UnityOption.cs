using System;
using LanguageExt;
using UnityEngine;

[Serializable]
public class UnityOption<T> {
	[SerializeField] Option<Option<T>> maybeValue;

	public bool isSome => maybeValue.IsSome;
	public Option<T> value => maybeValue.Flatten();

	public UnityOption(Option<Option<T>> maybeValue) {
		this.maybeValue = maybeValue;
	}
}