//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearTypeCDataComponent gearTypeCData { get { return (GearTypeCDataComponent)GetComponent(GameComponentsLookup.GearTypeCData); } }
    public bool hasGearTypeCData { get { return HasComponent(GameComponentsLookup.GearTypeCData); } }

    public void AddGearTypeCData(Rewind.Data.GearTypeCData newValue) {
        var index = GameComponentsLookup.GearTypeCData;
        var component = (GearTypeCDataComponent)CreateComponent(index, typeof(GearTypeCDataComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearTypeCData(Rewind.Data.GearTypeCData newValue) {
        var index = GameComponentsLookup.GearTypeCData;
        var component = (GearTypeCDataComponent)CreateComponent(index, typeof(GearTypeCDataComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearTypeCData() {
        RemoveComponent(GameComponentsLookup.GearTypeCData);
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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeCData;

    public static Entitas.IMatcher<GameEntity> GearTypeCData {
        get {
            if (_matcherGearTypeCData == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeCData);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeCData = matcher;
            }

            return _matcherGearTypeCData;
        }
    }
}
