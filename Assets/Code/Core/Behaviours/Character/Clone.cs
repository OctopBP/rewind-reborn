using Code.Helpers.Tracker;
using Rewind.SharedData;
using Rewind.Infrastructure;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Clone : MonoBehaviour, IPositionListener {
		[SerializeField] CharacterAnimator animator;
		
		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Clone backing, ITracker tracker, GameSettingsData gameSettings) :
				base(backing.gameObject, tracker)
			{
				var animator = new CharacterAnimator.Init(backing.animator, gameSettings);
				
				entity
					.SetClone(true)
					.SetCharacter(true)
					.SetMovable(true)
					.SetPathFollower(true)
					.AddView(backing.gameObject)
					.AddPosition(backing.transform.position)
					.AddCharacterState(CharacterState.Idle)
					.AddMoveState(MoveState.None)
					.AddPathFollowerSpeed(gameSettings._characterSpeed)
					.AddMoveComplete(true)
					.AddPositionListener(backing)
					.AddAnyClockStateListener(animator)
					.AddCharacterLookDirectionListener(animator)
					.AddCharacterStateListener(animator);
			}
			
			public void placeToPoint(PathPoint spawnPoint, Vector2 startPosition) => entity
				.ReplaceCurrentPoint(spawnPoint)
				.ReplacePosition(startPosition);
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}