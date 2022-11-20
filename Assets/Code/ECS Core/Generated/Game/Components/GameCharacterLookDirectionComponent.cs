//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CharacterLookDirectionComponent characterLookDirection { get { return (CharacterLookDirectionComponent)GetComponent(GameComponentsLookup.CharacterLookDirection); } }
    public bool hasCharacterLookDirection { get { return HasComponent(GameComponentsLookup.CharacterLookDirection); } }

    public void AddCharacterLookDirection(Rewind.ECSCore.Enums.CharacterLookDirection newValue) {
        var index = GameComponentsLookup.CharacterLookDirection;
        var component = (CharacterLookDirectionComponent)CreateComponent(index, typeof(CharacterLookDirectionComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCharacterLookDirection(Rewind.ECSCore.Enums.CharacterLookDirection newValue) {
        var index = GameComponentsLookup.CharacterLookDirection;
        var component = (CharacterLookDirectionComponent)CreateComponent(index, typeof(CharacterLookDirectionComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterLookDirection() {
        RemoveComponent(GameComponentsLookup.CharacterLookDirection);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterLookDirection;

    public static Entitas.IMatcher<GameEntity> CharacterLookDirection {
        get {
            if (_matcherCharacterLookDirection == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterLookDirection);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterLookDirection = matcher;
            }

            return _matcherCharacterLookDirection;
        }
    }
}