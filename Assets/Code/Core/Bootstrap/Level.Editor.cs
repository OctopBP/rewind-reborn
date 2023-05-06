#if UNITY_EDITOR
using Code.Helpers;
using Rewind.Behaviours;
using Rewind.ECSCore;
using Sirenix.OdinInspector;
using TablerIcons;
using UnityEngine;
using static TablerIcons.TablerIcons;

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
			connectors = FindObjectsOfType<Connector>();
			buttonsA = FindObjectsOfType<ButtonA>();
			doorsA = FindObjectsOfType<DoorA>();
			gearTypeA = FindObjectsOfType<GearTypeA>();
			gearTypeB = FindObjectsOfType<GearTypeB>();
			gearTypeC = FindObjectsOfType<GearTypeC>();
			leversA = FindObjectsOfType<LeverA>();
			platformsA = FindObjectsOfType<PlatformA>();
			puzzleGroups = FindObjectsOfType<PuzzleGroup>();
		}

		void OnDrawGizmos() {
			maybeDrawPointIcon(startPoint, Icons.IconPlayerPlayFilled);
			maybeDrawPointIcon(finishPoint, Icons.IconDoorExit);

			void maybeDrawPointIcon(PathPoint point, string iconName) => paths
				.findById(point.pathId)
				.Map(p => p.getWorldPosition(point.index)).IfSome(
					position => {
						var iconPos = position + Vector2.up * 0.5f;
						DrawIconGizmo(iconPos, iconName, ColorA.green);
						Gizmos.color = ColorA.gray;
						Gizmos.DrawLine(position, iconPos);
					});
			;
		}

	}
}
#endif