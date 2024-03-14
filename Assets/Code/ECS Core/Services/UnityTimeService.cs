using System;
using UnityEngine;

namespace Rewind.Services
{
	public class UnityTimeService : ITimeService
	{
		public float deltaTime => Time.deltaTime;
		public float inGameTime => Time.time;
		public DateTime utcNow => DateTime.UtcNow;
	}
}