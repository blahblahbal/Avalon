using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Savanna;

public class PlatformLeaf : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolidTop[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileNoFail[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileWaterDeath[Type] = false;
        Main.tileFrameImportant[Type] = true;
        AnimationFrameHeight = 72;
        TileID.Sets.DoesntGetReplacedWithTileReplacement[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
        TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
        TileObjectData.newTile.CoordinateWidth = 24;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
        TileObjectData.newTile.AnchorAlternateTiles = new int[] { TileID.WoodenSpikes };
        TileObjectData.newTile.LavaDeath = true;
        TileObjectData.newTile.AnchorTop = AnchorData.Empty;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 1, 1);
        TileObjectData.addTile(Type);
        DustType = ModContent.DustType<Dusts.TropicalDust>();
        AddMapEntry(new Color(82, 123, 35));
        RegisterItemDrop(ModContent.ItemType<Items.Placeable.Tile.PlatformLeaf>());
    }
    //public override IEnumerable<Item> GetItemDrops(int i, int j)
    //{
    //    yield return new Item(ModContent.ItemType<Items.Placeable.Tile.PlatformLeaf>());
    //}
    public override bool IsTileDangerous(int i, int j, Player player)
    {
        return true;
    }
}
