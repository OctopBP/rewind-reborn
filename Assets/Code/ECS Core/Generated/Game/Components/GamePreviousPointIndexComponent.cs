//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PreviousPointIndexComponent previousPointIndex { get { return (PreviousPointIndexComponent)GetComponent(GameComponentsLookup.PreviousPointIndex); } }
    public bool hasPreviousPointIndex { get { return HasComponent(GameComponentsLookup.PreviousPointIndex); } }

    public void AddPreviousPointIndex(int newValue) {
        var index = GameComponentsLookup.PreviousPointIndex;
        var component = (PreviousPointIndexComponent)CreateComponent(index, typeof(PreviousPointIndexComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePreviousPointIndex(int newValue) {
        var index = GameComponentsLookup.PreviousPointIndex;
        var component = (PreviousPointIndexComponent)CreateComponent(index, typeof(PreviousPointIndexComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePreviousPointIndex() {
        RemoveComponent(GameComponentsLookup.PreviousPointIndex);
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

    static Entitas.IMatcher<GameEntity> _matcherPreviousPointIndex;

    public static Entitas.IMatcher<GameEntity> PreviousPointIndex {
        get {
            if (_matcherPreviousPointIndex == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PreviousPointIndex);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPreviousPointIndex = matcher;
            }

            return _matcherPreviousPointIndex;
        }
    }
}