using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion;

public class HardenedSnotsand : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(64, 78, 59));
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        TileID.Sets.Conversion.HardenedSand[Type] = true;
        TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
        TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
        TileID.Sets.ChecksForMerge[Type] = true;
        ItemDrop = ModContent.ItemType<HardenedSnotsandBlock>();
        DustType = DustID.ScourgeOfTheCorruptor;
    }
}
