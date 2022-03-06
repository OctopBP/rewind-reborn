//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PointIndexComponent pointIndex { get { return (PointIndexComponent)GetComponent(GameComponentsLookup.PointIndex); } }
    public bool hasPointIndex { get { return HasComponent(GameComponentsLookup.PointIndex); } }

    public void AddPointIndex(PointIndexType newValue) {
        var index = GameComponentsLookup.PointIndex;
        var component = (PointIndexComponent)CreateComponent(index, typeof(PointIndexComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePointIndex(PointIndexType newValue) {
        var index = GameComponentsLookup.PointIndex;
        var component = (PointIndexComponent)CreateComponent(index, typeof(PointIndexComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePointIndex() {
        RemoveComponent(GameComponentsLookup.PointIndex);
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

    static Entitas.IMatcher<GameEntity> _matcherPointIndex;

    public static Entitas.IMatcher<GameEntity> PointIndex {
        get {
            if (_matcherPointIndex == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PointIndex);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPointIndex = matcher;
            }

            return _matcherPointIndex;
        }
    }
}
