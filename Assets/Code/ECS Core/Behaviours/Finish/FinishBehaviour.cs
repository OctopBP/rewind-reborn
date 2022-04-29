using Entitas;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using UniRx;
using UnityEngine;

namespace Rewind.Behaviours {
	public class FinishBehaviour : SelfInitializedView, IEventListener, IFinishReachedListener {
		[SerializeField] PathPointType pointIndex;

		public ReactiveProperty<bool> reached { get; } = new(false);		
		public PathPointType point => pointIndex;

		protected override void onAwake() {
			base.onAwake();
			setupPuzzleGroup();
		}

		void setupPuzzleGroup() {
			entity.with(x => x.isFinish = true);
			entity.AddPointIndex(pointIndex);
		}

		public void registerListeners(IEntity _) => entity.AddFinishReachedListener(this);
		public void unregisterListeners(IEntity _) => entity.RemoveFinishReachedListener(this);
		public void OnFinishReached(GameEntity _) => reached.Value = true;
	}
}