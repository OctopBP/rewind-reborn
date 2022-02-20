//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public InputTimeListenerComponent inputTimeListener { get { return (InputTimeListenerComponent)GetComponent(InputComponentsLookup.InputTimeListener); } }
    public bool hasInputTimeListener { get { return HasComponent(InputComponentsLookup.InputTimeListener); } }

    public void AddInputTimeListener(System.Collections.Generic.List<IInputTimeListener> newValue) {
        var index = InputComponentsLookup.InputTimeListener;
        var component = (InputTimeListenerComponent)CreateComponent(index, typeof(InputTimeListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceInputTimeListener(System.Collections.Generic.List<IInputTimeListener> newValue) {
        var index = InputComponentsLookup.InputTimeListener;
        var component = (InputTimeListenerComponent)CreateComponent(index, typeof(InputTimeListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveInputTimeListener() {
        RemoveComponent(InputComponentsLookup.InputTimeListener);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherInputTimeListener;

    public static Entitas.IMatcher<InputEntity> InputTimeListener {
        get {
            if (_matcherInputTimeListener == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.InputTimeListener);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherInputTimeListener = matcher;
            }

            return _matcherInputTimeListener;
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
public partial class InputEntity {

    public void AddInputTimeListener(IInputTimeListener value) {
        var listeners = hasInputTimeListener
            ? inputTimeListener.value
            : new System.Collections.Generic.List<IInputTimeListener>();
        listeners.Add(value);
        ReplaceInputTimeListener(listeners);
    }

    public void RemoveInputTimeListener(IInputTimeListener value, bool removeComponentWhenEmpty = true) {
        var listeners = inputTimeListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveInputTimeListener();
        } else {
            ReplaceInputTimeListener(listeners);
        }
    }
}