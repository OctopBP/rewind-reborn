using System;

namespace Rewind.Services.Autotest
{
	public class Button
	{
		private ButtonState state { get; set; } = ButtonState.Opened;

		public void Update(ButtonPress pressStatus)
		{
			state = pressStatus switch
			{
				ButtonPress.Up => ButtonState.Up,
				ButtonPress.Down => ButtonState.Down,
				_ => throw new ArgumentOutOfRangeException(nameof(pressStatus), pressStatus, null)
			};
		}

		public void Tick()
		{
			state = state switch
			{
				ButtonState.Down => ButtonState.Pressed,
				ButtonState.Pressed => ButtonState.Pressed,
				ButtonState.Opened => ButtonState.Opened,
				ButtonState.Up => ButtonState.Opened,
				_ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
			};
		}

		public bool GetButton() => state == ButtonState.Pressed;
		public bool GetButtonDown() => state == ButtonState.Down;
		public bool GetButtonUp() => state == ButtonState.Up;
	}
}