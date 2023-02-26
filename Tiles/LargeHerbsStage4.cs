using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Placeable.Tile.LargeHerbs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
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
        AddMapEntry(new Color(246, 197, 26), LanguageManager.Instance.GetText("Large Daybloom"));
        AddMapEntry(new Color(76, 150, 216), LanguageManager.Instance.GetText("Large Moonglow"));
        AddMapEntry(new Color(185, 214, 42), LanguageManager.Instance.GetText("Large Blinkroot"));
        AddMapEntry(new Color(167, 203, 37), LanguageManager.Instance.GetText("Large Deathweed"));
        AddMapEntry(new Color(72, 145, 125), LanguageManager.Instance.GetText("Large Waterleaf"));
        AddMapEntry(new Color(177, 69, 49), LanguageManager.Instance.GetText("Large Fireblossom"));
        AddMapEntry(new Color(40, 152, 240), LanguageManager.Instance.GetText("Large Shiverthorn"));
        AddMapEntry(Color.IndianRed, LanguageManager.Instance.GetText("Large Bloodberry"));
        AddMapEntry(new Color(216, 161, 50), LanguageManager.Instance.GetText("Large Sweetstem"));
        AddMapEntry(new Color(0, 200, 50), LanguageManager.Instance.GetText("Large Barfbush"));
        AddMapEntry(new Color(75, 184, 230), LanguageManager.Instance.GetText("Large Holybird"));
        AddMapEntry(new Color(191, 0, 81), LanguageManager.Instance.GetText("Large Twilight Plume"));
    }
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 18);
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
