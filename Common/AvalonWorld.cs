using System;
using Avalon.Hooks;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Placeable.Tile.LargeHerbs;
using Avalon.Tiles;
using Avalon.WorldGeneration.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Common;

public class AvalonWorld : ModSystem
{
    public enum CopperVariant
    {
        Copper,
        Tin,
        Bronze,
        Random,
    }

    public enum IronVariant
    {
        Iron,
        Lead,
        Nickel,
        Random,
    }

    public enum SilverVariant
    {
        Silver,
        Tungsten,
        Zinc,
        Random,
    }

    public enum GoldVariant
    {
        Gold,
        Platinum,
        Bismuth,
        Random,
    }

    public enum RhodiumVariant
    {
        Rhodium = 0,
        Osmium = 1,
        Iridium = 2,
    }

    public int JungleX = 0;

    public WorldEvil WorldEvil;

    public static CopperVariant CopperOre = CopperVariant.Random;
    public static IronVariant IronOre = IronVariant.Random;
    public static SilverVariant SilverOre = SilverVariant.Random;
    public static GoldVariant GoldOre = GoldVariant.Random;
    public static RhodiumVariant? RhodiumOre { get; set; }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["RhodiumOre"] = (byte?)RhodiumOre;
        tag["WorldEvil"] = (byte)WorldEvil;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("RhodiumOre"))
        {
            RhodiumOre = (RhodiumVariant)tag.Get<byte>("RhodiumOre");
        }

        WorldEvil = (WorldEvil)(tag.Get<byte?>("WorldEvil") ?? WorldGen.WorldGenParam_Evil);
    }

    /// <inheritdoc />
    public override void PreWorldGen()
    {
        WorldEvil = ModContent.GetInstance<ContagionSelectionMenu>().SelectedWorldEvil switch {
            ContagionSelectionMenu.WorldEvilSelection.Random => Main.rand.Next(Enum.GetValues<WorldEvil>()),
            ContagionSelectionMenu.WorldEvilSelection.Corruption => WorldEvil.Corruption,
            ContagionSelectionMenu.WorldEvilSelection.Crimson => WorldEvil.Crimson,
            ContagionSelectionMenu.WorldEvilSelection.Contagion => WorldEvil.Contagion,
            _ => throw new ArgumentOutOfRangeException(),
        };

        WorldGen.WorldGenParam_Evil = (int)WorldEvil;
    }

    public override void PostUpdateWorld()
    {
        float num2 = 3E-05f * (float)WorldGen.GetWorldUpdateRate();

        // float num3 = 1.5E-05f * (float)Main.worldRate;
        for (int num4 = 0; num4 < Main.maxTilesX * Main.maxTilesY * num2; num4++)
        {
            int num5 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
            int num6 = WorldGen.genRand.Next(10, /*(int)Main.worldSurface - 1*/ Main.maxTilesY - 20);
            int num7 = num5 - 1;
            int num8 = num5 + 2;
            int num9 = num6 - 1;
            int num10 = num6 + 2;
            if (num7 < 10)
            {
                num7 = 10;
            }

            if (num8 > Main.maxTilesX - 10)
            {
                num8 = Main.maxTilesX - 10;
            }

            if (num9 < 10)
            {
                num9 = 10;
            }

            if (num10 > Main.maxTilesY - 10)
            {
                num10 = Main.maxTilesY - 10;
            }

            #region lazite grass
            if (Main.tile[num5, num6].TileType == ModContent.TileType<LaziteGrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;

                // where lazite tallgrass would grow
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(15) && num14 == ModContent.TileType<LaziteGrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<LaziteShortGrass>(), true);
                    Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 10) * 18);
                    if (Main.tile[num5, num9].HasTile)
                    {
                        Tile t = Main.tile[num5, num9];
                        t.TileColor = Main.tile[num5, num6].TileColor;
                    }

                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9, 1);
                    }
                }

                bool flag2 = false;
                for (int m = num7; m < num8; m++)
                {
                    for (int n = num9; n < num10; n++)
                    {
                        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                        {
                            if (Main.tile[m, n].TileType == ModContent.TileType<BlastedStone>())
                            {
                                TileColorCache color = Main.tile[num5, num6].BlockColorAndCoating();
                                WorldGen.SpreadGrass(m, n, ModContent.TileType<BlastedStone>(),
                                    ModContent.TileType<LaziteGrass>(), false, color);
                            }

                            if (Main.tile[m, n].TileType == num14)
                            {
                                WorldGen.SquareTileFrame(m, n);
                                flag2 = true;
                            }
                        }
                    }
                }

                if (Main.netMode == NetmodeID.Server && flag2)
                {
                    NetMessage.SendTileSquare(-1, num5, num6, 3);
                }
            }
            #endregion lazite grass

            #region large herb growth
            if (Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
                Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
                Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
            {
                GrowLargeHerb(num5, num6);
            }
            #endregion large herb growth
        }
    }

    public static void GrowLargeHerb(int x, int y)
    {
        if (Main.tile[x, y].HasTile)
        {
            if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>() &&
                WorldGen.genRand.NextBool(8)) // phase 1 to 2
            {
                bool grow = false;
                if (Main.tile[x, y].TileFrameX == 108) // shiverthorn check
                {
                    if (Main.rand.NextBool(2))
                    {
                        grow = true;
                    }
                }
                else
                {
                    grow = true;
                }

                if (grow) {
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
                    if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
                    {
                        Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
                    }

                    if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
                    {
                        Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
                    }

                    if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
                    {
                        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
                    }

                    if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
                    {
                        Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
                    }

                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 2);
                        NetMessage.SendTileSquare(-1, x, y + 1, 2);
                        NetMessage.SendTileSquare(-1, x, y + 2, 2);
                        NetMessage.SendTileSquare(-1, x, y - 1, 2);
                        NetMessage.SendTileSquare(-1, x, y - 2, 2);
                    }

                    WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
                }
            }
            else if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>() &&
                     WorldGen.genRand.NextBool(7)) // phase 2 to 3
            {
                bool grow = false;
                if (Main.tile[x, y].TileFrameX == 108) // shiverthorn check
                {
                    if (Main.rand.NextBool(2))
                    {
                        grow = true;
                    }
                }
                else
                {
                    grow = true;
                }

                if (grow)
                {
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
                    if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
                    {
                        Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
                    }

                    if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
                    {
                        Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
                    }

                    if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
                    {
                        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
                    }

                    if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
                    {
                        Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
                    }

                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, x, y, 2);
                        NetMessage.SendTileSquare(-1, x, y + 1, 2);
                        NetMessage.SendTileSquare(-1, x, y + 2, 2);
                        NetMessage.SendTileSquare(-1, x, y - 1, 2);
                        NetMessage.SendTileSquare(-1, x, y - 2, 2);
                    }

                    WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
                }
            }
            else if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>() &&
                     WorldGen.genRand.NextBool(5)) // phase 3 to 4
            {
                Main.tile[x, y].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
                if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
                {
                    Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
                }

                if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
                {
                    Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
                }

                if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
                {
                    Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
                }

                if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
                {
                    Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
                }

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendTileSquare(-1, x, y, 2);
                    NetMessage.SendTileSquare(-1, x, y + 1, 2);
                    NetMessage.SendTileSquare(-1, x, y + 2, 2);
                    NetMessage.SendTileSquare(-1, x, y - 1, 2);
                    NetMessage.SendTileSquare(-1, x, y - 2, 2);
                }

                WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
            }
        }
    }

    public static void CheckLargeHerb(int x, int y, int type)
    {
        if (WorldGen.destroyObject)
        {
            return;
        }

        Tile t = Main.tile[x, y];
        int style = t.TileFrameX / 18;
        bool destroy = false;
        int fixedY = y;
        int yFrame = Main.tile[x, y].TileFrameY;
        fixedY -= yFrame / 18;
        if (!WorldGen.SolidTile2(x, fixedY + 3) || !Main.tile[x, fixedY].HasTile ||
            !Main.tile[x, fixedY + 1].HasTile || !Main.tile[x, fixedY + 2].HasTile)
        {
            destroy = true;
        }

        if (destroy)
        {
            WorldGen.destroyObject = true;
            for (int u = fixedY; u < fixedY + 3; u++)
            {
                WorldGen.KillTile(x, u);
            }

            // 469 through 471 are the immature tiles of the large herb; 472 is the mature version
            if (type == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
                type == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
                type == (ushort)ModContent.TileType<LargeHerbsStage3>())
            {
                int item = 0;
                switch (style)
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
                } // 3710 through 3719 are the seeds

                if (item > 0)
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, fixedY * 16, 16, 48, item);
                }
            }
            else
            {
                int item = 0;
                switch (style)
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
                }

                if (item > 0)
                {
                    Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, fixedY * 16, 16, 48, item);
                }

                // 3700 through 3709 are the large herbs
            }

            WorldGen.destroyObject = false;
        }
    }
}
