//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DoorAStateListenerComponent doorAStateListener { get { return (DoorAStateListenerComponent)GetComponent(GameComponentsLookup.DoorAStateListener); } }
    public bool hasDoorAStateListener { get { return HasComponent(GameComponentsLookup.DoorAStateListener); } }

    public void AddDoorAStateListener(System.Collections.Generic.List<IDoorAStateListener> newValue) {
        var index = GameComponentsLookup.DoorAStateListener;
        var component = (DoorAStateListenerComponent)CreateComponent(index, typeof(DoorAStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDoorAStateListener(System.Collections.Generic.List<IDoorAStateListener> newValue) {
        var index = GameComponentsLookup.DoorAStateListener;
        var component = (DoorAStateListenerComponent)CreateComponent(index, typeof(DoorAStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDoorAStateListener() {
        RemoveComponent(GameComponentsLookup.DoorAStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherDoorAStateListener;

    public static Entitas.IMatcher<GameEntity> DoorAStateListener {
        get {
            if (_matcherDoorAStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DoorAStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDoorAStateListener = matcher;
            }

            return _matcherDoorAStateListener;
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

    public void AddDoorAStateListener(IDoorAStateListener value) {
        var listeners = hasDoorAStateListener
            ? doorAStateListener.value
            : new System.Collections.Generic.List<IDoorAStateListener>();
        listeners.Add(value);
        ReplaceDoorAStateListener(listeners);
    }

    public void RemoveDoorAStateListener(IDoorAStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = doorAStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveDoorAStateListener();
        } else {
            ReplaceDoorAStateListener(listeners);
        }
    }
}
