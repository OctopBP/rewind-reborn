//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<LevelProgressListenerComponent> maybeLevelProgressListener { get { return HasComponent(GameComponentsLookup.LevelProgressListener) ? LanguageExt.Option<LevelProgressListenerComponent>.Some((LevelProgressListenerComponent)GetComponent(GameComponentsLookup.LevelProgressListener)) : LanguageExt.Option<LevelProgressListenerComponent>.None; } }
    public LanguageExt.Option<System.Collections.Generic.List<ILevelProgressListener>> maybeLevelProgressListener_value { get { return maybeLevelProgressListener.Map(_ => _.value); } }
    public LevelProgressListenerComponent levelProgressListener { get { return (LevelProgressListenerComponent)GetComponent(GameComponentsLookup.LevelProgressListener); } }
    public bool hasLevelProgressListener { get { return HasComponent(GameComponentsLookup.LevelProgressListener); } }

    public GameEntity AddLevelProgressListener(System.Collections.Generic.List<ILevelProgressListener> newValue) {
        var index = GameComponentsLookup.LevelProgressListener;
        var component = (LevelProgressListenerComponent)CreateComponent(index, typeof(LevelProgressListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceLevelProgressListener(System.Collections.Generic.List<ILevelProgressListener> newValue) {
        var index = GameComponentsLookup.LevelProgressListener;
        var component = (LevelProgressListenerComponent)CreateComponent(index, typeof(LevelProgressListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveLevelProgressListener() {
        RemoveComponent(GameComponentsLookup.LevelProgressListener);
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

    static Entitas.IMatcher<GameEntity> _matcherLevelProgressListener;

    public static Entitas.IMatcher<GameEntity> LevelProgressListener {
        get {
            if (_matcherLevelProgressListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LevelProgressListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLevelProgressListener = matcher;
            }

            return _matcherLevelProgressListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GameEntity AddLevelProgressListener(ILevelProgressListener value) {
        var listeners = hasLevelProgressListener
            ? levelProgressListener.value
            : new System.Collections.Generic.List<ILevelProgressListener>();
        listeners.Add(value);
        ReplaceLevelProgressListener(listeners);
        return this;
    }

    public GameEntity RemoveLevelProgressListener(ILevelProgressListener value, bool removeComponentWhenEmpty = true) {
        var listeners = levelProgressListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveLevelProgressListener();
        } else {
            ReplaceLevelProgressListener(listeners);
        }
        return this;
    }
}