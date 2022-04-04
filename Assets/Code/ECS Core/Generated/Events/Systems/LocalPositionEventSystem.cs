//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class LocalPositionEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ILocalPositionListener> _listenerBuffer;

    public LocalPositionEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ILocalPositionListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.LocalPosition)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasLocalPosition && entity.hasLocalPositionListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.localPosition;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.localPositionListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnLocalPosition(e, component.value);
            }
        }
    }
}