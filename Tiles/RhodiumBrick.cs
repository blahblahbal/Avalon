using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class RhodiumBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(187, 99, 115));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.RhodiumBrick>();
        HitSound = SoundID.Tink;
        DustType = DustID.t_LivingWood;
    }
}
