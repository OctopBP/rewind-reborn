//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeALockedListenerComponent gearTypeALockedListener { get { return (GearTypeALockedListenerComponent)GetComponent(GameComponentsLookup.GearTypeALockedListener); } }
    public bool hasGearTypeALockedListener { get { return HasComponent(GameComponentsLookup.GearTypeALockedListener); } }

    public void AddGearTypeALockedListener(System.Collections.Generic.List<IGearTypeALockedListener> newValue) {
        var index = GameComponentsLookup.GearTypeALockedListener;
        var component = (GearTypeALockedListenerComponent)CreateComponent(index, typeof(GearTypeALockedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearTypeALockedListener(System.Collections.Generic.List<IGearTypeALockedListener> newValue) {
        var index = GameComponentsLookup.GearTypeALockedListener;
        var component = (GearTypeALockedListenerComponent)CreateComponent(index, typeof(GearTypeALockedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearTypeALockedListener() {
        RemoveComponent(GameComponentsLookup.GearTypeALockedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeALockedListener;

    public static Entitas.IMatcher<GameEntity> GearTypeALockedListener {
        get {
            if (_matcherGearTypeALockedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeALockedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeALockedListener = matcher;
            }

            return _matcherGearTypeALockedListener;
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

    public void AddGearTypeALockedListener(IGearTypeALockedListener value) {
        var listeners = hasGearTypeALockedListener
            ? gearTypeALockedListener.value
            : new System.Collections.Generic.List<IGearTypeALockedListener>();
        listeners.Add(value);
        ReplaceGearTypeALockedListener(listeners);
    }

    public void RemoveGearTypeALockedListener(IGearTypeALockedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gearTypeALockedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGearTypeALockedListener();
        } else {
            ReplaceGearTypeALockedListener(listeners);
        }
    }
}