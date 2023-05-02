//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeAPreviousStateComponent gearTypeAPreviousState { get { return (GearTypeAPreviousStateComponent)GetComponent(GameComponentsLookup.GearTypeAPreviousState); } }
    public bool hasGearTypeAPreviousState { get { return HasComponent(GameComponentsLookup.GearTypeAPreviousState); } }

    public GameEntity AddGearTypeAPreviousState(Rewind.ECSCore.Enums.GearTypeAState newValue) {
        var index = GameComponentsLookup.GearTypeAPreviousState;
        var component = (GearTypeAPreviousStateComponent)CreateComponent(index, typeof(GearTypeAPreviousStateComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceGearTypeAPreviousState(Rewind.ECSCore.Enums.GearTypeAState newValue) {
        var index = GameComponentsLookup.GearTypeAPreviousState;
        var component = (GearTypeAPreviousStateComponent)CreateComponent(index, typeof(GearTypeAPreviousStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveGearTypeAPreviousState() {
        RemoveComponent(GameComponentsLookup.GearTypeAPreviousState);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeAPreviousState;

    public static Entitas.IMatcher<GameEntity> GearTypeAPreviousState {
        get {
            if (_matcherGearTypeAPreviousState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeAPreviousState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeAPreviousState = matcher;
            }

            return _matcherGearTypeAPreviousState;
        }
    }
}
