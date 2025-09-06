using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModLiquidLib.ModLoader;
using ModLiquidLib.Utils;
using Terraria;
using Terraria.ID;
using static Terraria.WaterfallManager;

namespace Avalon.ModSupport.MLL.Liquids;

public class CaesiumGasFall : ModLiquidFall
{
	//using predraw we reverse the rendering of this waterfall to draw upwards instead of downwards like normal
	//shorter than normal because waterfalls have a smaller rendering box when above them
	public override bool PreDraw(WaterfallData currentWaterfallData, int i, int j, SpriteBatch spriteBatch)
	{
		int Type = Slot;
		int yOff = 0;
		int x = currentWaterfallData.x;
		int y = currentWaterfallData.y;
		int prevNewX = 0;
		int prevNewY = 0;
		int dir = 0;
		int dirX = 0;
		int prevHasSlope = 0;
		int prevType = 0;
		int frame;
		int maxDist;

		if (currentWaterfallData.stopAtStep == 0)
		{
			return false;
		}
		frame = 32 * WaterfallFrame;

		int bounceCount = 0;
		maxDist = (int)(WaterfallDist / 3.2f);
		Color prevlightColor = Color.White;
		for (int step = 0; step < maxDist; step++)
		{
			if (bounceCount >= 2)
			{
				break;
			}
			Tile tile = Main.tile[x, y];
			if (tile.HasUnactuatedTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType] && !TileID.Sets.Platforms[tile.TileType] && tile.BlockType == 0)
			{
				break;
			}
			Tile leftTile = Main.tile[x - 1, y];
			Tile tileAbove = Main.tile[x, y - 1];
			Tile rightTile = Main.tile[x + 1, y];
			if (WorldGen.SolidTile(tileAbove) && !tile.IsHalfBlock)
			{
				yOff = -8;
			}
			else if (prevNewY != 0)
			{
				yOff = 0;
			}
			int hasSlope = 0;
			int prevDirX = dirX;
			int newX = 0;
			int newY = 0;
			bool topSlope = false;
			if (tileAbove.TopSlope && !tile.IsHalfBlock && tileAbove.TileType != 19)
			{
				topSlope = true;
				if (tileAbove.Slope == (SlopeType)1)
				{
					hasSlope = 1;
					newX = 1;
					dir = 1;
					dirX = dir;
				}
				else
				{
					hasSlope = -1;
					newX = -1;
					dir = -1;
					dirX = dir;
				}
				newY = -1;
			}
			else if (!WorldGen.SolidTile(tileAbove) && !tileAbove.BottomSlope && !tile.IsHalfBlock || !tileAbove.HasTile && !tile.IsHalfBlock)
			{
				bounceCount = 0;
				newY = -1;
				newX = 0;
			}
			else if ((WorldGen.SolidTile(leftTile) || leftTile.TopSlope || leftTile.LiquidAmount > 0) && !WorldGen.SolidTile(rightTile) && rightTile.LiquidAmount == 0)
			{
				if (dir == -1)
				{
					bounceCount++;
				}
				newX = 1;
				newY = 0;
				dir = 1;
			}
			else if ((WorldGen.SolidTile(rightTile) || rightTile.TopSlope || rightTile.LiquidAmount > 0) && !WorldGen.SolidTile(leftTile) && leftTile.LiquidAmount == 0)
			{
				if (dir == 1)
				{
					bounceCount++;
				}
				newX = -1;
				newY = 0;
				dir = -1;
			}
			else if ((!WorldGen.SolidTile(rightTile) && !tile.TopSlope || rightTile.LiquidAmount == 0) && !WorldGen.SolidTile(leftTile) && !tile.TopSlope && leftTile.LiquidAmount == 0)
			{
				newY = 0;
				newX = dir;
			}
			else
			{
				bounceCount++;
				newY = 0;
				newX = 0;
			}
			if (bounceCount >= 2)
			{
				dir *= -1;
				newX *= -1;
			}
			Color lightingColor = Lighting.GetColor(x, y);
			if (step > 50)
			{
				Main.instance.waterfallManager.TrySparkling(x, y, dir, lightingColor);
			}
			float alpha = Main.instance.waterfallManager.GetAlpha(1f, maxDist, Type, y, step, tile);
			lightingColor = Main.instance.waterfallManager.StylizeColor(alpha, maxDist, Type, y, step, tile, lightingColor);
			int liquidAmount = tile.LiquidAmount / 16;
			if (topSlope && dir != prevDirX)
			{
				int num39 = 2;
				if (prevDirX == 1)
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16 + 16 - num39) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount - num39), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
				else
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + 16 - num39) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount - num39), lightingColor, 0 | (SpriteEffects)2);
				}
			}
			if (prevNewX == 0 && hasSlope != 0 && prevNewY == 1 && dir != dirX)
			{
				hasSlope = 0;
				dir = dirX;
				lightingColor = Color.White;
				if (dir == 1)
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16 + 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
				else
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16 + 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
			}
			if (prevHasSlope != 0 && newX == 0 && newY == -1)
			{
				if (dir == 1)
				{
					if (prevType != Slot)
					{
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff + 8) - Main.screenPosition, new Rectangle(frame, 0, 16, 16 - liquidAmount - 8), prevlightColor, (SpriteEffects)1 | (SpriteEffects)2);
					}
					else
					{
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff + 8) - Main.screenPosition, new Rectangle(frame, 0, 16, 16 - liquidAmount - 8), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
					}
				}
				else
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff + 8) - Main.screenPosition, new Rectangle(frame, 0, 16, 16 - liquidAmount - 8), lightingColor, 0 | (SpriteEffects)2);
				}
			}
			if (yOff == -8 && prevNewY == 1 && prevHasSlope == 0)
			{
				if (dirX == -1)
				{
					if (prevType != Slot)
					{
						Main.instance.waterfallManager.DrawWaterfall(prevType, x, y, alpha, new Vector2(x * 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 8), prevlightColor, 0 | (SpriteEffects)2);
					}
					else
					{
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 8), lightingColor, 0 | (SpriteEffects)2);
					}
				}
				else if (prevType != Slot)
				{
					Main.instance.waterfallManager.DrawWaterfall(prevType, x, y, alpha, new Vector2(x * 16 - 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 8), prevlightColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
				else
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 8), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
			}
			if (hasSlope != 0 && prevNewX == 0)
			{
				if (prevDirX == 1)
				{
					if (prevType != Slot)
					{
						Main.instance.waterfallManager.DrawWaterfall(prevType, x, y, alpha, new Vector2(x * 16 - 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), prevlightColor, (SpriteEffects)1 | (SpriteEffects)2);
					}
					else
					{
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
					}
				}
				else if (prevType != Slot)
				{
					Main.instance.waterfallManager.DrawWaterfall(prevType, x, y, alpha, new Vector2(x * 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), prevlightColor, 0 | (SpriteEffects)2);
				}
				else
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, 0 | (SpriteEffects)2);
				}
			}
			if (newY == -1 && hasSlope == 0 && prevHasSlope == 0)
			{
				if (dir == -1)
				{
					if (prevNewY == 0)
					{
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff) - Main.screenPosition, new Rectangle(frame, 0, 16, 16 - liquidAmount), lightingColor, 0 | (SpriteEffects)2);
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 - 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, 0 | (SpriteEffects)2);
					}
					else if (prevType != Slot)
					{
						Main.instance.waterfallManager.DrawWaterfall(prevType, x, y, alpha, new Vector2(x * 16, y * 16 - 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), prevlightColor, 0 | (SpriteEffects)2);
					}
					else
					{
						Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 - 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, 0 | (SpriteEffects)2);
					}
				}
				else if (prevNewY == 0)
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff) - Main.screenPosition, new Rectangle(frame, 0, 16, 16 - liquidAmount), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16 - 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
				else if (prevType != Slot)
				{
					Main.instance.waterfallManager.DrawWaterfall(prevType, x, y, alpha, new Vector2(x * 16 - 16, y * 16 - 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), prevlightColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
				else
				{
					Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 - 16, y * 16 - 16) - Main.screenPosition, new Rectangle(frame, 24, 32, 16 - liquidAmount), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
				}
			}
			else
			{
				switch (newX)
				{
					case 1:
						if (Main.tile[x, y].LiquidAmount > 0 && !Main.tile[x, y].IsHalfBlock)
						{
							break;
						}
						if (hasSlope == 1)
						{
							for (int sLoop = 0; sLoop < 8; sLoop++)
							{
								int sideXOff = sLoop * 2;
								int frameXOff = 14 - sLoop * 2;
								int sideYOff = sideXOff;
								yOff = -8;
								if (prevNewX == 0 && sLoop < 2)
								{
									sideYOff = 4;
								}
								Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 + sideXOff, y * 16 + yOff + sideYOff) - Main.screenPosition, new Rectangle(16 + frame + frameXOff, 0, 2, 16 - yOff), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
							}
						}
						else
						{
							int height2 = 16;
							if (TileID.Sets.BlocksWaterDrawingBehindSelf[Main.tile[x, y].TileType])
							{
								height2 = 8;
							}
							else if (TileID.Sets.BlocksWaterDrawingBehindSelf[Main.tile[x, y - 1].TileType])
							{
								height2 = 8;
							}
							Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff) - Main.screenPosition, new Rectangle(16 + frame, 0, 16, height2), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
						}
						break;
					case -1:
						if (Main.tile[x, y].LiquidAmount > 0 && !Main.tile[x, y].IsHalfBlock)
						{
							break;
						}
						if (hasSlope == -1)
						{
							for (int sLoop = 0; sLoop < 8; sLoop++)
							{
								int sideXOff = sLoop * 2;
								int frameXOff = sLoop * 2;
								int sideYOff = 14 - sLoop * 2;
								yOff = -8;
								if (prevNewX == 0 && sLoop > 5)
								{
									sideYOff = 4;
								}
								Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16 + sideXOff, y * 16 + yOff + sideYOff) - Main.screenPosition, new Rectangle(16 + frame + frameXOff, 0, 2, 16 - yOff), lightingColor, (SpriteEffects)1 | (SpriteEffects)2);
							}
						}
						else
						{
							int height = 16;
							if (TileID.Sets.BlocksWaterDrawingBehindSelf[Main.tile[x, y].TileType])
							{
								height = 8;
							}
							else if (TileID.Sets.BlocksWaterDrawingBehindSelf[Main.tile[x, y - 1].TileType])
							{
								height = 8;
							}
							Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff) - Main.screenPosition, new Rectangle(16 + frame, 0, 16, height), lightingColor, 0 | (SpriteEffects)2);
						}
						break;
					case 0:
						if (newY == 0)
						{
							if (Main.tile[x, y].LiquidAmount <= 0 || Main.tile[x, y].IsHalfBlock)
							{
								Main.instance.waterfallManager.DrawWaterfall(Slot, x, y, alpha, new Vector2(x * 16, y * 16 + yOff) - Main.screenPosition, new Rectangle(16 + frame, 0, 16, 16), lightingColor, 0 | (SpriteEffects)2);
							}
							step = 1000;
						}
						break;
				}
			}
			if (tile.LiquidAmount > 0 && !tile.IsHalfBlock)
			{
				step = 1000;
			}
			prevNewY = newY;
			dirX = dir;
			prevNewX = newX;
			x += newX;
			y += newY;
			prevHasSlope = hasSlope;
			prevlightColor = lightingColor;
			if (prevType != Slot)
			{
				prevType = Slot;
			}
			if (leftTile.HasTile && (leftTile.TileType == 189 || leftTile.TileType == 196) || rightTile.HasTile && (rightTile.TileType == 189 || rightTile.TileType == 196) || tileAbove.HasTile && (tileAbove.TileType == 189 || tileAbove.TileType == 196))
			{
				maxDist = (int)(40f * (Main.maxTilesX / 4200f) * Main.gfxQuality);
			}
		}
		return false;
	}

	public override float? Alpha(int x, int y, float Alpha, int maxSteps, int s, Tile tileCache)
	{
		float num = 1f;
		if (s > maxSteps - 10)
		{
			num *= (maxSteps - s) / 10f;
		}
		return num;
	}

	public override bool PlayWaterfallSounds()
	{
		return false;
	}
}
