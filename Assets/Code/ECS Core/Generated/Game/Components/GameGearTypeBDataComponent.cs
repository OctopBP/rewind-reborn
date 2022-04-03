//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeBDataComponent gearTypeBData { get { return (GearTypeBDataComponent)GetComponent(GameComponentsLookup.GearTypeBData); } }
    public bool hasGearTypeBData { get { return HasComponent(GameComponentsLookup.GearTypeBData); } }

    public void AddGearTypeBData(Rewind.Data.GearTypeBData newValue) {
        var index = GameComponentsLookup.GearTypeBData;
        var component = (GearTypeBDataComponent)CreateComponent(index, typeof(GearTypeBDataComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearTypeBData(Rewind.Data.GearTypeBData newValue) {
        var index = GameComponentsLookup.GearTypeBData;
        var component = (GearTypeBDataComponent)CreateComponent(index, typeof(GearTypeBDataComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearTypeBData() {
        RemoveComponent(GameComponentsLookup.GearTypeBData);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeBData;

    public static Entitas.IMatcher<GameEntity> GearTypeBData {
        get {
            if (_matcherGearTypeBData == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeBData);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeBData = matcher;
            }

            return _matcherGearTypeBData;
        }
    }
}
