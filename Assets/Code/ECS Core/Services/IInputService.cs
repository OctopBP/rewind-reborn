namespace Rewind.Services
{
	public interface IInputService
	{
		bool GetMoveRightButton();
		bool GetMoveRightButtonDown();
		bool GetMoveRightButtonUp();

		bool GetMoveLeftButton();
		bool GetMoveLeftButtonDown();
		bool GetMoveLeftButtonUp();

		bool GetMoveUpButton();
		bool GetMoveUpButtonDown();
		bool GetMoveUpButtonUp();

		bool GetMoveDownButton();
		bool GetMoveDownButtonDown();
		bool GetMoveDownButtonUp();

		bool GetInteractButton();
		bool GetInteractButtonDown();
		bool GetInteractButtonUp();

		bool GetInteractSecondButton();
		bool GetInteractSecondButtonDown();
		bool GetInteractSecondButtonUp();

		bool GetRewindButton();
		bool GetRewindButtonDown();
		bool GetRewindButtonUp();
	}
}