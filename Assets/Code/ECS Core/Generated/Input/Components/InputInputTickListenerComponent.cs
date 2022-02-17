//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public InputTickListenerComponent inputTickListener { get { return (InputTickListenerComponent)GetComponent(InputComponentsLookup.InputTickListener); } }
    public bool hasInputTickListener { get { return HasComponent(InputComponentsLookup.InputTickListener); } }

    public void AddInputTickListener(System.Collections.Generic.List<IInputTickListener> newValue) {
        var index = InputComponentsLookup.InputTickListener;
        var component = (InputTickListenerComponent)CreateComponent(index, typeof(InputTickListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceInputTickListener(System.Collections.Generic.List<IInputTickListener> newValue) {
        var index = InputComponentsLookup.InputTickListener;
        var component = (InputTickListenerComponent)CreateComponent(index, typeof(InputTickListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveInputTickListener() {
        RemoveComponent(InputComponentsLookup.InputTickListener);
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

    static Entitas.IMatcher<InputEntity> _matcherInputTickListener;

    public static Entitas.IMatcher<InputEntity> InputTickListener {
        get {
            if (_matcherInputTickListener == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.InputTickListener);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherInputTickListener = matcher;
            }

            return _matcherInputTickListener;
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

    public void AddInputTickListener(IInputTickListener value) {
        var listeners = hasInputTickListener
            ? inputTickListener.value
            : new System.Collections.Generic.List<IInputTickListener>();
        listeners.Add(value);
        ReplaceInputTickListener(listeners);
    }

    public void RemoveInputTickListener(IInputTickListener value, bool removeComponentWhenEmpty = true) {
        var listeners = inputTickListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveInputTickListener();
        } else {
            ReplaceInputTickListener(listeners);
        }
    }
}