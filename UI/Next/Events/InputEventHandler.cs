using System;

namespace Avalon.UI.Next.Events;

public class InputEventArgs : EventArgs {
    public InputEventArgs(ExxoUIElement target) {
        Target = target;
    }

    public readonly ExxoUIElement Target;
}

public delegate void InputEventHandler(object? sender, InputEventArgs e);
