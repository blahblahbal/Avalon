using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Tropics;

public class Loam : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(113, 58, 29));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileMerge[TileID.Dirt][Type] = true;
        Main.tileMerge[Type][TileID.Dirt] = true;
        DustType = ModContent.DustType<TropicalMudDust>();
        TileID.Sets.CanBeDugByShovel[Type] = true;
        TileID.Sets.Mud[Type] = true;
        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;
    }
}
