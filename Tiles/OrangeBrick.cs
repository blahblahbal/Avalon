using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles;

public class OrangeBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(135, 76, 56));// 166, 87, 45));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
        ItemDrop = ModContent.ItemType<Items.Placeable.Tile.OrangeBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.OrangeDungeonDust>();
        TileID.Sets.GeneralPlacementTiles[Type] = false;
    }
}
