using System;
using Code.Helpers.Tracker;
using Rewind.ECSCore;
using Rewind.ECSCore.Features;
using Rewind.Services;
using Rewind.Services.Autotest;
using Rewind.Systems.ServiceRegistration;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Core {
	public class CoreBootstrap : MonoBehaviour {
		[SerializeField] UnityOption<AutotestInputService> autotestInputService;

		[SerializeField, Required] GameSettingsBehaviour gameSettings;
		[SerializeField, Required] Clock clock;
		[SerializeField, Required] Player player;
		[SerializeField, Required] Clone clone;
		
		public class Init: IDisposable {
			readonly Contexts contexts;
			readonly Entitas.Systems systems;

			readonly Player.Model playerModel;
			readonly Clone.Model cloneModel;

			public Init(CoreBootstrap backing) {
				var tracker = new DisposableTracker();
				backing.gameSettings.initialize();
				backing.clock.initialize(tracker);
				
				playerModel = new Player.Model(backing.player, tracker, backing.gameSettings.gameSettingsData);
				cloneModel = new Clone.Model(backing.clone, tracker, backing.gameSettings.gameSettingsData);
				
				contexts = Contexts.sharedInstance;
				var services = new Services.Services(
					new UnityTimeService(),
					backing.autotestInputService.value.Match<IInputService>(_ => _, new UnityInputService())
				);
				systems = createSystems(contexts, services);
				systems.Initialize();
			}
			
			public void placeCharacterToPoint(PathPoint spawnPoint) {
				playerModel.placeToPoint(spawnPoint);
				cloneModel.placeToPoint(spawnPoint);
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