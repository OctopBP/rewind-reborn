using System.Collections;
using Code.Base;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Rewind.ECSCore {
	public class Clock : EntityLinkBehaviour<Clock.Model>, IGameTimeListener, IAnyClockStateListener, IStatusValue {
		[Header("State")]
		[SerializeField, Required] Image bg;
		[SerializeField, Required] Color recordColor;
		[SerializeField, Required] Color rewindColor;
		[SerializeField, Required] Color replayColor;
		[SerializeField, Required] Volume volume;
		[SerializeField, Required] float transitionTime;

		[Header("Time")]
		[SerializeField, Required] Transform arrow;
		[SerializeField, Required] TMP_Text text;
		[SerializeField] float circleTime = 20;

		public float statusValue => model.entity.time.value;

		protected override Model createModel() => new Model(this);

		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Clock clock) : base(clock.gameObject) => entity
				.with(e => e.isClock = true)
				.with(e => e.AddClockState(ClockState.Record))
				.with(e => e.AddTime(0))
				.with(e => e.AddAnyClockStateListener(clock))
				.with(e => e.AddGameTimeListener(clock));
		}

		public void OnTime(GameEntity _, float value) {
			arrow.localRotation = Quaternion.AngleAxis(value.remap0(circleTime, 360), Vector3.back);
			text.SetText($"{value:F1}");
		}

		public void OnAnyClockState(GameEntity _, ClockState value) {
			bg.color = value switch {
				ClockState.Record => recordColor,
				ClockState.Rewind => rewindColor,
				ClockState.Replay => replayColor,
				_ => Color.white
			};

			StartCoroutine(setVolumeWeight(value.isRewind() ? 1 : 0));
		}

		IEnumerator setVolumeWeight(float weight) {
			var start = volume.weight; 
			for (var t = 0f; t < transitionTime; t += Time.deltaTime) {
				volume.weight = Mathf.Lerp(start, weight, t / transitionTime);
				yield return null;
			}
		
			volume.weight = weight;
		}
	}
}