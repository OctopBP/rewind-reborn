//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<ConditionGroupComponent> maybeConditionGroup { get { return HasComponent(GameComponentsLookup.ConditionGroup) ? LanguageExt.Option<ConditionGroupComponent>.Some((ConditionGroupComponent)GetComponent(GameComponentsLookup.ConditionGroup)) : LanguageExt.Option<ConditionGroupComponent>.None; } }
    public ConditionGroupComponent conditionGroup { get { return (ConditionGroupComponent)GetComponent(GameComponentsLookup.ConditionGroup); } }
    public bool hasConditionGroup { get { return HasComponent(GameComponentsLookup.ConditionGroup); } }

    public GameEntity AddConditionGroup(Rewind.LogicBuilder.ConditionGroup newValue) {
        var index = GameComponentsLookup.ConditionGroup;
        var component = (ConditionGroupComponent)CreateComponent(index, typeof(ConditionGroupComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceConditionGroup(Rewind.LogicBuilder.ConditionGroup newValue) {
        var index = GameComponentsLookup.ConditionGroup;
        var component = (ConditionGroupComponent)CreateComponent(index, typeof(ConditionGroupComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveConditionGroup() {
        RemoveComponent(GameComponentsLookup.ConditionGroup);
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

    static Entitas.IMatcher<GameEntity> _matcherConditionGroup;

    public static Entitas.IMatcher<GameEntity> ConditionGroup {
        get {
            if (_matcherConditionGroup == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ConditionGroup);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherConditionGroup = matcher;
            }

            return _matcherConditionGroup;
        }
    }
}
