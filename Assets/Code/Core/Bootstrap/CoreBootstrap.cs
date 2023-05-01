using System;
using Rewind.ECSCore.Features;
using Rewind.Services;
using Rewind.Services.Autotest;
using Rewind.Systems.ServiceRegistration;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Core {
	public class CoreBootstrap : MonoBehaviour {
		[SerializeField, Required] Level level;
		[SerializeField] UnityOption<AutotestInputService> autotestInputService;
		
		public class Model: IDisposable {
			public readonly Level.Model levelMode;
			readonly Contexts contexts;
			readonly Entitas.Systems systems;

			public Model(CoreBootstrap backing) {
				levelMode = new Level.Model(backing.level);
				
				contexts = Contexts.sharedInstance;
				var services = new Services.Services(new UnityTimeService(), backing.autotestInputService.value.Match(
					ai => (IInputService) ai,
					new UnityInputService()
				));
				systems = createSystems(contexts, services);
				systems.Initialize();
			}

			public void update() {
				systems.Execute();
				systems.Cleanup();
			}
			
			static Entitas.Systems createSystems(Contexts contexts, Services.Services services) =>
				new Feature(nameof(Systems))
					.Add(new ServiceRegistrationSystems(contexts, services))
					.Add(new ClockSystems(contexts))
					.Add(new GameSystems(contexts))
					.Add(new RenderSystems(contexts));

			public void Dispose() {
				systems.TearDown();
				systems.DeactivateReactiveSystems();
				systems.ClearReactiveSystems();
				contexts.Reset();
				foreach (var context in contexts.allContexts) {
					context.DestroyAllEntities();
				}
			}
		}
	}
}