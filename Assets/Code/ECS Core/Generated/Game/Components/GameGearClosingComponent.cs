//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly GearClosingComponent gearClosingComponent = new GearClosingComponent();

    public bool isGearClosing {
        get { return HasComponent(GameComponentsLookup.GearClosing); }
        set {
            if (value != isGearClosing) {
                var index = GameComponentsLookup.GearClosing;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : gearClosingComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherGearClosing;

    public static Entitas.IMatcher<GameEntity> GearClosing {
        get {
            if (_matcherGearClosing == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearClosing);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearClosing = matcher;
            }

            return _matcherGearClosing;
        }
    }
}
