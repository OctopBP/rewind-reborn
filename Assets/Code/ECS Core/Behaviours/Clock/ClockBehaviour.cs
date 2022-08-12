using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class ClockBehaviour : ComponentBehaviour {
		[SerializeField] ClockData data;

		protected override void onAwake() {
			entity.with(x => x.isClock = true);
			entity.AddClockState(ClockState.Record);
			entity.AddClockData(data);
			entity.AddTime(0);
		}
	}
}