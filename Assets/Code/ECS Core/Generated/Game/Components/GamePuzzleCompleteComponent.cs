//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PuzzleCompleteComponent puzzleCompleteComponent = new PuzzleCompleteComponent();

    public bool isPuzzleComplete {
        get { return HasComponent(GameComponentsLookup.PuzzleComplete); }
        set {
            if (value != isPuzzleComplete) {
                var index = GameComponentsLookup.PuzzleComplete;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : puzzleCompleteComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherPuzzleComplete;

    public static Entitas.IMatcher<GameEntity> PuzzleComplete {
        get {
            if (_matcherPuzzleComplete == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PuzzleComplete);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPuzzleComplete = matcher;
            }

            return _matcherPuzzleComplete;
        }
    }
}