using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Clone : EntityLinkBehaviour<Clone.Model, Clone.Model.Args>, IPositionListener {
		[SerializeField] CharacterAnimator animator;
		
		protected override Model createModel(Model.Args args) =>
			new Model(this, args.spawnPoint, args.gameSettings);
		
		public class Model : LinkedEntityModel<GameEntity> {
			public class Args {
				public PathPoint spawnPoint;
				public GameSettingsData gameSettings;
				
				public Args(PathPoint spawnPoint, GameSettingsData gameSettings) {
					this.spawnPoint = spawnPoint;
					this.gameSettings = gameSettings;
				}
			}
			
			public Model(Clone backing, PathPoint spawnPoint, GameSettingsData gameSettings) : base(backing.gameObject) {
				var animator = new CharacterAnimator.Init(backing.animator, gameSettings);
				
				entity
					.with(x => x.isClone = true)
					.with(x => x.isCharacter = true)
					.with(x => x.isMovable = true)
					.with(x => x.isPathFollower = true)
					.with(e => e.AddView(backing.gameObject))
					.with(e => e.AddCurrentPoint(spawnPoint))
					.with(e => e.AddPosition(backing.transform.position))
					.with(e => e.AddCharacterState(CharacterState.Idle))
					.with(e => e.AddMoveState(MoveState.None))
					.with(e => e.AddMoveComplete(true))
					.with(e => e.AddPositionListener(backing))
					.with(e => e.AddAnyClockStateListener(animator))
					.with(e => e.AddCharacterLookDirectionListener(animator))
					.with(e => e.AddCharacterStateListener(animator));
			}
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}