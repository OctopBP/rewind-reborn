#if UNITY_EDITOR
using System.Linq;
using Rewind.ECSCore;
using Rewind.ViewListeners;
using Sirenix.OdinInspector.Editor.Validation;

[assembly: RegisterValidator(typeof(LevelValidator))]
public class LevelValidator : SceneValidator {
	protected override void Validate(ValidationResult result) {
		var entityIdBehaviours = FindAllComponentsInSceneOfType<EntityIdBehaviour>().Select(_ => (_.id, _.name));
		var pathsTpl = FindAllComponentsInSceneOfType<WalkPath>().Select(_ => (id: _._pathId, _.name));
		var laddersTpl = FindAllComponentsInSceneOfType<Ladder>().Select(_ => (id: _._pathId, _.name));
		var conflicts = entityIdBehaviours.Concat(pathsTpl).Concat(laddersTpl)
			.GroupBy(e => e.id.guid).Where(_ => _.Count() > 1);

		foreach (var conflict in conflicts) {
			result.AddError($"Id conflict detected id: {conflict.Key} {string.Join(", ", conflict.Select(_ => _.name))}");
		}
	}
}
#endif
