using Avalon.Common;
using Avalon.Items.Placeable.Seed;
using Avalon.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Tiles.Herbs;

public class LargeHerbsStage1 : ModTile
{
    public override void SetStaticDefaults()
    {
        TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
        TileObjectData.newTile.Width = 1;
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateWidth = 16;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
        TileObjectData.newTile.CoordinatePadding = 2;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
        TileObjectData.addTile(Type);
        Main.tileFrameImportant[Type] = true;
        AddMapEntry(new Color(246, 197, 26), this.GetLocalization("MapEntry0")); // LanguageManager.Instance.GetText("Growing Large Daybloom"));
        AddMapEntry(new Color(76, 150, 216), this.GetLocalization("MapEntry1")); //LanguageManager.Instance.GetText("Growing Large Moonglow"));
        AddMapEntry(new Color(185, 214, 42), this.GetLocalization("MapEntry2")); // LanguageManager.Instance.GetText("Growing Large Blinkroot"));
        AddMapEntry(new Color(167, 203, 37), this.GetLocalization("MapEntry3")); // LanguageManager.Instance.GetText("Growing Large Deathweed"));
        AddMapEntry(new Color(72, 145, 125), this.GetLocalization("MapEntry4")); // LanguageManager.Instance.GetText("Growing Large Waterleaf"));
        AddMapEntry(new Color(177, 69, 49), this.GetLocalization("MapEntry5")); // LanguageManager.Instance.GetText("Growing Large Fireblossom"));
        AddMapEntry(new Color(40, 152, 240), this.GetLocalization("MapEntry6")); // LanguageManager.Instance.GetText("Growing Large Shiverthorn"));
        AddMapEntry(Color.IndianRed, this.GetLocalization("MapEntry7")); // LanguageManager.Instance.GetText("Growing Large Bloodberry"));
        AddMapEntry(new Color(216, 161, 50), this.GetLocalization("MapEntry8")); // LanguageManager.Instance.GetText("Growing Large Sweetstem"));
        AddMapEntry(new Color(0, 200, 50), this.GetLocalization("MapEntry9")); // LanguageManager.Instance.GetText("Growing Large Barfbush"));
        AddMapEntry(new Color(75, 184, 230), this.GetLocalization("MapEntry10")); // LanguageManager.Instance.GetText("Growing Large Holybird"));
        AddMapEntry(new Color(191, 0, 81), this.GetLocalization("MapEntry11")); //  LanguageManager.Instance.GetText("Growing Large Twilight Plume"));
    }
    public override ushort GetMapOption(int i, int j)
    {
        return (ushort)(Main.tile[i, j].TileFrameX / 18);
    }
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        bool intoRenderTargets = true;
        bool flag = intoRenderTargets || Main.LightingEveryFrame;

        if (Main.tile[i, j].TileFrameX % 18 == 0 && Main.tile[i, j].TileFrameY % 54 == 0 && flag)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, 4);
        }

        return false;
    }
    public override bool CreateDust(int i, int j, ref int type)
    {
        if (Main.tile[i, j].TileFrameY > 34)
        {
            switch (Main.tile[i, j].TileFrameX / 18)
            {
                case 0:
                case 1:
                case 9:
                    type = DustID.GrassBlades;
                    break;
                case 2:
                    type = DustID.WoodFurniture;
                    break;
                case 3:
                    type = DustID.CorruptPlants;
                    break;
                case 4:
                case 11:
                    type = DustID.SeaOatsOasis;
                    break;
                case 5:
                    type = DustID.Torch;
                    break;
                case 6:
                    type = DustID.Shiverthorn;
                    break;
                case 7:
                    type = DustID.CrimsonPlants;
                    break;
                case 8:
                    type = DustID.GoldCritter_LessOutline;
                    break;
                case 10:
                    type = DustID.HallowedPlants;
                    break;
            }
            return base.CreateDust(i, j, ref type);
        }
        return false;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        AvalonWorld.CheckLargeHerb(i, j, Type);
        noBreak = true;
        return true;
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        int item = 0;
        switch (Main.tile[i, j].TileFrameX / 18)
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
        yield return new Item(item);
    }
}
