using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Furniture.Crafting;

public class EvilAltarsPlaced : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(119, 101, 125), this.GetLocalization("MapEntry0"));
        AddMapEntry(new Color(214, 127, 133), this.GetLocalization("MapEntry1"));
        AddMapEntry(new Color(0, 250, 50), this.GetLocalization("MapEntry2"));
        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.newTile.CoordinateHeights = new int[]
        {
            16,
            18
        };
        TileObjectData.addTile(Type);
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        AdjTiles = new int[] { TileID.DemonAltar };
    }

    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int toDrop = 0;
        switch (Main.tile[i, j].TileFrameX / 54)
        {
            case 0:
                toDrop = ModContent.ItemType<Items.Placeable.Crafting.DemonAltar>();
                break;
            case 1:
                toDrop = ModContent.ItemType<Items.Placeable.Crafting.CrimsonAltar>();
                break;
            case 2:
                toDrop = ModContent.ItemType<Items.Placeable.Crafting.IckyAltar>();
                break;
        }
        yield return new Item(toDrop);
    }
}
