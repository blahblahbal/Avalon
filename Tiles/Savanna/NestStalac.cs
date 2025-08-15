using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Savanna;

public class NestStalac : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = false;
		Main.tileNoFail[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileObsidianKill[Type] = true;
		TileID.Sets.BreakableWhenPlacing[Type] = true;
		Main.tileMerge[ModContent.TileType<Nest>()][Type] = true;
		Main.tileMerge[Type][ModContent.TileType<Nest>()] = true;
		DustType = DustID.MarblePot;
		AddMapEntry(new Color(188, 166, 126));
		//AltLibrarySupport.TryAddStalactite(Type, ModContent.TileType<Nest>(), ModContent.TileType<HardenedSnotsand>(), ModContent.TileType<Snotsandstone>());
	}
	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		switch (tileFrameY)
		{
			case <= 18:
			case 72:
				offsetY = -2;
				break;

			case >= 36 and <= 54:
			case 90:
				offsetY = 2;
				break;
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		WorldGen.CheckTight(i, j);
		return false;
	}
}
