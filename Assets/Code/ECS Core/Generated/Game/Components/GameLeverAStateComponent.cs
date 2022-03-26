//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LeverAStateComponent leverAState { get { return (LeverAStateComponent)GetComponent(GameComponentsLookup.LeverAState); } }
    public bool hasLeverAState { get { return HasComponent(GameComponentsLookup.LeverAState); } }

    public void AddLeverAState(Rewind.ECSCore.Enums.LeverAState newValue) {
        var index = GameComponentsLookup.LeverAState;
        var component = (LeverAStateComponent)CreateComponent(index, typeof(LeverAStateComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceLeverAState(Rewind.ECSCore.Enums.LeverAState newValue) {
        var index = GameComponentsLookup.LeverAState;
        var component = (LeverAStateComponent)CreateComponent(index, typeof(LeverAStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveLeverAState() {
        RemoveComponent(GameComponentsLookup.LeverAState);
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

    static Entitas.IMatcher<GameEntity> _matcherLeverAState;

    public static Entitas.IMatcher<GameEntity> LeverAState {
        get {
            if (_matcherLeverAState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LeverAState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLeverAState = matcher;
            }

            return _matcherLeverAState;
        }
    }
}
