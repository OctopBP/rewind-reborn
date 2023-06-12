//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<ConnectorStateListenerComponent> maybeConnectorStateListener { get { return HasComponent(GameComponentsLookup.ConnectorStateListener) ? LanguageExt.Option<ConnectorStateListenerComponent>.Some((ConnectorStateListenerComponent)GetComponent(GameComponentsLookup.ConnectorStateListener)) : LanguageExt.Option<ConnectorStateListenerComponent>.None; } }
    public ConnectorStateListenerComponent connectorStateListener { get { return (ConnectorStateListenerComponent)GetComponent(GameComponentsLookup.ConnectorStateListener); } }
    public bool hasConnectorStateListener { get { return HasComponent(GameComponentsLookup.ConnectorStateListener); } }

    public GameEntity AddConnectorStateListener(System.Collections.Generic.List<IConnectorStateListener> newValue) {
        var index = GameComponentsLookup.ConnectorStateListener;
        var component = (ConnectorStateListenerComponent)CreateComponent(index, typeof(ConnectorStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceConnectorStateListener(System.Collections.Generic.List<IConnectorStateListener> newValue) {
        var index = GameComponentsLookup.ConnectorStateListener;
        var component = (ConnectorStateListenerComponent)CreateComponent(index, typeof(ConnectorStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveConnectorStateListener() {
        RemoveComponent(GameComponentsLookup.ConnectorStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherConnectorStateListener;

    public static Entitas.IMatcher<GameEntity> ConnectorStateListener {
        get {
            if (_matcherConnectorStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ConnectorStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherConnectorStateListener = matcher;
            }

            return _matcherConnectorStateListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GameEntity AddConnectorStateListener(IConnectorStateListener value) {
        var listeners = hasConnectorStateListener
            ? connectorStateListener.value
            : new System.Collections.Generic.List<IConnectorStateListener>();
        listeners.Add(value);
        ReplaceConnectorStateListener(listeners);
        return this;
    }

    public GameEntity RemoveConnectorStateListener(IConnectorStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = connectorStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveConnectorStateListener();
        } else {
            ReplaceConnectorStateListener(listeners);
        }
        return this;
    }
}
