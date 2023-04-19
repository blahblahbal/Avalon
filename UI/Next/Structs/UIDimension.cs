using Avalon.UI.Next.Enums;

namespace Avalon.UI.Next.Structs;

public readonly struct UIDimension {
    public readonly float Value = 0;
    public readonly ScreenUnit Unit = ScreenUnit.Pixels;

    public UIDimension(float value, ScreenUnit unit) {
        Value = value;
        Unit = unit;
    }

    public static UIDimension Fill => new(100, ScreenUnit.Percent);
}
