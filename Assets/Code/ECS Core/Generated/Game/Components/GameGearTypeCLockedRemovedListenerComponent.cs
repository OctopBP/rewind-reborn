//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<GearTypeCLockedRemovedListenerComponent> maybeGearTypeCLockedRemovedListener { get { return HasComponent(GameComponentsLookup.GearTypeCLockedRemovedListener) ? LanguageExt.Option<GearTypeCLockedRemovedListenerComponent>.Some((GearTypeCLockedRemovedListenerComponent)GetComponent(GameComponentsLookup.GearTypeCLockedRemovedListener)) : LanguageExt.Option<GearTypeCLockedRemovedListenerComponent>.None; } }
    public GearTypeCLockedRemovedListenerComponent gearTypeCLockedRemovedListener { get { return (GearTypeCLockedRemovedListenerComponent)GetComponent(GameComponentsLookup.GearTypeCLockedRemovedListener); } }
    public bool hasGearTypeCLockedRemovedListener { get { return HasComponent(GameComponentsLookup.GearTypeCLockedRemovedListener); } }

    public GameEntity AddGearTypeCLockedRemovedListener(System.Collections.Generic.List<IGearTypeCLockedRemovedListener> newValue) {
        var index = GameComponentsLookup.GearTypeCLockedRemovedListener;
        var component = (GearTypeCLockedRemovedListenerComponent)CreateComponent(index, typeof(GearTypeCLockedRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceGearTypeCLockedRemovedListener(System.Collections.Generic.List<IGearTypeCLockedRemovedListener> newValue) {
        var index = GameComponentsLookup.GearTypeCLockedRemovedListener;
        var component = (GearTypeCLockedRemovedListenerComponent)CreateComponent(index, typeof(GearTypeCLockedRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveGearTypeCLockedRemovedListener() {
        RemoveComponent(GameComponentsLookup.GearTypeCLockedRemovedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeCLockedRemovedListener;

    public static Entitas.IMatcher<GameEntity> GearTypeCLockedRemovedListener {
        get {
            if (_matcherGearTypeCLockedRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeCLockedRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeCLockedRemovedListener = matcher;
            }

            return _matcherGearTypeCLockedRemovedListener;
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

    public GameEntity AddGearTypeCLockedRemovedListener(IGearTypeCLockedRemovedListener value) {
        var listeners = hasGearTypeCLockedRemovedListener
            ? gearTypeCLockedRemovedListener.value
            : new System.Collections.Generic.List<IGearTypeCLockedRemovedListener>();
        listeners.Add(value);
        ReplaceGearTypeCLockedRemovedListener(listeners);
        return this;
    }

    public GameEntity RemoveGearTypeCLockedRemovedListener(IGearTypeCLockedRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gearTypeCLockedRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGearTypeCLockedRemovedListener();
        } else {
            ReplaceGearTypeCLockedRemovedListener(listeners);
        }
        return this;
    }
}
