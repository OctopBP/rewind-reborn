//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly GearTypeCLockedComponent gearTypeCLockedComponent = new GearTypeCLockedComponent();

    public GameEntity SetGearTypeCLocked(bool value) {
        isGearTypeCLocked = value;
        return this;
    }

    public bool isGearTypeCLocked {
        get { return HasComponent(GameComponentsLookup.GearTypeCLocked); }
        set {
            if (value != isGearTypeCLocked) {
                var index = GameComponentsLookup.GearTypeCLocked;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : gearTypeCLockedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherGearTypeCLocked;

    public static Entitas.IMatcher<GameEntity> GearTypeCLocked {
        get {
            if (_matcherGearTypeCLocked == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GearTypeCLocked);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGearTypeCLocked = matcher;
            }

            return _matcherGearTypeCLocked;
        }
    }
}
