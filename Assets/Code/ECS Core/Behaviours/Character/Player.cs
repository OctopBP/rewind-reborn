using Code.Helpers.Tracker;
using Rewind.Data;
using Rewind.ECSCore.Enums;
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
					.SetIsPlayer()
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
					.AddAnyClockStateListener(animatorModel)
					.AddCharacterLookDirectionListener(animatorModel)
					.AddCharacterStateListener(animatorModel);
			}

			public void placeToPoint(PathPoint spawnPoint) => entity.AddCurrentPoint(spawnPoint);
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}