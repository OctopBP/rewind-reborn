//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PlatformADataComponent platformAData { get { return (PlatformADataComponent)GetComponent(GameComponentsLookup.PlatformAData); } }
    public bool hasPlatformAData { get { return HasComponent(GameComponentsLookup.PlatformAData); } }

    public GameEntity AddPlatformAData(Rewind.SharedData.PlatformAData newValue) {
        var index = GameComponentsLookup.PlatformAData;
        var component = (PlatformADataComponent)CreateComponent(index, typeof(PlatformADataComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplacePlatformAData(Rewind.SharedData.PlatformAData newValue) {
        var index = GameComponentsLookup.PlatformAData;
        var component = (PlatformADataComponent)CreateComponent(index, typeof(PlatformADataComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemovePlatformAData() {
        RemoveComponent(GameComponentsLookup.PlatformAData);
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

    static Entitas.IMatcher<GameEntity> _matcherPlatformAData;

    public static Entitas.IMatcher<GameEntity> PlatformAData {
        get {
            if (_matcherPlatformAData == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlatformAData);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlatformAData = matcher;
            }

            return _matcherPlatformAData;
        }
    }
}
