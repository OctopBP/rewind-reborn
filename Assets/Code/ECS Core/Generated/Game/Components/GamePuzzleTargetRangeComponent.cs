//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PuzzleTargetRangeComponent puzzleTargetRange { get { return (PuzzleTargetRangeComponent)GetComponent(GameComponentsLookup.PuzzleTargetRange); } }
    public bool hasPuzzleTargetRange { get { return HasComponent(GameComponentsLookup.PuzzleTargetRange); } }

    public void AddPuzzleTargetRange(UnityEngine.Vector2[] newValue) {
        var index = GameComponentsLookup.PuzzleTargetRange;
        var component = (PuzzleTargetRangeComponent)CreateComponent(index, typeof(PuzzleTargetRangeComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePuzzleTargetRange(UnityEngine.Vector2[] newValue) {
        var index = GameComponentsLookup.PuzzleTargetRange;
        var component = (PuzzleTargetRangeComponent)CreateComponent(index, typeof(PuzzleTargetRangeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePuzzleTargetRange() {
        RemoveComponent(GameComponentsLookup.PuzzleTargetRange);
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

    static Entitas.IMatcher<GameEntity> _matcherPuzzleTargetRange;

    public static Entitas.IMatcher<GameEntity> PuzzleTargetRange {
        get {
            if (_matcherPuzzleTargetRange == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PuzzleTargetRange);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPuzzleTargetRange = matcher;
            }

            return _matcherPuzzleTargetRange;
        }
    }
}
