//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ButtonAStateComponent buttonAState { get { return (ButtonAStateComponent)GetComponent(GameComponentsLookup.ButtonAState); } }
    public bool hasButtonAState { get { return HasComponent(GameComponentsLookup.ButtonAState); } }

    public void AddButtonAState(Rewind.ECSCore.Enums.ButtonAState newValue) {
        var index = GameComponentsLookup.ButtonAState;
        var component = (ButtonAStateComponent)CreateComponent(index, typeof(ButtonAStateComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceButtonAState(Rewind.ECSCore.Enums.ButtonAState newValue) {
        var index = GameComponentsLookup.ButtonAState;
        var component = (ButtonAStateComponent)CreateComponent(index, typeof(ButtonAStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveButtonAState() {
        RemoveComponent(GameComponentsLookup.ButtonAState);
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

    static Entitas.IMatcher<GameEntity> _matcherButtonAState;

    public static Entitas.IMatcher<GameEntity> ButtonAState {
        get {
            if (_matcherButtonAState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ButtonAState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherButtonAState = matcher;
            }

            return _matcherButtonAState;
        }
    }
}
