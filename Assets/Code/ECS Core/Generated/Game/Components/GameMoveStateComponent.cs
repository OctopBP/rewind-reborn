//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MoveStateComponent moveState { get { return (MoveStateComponent)GetComponent(GameComponentsLookup.MoveState); } }
    public bool hasMoveState { get { return HasComponent(GameComponentsLookup.MoveState); } }

    public GameEntity AddMoveState(Rewind.SharedData.MoveState newValue) {
        var index = GameComponentsLookup.MoveState;
        var component = (MoveStateComponent)CreateComponent(index, typeof(MoveStateComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceMoveState(Rewind.SharedData.MoveState newValue) {
        var index = GameComponentsLookup.MoveState;
        var component = (MoveStateComponent)CreateComponent(index, typeof(MoveStateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveMoveState() {
        RemoveComponent(GameComponentsLookup.MoveState);
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

    static Entitas.IMatcher<GameEntity> _matcherMoveState;

    public static Entitas.IMatcher<GameEntity> MoveState {
        get {
            if (_matcherMoveState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MoveState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMoveState = matcher;
            }

            return _matcherMoveState;
        }
    }
}
