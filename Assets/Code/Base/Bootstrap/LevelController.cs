using Rewind.Behaviours;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Rewind.ECSCore {
	public class LevelController : MonoBehaviour, IStart {
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
		}
	}
}