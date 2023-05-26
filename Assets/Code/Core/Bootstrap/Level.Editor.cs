#if UNITY_EDITOR
using System.Linq;
using Rewind.Behaviours;
using Rewind.ECSCore;
using Rewind.ECSCore.Editor;
using Rewind.ViewListeners;
using Sirenix.OdinInspector;
using TablerIcons;
using UnityEngine;

namespace Rewind.Core {
	public partial class Level {
		[Button, PropertyOrder(-1), HorizontalGroup("Split", 0.5f)]
		void writePosition() {
			rootPosition = transform.position;
		}
		
		[Button, PropertyOrder(-1), VerticalGroup("Split/right")]
		void setPosition() {
			transform.position = rootPosition;
		}

		[Button]
		void bakeLevel() {
			paths = FindObjectsOfType<WalkPath>();
			ladders = FindObjectsOfType<Ladder>();
			connectors = FindObjectsOfType<PathConnector>();
			buttonsA = FindObjectsOfType<ButtonA>();
			doorsA = FindObjectsOfType<DoorA>();
			gearTypeA = FindObjectsOfType<GearTypeA>();
			gearTypeB = FindObjectsOfType<GearTypeB>();
			gearTypeC = FindObjectsOfType<GearTypeC>();
			leversA = FindObjectsOfType<LeverA>();
			platformsA = FindObjectsOfType<PlatformA>();
			puzzleGroups = FindObjectsOfType<PuzzleGroup>();
			actionTriggers = FindObjectsOfType<ActionTrigger>();
		}
		
		
		[Button]
		void validate() {
			var entityIdBehaviours = FindObjectsOfType<EntityIdBehaviour>().Select(_ => (id: _.id, _.name));
			var paths = FindObjectsOfType<WalkPath>().Select(_ => (id: _._pathId, _.name));
			var ladders = FindObjectsOfType<Ladder>().Select(_ => (id: _._pathId, _.name));
			var conflicts = entityIdBehaviours.Concat(paths).Concat(ladders)
				.GroupBy(e => e.id.guid).Where(_ => _.Count() > 1);

			foreach (var conflict in conflicts) {
				Debug.LogError($"Id conflict detected id: {conflict.Key} {string.Join(", ", conflict.Select(_ => _.name))}");
			}
			Debug.Log("Validation finished");
		}

		void OnDrawGizmos() {
			WalkPathEditorExt.drawPointIcon(paths, startPoint, Icons.IconPlayerPlayFilled, Vector2.up * 0.5f);
			WalkPathEditorExt.drawPointIcon(paths, finishPoint, Icons.IconDoorExit, Vector2.up * 0.5f);
		}
	}
}
#endif