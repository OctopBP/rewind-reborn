//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PuzzleGroupComponent puzzleGroupComponent = new PuzzleGroupComponent();

    public GameEntity SetPuzzleGroup(bool value) {
        isPuzzleGroup = value;
        return this;
    }

    public bool isPuzzleGroup {
        get { return HasComponent(GameComponentsLookup.PuzzleGroup); }
        set {
            if (value != isPuzzleGroup) {
                var index = GameComponentsLookup.PuzzleGroup;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : puzzleGroupComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherPuzzleGroup;

    public static Entitas.IMatcher<GameEntity> PuzzleGroup {
        get {
            if (_matcherPuzzleGroup == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PuzzleGroup);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPuzzleGroup = matcher;
            }

            return _matcherPuzzleGroup;
        }
    }
}
