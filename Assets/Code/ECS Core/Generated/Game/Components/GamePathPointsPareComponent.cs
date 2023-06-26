//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<PathPointsPareComponent> maybePathPointsPare { get { return HasComponent(GameComponentsLookup.PathPointsPare) ? LanguageExt.Option<PathPointsPareComponent>.Some((PathPointsPareComponent)GetComponent(GameComponentsLookup.PathPointsPare)) : LanguageExt.Option<PathPointsPareComponent>.None; } }
    public LanguageExt.Option<PathPointsPare> maybePathPointsPare_value { get { return maybePathPointsPare.Map(_ => _.value); } }
    public PathPointsPareComponent pathPointsPare { get { return (PathPointsPareComponent)GetComponent(GameComponentsLookup.PathPointsPare); } }
    public bool hasPathPointsPare { get { return HasComponent(GameComponentsLookup.PathPointsPare); } }

    public GameEntity AddPathPointsPare(PathPointsPare newValue) {
        var index = GameComponentsLookup.PathPointsPare;
        var component = (PathPointsPareComponent)CreateComponent(index, typeof(PathPointsPareComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePathPointsPare(PathPointsPare newValue) {
        var index = GameComponentsLookup.PathPointsPare;
        var component = (PathPointsPareComponent)CreateComponent(index, typeof(PathPointsPareComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePathPointsPare() {
        RemoveComponent(GameComponentsLookup.PathPointsPare);
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

    static Entitas.IMatcher<GameEntity> _matcherPathPointsPare;

    public static Entitas.IMatcher<GameEntity> PathPointsPare {
        get {
            if (_matcherPathPointsPare == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PathPointsPare);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPathPointsPare = matcher;
            }

            return _matcherPathPointsPare;
        }
    }
}
