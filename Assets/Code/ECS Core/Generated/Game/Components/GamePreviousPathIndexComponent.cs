//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PreviousPathIndexComponent previousPathIndex { get { return (PreviousPathIndexComponent)GetComponent(GameComponentsLookup.PreviousPathIndex); } }
    public bool hasPreviousPathIndex { get { return HasComponent(GameComponentsLookup.PreviousPathIndex); } }

    public void AddPreviousPathIndex(PathIndexType newValue) {
        var index = GameComponentsLookup.PreviousPathIndex;
        var component = (PreviousPathIndexComponent)CreateComponent(index, typeof(PreviousPathIndexComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePreviousPathIndex(PathIndexType newValue) {
        var index = GameComponentsLookup.PreviousPathIndex;
        var component = (PreviousPathIndexComponent)CreateComponent(index, typeof(PreviousPathIndexComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePreviousPathIndex() {
        RemoveComponent(GameComponentsLookup.PreviousPathIndex);
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

    static Entitas.IMatcher<GameEntity> _matcherPreviousPathIndex;

    public static Entitas.IMatcher<GameEntity> PreviousPathIndex {
        get {
            if (_matcherPreviousPathIndex == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PreviousPathIndex);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPreviousPathIndex = matcher;
            }

            return _matcherPreviousPathIndex;
        }
    }
}
