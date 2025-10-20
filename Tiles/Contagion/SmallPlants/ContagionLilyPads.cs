using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Dusts;

namespace Avalon.Tiles.Contagion.SmallPlants;

public class ContagionLilyPads : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileCut[Type] = true;
		Main.tileNoFail[Type] = true;

		TileID.Sets.TileCutIgnore.Regrowth[Type] = true;

		AddMapEntry(new Color(133, 150, 39));
		DustType = ModContent.DustType<ContagionDust>();
		HitSound = SoundID.Grass;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		Tile tileCache = Main.tile[i, j];
		int num9 = tileCache.LiquidAmount / 16;
		num9 -= 3;
		if (WorldGen.SolidTile(i, j - 1) && num9 > 8)
		{
			num9 = 8;
		}
		Dust.NewDust(new Vector2(i * 16, j * 16 - num9), 16, 16, type);
		return false;
	}

	public override void RandomUpdate(int i, int j)
	{
		if (j > Main.worldSurface)
		{
			if (Main.tile[i, j].LiquidAmount == 0 || Main.tile[i, j].LiquidAmount / 16 >= 9 && WorldGen.SolidTile(i, j - 1) || Main.tile[i, j - 1].LiquidAmount > 0 && Main.tile[i, j - 1].HasTile)
			{
				WorldGen.KillTile(i, j);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(17, -1, -1, null, 0, i, j);
				}
			}
			else
			{
				WorldGen.CheckLilyPad(i, j);
			}
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		WorldGen.CheckLilyPad(i, j);
		return false;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		Tile tileCache = Main.tile[i, j];
		int num19 = tileCache.LiquidAmount / 16;
		num19 -= 3;
		if (WorldGen.SolidTile(i, j - 1) && num19 > 8)
		{
			num19 = 8;
		}
		if (tileCache.LiquidAmount == 0)
		{
			Tile tileSafely = Framing.GetTileSafely(i, j + 1);
			if (tileSafely.HasUnactuatedTile)
			{
				switch (tileSafely.BlockType)
				{
					case (BlockType)1:
						num19 = -16 + Math.Max(8, tileSafely.LiquidAmount / 16);
						break;
					case (BlockType)2:
					case (BlockType)3:
						num19 -= 4;
						break;
				}
			}
		}
		offsetY -= num19;
	}
}
