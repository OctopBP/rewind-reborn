//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class InputTickEventSystem : Entitas.ReactiveSystem<InputEntity> {

    readonly System.Collections.Generic.List<IInputTickListener> _listenerBuffer;

    public InputTickEventSystem(Contexts contexts) : base(contexts.input) {
        _listenerBuffer = new System.Collections.Generic.List<IInputTickListener>();
    }

    protected override Entitas.ICollector<InputEntity> GetTrigger(Entitas.IContext<InputEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(InputMatcher.Tick)
        );
    }

    protected override bool Filter(InputEntity entity) {
        return entity.hasTick && entity.hasInputTickListener;
    }

    protected override void Execute(System.Collections.Generic.List<InputEntity> entities) {
        foreach (var e in entities) {
            var component = e.tick;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.inputTickListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTick(e, component.value);
            }
        }
    }
}