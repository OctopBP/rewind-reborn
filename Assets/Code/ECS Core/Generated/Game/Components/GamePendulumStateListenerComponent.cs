//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PendulumStateListenerComponent pendulumStateListener { get { return (PendulumStateListenerComponent)GetComponent(GameComponentsLookup.PendulumStateListener); } }
    public bool hasPendulumStateListener { get { return HasComponent(GameComponentsLookup.PendulumStateListener); } }

    public GameEntity AddPendulumStateListener(System.Collections.Generic.List<IPendulumStateListener> newValue) {
        var index = GameComponentsLookup.PendulumStateListener;
        var component = (PendulumStateListenerComponent)CreateComponent(index, typeof(PendulumStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePendulumStateListener(System.Collections.Generic.List<IPendulumStateListener> newValue) {
        var index = GameComponentsLookup.PendulumStateListener;
        var component = (PendulumStateListenerComponent)CreateComponent(index, typeof(PendulumStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePendulumStateListener() {
        RemoveComponent(GameComponentsLookup.PendulumStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherPendulumStateListener;

    public static Entitas.IMatcher<GameEntity> PendulumStateListener {
        get {
            if (_matcherPendulumStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PendulumStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPendulumStateListener = matcher;
            }

            return _matcherPendulumStateListener;
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

    public GameEntity AddPendulumStateListener(IPendulumStateListener value) {
        var listeners = hasPendulumStateListener
            ? pendulumStateListener.value
            : new System.Collections.Generic.List<IPendulumStateListener>();
        listeners.Add(value);
        ReplacePendulumStateListener(listeners);
        return this;
    }

    public GameEntity RemovePendulumStateListener(IPendulumStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = pendulumStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemovePendulumStateListener();
        } else {
            ReplacePendulumStateListener(listeners);
        }
        return this;
    }
}
