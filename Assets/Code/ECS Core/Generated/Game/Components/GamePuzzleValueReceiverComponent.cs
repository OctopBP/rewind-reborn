//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PuzzleValueReceiverComponent puzzleValueReceiver { get { return (PuzzleValueReceiverComponent)GetComponent(GameComponentsLookup.PuzzleValueReceiver); } }
    public bool hasPuzzleValueReceiver { get { return HasComponent(GameComponentsLookup.PuzzleValueReceiver); } }

    public GameEntity AddPuzzleValueReceiver(IPuzzleValueReceiver[] newValue) {
        var index = GameComponentsLookup.PuzzleValueReceiver;
        var component = (PuzzleValueReceiverComponent)CreateComponent(index, typeof(PuzzleValueReceiverComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePuzzleValueReceiver(IPuzzleValueReceiver[] newValue) {
        var index = GameComponentsLookup.PuzzleValueReceiver;
        var component = (PuzzleValueReceiverComponent)CreateComponent(index, typeof(PuzzleValueReceiverComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePuzzleValueReceiver() {
        RemoveComponent(GameComponentsLookup.PuzzleValueReceiver);
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

    static Entitas.IMatcher<GameEntity> _matcherPuzzleValueReceiver;

    public static Entitas.IMatcher<GameEntity> PuzzleValueReceiver {
        get {
            if (_matcherPuzzleValueReceiver == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PuzzleValueReceiver);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPuzzleValueReceiver = matcher;
            }

            return _matcherPuzzleValueReceiver;
        }
    }
}