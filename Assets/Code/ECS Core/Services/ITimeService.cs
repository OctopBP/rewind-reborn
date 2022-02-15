using System;

namespace Rewind.Services {
	public interface ITimeService {
		float deltaTime { get; }
		float inGameTime { get; }
		DateTime utcNow { get; }
	}
}