using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Systems;

// for now just used for darkening postdraw of tiles when actuated
public class TileGlowDrawing : ModSystem
{
	[Obsolete("Use the vanilla actColor method on your tile struct instead")]
	public static Color ActuatedColor(Color oldColor, Tile tile)
	{
		if (!tile.IsActuated)
		{
			return oldColor;
		}
		double num = 0.4;
		return new Color((int)(byte)(num * (double)(int)oldColor.R), (int)(byte)(num * (double)(int)oldColor.G), (int)(byte)(num * (double)(int)oldColor.B), (int)oldColor.A);
	}

	public static Color ActuatedRetroWallColor(Color oldColor, Tile tile)
	{
		if (!tile.Get<AvalonTileData>().IsWallActupainted)
		{
			return oldColor;
		}
		double num = 0.4;
		return new Color((int)(byte)(num * (double)(int)oldColor.R), (int)(byte)(num * (double)(int)oldColor.G), (int)(byte)(num * (double)(int)oldColor.B), (int)oldColor.A);
	}

	public static VertexColors ActuatedWallColor(VertexColors oldVertexColor, Tile tile)
	{
		if (!tile.Get<AvalonTileData>().IsWallActupainted)
		{
			return oldVertexColor;
		}
		double num = 0.4;
		Color TopLeft = new Color((int)(byte)(num * (double)(int)oldVertexColor.TopLeftColor.R), (int)(byte)(num * (double)(int)oldVertexColor.TopLeftColor.G), (int)(byte)(num * (double)(int)oldVertexColor.TopLeftColor.B), (int)oldVertexColor.TopLeftColor.A);
		Color TopRight = new Color((int)(byte)(num * (double)(int)oldVertexColor.TopRightColor.R), (int)(byte)(num * (double)(int)oldVertexColor.TopRightColor.G), (int)(byte)(num * (double)(int)oldVertexColor.TopRightColor.B), (int)oldVertexColor.TopRightColor.A);
		Color BottomLeft = new Color((int)(byte)(num * (double)(int)oldVertexColor.BottomLeftColor.R), (int)(byte)(num * (double)(int)oldVertexColor.BottomLeftColor.G), (int)(byte)(num * (double)(int)oldVertexColor.BottomLeftColor.B), (int)oldVertexColor.BottomLeftColor.A);
		Color BottomRight = new Color((int)(byte)(num * (double)(int)oldVertexColor.BottomRightColor.R), (int)(byte)(num * (double)(int)oldVertexColor.BottomRightColor.G), (int)(byte)(num * (double)(int)oldVertexColor.BottomRightColor.B), (int)oldVertexColor.BottomRightColor.A);
		return new VertexColors(TopLeft, TopRight, BottomLeft, BottomRight);
	}

	public static void DrawGlowmask(int i, int j, Color color, Asset<Texture2D> glow, int xOffset = 0, int yOffset = 0)
	{
		Tile tile = Main.tile[i, j];
		var zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			zero = Vector2.Zero;
		}

		Vector2 pos = new Vector2(i * 16 + xOffset, j * 16 + yOffset) + zero - Main.screenPosition;
		var frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
		var halfFrame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 8);
		color = ActuatedColor(color, tile);
		if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
		{
			Main.spriteBatch.Draw(glow.Value, pos, frame, color);
		}
		else if (tile.IsHalfBlock)
		{
			pos = new Vector2(i * 16, (j * 16) + 8) + zero - Main.screenPosition;
			Main.spriteBatch.Draw(glow.Value, pos, halfFrame, color);
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
			int addFrY = Main.tileFrame[tile.TileType] * 90;
			int addFrX = 0;
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
				Main.spriteBatch.Draw(glow.Value, vector + new Vector2(num9, q * num5 + num6), (Rectangle?)new Rectangle(tile.TileFrameX + addFrX + num9, tile.TileFrameY + addFrY + num8, num5, num7), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
			int num10 = ((slopeType <= 2) ? 14 : 0);
			Main.spriteBatch.Draw(glow.Value, vector + new Vector2(0f, num10), (Rectangle?)new Rectangle(tile.TileFrameX + addFrX, tile.TileFrameY + addFrY + num10, 16, 2), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
