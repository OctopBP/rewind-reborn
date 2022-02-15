namespace Rewind.Services {
	public class Services {
		readonly public ITimeService time;
		readonly public IInputService inputService;
		
		public Services(ITimeService time, IInputService inputService) {
			this.time = time;
			this.inputService = inputService;
		}
	}
}