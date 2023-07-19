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
		[Title("Custom")]
		[SerializeField, Readonly] UnityOption<AutotestInputService> autotestInputService;

		[Title("Level elements")]
		[SerializeField, Required, Readonly] GameSettingsBehaviour gameSettings;
		[SerializeField, Required, Readonly] Clock clock;
		[SerializeField, Required, Readonly] Player player;
		[SerializeField, Required, Readonly] Clone clone;
		
		[SerializeField, Readonly] LevelAudio levelAudio;
		
		public class Init : IDisposable {
			readonly Contexts contexts;
			readonly Entitas.Systems systems;

			readonly Player.Model playerModel;
			readonly Clone.Model cloneModel;
			
			public readonly LevelAudio levelAudio;

			public Init(CoreBootstrap backing) {
				levelAudio = backing.levelAudio;
				
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
			
			public void placeCharacterToPoint(PathPoint spawnPoint, Vector2 startPosition) {
				playerModel.placeToPoint(spawnPoint, startPosition);
				cloneModel.placeToPoint(spawnPoint, startPosition);
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