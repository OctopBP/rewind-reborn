namespace Rewind.Services {
	public interface IInputService {
		bool getMoveRightButton();
		bool getMoveRightButtonDown();
		bool getMoveRightButtonUp();

		bool getMoveLeftButton();
		bool getMoveLeftButtonDown();
		bool getMoveLeftButtonUp();

		bool getInteractButton();
		bool getInteractButtonDown();
		bool getInteractButtonUp();
		
		bool getRewindButton();
		bool getRewindButtonDown();
		bool getRewindButtonUp();
	}
}