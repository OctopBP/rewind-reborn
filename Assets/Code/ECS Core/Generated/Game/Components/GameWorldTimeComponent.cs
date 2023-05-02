//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity worldTimeEntity { get { return GetGroup(GameMatcher.WorldTime).GetSingleEntity(); } }
    public WorldTimeComponent worldTime { get { return worldTimeEntity.worldTime; } }
    public bool hasWorldTime { get { return worldTimeEntity != null; } }

    public GameEntity SetWorldTime(Rewind.Services.ITimeService newValue) {
        if (hasWorldTime) {
            throw new Entitas.EntitasException("Could not set WorldTime!\n" + this + " already has an entity with WorldTimeComponent!",
                "You should check if the context already has a worldTimeEntity before setting it or use context.ReplaceWorldTime().");
        }
        var entity = CreateEntity();
        entity.AddWorldTime(newValue);
        return entity;
    }

    public void ReplaceWorldTime(Rewind.Services.ITimeService newValue) {
        var entity = worldTimeEntity;
        if (entity == null) {
            entity = SetWorldTime(newValue);
        } else {
            entity.ReplaceWorldTime(newValue);
        }
    }

    public void RemoveWorldTime() {
        worldTimeEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public WorldTimeComponent worldTime { get { return (WorldTimeComponent)GetComponent(GameComponentsLookup.WorldTime); } }
    public bool hasWorldTime { get { return HasComponent(GameComponentsLookup.WorldTime); } }

    public GameEntity AddWorldTime(Rewind.Services.ITimeService newValue) {
        var index = GameComponentsLookup.WorldTime;
        var component = (WorldTimeComponent)CreateComponent(index, typeof(WorldTimeComponent));
        component.value = newValue;
        AddComponent(index, component);
        return this;
    }

    public GameEntity ReplaceWorldTime(Rewind.Services.ITimeService newValue) {
        var index = GameComponentsLookup.WorldTime;
        var component = (WorldTimeComponent)CreateComponent(index, typeof(WorldTimeComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
        return this;
    }

    public GameEntity RemoveWorldTime() {
        RemoveComponent(GameComponentsLookup.WorldTime);
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

    static Entitas.IMatcher<GameEntity> _matcherWorldTime;

    public static Entitas.IMatcher<GameEntity> WorldTime {
        get {
            if (_matcherWorldTime == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WorldTime);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWorldTime = matcher;
            }

            return _matcherWorldTime;
        }
    }
}
