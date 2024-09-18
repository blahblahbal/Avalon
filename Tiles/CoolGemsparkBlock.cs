using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Avalon.Tiles;

public class CoolGemsparkBlock : ModTile
{
    public static int R { get; private set; } = 160;
    public static int G { get; private set; } = 0;
    public static int B { get; private set; } = 0;
    private static int style = 0;

    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.DarkCyan);
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
		Main.tileLighted[Type] = true;
		TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[Type] = true;
		TileID.Sets.ForcedDirtMerging[Type] = true;
		TileID.Sets.GemsparkFramingTypes[Type] = Type;
		DustType = DustID.Ebonwood;
	}
	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = R / 255f;
		g = G / 255f;
		b = B / 255f;
	}
	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
		return false;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		Tile tile = Main.tile[i, j];
        Texture2D texture;
        //if (Main.canDrawColorTile(i, j))
        //{
        //    texture = TextureAssets.Tile[Type].Value;
        //}
        //else
        //{
        texture = TextureAssets.Tile[Type].Value;
        // }
        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
        if (Main.drawToScreen)
        {
            zero = Vector2.Zero;
        }
        Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), new Color(R, G, B), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
    }

    public static void StaticUpdate()
    {
        if (style == 0)
        {
            R -= 5;
            if (R <= 0)
            {
                R = 0;
                style = 1;
            }
        }
        if (style == 1)
        {
            G += 5;
            if (G >= 255)
            {
                G = 255;
            }
            if (G >= 160)
            {
                B -= 5;
                if (B <= 0)
                {
                    B = 0;
                    style = 2;
                }
            }
        }
        if (style == 2)
        {
            G -= 5;
            if (G <= 0)
            {
                G = 0;
            }
            if (G <= 160)
            {
                B += 5;
                if (B >= 255)
                {
                    B = 255;
                    style = 3;
                }
            }
        }
        if (style == 3)
        {
            R += 5;
            if (R >= 160)
            {
                R = 160;
                style = 0;
            }
        }
    }
}
