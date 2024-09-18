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
		Tile tileL = Main.tile[i - 1, j];
		Tile tileR = Main.tile[i + 1, j];
		Tile tileUp = Main.tile[i, j - 1];
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

		int num11 = 2; // 4 for tiles that aren't in TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue
		int addFrY = Main.tileFrame[Type] * 90;
		int addFrX = 0;
		Vector2 pos = new Vector2(i * 16, j * 16) + zero - Main.screenPosition;
		var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
		var halfFrame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 8);
		Color color = new Color(R, G, B);
		if (tileL.IsHalfBlock && tileR.IsHalfBlock && tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(texture, pos + new Vector2(0f, 8f), new Rectangle(tile.TileFrameX + addFrX, addFrY + tile.TileFrameY + 8, 16, 8), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Rectangle value3 = new Rectangle(126 + addFrX, addFrY, 16, 8);
			if (tileUp.HasTile)
			{
				if (!tileUp.BottomSlope)
				{
					if (tileUp.TileType == tile.TileType)
					{
						value3 = new Rectangle(90 + addFrX, addFrY, 16, 8);
					}
				}
			}
			Main.spriteBatch.Draw(texture, pos, value3, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
		else if (tileL.IsHalfBlock && tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(texture, pos + new Vector2(0f, 8f), new Rectangle(tile.TileFrameX + addFrX, addFrY + tile.TileFrameY + 8, 16, 8), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(texture, pos + new Vector2(num11, 0f), new Rectangle(tile.TileFrameX + num11 + addFrX, addFrY + tile.TileFrameY, 16 - num11, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			
			// no idea what these next two even draw, vanilla does it though so
			Main.spriteBatch.Draw(texture, pos, new Rectangle(144 + addFrX, addFrY, num11, 8), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(texture, pos, new Rectangle(148 + addFrX, addFrY, 2, 2), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
		else if (tileR.IsHalfBlock && tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(texture, pos + new Vector2(0f, 8f), new Rectangle(tile.TileFrameX + addFrX, addFrY + tile.TileFrameY + 8, 16, 8), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(texture, pos, new Rectangle(tile.TileFrameX + addFrX, addFrY + tile.TileFrameY, 16 - num11, 16), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			
			// no idea what these next two even draw, vanilla does it though so
			Main.spriteBatch.Draw(texture, pos + new Vector2(16 - num11, 0f), new Rectangle(144 + (16 - num11), 0, num11, 8), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(texture, pos + new Vector2(14f, 0f), new Rectangle(156, 0, 2, 2), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
		else if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(texture, pos, frame, color);
		}
		else if (tile.IsHalfBlock)
		{
			pos = new Vector2(i * 16, (j * 16) + 8) + zero - Main.screenPosition;
			Main.spriteBatch.Draw(texture, pos, halfFrame, color);
		}
		else
		{
			Vector2 screenOffset = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				screenOffset = Vector2.Zero;
			}
			Vector2 vector = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + screenOffset;
			int slopeType = (int)tile.Slope;
			int num5 = 2;
			for (int q = 0; q < 8; q++)
			{
				int num6 = q * -2;
				int num7 = 16 - q * 2;
				int num8 = 16 - num7;
				int num9;
				switch (slopeType)
				{
					case 1:
						num6 = 0;
						num9 = q * 2;
						num7 = 14 - q * 2;
						num8 = 0;
						break;
					case 2:
						num6 = 0;
						num9 = 16 - q * 2 - 2;
						num7 = 14 - q * 2;
						num8 = 0;
						break;
					case 3:
						num9 = q * 2;
						break;
					default:
						num9 = 16 - q * 2 - 2;
						break;
				}
				Main.spriteBatch.Draw(texture, vector + new Vector2(num9, q * num5 + num6), (Rectangle?)new Rectangle(tile.TileFrameX + addFrX + num9, tile.TileFrameY + addFrY + num8, num5, num7), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
			int num10 = ((slopeType <= 2) ? 14 : 0);
			Main.spriteBatch.Draw(texture, vector + new Vector2(0f, num10), (Rectangle?)new Rectangle(tile.TileFrameX + addFrX, tile.TileFrameY + addFrY + num10, 16, 2), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
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
