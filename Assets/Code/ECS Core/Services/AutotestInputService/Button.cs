using System;

namespace Rewind.Services.Autotest {
	public class Button {
		ButtonState state { get; set; } = ButtonState.Opened;

		public void update(ButtonPress pressStatus) {
			state = pressStatus switch {
				ButtonPress.Up => ButtonState.Up,
				ButtonPress.Down => ButtonState.Down,
				_ => throw new ArgumentOutOfRangeException(nameof(pressStatus), pressStatus, null)
			};
		}

		public void tick() {
			state = state switch {
				ButtonState.Down => ButtonState.Pressed,
				ButtonState.Pressed => ButtonState.Pressed,
				ButtonState.Opened => ButtonState.Opened,
				ButtonState.Up => ButtonState.Opened,
				_ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
			};
		}

		public bool getButton() => state == ButtonState.Pressed;
		public bool getButtonDown() => state == ButtonState.Down;
		public bool getButtonUp() => state == ButtonState.Up;
	}
}