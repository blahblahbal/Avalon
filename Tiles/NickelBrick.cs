using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class NickelBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(82, 112, 122));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileShine2[Type] = true;
        Main.tileShine[Type] = 2000;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.NickelBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.NickelDust>();
    }
}
