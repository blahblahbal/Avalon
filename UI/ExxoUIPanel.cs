using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;

namespace Avalon.UI;

public class ExxoUIPanel : ExxoUIAdapter<UIPanel>
{
    public ExxoUIPanel() : base(new UIPanel()) { }

    public static Color DefaultBackgroundColor => new Color(63, 82, 151) * 0.7f;
    public static Color DefaultBorderColor => Color.Black;

    public Color BorderColor
    {
        get => ChildBase.BorderColor;
        set => ChildBase.BorderColor = value;
    }

    public Color BackgroundColor
    {
        get => ChildBase.BackgroundColor;
        set => ChildBase.BackgroundColor = value;
    }

    public void ResetColor()
    {
        BorderColor = DefaultBorderColor;
        BackgroundColor = DefaultBackgroundColor;
    }
}
