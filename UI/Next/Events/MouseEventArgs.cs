using Microsoft.Xna.Framework;

namespace Avalon.UI.Next.Events;

public class MouseEventArgs : InputEventArgs {
    public MouseEventArgs(ExxoUIElement target, Vector2 mousePosition) : base(target) {
        MousePosition = mousePosition;
    }

    public readonly Vector2 MousePosition;
}
