using Rewind.Extensions;
using Rewind.Infrastructure;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class Finish : MonoBehaviour {
		[SerializeField] PathPointType pointIndex;

		public Model initialize() => new Model(pointIndex);

		public class Model : EntityModel<GameEntity>, IFinishReachedListener {
			public readonly ReactiveCommand reached = new ReactiveCommand();

			public Model(PathPointType pointIndex) => entity
				.with(e => e.isFinish = true)
				.with(e => e.AddPointIndex(pointIndex))
				.with(e => e.AddFinishReachedListener(this));

			public void OnFinishReached(GameEntity _) => reached.Execute();
		}
	}
}