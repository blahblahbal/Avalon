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
        ItemDrop = ModContent.ItemType<SnotsandstoneBlock>();
        DustType = DustID.ScourgeOfTheCorruptor;
    }
}
