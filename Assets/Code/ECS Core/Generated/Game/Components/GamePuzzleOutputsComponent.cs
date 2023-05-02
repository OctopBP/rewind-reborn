//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PuzzleOutputsComponent puzzleOutputs { get { return (PuzzleOutputsComponent)GetComponent(GameComponentsLookup.PuzzleOutputs); } }
    public bool hasPuzzleOutputs { get { return HasComponent(GameComponentsLookup.PuzzleOutputs); } }

    public GameEntity AddPuzzleOutputs(System.Collections.Generic.List<System.Guid> newValue) {
        var index = GameComponentsLookup.PuzzleOutputs;
        var component = (PuzzleOutputsComponent)CreateComponent(index, typeof(PuzzleOutputsComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePuzzleOutputs(System.Collections.Generic.List<System.Guid> newValue) {
        var index = GameComponentsLookup.PuzzleOutputs;
        var component = (PuzzleOutputsComponent)CreateComponent(index, typeof(PuzzleOutputsComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePuzzleOutputs() {
        RemoveComponent(GameComponentsLookup.PuzzleOutputs);
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

    static Entitas.IMatcher<GameEntity> _matcherPuzzleOutputs;

    public static Entitas.IMatcher<GameEntity> PuzzleOutputs {
        get {
            if (_matcherPuzzleOutputs == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PuzzleOutputs);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPuzzleOutputs = matcher;
            }

            return _matcherPuzzleOutputs;
        }
    }
}
