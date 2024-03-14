using System;
using Code.Helpers.Tracker;
using Rewind.ECSCore;
using Rewind.ECSCore.Features;
using Rewind.Services;
using Rewind.Services.Autotest;
using Rewind.Systems.ServiceRegistration;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Core
{
	public class CoreBootstrap : MonoBehaviour
	{
		[Title("Custom")]
		[SerializeField] private UnityOption<AutotestInputService> autotestInputService;

		[Title("Level elements")]
		[SerializeField, Required] private GameSettingsBehaviour gameSettings;
		[SerializeField, Required] private Clock clock;
		[SerializeField, Required] private Player player;
		[SerializeField, Required] private Clone clone;
		
		[SerializeField] private LevelAudio levelAudio;
		
		public class Init : IDisposable
		{
			private readonly Contexts contexts;
			private readonly Entitas.Systems systems;

			private readonly Player.Model playerModel;
			private readonly Clone.Model cloneModel;
			
			public readonly LevelAudio LevelAudio;

			public Init(CoreBootstrap backing)
			{
				LevelAudio = backing.levelAudio;
				
				var tracker = new DisposableTracker();
				backing.gameSettings.Initialize();
				backing.clock.Initialize(tracker);
				
				playerModel = new Player.Model(backing.player, tracker, backing.gameSettings.gameSettingsData);
				cloneModel = new Clone.Model(backing.clone, tracker, backing.gameSettings.gameSettingsData);
				
				contexts = Contexts.sharedInstance;
				var services = new Services.Services(
					new UnityTimeService(),
					backing.autotestInputService.Value.Match<IInputService>(_ => _, new UnityInputService())
				);
				systems = CreateSystems(contexts, services);
				systems.Initialize();
			}
			
			public void PlaceCharacterToPoint(PathPoint spawnPoint, Vector2 startPosition)
            {
				playerModel.PlaceToPoint(spawnPoint, startPosition);
				cloneModel.PlaceToPoint(spawnPoint, startPosition);
			}

			public void Update()
            {
				systems.Execute();
				systems.Cleanup();
			}

			private static Entitas.Systems CreateSystems(Contexts contexts, Services.Services services) =>
				new Feature(nameof(Systems))
					.Add(new ServiceRegistrationSystems(contexts, services))
					.Add(new ClockSystems(contexts))
					.Add(new GameSystems(contexts))
					.Add(new RenderSystems(contexts));

			public void Dispose()
            {
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