//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<GearTypeCPreviousStateComponent> maybeGearTypeCPreviousState { get { return HasComponent(GameComponentsLookup.GearTypeCPreviousState) ? LanguageExt.Option<GearTypeCPreviousStateComponent>.Some((GearTypeCPreviousStateComponent)GetComponent(GameComponentsLookup.GearTypeCPreviousState)) : LanguageExt.Option<GearTypeCPreviousStateComponent>.None; } }
    public GearTypeCPreviousStateComponent gearTypeCPreviousState { get { return (GearTypeCPreviousStateComponent)GetComponent(GameComponentsLookup.GearTypeCPreviousState); } }
    public bool hasGearTypeCPreviousState { get { return HasComponent(GameComponentsLookup.GearTypeCPreviousState); } }

    public GameEntity AddGearTypeCPreviousState(Rewind.SharedData.GearTypeCState newValue) {
        var index = GameComponentsLookup.GearTypeCPreviousState;
        var component = (GearTypeCPreviousStateComponent)CreateComponent(index, typeof(GearTypeCPreviousStateComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceGearTypeCPreviousState(Rewind.SharedData.GearTypeCState newValue) {
        var index = GameComponentsLookup.GearTypeCPreviousState;
        var component = (GearTypeCPreviousStateComponent)CreateComponent(index, typeof(GearTypeCPreviousStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveGearTypeCPreviousState() {
        RemoveComponent(GameComponentsLookup.GearTypeCPreviousState);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeCPreviousState;

    public static Entitas.IMatcher<GameEntity> GearTypeCPreviousState {
        get {
            if (_matcherGearTypeCPreviousState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeCPreviousState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeCPreviousState = matcher;
            }

            return _matcherGearTypeCPreviousState;
        }
    }
}
