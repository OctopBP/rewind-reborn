//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly StartReachedComponent startReachedComponent = new StartReachedComponent();

    public GameEntity SetStartReached(bool value) {
        isStartReached = value;
        return this;
    }

    public bool isStartReached {
        get { return HasComponent(GameComponentsLookup.StartReached); }
        set {
            if (value != isStartReached) {
                var index = GameComponentsLookup.StartReached;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : startReachedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherStartReached;

    public static Entitas.IMatcher<GameEntity> StartReached {
        get {
            if (_matcherStartReached == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.StartReached);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherStartReached = matcher;
            }

            return _matcherStartReached;
        }
    }
}
