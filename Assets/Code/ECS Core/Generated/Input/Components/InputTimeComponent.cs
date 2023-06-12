//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public LanguageExt.Option<TimeComponent> maybeTime { get { return HasComponent(InputComponentsLookup.Time) ? LanguageExt.Option<TimeComponent>.Some((TimeComponent)GetComponent(InputComponentsLookup.Time)) : LanguageExt.Option<TimeComponent>.None; } }
    public TimeComponent time { get { return (TimeComponent)GetComponent(InputComponentsLookup.Time); } }
    public bool hasTime { get { return HasComponent(InputComponentsLookup.Time); } }

    public InputEntity AddTime(float newValue) {
        var index = InputComponentsLookup.Time;
        var component = (TimeComponent)CreateComponent(index, typeof(TimeComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public InputEntity ReplaceTime(float newValue) {
        var index = InputComponentsLookup.Time;
        var component = (TimeComponent)CreateComponent(index, typeof(TimeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public InputEntity RemoveTime() {
        RemoveComponent(InputComponentsLookup.Time);
        return this;
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity : ITimeEntity<InputEntity> { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherTime;

    public static Entitas.IMatcher<InputEntity> Time {
        get {
            if (_matcherTime == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Time);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherTime = matcher;
            }

            return _matcherTime;
        }
    }
}
