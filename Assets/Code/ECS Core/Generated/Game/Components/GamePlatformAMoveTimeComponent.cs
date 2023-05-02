//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PlatformAMoveTimeComponent platformAMoveTime { get { return (PlatformAMoveTimeComponent)GetComponent(GameComponentsLookup.PlatformAMoveTime); } }
    public bool hasPlatformAMoveTime { get { return HasComponent(GameComponentsLookup.PlatformAMoveTime); } }

    public GameEntity AddPlatformAMoveTime(float newValue) {
        var index = GameComponentsLookup.PlatformAMoveTime;
        var component = (PlatformAMoveTimeComponent)CreateComponent(index, typeof(PlatformAMoveTimeComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePlatformAMoveTime(float newValue) {
        var index = GameComponentsLookup.PlatformAMoveTime;
        var component = (PlatformAMoveTimeComponent)CreateComponent(index, typeof(PlatformAMoveTimeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePlatformAMoveTime() {
        RemoveComponent(GameComponentsLookup.PlatformAMoveTime);
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

    static Entitas.IMatcher<GameEntity> _matcherPlatformAMoveTime;

    public static Entitas.IMatcher<GameEntity> PlatformAMoveTime {
        get {
            if (_matcherPlatformAMoveTime == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlatformAMoveTime);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlatformAMoveTime = matcher;
            }

            return _matcherPlatformAMoveTime;
        }
    }
}
