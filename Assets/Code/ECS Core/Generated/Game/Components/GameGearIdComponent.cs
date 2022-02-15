//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GearIdComponent gearId { get { return (GearIdComponent)GetComponent(GameComponentsLookup.GearId); } }
    public bool hasGearId { get { return HasComponent(GameComponentsLookup.GearId); } }

    public void AddGearId(int newValue) {
        var index = GameComponentsLookup.GearId;
        var component = (GearIdComponent)CreateComponent(index, typeof(GearIdComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGearId(int newValue) {
        var index = GameComponentsLookup.GearId;
        var component = (GearIdComponent)CreateComponent(index, typeof(GearIdComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGearId() {
        RemoveComponent(GameComponentsLookup.GearId);
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

    static Entitas.IMatcher<GameEntity> _matcherGearId;

    public static Entitas.IMatcher<GameEntity> GearId {
        get {
            if (_matcherGearId == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearId);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearId = matcher;
            }

            return _matcherGearId;
        }
    }
}
