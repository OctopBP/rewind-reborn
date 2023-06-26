//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<AnyClockStateListenerComponent> maybeAnyClockStateListener { get { return HasComponent(GameComponentsLookup.AnyClockStateListener) ? LanguageExt.Option<AnyClockStateListenerComponent>.Some((AnyClockStateListenerComponent)GetComponent(GameComponentsLookup.AnyClockStateListener)) : LanguageExt.Option<AnyClockStateListenerComponent>.None; } }
    public LanguageExt.Option<System.Collections.Generic.List<IAnyClockStateListener>> maybeAnyClockStateListener_value { get { return maybeAnyClockStateListener.Map(_ => _.value); } }
    public AnyClockStateListenerComponent anyClockStateListener { get { return (AnyClockStateListenerComponent)GetComponent(GameComponentsLookup.AnyClockStateListener); } }
    public bool hasAnyClockStateListener { get { return HasComponent(GameComponentsLookup.AnyClockStateListener); } }

    public GameEntity AddAnyClockStateListener(System.Collections.Generic.List<IAnyClockStateListener> newValue) {
        var index = GameComponentsLookup.AnyClockStateListener;
        var component = (AnyClockStateListenerComponent)CreateComponent(index, typeof(AnyClockStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceAnyClockStateListener(System.Collections.Generic.List<IAnyClockStateListener> newValue) {
        var index = GameComponentsLookup.AnyClockStateListener;
        var component = (AnyClockStateListenerComponent)CreateComponent(index, typeof(AnyClockStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveAnyClockStateListener() {
        RemoveComponent(GameComponentsLookup.AnyClockStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherAnyClockStateListener;

    public static Entitas.IMatcher<GameEntity> AnyClockStateListener {
        get {
            if (_matcherAnyClockStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AnyClockStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnyClockStateListener = matcher;
            }

            return _matcherAnyClockStateListener;
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

    public GameEntity AddAnyClockStateListener(IAnyClockStateListener value) {
        var listeners = hasAnyClockStateListener
            ? anyClockStateListener.value
            : new System.Collections.Generic.List<IAnyClockStateListener>();
        listeners.Add(value);
        ReplaceAnyClockStateListener(listeners);
        return this;
    }

    public GameEntity RemoveAnyClockStateListener(IAnyClockStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyClockStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyClockStateListener();
        } else {
            ReplaceAnyClockStateListener(listeners);
        }
        return this;
    }
}
