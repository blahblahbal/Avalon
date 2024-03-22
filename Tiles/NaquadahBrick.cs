using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class NaquadahBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(Color.Blue);
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 2050;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.NaquadahBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.NaquadahDust>();
    }
}
