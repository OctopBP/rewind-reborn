//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TraveledValueComponent traveledValue { get { return (TraveledValueComponent)GetComponent(GameComponentsLookup.TraveledValue); } }
    public bool hasTraveledValue { get { return HasComponent(GameComponentsLookup.TraveledValue); } }

    public GameEntity AddTraveledValue(float newValue) {
        var index = GameComponentsLookup.TraveledValue;
        var component = (TraveledValueComponent)CreateComponent(index, typeof(TraveledValueComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceTraveledValue(float newValue) {
        var index = GameComponentsLookup.TraveledValue;
        var component = (TraveledValueComponent)CreateComponent(index, typeof(TraveledValueComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveTraveledValue() {
        RemoveComponent(GameComponentsLookup.TraveledValue);
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

    static Entitas.IMatcher<GameEntity> _matcherTraveledValue;

    public static Entitas.IMatcher<GameEntity> TraveledValue {
        get {
            if (_matcherTraveledValue == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TraveledValue);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTraveledValue = matcher;
            }

            return _matcherTraveledValue;
        }
    }
}
