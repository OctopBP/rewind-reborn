//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public FinishReachedListenerComponent finishReachedListener { get { return (FinishReachedListenerComponent)GetComponent(GameComponentsLookup.FinishReachedListener); } }
    public bool hasFinishReachedListener { get { return HasComponent(GameComponentsLookup.FinishReachedListener); } }

    public GameEntity AddFinishReachedListener(System.Collections.Generic.List<IFinishReachedListener> newValue) {
        var index = GameComponentsLookup.FinishReachedListener;
        var component = (FinishReachedListenerComponent)CreateComponent(index, typeof(FinishReachedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceFinishReachedListener(System.Collections.Generic.List<IFinishReachedListener> newValue) {
        var index = GameComponentsLookup.FinishReachedListener;
        var component = (FinishReachedListenerComponent)CreateComponent(index, typeof(FinishReachedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveFinishReachedListener() {
        RemoveComponent(GameComponentsLookup.FinishReachedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherFinishReachedListener;

    public static Entitas.IMatcher<GameEntity> FinishReachedListener {
        get {
            if (_matcherFinishReachedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FinishReachedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFinishReachedListener = matcher;
            }

            return _matcherFinishReachedListener;
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

    public GameEntity AddFinishReachedListener(IFinishReachedListener value) {
        var listeners = hasFinishReachedListener
            ? finishReachedListener.value
            : new System.Collections.Generic.List<IFinishReachedListener>();
        listeners.Add(value);
        ReplaceFinishReachedListener(listeners);
        return this;
    }

    public GameEntity RemoveFinishReachedListener(IFinishReachedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = finishReachedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveFinishReachedListener();
        } else {
            ReplaceFinishReachedListener(listeners);
        }
        return this;
    }
}
