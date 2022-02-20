using System;
using Entitas;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	[Serializable]
	class StatusIndicator {
		public string text;
		public Color color;
	}

	public class GearTypeABehaviour : SelfInitializedView, IEventListener, IGearTypeAStateListener {
		[SerializeField] GearTypeAData data;
		[SerializeField] int pointIndex;
		[SerializeField] int pathIndex;

		[Header("Status indication")] [SerializeField]
		TMP_Text statusText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator closingStatus;
		[SerializeField] StatusIndicator openingStatus;
		[SerializeField] StatusIndicator openedStatus;

		protected override void onAwake() {
			base.onAwake();
			setupGear();
		}

		void setupGear() {
			entity.AddId(Guid.NewGuid());

			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isGearTypeA = true);

			entity.AddGearTypeAData(data);
			entity.AddGearTypeAState(GearTypeAState.Closed);

			entity.AddPathIndex(pathIndex);
			entity.AddPointIndex(pointIndex);

			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}

		public void registerListeners(IEntity _) =>
			entity.AddGearTypeAStateListener(this);

		public void unregisterListeners(IEntity _) =>
			entity.RemoveGearTypeAStateListener(this);

		public void OnGearTypeAState(GameEntity _, GearTypeAState value) {
			var status = value switch {
				GearTypeAState.Closed => closedStatus,
				GearTypeAState.Closing => closingStatus,
				GearTypeAState.Opened => openedStatus,
				GearTypeAState.Opening => openingStatus,
				_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
			};

			statusText.SetText(status.text);
			statusText.color = status.color;
		}
	}
}