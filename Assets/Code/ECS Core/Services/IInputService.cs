namespace Rewind.Services {
	public interface IInputService {
		bool getMoveRightButton();
		bool getMoveRightButtonDown();
		bool getMoveRightButtonUp();

		bool getMoveLeftButton();
		bool getMoveLeftButtonDown();
		bool getMoveLeftButtonUp();

		bool getMoveUpButton();
		bool getMoveUpButtonDown();
		bool getMoveUpButtonUp();

		bool getMoveDownButton();
		bool getMoveDownButtonDown();
		bool getMoveDownButtonUp();

		bool getInteractButton();
		bool getInteractButtonDown();
		bool getInteractButtonUp();
		
		bool getRewindButton();
		bool getRewindButtonDown();
		bool getRewindButtonUp();
	}
}