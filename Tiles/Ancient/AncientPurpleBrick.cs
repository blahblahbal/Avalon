using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Ancient;

public class AncientPurpleBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(102, 78, 123));//(82, 52, 156)); 94, 71, 117));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.AncientPurpleBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.PurpleDungeonDust>();
        TileID.Sets.GeneralPlacementTiles[Type] = false;
    }
}
