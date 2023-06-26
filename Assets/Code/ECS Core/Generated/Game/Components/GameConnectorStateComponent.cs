//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<ConnectorStateComponent> maybeConnectorState { get { return HasComponent(GameComponentsLookup.ConnectorState) ? LanguageExt.Option<ConnectorStateComponent>.Some((ConnectorStateComponent)GetComponent(GameComponentsLookup.ConnectorState)) : LanguageExt.Option<ConnectorStateComponent>.None; } }
    public LanguageExt.Option<Rewind.SharedData.ConnectorState> maybeConnectorState_value { get { return maybeConnectorState.Map(_ => _.value); } }
    public ConnectorStateComponent connectorState { get { return (ConnectorStateComponent)GetComponent(GameComponentsLookup.ConnectorState); } }
    public bool hasConnectorState { get { return HasComponent(GameComponentsLookup.ConnectorState); } }

    public GameEntity AddConnectorState(Rewind.SharedData.ConnectorState newValue) {
        var index = GameComponentsLookup.ConnectorState;
        var component = (ConnectorStateComponent)CreateComponent(index, typeof(ConnectorStateComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceConnectorState(Rewind.SharedData.ConnectorState newValue) {
        var index = GameComponentsLookup.ConnectorState;
        var component = (ConnectorStateComponent)CreateComponent(index, typeof(ConnectorStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveConnectorState() {
        RemoveComponent(GameComponentsLookup.ConnectorState);
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

    static Entitas.IMatcher<GameEntity> _matcherConnectorState;

    public static Entitas.IMatcher<GameEntity> ConnectorState {
        get {
            if (_matcherConnectorState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ConnectorState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherConnectorState = matcher;
            }

            return _matcherConnectorState;
        }
    }
}
