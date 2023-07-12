using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class PositionComponent : ValueComponent<Vector2> { }