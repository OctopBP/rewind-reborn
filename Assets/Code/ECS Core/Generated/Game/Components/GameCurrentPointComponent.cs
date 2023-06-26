//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<CurrentPointComponent> maybeCurrentPoint { get { return HasComponent(GameComponentsLookup.CurrentPoint) ? LanguageExt.Option<CurrentPointComponent>.Some((CurrentPointComponent)GetComponent(GameComponentsLookup.CurrentPoint)) : LanguageExt.Option<CurrentPointComponent>.None; } }
    public LanguageExt.Option<PathPoint> maybeCurrentPoint_value { get { return maybeCurrentPoint.Map(_ => _.value); } }
    public CurrentPointComponent currentPoint { get { return (CurrentPointComponent)GetComponent(GameComponentsLookup.CurrentPoint); } }
    public bool hasCurrentPoint { get { return HasComponent(GameComponentsLookup.CurrentPoint); } }

    public GameEntity AddCurrentPoint(PathPoint newValue) {
        var index = GameComponentsLookup.CurrentPoint;
        var component = (CurrentPointComponent)CreateComponent(index, typeof(CurrentPointComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceCurrentPoint(PathPoint newValue) {
        var index = GameComponentsLookup.CurrentPoint;
        var component = (CurrentPointComponent)CreateComponent(index, typeof(CurrentPointComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveCurrentPoint() {
        RemoveComponent(GameComponentsLookup.CurrentPoint);
        return this;
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCurrentPoint;

    public static Entitas.IMatcher<GameEntity> CurrentPoint {
        get {
            if (_matcherCurrentPoint == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentPoint);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentPoint = matcher;
            }

            return _matcherCurrentPoint;
        }
    }
}
