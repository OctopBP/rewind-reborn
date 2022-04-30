using Rewind.Behaviours;
using Rewind.ECSCore.Features;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Rewind.Services;
using Rewind.Systems.ServiceRegistration;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Rewind.ECSCore {
	public class CoreBootstrap : MonoBehaviour, IStart, IUpdate, IOnDestroy {
		[SerializeField] bool useAutotest;
		[SerializeField] AutotestInputService autotestInputService;

		[Space, SerializeField, Required] PlayerBehaviour player;
		[SerializeField, Required] CloneBehaviour clone;
		[SerializeField] PathPointType startIndex;
		[SerializeField] float speed;

		[Space, SerializeField, Required] FinishBehaviour finishTrigger;

		Contexts contexts;
		Entitas.Systems systems;
		Services.Services services;

		public ReactiveProperty<bool> levelCompleted => finishTrigger.reached;

		public void Start() {
			player.init(startIndex, speed);
			clone.init(startIndex, speed);

			contexts = Contexts.sharedInstance;
			services = new(new UnityTimeService(), useAutotest ? autotestInputService : new UnityInputService());
			systems = createSystems(contexts, services);
			systems.Initialize();
		}

		public void Update() {
			systems.Execute();
			systems.Cleanup();
		}

		public void OnDestroy() {
			systems.TearDown();
			systems.DeactivateReactiveSystems();
			systems.ClearReactiveSystems();
			contexts.Reset();
			foreach (var context in contexts.allContexts) {
				context.DestroyAllEntities();
			}
		}

		static Entitas.Systems createSystems(Contexts contexts, Services.Services services) =>
			new Feature(nameof(Systems))
				.Add(new ServiceRegistrationSystems(contexts, services))
				.Add(new ClockSystems(contexts))
				.Add(new GameSystems(contexts))
				.Add(new RenderSystems(contexts));
	}
}