using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;

namespace ExxoAvalonOrigins.UI;

public class UITextCustom : UIText
{
    public bool Hidden { get; set; } = false;

    public UITextCustom(string text, float textScale = 1, bool large = false) : base(text, textScale, large)
    {
    }

    public UITextCustom(LocalizedText text, float textScale = 1, bool large = false) : base(text, textScale, large)
    {
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        if (!Hidden)
        {
            base.DrawSelf(spriteBatch);
        }
    }
}