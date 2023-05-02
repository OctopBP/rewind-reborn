//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MoveCompleteListenerComponent moveCompleteListener { get { return (MoveCompleteListenerComponent)GetComponent(GameComponentsLookup.MoveCompleteListener); } }
    public bool hasMoveCompleteListener { get { return HasComponent(GameComponentsLookup.MoveCompleteListener); } }

    public GameEntity AddMoveCompleteListener(System.Collections.Generic.List<IMoveCompleteListener> newValue) {
        var index = GameComponentsLookup.MoveCompleteListener;
        var component = (MoveCompleteListenerComponent)CreateComponent(index, typeof(MoveCompleteListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceMoveCompleteListener(System.Collections.Generic.List<IMoveCompleteListener> newValue) {
        var index = GameComponentsLookup.MoveCompleteListener;
        var component = (MoveCompleteListenerComponent)CreateComponent(index, typeof(MoveCompleteListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveMoveCompleteListener() {
        RemoveComponent(GameComponentsLookup.MoveCompleteListener);
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

    static Entitas.IMatcher<GameEntity> _matcherMoveCompleteListener;

    public static Entitas.IMatcher<GameEntity> MoveCompleteListener {
        get {
            if (_matcherMoveCompleteListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MoveCompleteListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMoveCompleteListener = matcher;
            }

            return _matcherMoveCompleteListener;
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

    public GameEntity AddMoveCompleteListener(IMoveCompleteListener value) {
        var listeners = hasMoveCompleteListener
            ? moveCompleteListener.value
            : new System.Collections.Generic.List<IMoveCompleteListener>();
        listeners.Add(value);
        ReplaceMoveCompleteListener(listeners);
        return this;
    }

    public GameEntity RemoveMoveCompleteListener(IMoveCompleteListener value, bool removeComponentWhenEmpty = true) {
        var listeners = moveCompleteListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveMoveCompleteListener();
        } else {
            ReplaceMoveCompleteListener(listeners);
        }
        return this;
    }
}
