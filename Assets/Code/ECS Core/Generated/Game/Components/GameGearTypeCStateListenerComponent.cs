//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<GearTypeCStateListenerComponent> maybeGearTypeCStateListener { get { return HasComponent(GameComponentsLookup.GearTypeCStateListener) ? LanguageExt.Option<GearTypeCStateListenerComponent>.Some((GearTypeCStateListenerComponent)GetComponent(GameComponentsLookup.GearTypeCStateListener)) : LanguageExt.Option<GearTypeCStateListenerComponent>.None; } }
    public LanguageExt.Option<System.Collections.Generic.List<IGearTypeCStateListener>> maybeGearTypeCStateListener_value { get { return maybeGearTypeCStateListener.Map(_ => _.value); } }
    public GearTypeCStateListenerComponent gearTypeCStateListener { get { return (GearTypeCStateListenerComponent)GetComponent(GameComponentsLookup.GearTypeCStateListener); } }
    public bool hasGearTypeCStateListener { get { return HasComponent(GameComponentsLookup.GearTypeCStateListener); } }

    public GameEntity AddGearTypeCStateListener(System.Collections.Generic.List<IGearTypeCStateListener> newValue) {
        var index = GameComponentsLookup.GearTypeCStateListener;
        var component = (GearTypeCStateListenerComponent)CreateComponent(index, typeof(GearTypeCStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceGearTypeCStateListener(System.Collections.Generic.List<IGearTypeCStateListener> newValue) {
        var index = GameComponentsLookup.GearTypeCStateListener;
        var component = (GearTypeCStateListenerComponent)CreateComponent(index, typeof(GearTypeCStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveGearTypeCStateListener() {
        RemoveComponent(GameComponentsLookup.GearTypeCStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeCStateListener;

    public static Entitas.IMatcher<GameEntity> GearTypeCStateListener {
        get {
            if (_matcherGearTypeCStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeCStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeCStateListener = matcher;
            }

            return _matcherGearTypeCStateListener;
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

    public GameEntity AddGearTypeCStateListener(IGearTypeCStateListener value) {
        var listeners = hasGearTypeCStateListener
            ? gearTypeCStateListener.value
            : new System.Collections.Generic.List<IGearTypeCStateListener>();
        listeners.Add(value);
        ReplaceGearTypeCStateListener(listeners);
        return this;
    }

    public GameEntity RemoveGearTypeCStateListener(IGearTypeCStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = gearTypeCStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveGearTypeCStateListener();
        } else {
            ReplaceGearTypeCStateListener(listeners);
        }
        return this;
    }
}
