using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class LocalPositionComponent : ValueComponent<Vector2> { }