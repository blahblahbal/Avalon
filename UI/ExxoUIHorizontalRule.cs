using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace Avalon.UI;

internal class ExxoUIHorizontalRule : ExxoUIElement
{
    private readonly Asset<Texture2D> dividerTexture;

    public ExxoUIHorizontalRule()
    {
        dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", AssetRequestMode.ImmediateLoad);
        Height.Set(dividerTexture.Height(), 0);
    }

    /// <inheritdoc />
    public override bool IsDynamicallySized => false;

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        Vector2 position = GetDimensions().Position();
        spriteBatch.Draw(dividerTexture.Value, position.ToNearestPixel(), null, Color.White, 0f, Vector2.Zero,
            new Vector2(GetDimensions().Width / 8f, 1f), SpriteEffects.None, 0f);
    }
}
