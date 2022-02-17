//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeAStateListenerComponent gearTypeAStateListener { get { return (GearTypeAStateListenerComponent)GetComponent(GameComponentsLookup.GearTypeAStateListener); } }
    public bool hasGearTypeAStateListener { get { return HasComponent(GameComponentsLookup.GearTypeAStateListener); } }

    public void AddGearTypeAStateListener(System.Collections.Generic.List<IGearTypeAStateListener> newValue) {
        var index = GameComponentsLookup.GearTypeAStateListener;
        var component = (GearTypeAStateListenerComponent)CreateComponent(index, typeof(GearTypeAStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearTypeAStateListener(System.Collections.Generic.List<IGearTypeAStateListener> newValue) {
        var index = GameComponentsLookup.GearTypeAStateListener;
        var component = (GearTypeAStateListenerComponent)CreateComponent(index, typeof(GearTypeAStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearTypeAStateListener() {
        RemoveComponent(GameComponentsLookup.GearTypeAStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeAStateListener;

    public static Entitas.IMatcher<GameEntity> GearTypeAStateListener {
        get {
            if (_matcherGearTypeAStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeAStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeAStateListener = matcher;
            }

            return _matcherGearTypeAStateListener;
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

    public void AddGearTypeAStateListener(IGearTypeAStateListener value) {
        var listeners = hasGearTypeAStateListener
            ? gearTypeAStateListener.value
            : new System.Collections.Generic.List<IGearTypeAStateListener>();
        listeners.Add(value);
        ReplaceGearTypeAStateListener(listeners);
    }

    public void RemoveGearTypeAStateListener(IGearTypeAStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gearTypeAStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGearTypeAStateListener();
        } else {
            ReplaceGearTypeAStateListener(listeners);
        }
    }
}