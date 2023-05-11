using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class Snotsandstone : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(91, 109, 86));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.Sandstone[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.isDesertBiomeSand[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
        Common.TileMerge.MergeWith(Type, ModContent.TileType<Snotsand>());
        Common.TileMerge.MergeWith(Type, ModContent.TileType<HardenedSnotsand>());
        DustType = DustID.ScourgeOfTheCorruptor;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        Common.TileMerge.MergeWithFrame(i, j, Type, ModContent.TileType<Snotsand>(), false, false, false, false, resetFrame);
        Common.TileMerge.MergeWithFrame(i, j, Type, ModContent.TileType<HardenedSnotsand>(), false, false, false, false, resetFrame);
        return false;
    }
}
