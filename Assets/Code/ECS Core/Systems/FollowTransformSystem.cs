using Entitas;

public class FollowTransformSystem : IExecuteSystem {
	readonly IGroup<GameEntity> followers;

	public FollowTransformSystem(Contexts contexts) {
		followers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Position, GameMatcher.FollowTransform
		));
	}

	public void Execute() {
		foreach (var follower in followers.GetEntities()) {
			follower.ReplacePosition(follower.followTransform.value.position);
		}
	}
}