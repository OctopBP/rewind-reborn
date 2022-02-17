//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class ClockStateEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IClockStateListener> _listenerBuffer;

    public ClockStateEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IClockStateListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.ClockState)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasClockState && entity.hasClockStateListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.clockState;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.clockStateListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnClockState(e, component.value);
            }
        }
    }
}