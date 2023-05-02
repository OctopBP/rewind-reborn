using Code.Helpers.Tracker;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Infrastructure;
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
					.SetIsClone()
					.SetIsCharacter()
					.SetIsMovable()
					.SetIsPathFollower()
					.AddView(backing.gameObject)
					.AddPosition(backing.transform.position)
					.AddCharacterState(CharacterState.Idle)
					.AddMoveState(MoveState.None)
					.AddPathFollowerSpeed(gameSettings.characterSpeed)
					.AddMoveComplete(true)
					.AddPositionListener(backing)
					.AddAnyClockStateListener(animator)
					.AddCharacterLookDirectionListener(animator)
					.AddCharacterStateListener(animator);
			}
			
			public void placeToPoint(PathPoint spawnPoint) => entity.AddCurrentPoint(spawnPoint);
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}