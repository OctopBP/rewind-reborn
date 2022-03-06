//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RewindPointIndexComponent rewindPointIndex { get { return (RewindPointIndexComponent)GetComponent(GameComponentsLookup.RewindPointIndex); } }
    public bool hasRewindPointIndex { get { return HasComponent(GameComponentsLookup.RewindPointIndex); } }

    public void AddRewindPointIndex(PointIndexType newValue) {
        var index = GameComponentsLookup.RewindPointIndex;
        var component = (RewindPointIndexComponent)CreateComponent(index, typeof(RewindPointIndexComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRewindPointIndex(PointIndexType newValue) {
        var index = GameComponentsLookup.RewindPointIndex;
        var component = (RewindPointIndexComponent)CreateComponent(index, typeof(RewindPointIndexComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRewindPointIndex() {
        RemoveComponent(GameComponentsLookup.RewindPointIndex);
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

    static Entitas.IMatcher<GameEntity> _matcherRewindPointIndex;

    public static Entitas.IMatcher<GameEntity> RewindPointIndex {
        get {
            if (_matcherRewindPointIndex == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RewindPointIndex);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRewindPointIndex = matcher;
            }

            return _matcherRewindPointIndex;
        }
    }
}
