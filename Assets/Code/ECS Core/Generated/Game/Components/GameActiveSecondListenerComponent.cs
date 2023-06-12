//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<ActiveSecondListenerComponent> maybeActiveSecondListener { get { return HasComponent(GameComponentsLookup.ActiveSecondListener) ? LanguageExt.Option<ActiveSecondListenerComponent>.Some((ActiveSecondListenerComponent)GetComponent(GameComponentsLookup.ActiveSecondListener)) : LanguageExt.Option<ActiveSecondListenerComponent>.None; } }
    public ActiveSecondListenerComponent activeSecondListener { get { return (ActiveSecondListenerComponent)GetComponent(GameComponentsLookup.ActiveSecondListener); } }
    public bool hasActiveSecondListener { get { return HasComponent(GameComponentsLookup.ActiveSecondListener); } }

    public GameEntity AddActiveSecondListener(System.Collections.Generic.List<IActiveSecondListener> newValue) {
        var index = GameComponentsLookup.ActiveSecondListener;
        var component = (ActiveSecondListenerComponent)CreateComponent(index, typeof(ActiveSecondListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceActiveSecondListener(System.Collections.Generic.List<IActiveSecondListener> newValue) {
        var index = GameComponentsLookup.ActiveSecondListener;
        var component = (ActiveSecondListenerComponent)CreateComponent(index, typeof(ActiveSecondListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveActiveSecondListener() {
        RemoveComponent(GameComponentsLookup.ActiveSecondListener);
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

    static Entitas.IMatcher<GameEntity> _matcherActiveSecondListener;

    public static Entitas.IMatcher<GameEntity> ActiveSecondListener {
        get {
            if (_matcherActiveSecondListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActiveSecondListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActiveSecondListener = matcher;
            }

            return _matcherActiveSecondListener;
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

    public GameEntity AddActiveSecondListener(IActiveSecondListener value) {
        var listeners = hasActiveSecondListener
            ? activeSecondListener.value
            : new System.Collections.Generic.List<IActiveSecondListener>();
        listeners.Add(value);
        ReplaceActiveSecondListener(listeners);
        return this;
    }

    public GameEntity RemoveActiveSecondListener(IActiveSecondListener value, bool removeComponentWhenEmpty = true) {
        var listeners = activeSecondListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveActiveSecondListener();
        } else {
            ReplaceActiveSecondListener(listeners);
        }
        return this;
    }
}
