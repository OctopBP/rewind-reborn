//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LanguageExt.Option<ParentEntityComponent> maybeParentEntity { get { return HasComponent(GameComponentsLookup.ParentEntity) ? LanguageExt.Option<ParentEntityComponent>.Some((ParentEntityComponent)GetComponent(GameComponentsLookup.ParentEntity)) : LanguageExt.Option<ParentEntityComponent>.None; } }
    public ParentEntityComponent parentEntity { get { return (ParentEntityComponent)GetComponent(GameComponentsLookup.ParentEntity); } }
    public bool hasParentEntity { get { return HasComponent(GameComponentsLookup.ParentEntity); } }

    public GameEntity AddParentEntity(GameEntity newValue) {
        var index = GameComponentsLookup.ParentEntity;
        var component = (ParentEntityComponent)CreateComponent(index, typeof(ParentEntityComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceParentEntity(GameEntity newValue) {
        var index = GameComponentsLookup.ParentEntity;
        var component = (ParentEntityComponent)CreateComponent(index, typeof(ParentEntityComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveParentEntity() {
        RemoveComponent(GameComponentsLookup.ParentEntity);
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

    static Entitas.IMatcher<GameEntity> _matcherParentEntity;

    public static Entitas.IMatcher<GameEntity> ParentEntity {
        get {
            if (_matcherParentEntity == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ParentEntity);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherParentEntity = matcher;
            }

            return _matcherParentEntity;
        }
    }
}
