//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly GearTypeALockedComponent gearTypeALockedComponent = new GearTypeALockedComponent();

    public bool isGearTypeALocked {
        get { return HasComponent(GameComponentsLookup.GearTypeALocked); }
        set {
            if (value != isGearTypeALocked) {
                var index = GameComponentsLookup.GearTypeALocked;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : gearTypeALockedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeALocked;

    public static Entitas.IMatcher<GameEntity> GearTypeALocked {
        get {
            if (_matcherGearTypeALocked == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeALocked);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeALocked = matcher;
            }

            return _matcherGearTypeALocked;
        }
    }
}