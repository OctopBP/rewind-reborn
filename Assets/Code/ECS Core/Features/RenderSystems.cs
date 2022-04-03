namespace Rewind.ECSCore.Features {
	public class RenderSystems : Feature {
		public RenderSystems(Contexts contexts) : base(nameof(RenderSystems)) {
			Add(new PositionSystem(contexts));
			Add(new ParentTransformSystem(contexts));
			Add(new RotationSystem(contexts));
			Add(new DisableViewSystem(contexts));
		}
	}
}