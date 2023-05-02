//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly FinishReachedComponent finishReachedComponent = new FinishReachedComponent();

    public GameEntity SetFinishReached(bool value) {
        isFinishReached = value;
        return this;
    }

    public GameEntity SetIsFinishReached() {
        isFinishReached = true;
        return this;
    }

    public GameEntity SetIsNotFinishReached() {
        isFinishReached = false;
        return this;
    }

    public bool isFinishReached {
        get { return HasComponent(GameComponentsLookup.FinishReached); }
        set {
            if (value != isFinishReached) {
                var index = GameComponentsLookup.FinishReached;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : finishReachedComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherFinishReached;

    public static Entitas.IMatcher<GameEntity> FinishReached {
        get {
            if (_matcherFinishReached == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FinishReached);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFinishReached = matcher;
            }

            return _matcherFinishReached;
        }
    }
}
