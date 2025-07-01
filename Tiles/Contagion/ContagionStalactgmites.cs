using Avalon.Dusts;
using Avalon.ModSupport;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class ContagionStalactgmites : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = false;
        Main.tileNoFail[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        TileID.Sets.BreakableWhenPlacing[Type] = true;
        Main.tileMerge[ModContent.TileType<Chunkstone>()][Type] = true;
        Main.tileMerge[Type][ModContent.TileType<Chunkstone>()] = true;
        DustType = ModContent.DustType<ContagionDust>();
        AddMapEntry(new Color(83, 103, 76));
		AltLibrarySupport.TryAddStalactite(Type, ModContent.TileType<Chunkstone>(), ModContent.TileType<HardenedSnotsand>(), ModContent.TileType<Snotsandstone>());
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
