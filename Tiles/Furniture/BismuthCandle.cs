using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture;

public class BismuthCandle : CandleTemplate
{
	private static Asset<Texture2D> flameTexture;
	public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.BismuthCandle>();
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
    {
        Tile tile = Main.tile[i, j];
        if (tile.TileFrameX == 0)
        {
            r = 1f;
            g = 0.95f;
            b = 0.65f;
        }
    }
    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)((ulong)i));
        var color = new Color(224, 104, 147, 0);
        int frameX = Main.tile[i, j].TileFrameX;
        int frameY = Main.tile[i, j].TileFrameY;
        int width = 18;
        int offsetY = -4;
        int height = 20;
        int offsetX = 1;
        var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        for (int k = 0; k < 7; k++)
        {
            float x = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
            float y = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;
            Main.spriteBatch.Draw(flameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f + x, j * 16 - (int)Main.screenPosition.Y + offsetY + y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
        }
    }
}
