using Entitas;
using UnityEngine;

namespace Rewind.ECSCore {
	public class CoreBootstrap : MonoBehaviour {
		Contexts contexts;
		Systems systems;

		void Start() {
			contexts = Contexts.sharedInstance;
			systems = createSystems(contexts);
			systems.Initialize();
		}

		void Update() {
			systems.Execute();
			systems.Cleanup();
		}

		static Systems createSystems(Contexts contexts) => new Feature(nameof(Systems));
	}
}