//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeCLockedListenerComponent gearTypeCLockedListener { get { return (GearTypeCLockedListenerComponent)GetComponent(GameComponentsLookup.GearTypeCLockedListener); } }
    public bool hasGearTypeCLockedListener { get { return HasComponent(GameComponentsLookup.GearTypeCLockedListener); } }

    public void AddGearTypeCLockedListener(System.Collections.Generic.List<IGearTypeCLockedListener> newValue) {
        var index = GameComponentsLookup.GearTypeCLockedListener;
        var component = (GearTypeCLockedListenerComponent)CreateComponent(index, typeof(GearTypeCLockedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearTypeCLockedListener(System.Collections.Generic.List<IGearTypeCLockedListener> newValue) {
        var index = GameComponentsLookup.GearTypeCLockedListener;
        var component = (GearTypeCLockedListenerComponent)CreateComponent(index, typeof(GearTypeCLockedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearTypeCLockedListener() {
        RemoveComponent(GameComponentsLookup.GearTypeCLockedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeCLockedListener;

    public static Entitas.IMatcher<GameEntity> GearTypeCLockedListener {
        get {
            if (_matcherGearTypeCLockedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeCLockedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeCLockedListener = matcher;
            }

            return _matcherGearTypeCLockedListener;
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

    public void AddGearTypeCLockedListener(IGearTypeCLockedListener value) {
        var listeners = hasGearTypeCLockedListener
            ? gearTypeCLockedListener.value
            : new System.Collections.Generic.List<IGearTypeCLockedListener>();
        listeners.Add(value);
        ReplaceGearTypeCLockedListener(listeners);
    }

    public void RemoveGearTypeCLockedListener(IGearTypeCLockedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gearTypeCLockedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGearTypeCLockedListener();
        } else {
            ReplaceGearTypeCLockedListener(listeners);
        }
    }
}