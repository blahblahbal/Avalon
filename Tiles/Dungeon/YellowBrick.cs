using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Tiles.Dungeon;

public class YellowBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(91, 84, 52));//(82, 52, 156)); 94, 71, 117));
        Main.tileSolid[Type] = true;
        Main.tileMergeDirt[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileDungeon[Type] = true;
        //ItemDrop = ModContent.ItemType<Items.Placeable.Tile.YellowBrick>();
        HitSound = SoundID.Tink;
        DustType = ModContent.DustType<Dusts.YellowDungeonDust>();
        TileID.Sets.GeneralPlacementTiles[Type] = false;
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        yield return new Item(ModContent.ItemType<Items.Placeable.Tile.YellowBrick>());
    }
}
