using Code.Helpers.Tracker;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class Finish : MonoBehaviour {
		[SerializeField] public PathPoint pointIndex;

		public class Model : TrackedEntityModel<GameEntity>, IFinishReachedListener {
			public readonly ReactiveCommand reached = new ReactiveCommand();

			public Model(Finish backing, ITracker tracker) : base(tracker) => entity
				.SetIsFinish()
				.AddCurrentPoint(backing.pointIndex)
				.AddFinishReachedListener(this);

			public void OnFinishReached(GameEntity _) => reached.Execute();
		}
	}
}