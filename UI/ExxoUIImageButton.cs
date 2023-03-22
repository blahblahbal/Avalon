using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Avalon.UI;

internal class ExxoUIImageButton : ExxoUIImage
{
    public ExxoUIImageButton(Asset<Texture2D> texture) : base(texture) { }
    public float OpacityActive { get; set; } = 1f;
    public float OpacityInactive { get; set; } = 0.4f;

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        Color oldColor = Color;
        Color *= IsMouseHovering ? OpacityActive : OpacityInactive;
        base.DrawSelf(spriteBatch);
        Color = oldColor;
    }

    protected override void FirstMouseOver(UIMouseEvent evt)
    {
        base.FirstMouseOver(evt);
        SoundEngine.PlaySound(SoundID.MenuTick);
    }
}
