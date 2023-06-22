using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalon.Hooks;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Placeable.Tile.LargeHerbs;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Herbs;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.Utilities;

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

    private static bool crackedBrick;

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
        AvalonConfig config = ModContent.GetInstance<AvalonConfig>();
        Dictionary<string, AvalonConfig.WorldDataValues> tempDict = config.GetWorldData();
        AvalonConfig.WorldDataValues worldData;

        if (WorldEvil == WorldEvil.Contagion)
        {
            worldData.contagion = true;
        }
        else
        {
            worldData.contagion = false;
        }

        string path = Path.ChangeExtension(Main.worldPathName, ".twld");
        tempDict[path] = worldData;
        config.SetWorldData(tempDict);

        AvalonConfig.Save(config);
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

            #region killing things if the block above/below isn't the necessary type
            // kill contagion vines if block above isn't contagion grass
            if (!(Main.tile[num5, num9].TileType == ModContent.TileType<Ickgrass>() || Main.tile[num5, num9].TileType == ModContent.TileType<ContagionVines>()) &&
                Main.tile[num5, num6].TileType == ModContent.TileType<ContagionVines>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill contagion short grass if block below isn't contagion grass
            if (Main.tile[num5, num6].TileType != ModContent.TileType<Ickgrass>() && Main.tile[num5, num9].TileType == ModContent.TileType<ContagionShortGrass>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill barfbush if block below isn't contagion grass or chunkstone
            if (!(Main.tile[num5, num6].TileType == ModContent.TileType<Ickgrass>() || Main.tile[num5, num6].TileType == ModContent.TileType<Chunkstone>()) &&
                Main.tile[num5, num9].TileType == ModContent.TileType<Tiles.Herbs.Barfbush>())
            {
                WorldGen.KillTile(num5, num6);
            }
            #endregion

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

            #region contagion shortgrass/barfbush spawning
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Ickgrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(5) && num14 == ModContent.TileType<Ickgrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<ContagionShortGrass>(), true);
                    Main.tile[num5, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 11) * 18);
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

                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(num6 > Main.worldSurface ? 500 : 200) && num14 == ModContent.TileType<Ickgrass>())
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Tiles.Herbs.Barfbush>(), true, false, -1, 0);
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
                            if (Main.tile[m, n].TileType == 0 || (num14 == ModContent.TileType<Ickgrass>() && Main.tile[m, n].TileType == TileID.Grass))
                            {
                                TileColorCache color = Main.tile[num5, num6].BlockColorAndCoating();
                                WorldGen.SpreadGrass(m, n, 0, num14, false, color);
                                if (num14 == ModContent.TileType<Ickgrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.Grass, num14, false, color);
                                }
                                if (num14 == ModContent.TileType<Ickgrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.HallowedGrass, num14, false, color);
                                }
                                if (Main.tile[m, n].TileType == num14)
                                {
                                    WorldGen.SquareTileFrame(m, n, true);
                                    flag2 = true;
                                }
                            }
                            if (Main.tile[m, n].TileType == 0 || (num14 == 109 && Main.tile[m, n].TileType == 2) || (num14 == 109 && Main.tile[m, n].TileType == 23) || (num14 == 109 && Main.tile[m, n].TileType == 199))
                            {
                                if (num14 == TileID.HallowedGrass)
                                {
                                    TileColorCache color = Main.tile[num5, num6].BlockColorAndCoating();
                                    WorldGen.SpreadGrass(m, n, ModContent.TileType<Ickgrass>(), num14, false, color);
                                }
                            }
                        }
                    }
                }
                if (Main.netMode == NetmodeID.Server && flag2)
                {
                    NetMessage.SendTileSquare(-1, num5, num6, 3);
                }
            }
            #endregion contagion shortgrass/barfbush spawning

            #region impvines growing
            if ((Main.tile[num5, num6].TileType == ModContent.TileType<Ectograss>() ||
                 Main.tile[num5, num6].TileType == ModContent.TileType<Ectovines>()) &&
                WorldGen.genRand.NextBool(15) && !Main.tile[num5, num6 + 1].HasTile && // change back to NextBool(15)
                Main.tile[num5, num6 + 1].LiquidType != LiquidID.Lava)
            {
                bool flag10 = false;
                for (int num47 = num6; num47 > num6 - 10; num47--)
                {
                    if (Main.tile[num5, num47].BottomSlope)
                    {
                        flag10 = false;
                        break;
                    }

                    if (Main.tile[num5, num47].HasTile &&
                        Main.tile[num5, num47].TileType == ModContent.TileType<Ectograss>() &&
                        !Main.tile[num5, num47].BottomSlope)
                    {
                        flag10 = true;
                        break;
                    }
                }

                if (flag10)
                {
                    int num48 = num5;
                    int num49 = num6 + 1;
                    Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<Ectovines>();

                    Tile t = Main.tile[num48, num49];
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(num48, num49);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, num48, num49, 3);
                    }
                }
            }
            #endregion impvines

            #region contagion vines growing
            if ((Main.tile[num5, num6].TileType == ModContent.TileType<Ickgrass>() ||
                 Main.tile[num5, num6].TileType == ModContent.TileType<ContagionVines>()) &&
                WorldGen.genRand.NextBool(15) && !Main.tile[num5, num6 + 1].HasTile &&
                Main.tile[num5, num6 + 1].LiquidType != LiquidID.Lava)
            {
                bool flag10 = false;
                for (int num47 = num6; num47 > num6 - 10; num47--)
                {
                    if (Main.tile[num5, num47].BottomSlope)
                    {
                        flag10 = false;
                        break;
                    }

                    if (Main.tile[num5, num47].HasTile &&
                        Main.tile[num5, num47].TileType == ModContent.TileType<Ickgrass>() &&
                        !Main.tile[num5, num47].BottomSlope)
                    {
                        flag10 = true;
                        break;
                    }
                }

                if (flag10)
                {
                    int num48 = num5;
                    int num49 = num6 + 1;
                    Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<ContagionVines>();

                    Tile t = Main.tile[num48, num49];
                    t.HasTile = true;
                    WorldGen.SquareTileFrame(num48, num49);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendTileSquare(-1, num48, num49, 3);
                    }
                }
            }
            #endregion tropical vines
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
                    case 11:
                        item = ModContent.ItemType<LargeTwilightPlumeSeed>();
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
                    case 11:
                        item = ModContent.ItemType<LargeTwilightPlume>();
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
    public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
    {
        if (Main.LocalPlayer.GetModPlayer<Players.AvalonBiomePlayer>().ZoneContagion)
        {
            float Strength = ModContent.GetInstance<BiomeTileCounts>().ContagionTiles / 350f;
            Strength = Math.Min(Strength, 1f);

            int sunR = backgroundColor.R;
            int sunG = backgroundColor.G;
            int sunB = backgroundColor.B;
            sunR -= (int)(212f * Strength / 2 * (backgroundColor.R / 255f));
            sunB -= (int)(255f * Strength / 2 * (backgroundColor.B / 255f));
            sunG -= (int)(127f * Strength / 2 * (backgroundColor.G / 255f));
            sunR = Utils.Clamp(sunR, 15, 255);
            sunG = Utils.Clamp(sunG, 15, 255);
            sunB = Utils.Clamp(sunB, 15, 255);
            backgroundColor.R = (byte)sunR;
            backgroundColor.G = (byte)sunG;
            backgroundColor.B = (byte)sunB;
        }
    }
    public static void ShatterCrackedBricks(int i, int j, Tile tileCache, bool fail)
    {
        if (tileCache.TileType != ModContent.TileType<CrackedOrangeBrick>() && tileCache.TileType != ModContent.TileType<CrackedPurpleBrick>() || Main.netMode == NetmodeID.MultiplayerClient || crackedBrick || j < Main.maxTilesY - 200)
        {
            return;
        }
        crackedBrick = true;
        for (int k = i - 4; k <= i + 4; k++)
        {
            for (int l = j - 4; l <= j + 4; l++)
            {
                int maxValue = 15;
                if (!WorldGen.SolidTile(k, l + 1))
                {
                    maxValue = 7;
                }
                else if (k == i && l == j - 1 && !fail)
                {
                    maxValue = 7;
                }
                if ((k != i || l != j) && Main.tile[k, l].HasTile && (Main.tile[k, l].TileType == ModContent.TileType<CrackedOrangeBrick>() || Main.tile[k, l].TileType == ModContent.TileType<CrackedPurpleBrick>()) && WorldGen.genRand.NextBool(maxValue))
                {
                    WorldGen.KillTile(k, l, fail: false, effectOnly: false, noItem: true);
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, k, l);
                    }
                }
            }
        }
        crackedBrick = false;
    }

    public static void ConvertFromThings(int x, int y, int convert, bool tileframe = true)
    {
        Tile tile = Main.tile[x, y];
        int type = tile.TileType;
        int wall = tile.WallType;
        if (!WorldGen.InWorld(x, y, 1))
        {
            return;
        }
        // convert from contagion to purity
        if (convert == 0)
        {
            if (Main.tile[x, y] != null)
            {
                if (wall == ModContent.WallType<Walls.ContagionGrassWall>())
                {
                    Main.tile[x, y].WallType = WallID.GrassUnsafe;
                }
                else if (wall == ModContent.WallType<Walls.ChunkstoneWall>() || wall == ModContent.WallType<Walls.ChunkstoneWallSafe>())
                {
                    Main.tile[x, y].WallType = WallID.Stone;
                }
                else if (wall == ModContent.WallType<Walls.ContagionLumpWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionLumpWall>())
                {
                    Main.tile[x, y].WallType = WallID.RocksUnsafe1;
                }
                else if (wall == ModContent.WallType<Walls.ContagionMouldWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionMouldWall>())
                {
                    Main.tile[x, y].WallType = WallID.RocksUnsafe2;
                }
                else if (wall == ModContent.WallType<Walls.ContagionCystWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionCystWall>())
                {
                    Main.tile[x, y].WallType = WallID.RocksUnsafe3;
                }
                else if (wall == ModContent.WallType<Walls.ContagionBoilWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionBoilWall>())
                {
                    Main.tile[x, y].WallType = WallID.RocksUnsafe4;
                }
                else if (wall == ModContent.WallType<Walls.HardenedSnotsandWallUnsafe>() || wall == ModContent.WallType<Walls.HardenedSnotsandWall>())
                {
                    Main.tile[x, y].WallType = WallID.HardenedSand;
                }
                else if (wall == ModContent.WallType<Walls.SnotsandstoneWallUnsafe>() || wall == ModContent.WallType<Walls.SnotsandstoneWall>())
                {
                    Main.tile[x, y].WallType = WallID.Sandstone;
                }
            }
            if (Main.tile[x, y] != null)
            {
                if (type == ModContent.TileType<Ickgrass>())
                {
                    tile.TileType = TileID.Grass;
                }
                else if (type == ModContent.TileType<YellowIce>())
                {
                    tile.TileType = TileID.IceBlock;
                }
                else if (type == ModContent.TileType<Snotsand>())
                {
                    tile.TileType = TileID.Sand;
                }
                else if (type == ModContent.TileType<Chunkstone>())
                {
                    tile.TileType = TileID.Stone;
                }
                else if (type == ModContent.TileType<Snotsandstone>())
                {
                    tile.TileType = TileID.Sandstone;
                }
                else if (type == ModContent.TileType<HardenedSnotsand>())
                {
                    tile.TileType = TileID.HardenedSand;
                }
                //else if (type == ModContent.TileType<ContagionShortGrass>())
                //{
                //    tile.type = TileID.Plants;
                //}
                //if (TileID.Sets.Conversion.Grass[type] || type == 0)
                //{
                //    WorldGen.SquareTileFrame(x, y);
                //}
            }
        }
        if (convert == 1)
        {
            if (Main.tile[x, y] != null)
            {
                if (wall == ModContent.WallType<Walls.ContagionGrassWall>() || wall == WallID.CrimsonGrassUnsafe || wall == WallID.CorruptGrassUnsafe || wall == WallID.HallowedGrassUnsafe)
                {
                    Main.tile[x, y].WallType = WallID.JungleUnsafe;
                }
                else if (wall == WallID.DirtUnsafe)
                {
                    Main.tile[x, y].WallType = WallID.MudUnsafe;
                }
            }
            if (Main.tile[x, y] != null)
            {
                if (type == ModContent.TileType<Ickgrass>() || type == TileID.CrimsonGrass || type == TileID.CorruptGrass || type == TileID.Grass || type == TileID.HallowedGrass)
                {
                    tile.TileType = TileID.JungleGrass;
                }
                else if (type == TileID.Dirt)
                {
                    tile.TileType = TileID.Mud;
                }
                else if (type == ModContent.TileType<Snotsand>() || type == TileID.Sand || type == TileID.Crimsand || type == TileID.Ebonsand || type == TileID.Pearlsand)
                {
                    tile.TileType = TileID.Sand;
                }
                else if (type == ModContent.TileType<Chunkstone>() || type == TileID.Pearlstone || type == TileID.Crimstone || type == TileID.Ebonstone)
                {
                    tile.TileType = TileID.Stone;
                }
                else if (type == ModContent.TileType<Snotsandstone>() || type == TileID.HallowSandstone || type == TileID.CrimsonSandstone || type == TileID.CorruptSandstone)
                {
                    tile.TileType = TileID.Sandstone;
                }
                else if (type == ModContent.TileType<HardenedSnotsand>() || type == TileID.HallowHardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.CorruptHardenedSand)
                {
                    tile.TileType = TileID.HardenedSand;
                }
                //else if (type == ModContent.TileType<YellowIce>() || type == TileID.HallowedIce || type == TileID.FleshIce || type == TileID.CorruptIce || type == TileID.IceBlock)
                //{
                //    tile.type = (ushort)ModContent.TileType<GreenIce>();
                //}
                //if (TileID.Sets.Conversion.Grass[type] || type == 0)
                //{
                //    WorldGen.SquareTileFrame(x, y);
                //}
            }
        }
        // contagion from things
        if (convert == 2)
        {
            if (Main.tile[x, y] != null)
            {
                if (WallID.Sets.Conversion.Grass[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionGrassWall>();
                }
                else if (WallID.Sets.Conversion.Stone[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ChunkstoneWall>();
                }
                else if (WallID.Sets.Conversion.NewWall1[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionLumpWallUnsafe>();
                }
                else if (WallID.Sets.Conversion.NewWall2[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionMouldWallUnsafe>();
                }
                else if (WallID.Sets.Conversion.NewWall3[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionCystWallUnsafe>();
                }
                else if (WallID.Sets.Conversion.NewWall4[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionBoilWallUnsafe>();
                }
                else if (WallID.Sets.Conversion.HardenedSand[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.HardenedSnotsandWallUnsafe>();
                }
                else if (WallID.Sets.Conversion.Sandstone[wall])
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.SnotsandstoneWallUnsafe>();
                }
            }
            if (Main.tile[x, y] != null)
            {
                if (TileID.Sets.Conversion.Grass[type] && type != ModContent.TileType<Ickgrass>())
                {
                    tile.TileType = (ushort)ModContent.TileType<Ickgrass>();
                }
                else if (TileID.Sets.Conversion.Ice[type] && type != ModContent.TileType<YellowIce>())
                {
                    tile.TileType = (ushort)ModContent.TileType<YellowIce>();
                }
                else if (TileID.Sets.Conversion.Sand[type] && type != ModContent.TileType<Snotsand>())
                {
                    tile.TileType = (ushort)ModContent.TileType<Snotsand>();
                }
                else if (TileID.Sets.Conversion.Stone[type] && type != ModContent.TileType<Chunkstone>())
                {
                    tile.TileType = (ushort)ModContent.TileType<Chunkstone>();
                }
                else if (TileID.Sets.Conversion.Sandstone[type] && type != ModContent.TileType<Snotsandstone>())
                {
                    tile.TileType = (ushort)ModContent.TileType<Snotsandstone>();
                }
                else if (TileID.Sets.Conversion.HardenedSand[type] && type != ModContent.TileType<HardenedSnotsand>())
                {
                    tile.TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                }
                //else if (type == ModContent.TileType<ContagionShortGrass>())
                //{
                //    tile.type = TileID.Plants;
                //}
                //if (TileID.Sets.Conversion.Grass[type] || type == 0)
                //{
                //    WorldGen.SquareTileFrame(x, y);
                //}
            }
        }
        //if (tileframe)
        //{
        //    if (Main.netMode == NetmodeID.SinglePlayer)
        //    {
        //        WorldGen.SquareTileFrame(x, y);
        //    }
        //    else if (Main.netMode == NetmodeID.Server)
        //    {
        //        NetMessage.SendTileSquare(-1, x, y, 1);
        //    }
        //}
    }
    public override void Load()
    {
        Terraria.GameContent.UI.States.On_UIWorldSelect.UpdateWorldsList += On_UIWorldSelect_UpdateWorldsList;
    }

    public override void Unload()
    {
        Terraria.GameContent.UI.States.On_UIWorldSelect.UpdateWorldsList -= On_UIWorldSelect_UpdateWorldsList;
    }

    private void On_UIWorldSelect_UpdateWorldsList(Terraria.GameContent.UI.States.On_UIWorldSelect.orig_UpdateWorldsList orig, Terraria.GameContent.UI.States.UIWorldSelect self)
    {
        orig(self);

        UIList WorldList = (UIList)typeof(UIWorldSelect).GetField("_worldList", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
        foreach (var item in WorldList)
        {
            if (item is UIWorldListItem)
            {
                UIElement _WorldIcon = (UIElement)typeof(UIWorldListItem).GetField("_worldIcon", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(item);
                //_WorldIcon = GetIconElement();

                UIElement WorldIcon = (UIElement)typeof(UIWorldListItem).GetField("_worldIcon", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(item);
                WorldFileData Data = (WorldFileData)typeof(AWorldListItem).GetField("_data", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(item);

                var path = Path.ChangeExtension(Data.Path, ".twld");

                AvalonConfig config = ModContent.GetInstance<AvalonConfig>();
                Dictionary<string, AvalonConfig.WorldDataValues> tempDict = config.GetWorldData();

                if (!tempDict.ContainsKey(path))
                {
                    byte[] buf = FileUtilities.ReadAllBytes(path, Data.IsCloudSave);
                    var stream = new MemoryStream(buf);
                    var tag = TagIO.FromStream(stream);
                    bool containsMod = false;

                    if (tag.ContainsKey("modData"))
                    {
                        foreach (TagCompound modDataTag in tag.GetList<TagCompound>("modData").Skip(2))
                        {
                            if (modDataTag.Get<string>("mod") == ModContent.GetInstance<AvalonConfig>().Mod.Name)
                            {
                                TagCompound dataTag = modDataTag.Get<TagCompound>("data");
                                AvalonConfig.WorldDataValues worldData;

                                worldData.contagion = dataTag.Get<bool>("ExxoAvalonOrigins:WorldEvil.Contagion"); //Have no idea how the contagion is saved in the .tmod file. replace if this isn't correct
                                tempDict[path] = worldData;

                                containsMod = true;

                                break;
                            }
                        }

                        if (!containsMod)
                        {
                            AvalonConfig.WorldDataValues worldData;

                            worldData.contagion = false;
                            tempDict[path] = worldData;
                        }

                        config.SetWorldData(tempDict);
                        AvalonConfig.Save(config);
                    }
                }

                #region RegularSeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && !Data.DrunkWorld && !Data.Anniversary && !Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay")) //the altlib textures arent in the mod anymore, you'll need to restore them and put their paths here. The full icons require IL editing to add which i am not capable of
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region AnniversarySeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && !Data.DrunkWorld && Data.Anniversary && !Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_Anniversary"))
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region DontStarveSeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && !Data.DrunkWorld && !Data.Anniversary && Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_DST"))
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region DrunkSeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && Data.DrunkWorld && !Data.Anniversary && !Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay"))
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region FTWSeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && !Data.DrunkWorld && !Data.Anniversary && !Data.DontStarve && Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_FTW"))
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region NotTheBeesSeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && !Data.DrunkWorld && !Data.Anniversary && !Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_NotTheBees"))
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region NoTrapsSeedIcon
                if (tempDict[path].contagion && !Data.RemixWorld && !Data.DrunkWorld && !Data.Anniversary && !Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && Data.NoTrapsWorld && Data.IsHardMode)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay")) //This should have the same if not similar texture to the normal one
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region RemixSeedIcon
                if (tempDict[path].contagion && Data.RemixWorld && !Data.DrunkWorld && !Data.Anniversary && !Data.DontStarve && !Data.ForTheWorthy && !Data.ZenithWorld && !Data.NotTheBees && !Data.NoTrapsWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    UIImage element = new UIImage(ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_Remix"))
                    {
                        Top = new StyleDimension(0f, 0f),
                        Left = new StyleDimension(0f, 0f),
                        IgnoresMouseInteraction = true
                    };
                    worldIcon.Append(element);
                }
                #endregion

                #region EverythingSeedIcon
                if (tempDict[path].contagion && Data.RemixWorld && Data.DrunkWorld)
                {
                    UIElement worldIcon = WorldIcon;
                    Asset<Texture2D> obj = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_Everything", (AssetRequestMode)1); //the everything seed is a mess in 1.4.4 and most likely needs an IL to work properly. This texture would be an overlay version of the whole sprite sheet
                    UIImageFramed uIImageFramed = new UIImageFramed(obj, obj.Frame(7, 16));
                    uIImageFramed.Left = new StyleDimension(0f, 0f);
                    uIImageFramed.OnUpdate += UpdateGlitchAnimation;
                    worldIcon.Append(uIImageFramed);
                }
                #endregion
            }
        }
    }

    //the following is the animating of the icon, this is synced with the normal animation because ima goober that cant IL or reflect properly for the life of me
    protected UIElement GetIconElement()
    {
        WorldFileData Data = (WorldFileData)typeof(AWorldListItem).GetField("_data", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(null);
        if (Data.DrunkWorld && Data.RemixWorld)
        {
            Asset<Texture2D> obj = ModContent.Request<Texture2D>("Avalon/Assets/Textures/UI/WorldCreation/IconContagionOverlay_Everything");
            UIImageFramed uIImageFramed = new UIImageFramed(obj, obj.Frame(7, 16));
            uIImageFramed.Left = new StyleDimension(4f, 0f);
            uIImageFramed.OnUpdate += UpdateGlitchAnimation;
            return uIImageFramed;
        }
        return null;
    }

    protected int _glitchFrameCounter;

    protected int _glitchFrame;

    protected int _glitchVariation;

    private void UpdateGlitchAnimation(UIElement affectedElement)
    {
        _ = _glitchFrame;
        int minValue = 3;
        int num = 3;
        if (_glitchFrame == 0)
        {
            minValue = 15;
            num = 120;
        }
        if (++_glitchFrameCounter >= Main.rand.Next(minValue, num + 1))
        {
            _glitchFrameCounter = 0;
            _glitchFrame = (_glitchFrame + 1) % 16;
            if ((_glitchFrame == 4 || _glitchFrame == 8 || _glitchFrame == 12) && Main.rand.Next(3) == 0)
            {
                _glitchVariation = Main.rand.Next(7);
            }
        }
        (affectedElement as UIImageFramed).SetFrame(7, 16, _glitchVariation, _glitchFrame, 0, 0);
    }
}
