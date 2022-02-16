//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ClockStateListenerComponent clockStateListener { get { return (ClockStateListenerComponent)GetComponent(GameComponentsLookup.ClockStateListener); } }
    public bool hasClockStateListener { get { return HasComponent(GameComponentsLookup.ClockStateListener); } }

    public void AddClockStateListener(System.Collections.Generic.List<IClockStateListener> newValue) {
        var index = GameComponentsLookup.ClockStateListener;
        var component = (ClockStateListenerComponent)CreateComponent(index, typeof(ClockStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceClockStateListener(System.Collections.Generic.List<IClockStateListener> newValue) {
        var index = GameComponentsLookup.ClockStateListener;
        var component = (ClockStateListenerComponent)CreateComponent(index, typeof(ClockStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveClockStateListener() {
        RemoveComponent(GameComponentsLookup.ClockStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherClockStateListener;

    public static Entitas.IMatcher<GameEntity> ClockStateListener {
        get {
            if (_matcherClockStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ClockStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherClockStateListener = matcher;
            }

            return _matcherClockStateListener;
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

    public void AddClockStateListener(IClockStateListener value) {
        var listeners = hasClockStateListener
            ? clockStateListener.value
            : new System.Collections.Generic.List<IClockStateListener>();
        listeners.Add(value);
        ReplaceClockStateListener(listeners);
    }

    public void RemoveClockStateListener(IClockStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = clockStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveClockStateListener();
        } else {
            ReplaceClockStateListener(listeners);
        }
    }
}
