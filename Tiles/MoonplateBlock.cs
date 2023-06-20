using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class MoonplateBlock : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(162, 176, 183));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.MoonplateBlock>();
        HitSound = SoundID.Tink;
        DustType = DustID.Ghost;
    }
}
