using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class BismuthBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(187, 89, 192));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.BismuthBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.BismuthDust>();
    }
}
