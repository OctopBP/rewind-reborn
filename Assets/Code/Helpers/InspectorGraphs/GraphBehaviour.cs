using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs
{
	public class GraphBehaviour : MonoBehaviour
    {
		[SerializeField] private int accuracy = 1;
		[SerializeField] private bool fixedTime;
		[SerializeField, TableList] public List<GraphInfo> infos = new();

		public static Init init;

		private void Start()
        {
			init = new(infos, accuracy);
		}

		private void LateUpdate()
        {
			if (!fixedTime) init.Update();
		}

		private void FixedUpdate()
        {
			if (fixedTime) init.Update();
		}

		public class Init
        {
			private readonly List<GraphInfo> infos;
			private readonly int accuracy;

			public record TimeLine(float Time, TimeLine.Type TimeLineType)
            {
				public enum Type { StartRecord, Record, Rewind, Replay }

				public float Time { get; } = Time;
				public Type TimeLineType { get; } = TimeLineType;
			}

			public readonly List<TimeLine> TimeLines = new();

			private int tics;

			public Init(List<GraphInfo> infos, int accuracy)
            {
				this.infos = infos;
				this.accuracy = accuracy;
			}

			public void Update()
            {
				if (tics++ % accuracy != 0) return;

				foreach (var info in infos)
                {
					info.WriteValue();
				}
			}
		}
	}
}