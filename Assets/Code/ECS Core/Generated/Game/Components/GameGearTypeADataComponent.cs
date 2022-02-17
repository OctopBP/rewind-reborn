//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeADataComponent gearTypeAData { get { return (GearTypeADataComponent)GetComponent(GameComponentsLookup.GearTypeAData); } }
    public bool hasGearTypeAData { get { return HasComponent(GameComponentsLookup.GearTypeAData); } }

    public void AddGearTypeAData(Rewind.Data.GearTypeAData newValue) {
        var index = GameComponentsLookup.GearTypeAData;
        var component = (GearTypeADataComponent)CreateComponent(index, typeof(GearTypeADataComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearTypeAData(Rewind.Data.GearTypeAData newValue) {
        var index = GameComponentsLookup.GearTypeAData;
        var component = (GearTypeADataComponent)CreateComponent(index, typeof(GearTypeADataComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearTypeAData() {
        RemoveComponent(GameComponentsLookup.GearTypeAData);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeAData;

    public static Entitas.IMatcher<GameEntity> GearTypeAData {
        get {
            if (_matcherGearTypeAData == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeAData);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeAData = matcher;
            }

            return _matcherGearTypeAData;
        }
    }
}