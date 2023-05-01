using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Player : EntityLinkBehaviour<Player.Model, Player.Model.Args>, IPositionListener {
		[SerializeField] CharacterAnimator animator;
		
		protected override Model createModel(Model.Args args) => new Model(this, args);

		public class Model : LinkedEntityModel<GameEntity> {
			public class Args {
				public PathPoint spawnPoint;
				public GameSettingsData gameSettings;
				
				public Args(PathPoint spawnPoint, GameSettingsData gameSettings) {
					this.spawnPoint = spawnPoint;
					this.gameSettings = gameSettings;
				}
			}

			public Model(Player backing, Args args) : base(backing.gameObject) {
				var animatorModel = new CharacterAnimator.Init(backing.animator, args.gameSettings);
				entity
					.with(x => x.isPlayer = true)
					.with(x => x.isCharacter = true)
					.with(x => x.isMovable = true)
					.with(x => x.isPathFollower = true)
					.with(e => e.AddView(backing.gameObject))
					.with(e => e.AddCurrentPoint(args.spawnPoint))
					.with(e => e.AddPosition(backing.transform.position))
					.with(e => e.AddCharacterState(CharacterState.Idle))
					.with(e => e.AddMoveState(MoveState.None))
					.with(e => e.AddMoveComplete(true))
					.with(e => e.AddPositionListener(backing))
					.with(e => e.AddAnyClockStateListener(animatorModel))
					.with(e => e.AddCharacterLookDirectionListener(animatorModel))
					.with(e => e.AddCharacterStateListener(animatorModel));
			}
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}