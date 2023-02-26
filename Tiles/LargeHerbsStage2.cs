using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Placeable.Seed;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ExxoAvalonOrigins.Tiles;

public class LargeHerbsStage2 : ModTile
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
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        AddMapEntry(new Color(246, 197, 26), LanguageManager.Instance.GetText("Growing Large Daybloom"));
        AddMapEntry(new Color(76, 150, 216), LanguageManager.Instance.GetText("Growing Large Moonglow"));
        AddMapEntry(new Color(185, 214, 42), LanguageManager.Instance.GetText("Growing Large Blinkroot"));
        AddMapEntry(new Color(167, 203, 37), LanguageManager.Instance.GetText("Growing Large Deathweed"));
        AddMapEntry(new Color(72, 145, 125), LanguageManager.Instance.GetText("Growing Large Waterleaf"));
        AddMapEntry(new Color(177, 69, 49), LanguageManager.Instance.GetText("Growing Large Fireblossom"));
        AddMapEntry(new Color(40, 152, 240), LanguageManager.Instance.GetText("Growing Large Shiverthorn"));
        AddMapEntry(Color.IndianRed, LanguageManager.Instance.GetText("Growing Large Bloodberry"));
        AddMapEntry(new Color(216, 161, 50), LanguageManager.Instance.GetText("Growing Large Sweetstem"));
        AddMapEntry(new Color(0, 200, 50), LanguageManager.Instance.GetText("Growing Large Barfbush"));
        AddMapEntry(new Color(75, 184, 230), LanguageManager.Instance.GetText("Growing Large Holybird"));
        AddMapEntry(new Color(191, 0, 81), LanguageManager.Instance.GetText("Growing Large Twilight Plume"));
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
                item = ModContent.ItemType<LargeDaybloomSeed>();
                break;
            case 1:
                item = ModContent.ItemType<LargeMoonglowSeed>();
                break;
            case 2:
                item = ModContent.ItemType<LargeBlinkrootSeed>();
                break;
            case 3:
                item = ModContent.ItemType<LargeDeathweedSeed>();
                break;
            case 4:
                item = ModContent.ItemType<LargeWaterleafSeed>();
                break;
            case 5:
                item = ModContent.ItemType<LargeFireblossomSeed>();
                break;
            case 6:
                item = ModContent.ItemType<LargeShiverthornSeed>();
                break;
            case 7:
                item = ModContent.ItemType<LargeBloodberrySeed>();
                break;
            case 8:
                item = ModContent.ItemType<LargeSweetstemSeed>();
                break;
            case 9:
                item = ModContent.ItemType<LargeBarfbushSeed>();
                break;
            case 10:
                item = ModContent.ItemType<LargeHolybirdSeed>();
                break;
            case 11:
                item = ModContent.ItemType<LargeTwilightPlumeSeed>();
                break;
        }
        if (item > 0) Item.NewItem(WorldGen.GetItemSource_FromTileBreak(i, j), i * 16, j * 16, 16, 48, item);
    }
}
