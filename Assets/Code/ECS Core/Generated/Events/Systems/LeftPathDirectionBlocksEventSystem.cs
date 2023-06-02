//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class LeftPathDirectionBlocksEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ILeftPathDirectionBlocksListener> _listenerBuffer;

    public LeftPathDirectionBlocksEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ILeftPathDirectionBlocksListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.LeftPathDirectionBlocks)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasLeftPathDirectionBlocks && entity.hasLeftPathDirectionBlocksListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.leftPathDirectionBlocks;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.leftPathDirectionBlocksListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnLeftPathDirectionBlocks(e, component.value);
            }
        }
    }
}