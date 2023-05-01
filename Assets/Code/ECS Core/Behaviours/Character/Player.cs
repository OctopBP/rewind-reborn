using Code.Helpers.Tracker;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Player : MonoBehaviour, IPositionListener {
		[SerializeField] CharacterAnimator animator;
		
		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Player backing, ITracker tracker, GameSettingsData gameSettings) :
				base(backing.gameObject, tracker)
			{
				var animatorModel = new CharacterAnimator.Init(backing.animator, gameSettings);
				entity
					.with(x => x.isPlayer = true)
					.with(x => x.isCharacter = true)
					.with(x => x.isMovable = true)
					.with(x => x.isPathFollower = true)
					.with(e => e.AddView(backing.gameObject))
					.with(e => e.AddPosition(backing.transform.position))
					.with(e => e.AddCharacterState(CharacterState.Idle))
					.with(e => e.AddMoveState(MoveState.None))
					.with(e => e.AddMoveComplete(true))
					.with(e => e.AddPositionListener(backing))
					.with(e => e.AddAnyClockStateListener(animatorModel))
					.with(e => e.AddCharacterLookDirectionListener(animatorModel))
					.with(e => e.AddCharacterStateListener(animatorModel));
			}

			public void placeToPoint(PathPoint spawnPoint) => entity
				.with(e => e.AddCurrentPoint(spawnPoint));
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}