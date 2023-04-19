using Avalon.UI.Next.Enums;
using Microsoft.Xna.Framework;

namespace Avalon.UI.Next.Events;

public class MouseButtonEventArgs : MouseEventArgs {
    public MouseButtonEventArgs(ExxoUIElement target, Vector2 mousePosition, ButtonType buttonType, bool pressed,
                                bool doubleClick, float? factor = null) : base(target, mousePosition) {
        ButtonType = buttonType;
        Pressed = pressed;
        DoubleClick = doubleClick;
        Factor = factor;
    }

    public readonly bool Pressed;
    public readonly bool DoubleClick;
    public readonly ButtonType ButtonType;
    public readonly float? Factor;
}
