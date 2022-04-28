//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ActiveSecondRemovedListenerComponent activeSecondRemovedListener { get { return (ActiveSecondRemovedListenerComponent)GetComponent(GameComponentsLookup.ActiveSecondRemovedListener); } }
    public bool hasActiveSecondRemovedListener { get { return HasComponent(GameComponentsLookup.ActiveSecondRemovedListener); } }

    public void AddActiveSecondRemovedListener(System.Collections.Generic.List<IActiveSecondRemovedListener> newValue) {
        var index = GameComponentsLookup.ActiveSecondRemovedListener;
        var component = (ActiveSecondRemovedListenerComponent)CreateComponent(index, typeof(ActiveSecondRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceActiveSecondRemovedListener(System.Collections.Generic.List<IActiveSecondRemovedListener> newValue) {
        var index = GameComponentsLookup.ActiveSecondRemovedListener;
        var component = (ActiveSecondRemovedListenerComponent)CreateComponent(index, typeof(ActiveSecondRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveActiveSecondRemovedListener() {
        RemoveComponent(GameComponentsLookup.ActiveSecondRemovedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherActiveSecondRemovedListener;

    public static Entitas.IMatcher<GameEntity> ActiveSecondRemovedListener {
        get {
            if (_matcherActiveSecondRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActiveSecondRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActiveSecondRemovedListener = matcher;
            }

            return _matcherActiveSecondRemovedListener;
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

    public void AddActiveSecondRemovedListener(IActiveSecondRemovedListener value) {
        var listeners = hasActiveSecondRemovedListener
            ? activeSecondRemovedListener.value
            : new System.Collections.Generic.List<IActiveSecondRemovedListener>();
        listeners.Add(value);
        ReplaceActiveSecondRemovedListener(listeners);
    }

    public void RemoveActiveSecondRemovedListener(IActiveSecondRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = activeSecondRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveActiveSecondRemovedListener();
        } else {
            ReplaceActiveSecondRemovedListener(listeners);
        }
    }
}