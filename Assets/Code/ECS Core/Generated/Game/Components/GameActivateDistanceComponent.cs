//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ActivateDistanceComponent activateDistance { get { return (ActivateDistanceComponent)GetComponent(GameComponentsLookup.ActivateDistance); } }
    public bool hasActivateDistance { get { return HasComponent(GameComponentsLookup.ActivateDistance); } }

    public void AddActivateDistance(float newValue) {
        var index = GameComponentsLookup.ActivateDistance;
        var component = (ActivateDistanceComponent)CreateComponent(index, typeof(ActivateDistanceComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceActivateDistance(float newValue) {
        var index = GameComponentsLookup.ActivateDistance;
        var component = (ActivateDistanceComponent)CreateComponent(index, typeof(ActivateDistanceComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveActivateDistance() {
        RemoveComponent(GameComponentsLookup.ActivateDistance);
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

    static Entitas.IMatcher<GameEntity> _matcherActivateDistance;

    public static Entitas.IMatcher<GameEntity> ActivateDistance {
        get {
            if (_matcherActivateDistance == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActivateDistance);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActivateDistance = matcher;
            }

            return _matcherActivateDistance;
        }
    }
}