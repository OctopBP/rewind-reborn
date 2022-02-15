//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PathFollowerSpeed pathFollowerSpeed { get { return (PathFollowerSpeed)GetComponent(GameComponentsLookup.PathFollowerSpeed); } }
    public bool hasPathFollowerSpeed { get { return HasComponent(GameComponentsLookup.PathFollowerSpeed); } }

    public void AddPathFollowerSpeed(float newValue) {
        var index = GameComponentsLookup.PathFollowerSpeed;
        var component = (PathFollowerSpeed)CreateComponent(index, typeof(PathFollowerSpeed));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePathFollowerSpeed(float newValue) {
        var index = GameComponentsLookup.PathFollowerSpeed;
        var component = (PathFollowerSpeed)CreateComponent(index, typeof(PathFollowerSpeed));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePathFollowerSpeed() {
        RemoveComponent(GameComponentsLookup.PathFollowerSpeed);
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

    static Entitas.IMatcher<GameEntity> _matcherPathFollowerSpeed;

    public static Entitas.IMatcher<GameEntity> PathFollowerSpeed {
        get {
            if (_matcherPathFollowerSpeed == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PathFollowerSpeed);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPathFollowerSpeed = matcher;
            }

            return _matcherPathFollowerSpeed;
        }
    }
}
