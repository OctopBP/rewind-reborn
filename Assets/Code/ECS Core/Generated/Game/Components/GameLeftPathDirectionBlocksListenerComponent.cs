//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LeftPathDirectionBlocksListenerComponent leftPathDirectionBlocksListener { get { return (LeftPathDirectionBlocksListenerComponent)GetComponent(GameComponentsLookup.LeftPathDirectionBlocksListener); } }
    public bool hasLeftPathDirectionBlocksListener { get { return HasComponent(GameComponentsLookup.LeftPathDirectionBlocksListener); } }

    public GameEntity AddLeftPathDirectionBlocksListener(System.Collections.Generic.List<ILeftPathDirectionBlocksListener> newValue) {
        var index = GameComponentsLookup.LeftPathDirectionBlocksListener;
        var component = (LeftPathDirectionBlocksListenerComponent)CreateComponent(index, typeof(LeftPathDirectionBlocksListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceLeftPathDirectionBlocksListener(System.Collections.Generic.List<ILeftPathDirectionBlocksListener> newValue) {
        var index = GameComponentsLookup.LeftPathDirectionBlocksListener;
        var component = (LeftPathDirectionBlocksListenerComponent)CreateComponent(index, typeof(LeftPathDirectionBlocksListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveLeftPathDirectionBlocksListener() {
        RemoveComponent(GameComponentsLookup.LeftPathDirectionBlocksListener);
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

    static Entitas.IMatcher<GameEntity> _matcherLeftPathDirectionBlocksListener;

    public static Entitas.IMatcher<GameEntity> LeftPathDirectionBlocksListener {
        get {
            if (_matcherLeftPathDirectionBlocksListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LeftPathDirectionBlocksListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLeftPathDirectionBlocksListener = matcher;
            }

            return _matcherLeftPathDirectionBlocksListener;
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

    public GameEntity AddLeftPathDirectionBlocksListener(ILeftPathDirectionBlocksListener value) {
        var listeners = hasLeftPathDirectionBlocksListener
            ? leftPathDirectionBlocksListener.value
            : new System.Collections.Generic.List<ILeftPathDirectionBlocksListener>();
        listeners.Add(value);
        ReplaceLeftPathDirectionBlocksListener(listeners);
        return this;
    }

    public GameEntity RemoveLeftPathDirectionBlocksListener(ILeftPathDirectionBlocksListener value, bool removeComponentWhenEmpty = true) {
        var listeners = leftPathDirectionBlocksListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveLeftPathDirectionBlocksListener();
        } else {
            ReplaceLeftPathDirectionBlocksListener(listeners);
        }
        return this;
    }
}