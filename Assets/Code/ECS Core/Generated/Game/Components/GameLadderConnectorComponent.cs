//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<LadderConnectorComponent> maybeLadderConnector { get { return HasComponent(GameComponentsLookup.LadderConnector) ? LanguageExt.Option<LadderConnectorComponent>.Some((LadderConnectorComponent)GetComponent(GameComponentsLookup.LadderConnector)) : LanguageExt.Option<LadderConnectorComponent>.None; } }
    public LadderConnectorComponent ladderConnector { get { return (LadderConnectorComponent)GetComponent(GameComponentsLookup.LadderConnector); } }
    public bool hasLadderConnector { get { return HasComponent(GameComponentsLookup.LadderConnector); } }

    public GameEntity AddLadderConnector(PathPoint newValue) {
        var index = GameComponentsLookup.LadderConnector;
        var component = (LadderConnectorComponent)CreateComponent(index, typeof(LadderConnectorComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceLadderConnector(PathPoint newValue) {
        var index = GameComponentsLookup.LadderConnector;
        var component = (LadderConnectorComponent)CreateComponent(index, typeof(LadderConnectorComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveLadderConnector() {
        RemoveComponent(GameComponentsLookup.LadderConnector);
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

    static Entitas.IMatcher<GameEntity> _matcherLadderConnector;

    public static Entitas.IMatcher<GameEntity> LadderConnector {
        get {
            if (_matcherLadderConnector == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LadderConnector);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLadderConnector = matcher;
            }

            return _matcherLadderConnector;
        }
    }
}
