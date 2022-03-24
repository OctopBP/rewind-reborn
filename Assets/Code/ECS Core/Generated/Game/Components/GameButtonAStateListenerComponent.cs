//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ButtonAStateListenerComponent buttonAStateListener { get { return (ButtonAStateListenerComponent)GetComponent(GameComponentsLookup.ButtonAStateListener); } }
    public bool hasButtonAStateListener { get { return HasComponent(GameComponentsLookup.ButtonAStateListener); } }

    public void AddButtonAStateListener(System.Collections.Generic.List<IButtonAStateListener> newValue) {
        var index = GameComponentsLookup.ButtonAStateListener;
        var component = (ButtonAStateListenerComponent)CreateComponent(index, typeof(ButtonAStateListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceButtonAStateListener(System.Collections.Generic.List<IButtonAStateListener> newValue) {
        var index = GameComponentsLookup.ButtonAStateListener;
        var component = (ButtonAStateListenerComponent)CreateComponent(index, typeof(ButtonAStateListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveButtonAStateListener() {
        RemoveComponent(GameComponentsLookup.ButtonAStateListener);
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

    static Entitas.IMatcher<GameEntity> _matcherButtonAStateListener;

    public static Entitas.IMatcher<GameEntity> ButtonAStateListener {
        get {
            if (_matcherButtonAStateListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ButtonAStateListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherButtonAStateListener = matcher;
            }

            return _matcherButtonAStateListener;
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

    public void AddButtonAStateListener(IButtonAStateListener value) {
        var listeners = hasButtonAStateListener
            ? buttonAStateListener.value
            : new System.Collections.Generic.List<IButtonAStateListener>();
        listeners.Add(value);
        ReplaceButtonAStateListener(listeners);
    }

    public void RemoveButtonAStateListener(IButtonAStateListener value, bool removeComponentWhenEmpty = true) {
        var listeners = buttonAStateListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveButtonAStateListener();
        } else {
            ReplaceButtonAStateListener(listeners);
        }
    }
}
