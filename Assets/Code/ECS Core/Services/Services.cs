namespace Rewind.Services {
	public class Services {
		public readonly ITimeService time;
		public readonly IInputService inputService;
		
		public Services(ITimeService time, IInputService inputService) {
			this.time = time;
			this.inputService = inputService;
		}
	}
}