using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class LocalPositionComponent : IComponent {
	public Vector2 value;
}