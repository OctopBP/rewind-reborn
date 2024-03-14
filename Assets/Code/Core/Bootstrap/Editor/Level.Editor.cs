#if UNITY_EDITOR
using Rewind.Behaviours;
using Rewind.ECSCore;
using Rewind.ECSCore.Editor;
using Sirenix.OdinInspector;
using TablerIcons;
using UnityEngine;

namespace Rewind.Core
{
	public partial class Level
	{
		[Button, PropertyOrder(-1), HorizontalGroup("Split", 0.5f)]
		private void WritePosition()
		{
			rootPosition = transform.position;
		}
		
		[Button, PropertyOrder(-1), VerticalGroup("Split/right")]
		private void SetPosition()
		{
			transform.position = rootPosition;
		}

		[Button]
		private void BakeLevel()
		{
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

		private void OnDrawGizmos()
		{
			WalkPathEditorExt.DrawPointIcon(paths, startPoint, Icons.IconPlayerPlayFilled, Vector2.up * 0.5f);
			WalkPathEditorExt.DrawPointIcon(paths, finishPoint, Icons.IconDoorExit, Vector2.up * 0.5f);
		}
	}
}
#endif