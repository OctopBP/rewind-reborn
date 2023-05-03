using Code.Helpers.Tracker;
using Rewind.SharedData;
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
					.SetPlayer(true)
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
					.AddAnyClockStateListener(animatorModel)
					.AddCharacterLookDirectionListener(animatorModel)
					.AddCharacterStateListener(animatorModel);
			}

			public void placeToPoint(PathPoint spawnPoint) => entity.ReplaceCurrentPoint(spawnPoint);
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}