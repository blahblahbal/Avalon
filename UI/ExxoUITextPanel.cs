using Terraria.Localization;

namespace Avalon.UI;

public class ExxoUITextPanel : ExxoUIPanel
{
    public ExxoUITextPanel(string text, float textScale = 1f, bool large = false) : this(new ExxoUIText(text, textScale,
        large))
    {
    }

    public ExxoUITextPanel(LocalizedText text, float textScale = 1f, bool large = false) : this(new ExxoUIText(text,
        textScale, large))
    {
    }

    protected ExxoUITextPanel(ExxoUIText textElement)
    {
        TextElement = textElement;
        TextElement.OnInternalTextChange += (sender, _) =>
        {
            if (sender is not ExxoUIText text)
            {
                return;
            }

            MinWidth.Pixels = text.MinWidth.Pixels + PaddingLeft + PaddingRight;
            MinHeight.Pixels = text.MinHeight.Pixels + PaddingBottom + PaddingTop;
        };
        Append(TextElement);
    }

    public ExxoUIText TextElement { get; }
}
