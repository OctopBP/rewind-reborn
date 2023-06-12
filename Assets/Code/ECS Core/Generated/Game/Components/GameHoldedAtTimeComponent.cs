//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<HoldedAtTimeComponent> maybeHoldedAtTime { get { return HasComponent(GameComponentsLookup.HoldedAtTime) ? LanguageExt.Option<HoldedAtTimeComponent>.Some((HoldedAtTimeComponent)GetComponent(GameComponentsLookup.HoldedAtTime)) : LanguageExt.Option<HoldedAtTimeComponent>.None; } }
    public HoldedAtTimeComponent holdedAtTime { get { return (HoldedAtTimeComponent)GetComponent(GameComponentsLookup.HoldedAtTime); } }
    public bool hasHoldedAtTime { get { return HasComponent(GameComponentsLookup.HoldedAtTime); } }

    public GameEntity AddHoldedAtTime(float newValue) {
        var index = GameComponentsLookup.HoldedAtTime;
        var component = (HoldedAtTimeComponent)CreateComponent(index, typeof(HoldedAtTimeComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceHoldedAtTime(float newValue) {
        var index = GameComponentsLookup.HoldedAtTime;
        var component = (HoldedAtTimeComponent)CreateComponent(index, typeof(HoldedAtTimeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveHoldedAtTime() {
        RemoveComponent(GameComponentsLookup.HoldedAtTime);
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

    static Entitas.IMatcher<GameEntity> _matcherHoldedAtTime;

    public static Entitas.IMatcher<GameEntity> HoldedAtTime {
        get {
            if (_matcherHoldedAtTime == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HoldedAtTime);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHoldedAtTime = matcher;
            }

            return _matcherHoldedAtTime;
        }
    }
}
