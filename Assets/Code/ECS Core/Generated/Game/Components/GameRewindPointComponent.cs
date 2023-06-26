//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<RewindPointComponent> maybeRewindPoint { get { return HasComponent(GameComponentsLookup.RewindPoint) ? LanguageExt.Option<RewindPointComponent>.Some((RewindPointComponent)GetComponent(GameComponentsLookup.RewindPoint)) : LanguageExt.Option<RewindPointComponent>.None; } }
    public LanguageExt.Option<PathPoint> maybeRewindPoint_value { get { return maybeRewindPoint.Map(_ => _.value); } }
    public RewindPointComponent rewindPoint { get { return (RewindPointComponent)GetComponent(GameComponentsLookup.RewindPoint); } }
    public bool hasRewindPoint { get { return HasComponent(GameComponentsLookup.RewindPoint); } }

    public GameEntity AddRewindPoint(PathPoint newValue) {
        var index = GameComponentsLookup.RewindPoint;
        var component = (RewindPointComponent)CreateComponent(index, typeof(RewindPointComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceRewindPoint(PathPoint newValue) {
        var index = GameComponentsLookup.RewindPoint;
        var component = (RewindPointComponent)CreateComponent(index, typeof(RewindPointComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveRewindPoint() {
        RemoveComponent(GameComponentsLookup.RewindPoint);
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

    static Entitas.IMatcher<GameEntity> _matcherRewindPoint;

    public static Entitas.IMatcher<GameEntity> RewindPoint {
        get {
            if (_matcherRewindPoint == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RewindPoint);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRewindPoint = matcher;
            }

            return _matcherRewindPoint;
        }
    }
}
