//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CharacterStateComponent characterState { get { return (CharacterStateComponent)GetComponent(GameComponentsLookup.CharacterState); } }
    public bool hasCharacterState { get { return HasComponent(GameComponentsLookup.CharacterState); } }

    public void AddCharacterState(Rewind.ECSCore.Enums.CharacterState newValue) {
        var index = GameComponentsLookup.CharacterState;
        var component = (CharacterStateComponent)CreateComponent(index, typeof(CharacterStateComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacterState(Rewind.ECSCore.Enums.CharacterState newValue) {
        var index = GameComponentsLookup.CharacterState;
        var component = (CharacterStateComponent)CreateComponent(index, typeof(CharacterStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterState() {
        RemoveComponent(GameComponentsLookup.CharacterState);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterState;

    public static Entitas.IMatcher<GameEntity> CharacterState {
        get {
            if (_matcherCharacterState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterState = matcher;
            }

            return _matcherCharacterState;
        }
    }
}
