using Rewind.Extensions;
using Rewind.Infrastructure;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class FinishBehaviour : ComponentBehaviour {
		[SerializeField] PathPointType pointIndex;

		public ReactiveProperty<bool> reached { get; } = new(false);		
		public PathPointType point => pointIndex;

		protected override void onAwake() {
			entity.with(x => x.isFinish = true);
			entity.AddPointIndex(pointIndex);
		}
	}
}