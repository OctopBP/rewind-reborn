//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public StartReachedListenerComponent startReachedListener { get { return (StartReachedListenerComponent)GetComponent(GameComponentsLookup.StartReachedListener); } }
    public bool hasStartReachedListener { get { return HasComponent(GameComponentsLookup.StartReachedListener); } }

    public GameEntity AddStartReachedListener(System.Collections.Generic.List<IStartReachedListener> newValue) {
        var index = GameComponentsLookup.StartReachedListener;
        var component = (StartReachedListenerComponent)CreateComponent(index, typeof(StartReachedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceStartReachedListener(System.Collections.Generic.List<IStartReachedListener> newValue) {
        var index = GameComponentsLookup.StartReachedListener;
        var component = (StartReachedListenerComponent)CreateComponent(index, typeof(StartReachedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveStartReachedListener() {
        RemoveComponent(GameComponentsLookup.StartReachedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherStartReachedListener;

    public static Entitas.IMatcher<GameEntity> StartReachedListener {
        get {
            if (_matcherStartReachedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.StartReachedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherStartReachedListener = matcher;
            }

            return _matcherStartReachedListener;
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

    public GameEntity AddStartReachedListener(IStartReachedListener value) {
        var listeners = hasStartReachedListener
            ? startReachedListener.value
            : new System.Collections.Generic.List<IStartReachedListener>();
        listeners.Add(value);
        ReplaceStartReachedListener(listeners);
        return this;
    }

    public GameEntity RemoveStartReachedListener(IStartReachedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = startReachedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveStartReachedListener();
        } else {
            ReplaceStartReachedListener(listeners);
        }
        return this;
    }
}