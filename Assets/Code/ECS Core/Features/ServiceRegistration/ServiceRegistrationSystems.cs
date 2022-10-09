using Rewind.Services;

namespace Rewind.Systems.ServiceRegistration {
	public class ServiceRegistrationSystems : Feature {
		public ServiceRegistrationSystems(Contexts contexts, Services.Services services) : base(
			nameof(ServiceRegistrationSystems)
		) {
			Add(new RegisterServiceSystem<ITimeService>(services.time, contexts.game.ReplaceWorldTime));
			Add(new RegisterServiceSystem<IInputService>(services.inputService, contexts.input.ReplaceInput));
		}
	}
}