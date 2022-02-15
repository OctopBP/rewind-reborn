using Rewind.ECSCore.Features;
using Rewind.Services;
using Rewind.Systems.ServiceRegistration;
using UnityEngine;

namespace Rewind.ECSCore {
	public class CoreBootstrap : MonoBehaviour {
		Contexts contexts;
		Entitas.Systems systems;
		Services.Services services;

		void Start() {
			contexts = Contexts.sharedInstance;
			services = new(new UnityTimeService(), new UnityInputService());
			systems = createSystems(contexts, services);
			systems.Initialize();
		}

		void Update() {
			systems.Execute();
			systems.Cleanup();
		}

		static Entitas.Systems createSystems(Contexts contexts, Services.Services services) =>
			new Feature(nameof(Systems))
				.Add(new ServiceRegistrationSystems(contexts, services))
				.Add(new ClockSystems(contexts))
				.Add(new GameSystems(contexts))
				.Add(new RenderSystems(contexts));
	}
}