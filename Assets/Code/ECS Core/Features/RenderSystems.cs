namespace Rewind.ECSCore.Features {
	public class RenderSystems : Feature {
		public RenderSystems(Contexts contexts) : base(nameof(RenderSystems)) {
			Add(new PositionSystem(contexts));
			Add(new RotationSystem(contexts));
		}
	}
}