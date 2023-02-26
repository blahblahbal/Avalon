using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Placeable.Tile.LargeHerbs;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExxoAvalonOrigins.Tiles;

public class LargeHerbsStage4 : ModTile
{
    public override void SetStaticDefaults()
    {
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.DrawYOffset = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        AvalonWorld.CheckLargeHerb(i, j, Type);
        noBreak = true;
        return true;
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        int item = 0;
        switch (frameX / 18)
        {
            case 0:
                item = ModContent.ItemType<LargeDaybloom>();
                break;
            case 1:
                item = ModContent.ItemType<LargeMoonglow>();
                break;
            case 2:
                item = ModContent.ItemType<LargeBlinkroot>();
                break;
            case 3:
                item = ModContent.ItemType<LargeDeathweed>();
                break;
            case 4:
                item = ModContent.ItemType<LargeWaterleaf>();
                break;
            case 5:
                item = ModContent.ItemType<LargeFireblossom>();
                break;
            case 6:
                item = ModContent.ItemType<LargeShiverthorn>();
                break;
            case 7:
                item = ModContent.ItemType<LargeBloodberry>();
                break;
            case 8:
                item = ModContent.ItemType<LargeSweetstem>();
                break;
            case 9:
                item = ModContent.ItemType<LargeBarfbush>();
                break;
            case 10:
                item = ModContent.ItemType<LargeHolybird>();
                break;
            case 11:
                item = ModContent.ItemType<LargeTwilightPlume>();
                break;
        }
        if (item > 0) Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 48, item);
    }
}
