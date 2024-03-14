using Code.Helpers.Tracker;
using Rewind.SharedData;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore
{
	public class Player : MonoBehaviour, IPositionListener
	{
		[SerializeField] private CharacterAnimator animator;
		
		public class Model : LinkedEntityModel<GameEntity>
		{
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
					// TODO: Should we also multiply PathFollowerSpeed by gameSettings._clockNormalSpeed?
					// If so we should also change PathFollowerSpeed on time direction change
					.AddPathFollowerSpeed(gameSettings._stepSize)
					.AddMoveComplete(true)
					.AddCharacterLookDirection(CharacterLookDirection.Right)
					.AddTraveledValue(0)
					.AddPositionListener(backing)
					.AddAnyClockStateListener(animatorModel)
					.AddCharacterLookDirectionListener(animatorModel)
					.AddCharacterStateListener(animatorModel);
			}

			public void PlaceToPoint(PathPoint spawnPoint, Vector2 startPosition) => entity
				.ReplaceCurrentPoint(spawnPoint)
				.ReplacePosition(startPosition);
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}