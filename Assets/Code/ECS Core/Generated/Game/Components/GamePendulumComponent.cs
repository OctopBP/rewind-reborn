//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PendulumComponent pendulumComponent = new PendulumComponent();

    public GameEntity SetPendulum(bool value) {
        isPendulum = value;
        return this;
    }

    public GameEntity SetIsPendulum() {
        isPendulum = true;
        return this;
    }

    public GameEntity SetIsNotPendulum() {
        isPendulum = false;
        return this;
    }

    public bool isPendulum {
        get { return HasComponent(GameComponentsLookup.Pendulum); }
        set {
            if (value != isPendulum) {
                var index = GameComponentsLookup.Pendulum;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : pendulumComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherPendulum;

    public static Entitas.IMatcher<GameEntity> Pendulum {
        get {
            if (_matcherPendulum == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Pendulum);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPendulum = matcher;
            }

            return _matcherPendulum;
        }
    }
}
