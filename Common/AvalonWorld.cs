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
using Terraria.Chat;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.GameContent;
using Avalon.NPCs.PreHardmode;

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

    public static int DungeonLocationX;
    public static int JungleLocationX;

    public static CopperVariant CopperOre = CopperVariant.Random;
    public static IronVariant IronOre = IronVariant.Random;
    public static SilverVariant SilverOre = SilverVariant.Random;
    public static GoldVariant GoldOre = GoldVariant.Random;
    public static RhodiumVariant? RhodiumOre { get; set; }

    public static int totalSick; //Amount of Tiles 
    public static int totalSick2; //Amount of tiles calculated again
    public static byte tSick; //Percent of world is infected
    public static int[] NewTileCounts = new int[TileLoader.TileCount]; //to account for all new modded tiles and not just the vanilla tile amount

    public override void OnWorldUnload() //Here we reset the numbers for the calculations to make sure they dont carry over to other worlds
    {
        totalSick = 0;
        totalSick2 = 0;
        tSick = 0;
        //Im not too fimiliar how contagion world tags work but imo it would be best if it was reset in an OnWorldUnload and OnWorldLoad somewhere
    }
    public override void PostAddRecipes()
    {
        ItemTrader.ChlorophyteExtractinator = Hooks.Extractinator.CreateAvalonChlorophyteExtractinator();
    }
    public override void SaveWorldData(TagCompound tag)
    {
        tag["Avalon:RhodiumOre"] = (byte?)RhodiumOre;
        tag["Avalon:WorldEvil"] = (byte)WorldEvil;
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

        tag["Avalon:JungleX"] = JungleLocationX;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("Avalon:JungleX"))
        {
            JungleLocationX = tag.GetAsInt("Avalon:JungleX");
        }

        if (tag.ContainsKey("Avalon:RhodiumOre"))
        {
            RhodiumOre = (RhodiumVariant)tag.Get<byte>("Avalon:RhodiumOre");
        }

        if (tag.ContainsKey("RhodiumOre"))
        {
            RhodiumOre = (RhodiumVariant)tag.Get<byte>("RhodiumOre");
        }

        WorldEvil = (WorldEvil)(tag.Get<byte?>("Avalon:WorldEvil") ?? WorldGen.WorldGenParam_Evil);

        for (var i = 0; i < Main.tile.Width; ++i)
            for (var j = 0; j < Main.tile.Height; ++j)
                if (ContagionCountCollection.Contains(Main.tile[i, j].TileType)) //Better calculations, this is only loaded when the world is loaded
                    totalSick2++;                                                //to make sure that the world doesn't lag when calculating normally


    }
    public override void PreUpdateWorld()
    {
        Main.tileSolidTop[ModContent.TileType<FallenStarTile>()] = Main.dayTime;
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
        // these 2 (num12 and 13) are used for herb spawning; if I revert a change we'll need them
        int num12 = 151;
        int num13 = (int)Utils.Lerp(num12, num12 * 2.8, Utils.Clamp(Main.maxTilesX / 4200.0 - 1.0, 0.0, 1.0));
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
            int num11 = num6 + 1;
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

            #region bone fish spawning
            for (int i = 0; i < 255; i++)
            {
                Player p = Main.player[i];
                if (p.active && !p.dead)
                {
                    float dist = Vector2.Distance(p.position, new Vector2(num5, num6) * 16);
                    if (p.ZoneUnderworldHeight && dist > Main.screenWidth / 2 && dist < Main.screenWidth)
                    {
                        if (Main.tile[num5, num6].LiquidType == LiquidID.Lava && Main.tile[num5, num6].LiquidAmount > 70 && Main.rand.NextBool(AvalonReflection.NPC_spawnRate / 40))
                        {
                            NPC.NewNPC(p.GetSource_Misc(""), num5 * 16, num6 * 16, ModContent.NPCType<BoneFish>());
                        }
                    }
                }
            }
            #endregion

            #region contagion thorny bushes
            if (TileID.Sets.SpreadOverground[Main.tile[num5, num6].TileType])
            {
                int type = Main.tile[num5, num6].TileType;
                if ((type == ModContent.TileType<ContagionThornyBushes>()) && WorldGen.genRand.NextBool(3))
                {
                    WorldGen.GrowSpike(num5, num6, (ushort)ModContent.TileType<ContagionThornyBushes>(), (ushort)ModContent.TileType<Ickgrass>());
                }
                else if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(10) && (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<ContagionJungleGrass>()))
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<ContagionThornyBushes>(), mute: true);
                }
            }
            #endregion

            #region bloodberry and holybird spawning
            if (Main.tile[num5, num9].TileType == TileID.ImmatureHerbs && Main.tile[num5, num9].HasTile && Main.tile[num5, num9].TileFrameX == 54 &&
                (Main.tile[num5, num6].TileType == TileID.CrimsonGrass || Main.tile[num5, num6].TileType == TileID.Crimstone))
            {
                Tile t = Main.tile[num5, num9];
                t.TileType = (ushort)ModContent.TileType<Bloodberry>();
                t.TileFrameX = 0;
            }

            if (Main.tile[num5, num9].TileType == TileID.ImmatureHerbs && Main.tile[num5, num9].HasTile && Main.tile[num5, num9].TileFrameX == 0 &&
                Main.tile[num5, num6].TileType == TileID.HallowedGrass)
            {
                Tile t = Main.tile[num5, num9];
                t.TileType = (ushort)ModContent.TileType<Holybird>();
                t.TileFrameX = 0;
            }
            #endregion

            #region planter box grass growth
            if (Main.tile[num5, num6].TileType == ModContent.TileType<PlanterBoxes>())
            {
                if (!Main.tile[num5, num9].HasTile && WorldGen.genRand.NextBool(2))
                {
                    Tile tile = Main.tile[num5, num9];
                    tile.HasTile = true;
                    tile.TileType = TileID.Plants;
                    int style = WorldGen.genRand.NextFromList<int>(6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 24, 27, 30, 33, 36, 39, 42);
                    switch (style)
                    {
                        case 21:
                        case 24:
                        case 27:
                        case 30:
                        case 33:
                        case 36:
                        case 39:
                        case 42:
                            style += WorldGen.genRand.Next(3);
                            break;
                    }
                    tile.TileFrameX = (short)(style * 18);
                    //WorldGen.PlaceTile(num5, num9, 3, mute: true);
                    if (Main.netMode == NetmodeID.Server && Main.tile[num5, num9].HasTile)
                    {
                        NetMessage.SendTileSquare(-1, num5, num9);
                    }
                }
            }
            #endregion

            #region killing things if the block above/below isn't the necessary type
            // kill contagion vines if block above isn't contagion grass
            if (!(Main.tile[num5, num9].TileType == ModContent.TileType<Ickgrass>() || Main.tile[num5, num9].TileType == ModContent.TileType<ContagionJungleGrass>() || Main.tile[num5, num9].TileType == ModContent.TileType<ContagionVines>()) &&
                Main.tile[num5, num6].TileType == ModContent.TileType<ContagionVines>())
            {
                WorldGen.KillTile(num5, num6);
            }
            //// kill contagion stalac if the block above isn't chunkstone
            //if (Main.tile[num5, num9].TileType != ModContent.TileType<Chunkstone>() &&
            //    Main.tile[num5, num6].TileType == ModContent.TileType<ContagionStalactgmites>() &&
            //    (Main.tile[num5, num6].TileFrameY == 0 || Main.tile[num5, num6].TileFrameY == 72))
            //{
            //    WorldGen.KillTile(num5, num6);
            //}
            //// kill contagion stalac if the block below isn't chunkstone
            //if (Main.tile[num5, num11].TileType != ModContent.TileType<Chunkstone>() &&
            //    Main.tile[num5, num6].TileType == ModContent.TileType<ContagionStalactgmites>() &&
            //    (Main.tile[num5, num6].TileFrameY == 54 || Main.tile[num5, num6].TileFrameY == 90))
            //{
            //    WorldGen.KillTile(num5, num6);
            //}
            // kill contagion short grass if block below isn't contagion grass
            if (!(Main.tile[num5, num11].TileType == ModContent.TileType<Ickgrass>() || Main.tile[num5, num11].TileType == ModContent.TileType<ContagionJungleGrass>()) && Main.tile[num5, num6].TileType == ModContent.TileType<ContagionShortGrass>())
            {
                WorldGen.KillTile(num5, num6);
            }
            // kill barfbush if block below isn't contagion grass or chunkstone
            if (!(Main.tile[num5, num11].TileType == ModContent.TileType<Ickgrass>() || Main.tile[num5, num11].TileType == ModContent.TileType<ContagionJungleGrass>() || Main.tile[num5, num11].TileType == ModContent.TileType<Chunkstone>()) &&
                Main.tile[num5, num6].TileType == ModContent.TileType<Barfbush>())
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
            #endregion

            #region large herb growth
            if (Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
                Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
                Main.tile[num5, num6].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
            {
                GrowLargeHerb(num5, num6);
            }
            #endregion

            #region impgrass
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Ectograss>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                bool flag2 = false;
                for (int m = num7; m < num8; m++)
                {
                    for (int n = num9; n < num10; n++)
                    {
                        if ((num5 != m || num6 != n) && Main.tile[m, n].HasTile)
                        {
                            if (Main.tile[m, n].TileType == TileID.Ash)
                            {
                                TileColorCache color = Main.tile[num5, num6].BlockColorAndCoating();
                                WorldGen.SpreadGrass(m, n, TileID.Ash, ModContent.TileType<Ectograss>(), false,
                                    color);
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
            #endregion

            #region contagion shortgrass/barfbush spawning
            if (Main.tile[num5, num6].TileType == ModContent.TileType<Ickgrass>() || Main.tile[num5, num6].TileType == ModContent.TileType<ContagionJungleGrass>())
            {
                int num14 = Main.tile[num5, num6].TileType;
                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 &&
                    !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid &&
                    WorldGen.genRand.NextBool(5) && (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>()))
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

                if (!Main.tile[num5, num9].HasTile && Main.tile[num5, num9].LiquidAmount == 0 && !Main.tile[num5, num6].IsHalfBlock && Main.tile[num5, num6].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(num6 > Main.worldSurface ? 500 : 200) && (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>()))
                {
                    WorldGen.PlaceTile(num5, num9, ModContent.TileType<Barfbush>(), true, false, -1, 0);
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
                            if (Main.tile[m, n].TileType == 0 || ((num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>()) && Main.tile[m, n].TileType == TileID.Grass))
                            {
                                TileColorCache color = Main.tile[num5, num6].BlockColorAndCoating();
                                WorldGen.SpreadGrass(m, n, 0, num14, false, color);
                                if (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.Grass, ModContent.TileType<Ickgrass>(), false, color);
                                }
                                if (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.JungleGrass, ModContent.TileType<ContagionJungleGrass>(), false, color);
                                }
                                if (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>())
                                {
                                    WorldGen.SpreadGrass(m, n, TileID.HallowedGrass, ModContent.TileType<Ickgrass>(), false, color);
                                }
                                if (Main.tile[m, n].TileType == ModContent.TileType<Ickgrass>() || Main.tile[m, n].TileType == ModContent.TileType<ContagionJungleGrass>())
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
            #endregion

            #region ectovines growing
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
            #endregion

            #region contagion vines growing
            if ((Main.tile[num5, num6].TileType == ModContent.TileType<Ickgrass>() ||
                 Main.tile[num5, num6].TileType == ModContent.TileType<ContagionJungleGrass>() ||
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
                        (Main.tile[num5, num47].TileType == ModContent.TileType<Ickgrass>() ||
                         Main.tile[num5, num47].TileType == ModContent.TileType<ContagionJungleGrass>()) &&
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
            #endregion
        }
    }

    public static void GenerateSulphur()
    {
        Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

        for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
        {
            int x = Main.rand.Next(100, Main.maxTilesX - 100);
            int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 150);
            WorldGen.OreRunner(x, y, Main.rand.Next(3, 6), Main.rand.Next(3, 5),
                (ushort)ModContent.TileType<Tiles.Ores.SulphurOre>());
        }

        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            Main.NewText("The underground smells like rotten eggs...", 210, 183, 4);
        }
        else if (Main.netMode == NetmodeID.Server)
        {
            ChatHelper.BroadcastChatMessage(
                NetworkText.FromLiteral("The underground smells like rotten eggs..."),
                new Color(210, 183, 4));
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
        float ContagionStrength = (float)ModContent.GetInstance<BiomeTileCounts>().ContagionTiles / 350f;
        ContagionStrength = Math.Min(ContagionStrength, 1f);

        int sunR = backgroundColor.R;
        int sunG = backgroundColor.G;
        int sunB = backgroundColor.B;
        sunR -= (int)(212f * ContagionStrength / 2 * (backgroundColor.R / 255f));
        sunB -= (int)(255f * ContagionStrength / 2 * (backgroundColor.B / 255f));
        sunG -= (int)(127f * ContagionStrength / 2 * (backgroundColor.G / 255f));
        sunR = Utils.Clamp(sunR, 15, 255);
        sunG = Utils.Clamp(sunG, 15, 255);
        sunB = Utils.Clamp(sunB, 15, 255);
        backgroundColor.R = (byte)sunR;
        backgroundColor.G = (byte)sunG;
        backgroundColor.B = (byte)sunB;

        tileColor.R -= (byte)(212f * ContagionStrength / 2.2f * (backgroundColor.R / 255f) * 1.6f);
        tileColor.G -= (byte)(212f * ContagionStrength / 2.4f * (backgroundColor.G / 255f) * 1.5f);
        tileColor.B -= (byte)(212f * ContagionStrength / 1.8f * (backgroundColor.B / 255f) * 1.8f);
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
                Tile tileUp = Main.tile[x, y - 1];
                int typeUp = tileUp.TileType;
                Tile tileDown = Main.tile[x, y + 1];
                int typeDown = tileDown.TileType;
                if (type == ModContent.TileType<Ickgrass>())
                {
                    tile.TileType = TileID.Grass;
                    if (typeDown == ModContent.TileType<ContagionVines>())
                    {
                        for (int downVar = 1; typeDown == ModContent.TileType<ContagionVines>() && !Main.tileSolid[typeDown]; downVar++)
                        {
                            Tile tileDownVar = Main.tile[x, y + downVar];
                            int typeDownVar = tileDownVar.TileType;
                            if (typeDownVar == ModContent.TileType<ContagionVines>())
                            {
                                tileDownVar.TileType = TileID.Vines;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (type == ModContent.TileType<ContagionJungleGrass>())
                {
                    tile.TileType = TileID.JungleGrass;
                    if (typeDown == ModContent.TileType<ContagionVines>())
                    {
                        for (int downVar = 1; typeDown == ModContent.TileType<ContagionVines>() && !Main.tileSolid[typeDown]; downVar++)
                        {
                            Tile tileDownVar = Main.tile[x, y + downVar];
                            int typeDownVar = tileDownVar.TileType;
                            if (typeDownVar == ModContent.TileType<ContagionVines>())
                            {
                                tileDownVar.TileType = TileID.JungleVines;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
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
                else if (type == ModContent.TileType<ContagionShortGrass>())
                {
                    tile.TileType = TileID.Plants;
                }
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
                if (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<ContagionJungleGrass>() || type == TileID.CrimsonGrass || type == TileID.CrimsonJungleGrass || type == TileID.CorruptGrass || type == TileID.CorruptJungleGrass || type == TileID.Grass || type == TileID.HallowedGrass)
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
                Tile tileUp = Main.tile[x, y - 1];
                int typeUp = tileUp.TileType;
                Tile tileDown = Main.tile[x, y + 1];
                int typeDown = tileDown.TileType;
                if (TileID.Sets.Conversion.Grass[type] && type != ModContent.TileType<Ickgrass>())
                {
                    tile.TileType = (ushort)ModContent.TileType<Ickgrass>();
                    if (typeUp == TileID.Plants || typeUp == TileID.Plants2 || typeUp == TileID.CorruptPlants || typeUp == TileID.CrimsonPlants || typeUp == TileID.HallowedPlants || typeUp == TileID.HallowedPlants2 || typeUp == TileID.JunglePlants || typeUp == TileID.JunglePlants2)
                    {
                        tileUp.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                    }
                    if (TileID.Sets.IsVine[typeDown] && typeDown != ModContent.TileType<ContagionVines>())
                    {
                        for (int downVar = 1; TileID.Sets.IsVine[typeDown] && !Main.tileSolid[typeDown]; downVar++)
                        {
                            Tile tileDownVar = Main.tile[x, y + downVar];
                            int typeDownVar = tileDownVar.TileType;
                            if (TileID.Sets.IsVine[typeDownVar] && typeDownVar != ModContent.TileType<ContagionVines>())
                            {
                            tileDownVar.TileType = (ushort)ModContent.TileType<ContagionVines>();
                            }
                            else
                            {
                                break;
                            }    
                        }
                    }
                }
                if (TileID.Sets.Conversion.JungleGrass[type] && type != ModContent.TileType<ContagionJungleGrass>())
                {
                    tile.TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                    if (typeUp == TileID.Plants || typeUp == TileID.Plants2 || typeUp == TileID.CorruptPlants || typeUp == TileID.CrimsonPlants || typeUp == TileID.HallowedPlants || typeUp == TileID.HallowedPlants2 || typeUp == TileID.JunglePlants || typeUp == TileID.JunglePlants2)
                    {
                        tileUp.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                    }
                    if (TileID.Sets.IsVine[typeDown] && typeDown != ModContent.TileType<ContagionVines>())
                    {
                        for (int downVar = 1; TileID.Sets.IsVine[typeDown] && !Main.tileSolid[typeDown]; downVar++)
                        {
                            Tile tileDownVar = Main.tile[x, y + downVar];
                            int typeDownVar = tileDownVar.TileType;
                            if (TileID.Sets.IsVine[typeDownVar] && typeDownVar != ModContent.TileType<ContagionVines>())
                            {
                                tileDownVar.TileType = (ushort)ModContent.TileType<ContagionVines>();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
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
                else if (type == TileID.Plants || type == TileID.Plants2 || type == TileID.CorruptPlants || type == TileID.CrimsonPlants || type == TileID.HallowedPlants || type == TileID.HallowedPlants2 || type == TileID.JunglePlants || type == TileID.JunglePlants2)
                {
                    if ((tile.TileFrameX > 144 && tile.TileFrameX < 162 || tile.TileFrameX % 414 > 144 && tile.TileFrameX % 414 < 162) && type != TileID.CrimsonPlants)
                    {
                        tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                        tile.TileFrameX %= 414 - 18;
                    }
                    else if (tile.TileFrameX % 414 == 144 && type == TileID.CrimsonPlants)
                    {
                        tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                        tile.TileFrameX %= 414 - 18;
                    }
                    else if (tile.TileFrameX % 414 == 270 && type == TileID.CrimsonPlants)
                    {
                        tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                        tile.TileFrameX = 144;
                    }
                    else
                    {
                        tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
                        tile.TileFrameX %= 414;
                    }
                    if (TileID.Sets.Conversion.Grass[typeDown] && typeDown != ModContent.TileType<Ickgrass>())
                    {
                        tileDown.TileType = (ushort)ModContent.TileType<Ickgrass>();
                    }
                    if (TileID.Sets.Conversion.JungleGrass[typeDown] && typeDown != ModContent.TileType<Ickgrass>())
                    {
                        tileDown.TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                    }
                }
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
        On_WorldGen.CountTiles += On_WorldGen_CountTiles;
        On_WorldGen.AddUpAlignmentCounts += On_WorldGen_AddUpAlignmentCounts;
    }

    public override void Unload()
    {
        Terraria.GameContent.UI.States.On_UIWorldSelect.UpdateWorldsList -= On_UIWorldSelect_UpdateWorldsList;
        On_WorldGen.CountTiles -= On_WorldGen_CountTiles;
        On_WorldGen.AddUpAlignmentCounts -= On_WorldGen_AddUpAlignmentCounts;
    }

    #region World%Calculations
    private void On_WorldGen_AddUpAlignmentCounts(On_WorldGen.orig_AddUpAlignmentCounts orig, bool clearCounts)
    {
        orig.Invoke(clearCounts);
        if (clearCounts)
        {
            totalSick2 = 0;
        }
        /*for (int i = 0; i < ContagionCountCollection.Count; i++) {
            totalSick2 += WorldGen.tileCounts[ContagionCountCollection[i]]; //Vanilla's way of calulating the tiles, we dont use this due to falws 
        }*/

        WorldGen.totalSolid2 +=
            WorldGen.tileCounts[ModContent.TileType<Chunkstone>()] +
            WorldGen.tileCounts[ModContent.TileType<Ickgrass>()] +
            WorldGen.tileCounts[ModContent.TileType<ContagionJungleGrass>()] +
            WorldGen.tileCounts[ModContent.TileType<Snotsand>()] +
            WorldGen.tileCounts[ModContent.TileType<YellowIce>()] +
            WorldGen.tileCounts[ModContent.TileType<Snotsandstone>()] +
            WorldGen.tileCounts[ModContent.TileType<HardenedSnotsand>()];

        Array.Clear(WorldGen.tileCounts, 0, WorldGen.tileCounts.Length);
    }

    private void On_WorldGen_CountTiles(On_WorldGen.orig_CountTiles orig, int X)
    {
        orig.Invoke(X);
        if (X == 0)
        {
            totalSick = totalSick2;
            tSick = (byte)Math.Round((double)totalSick / (double)WorldGen.totalSolid * 100.0);
            if (tSick == 0 && totalSick > 0)
            {
                tSick = 1;
            }
            if (Main.netMode == 2)
            {
                NetMessage.SendData(MessageID.TileCounts);
            }
            totalSick2 = 0;
        }
        ushort num = 0;
        ushort num2 = 0;
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        do
        {
            int num6;
            int num7;
            if (num4 == 0)
            {
                num6 = 0;
                num5 = (int)(Main.worldSurface + 1.0);
                num7 = 5;
            }
            else
            {
                num6 = num5;
                num5 = Main.maxTilesY;
                num7 = 1;
            }
            for (int i = num6; i < num5; i++)
            {
                Tile tile = Main.tile[X, i];
                if (tile == null)
                {
                    Tile TILE = Main.tile[X, i];
                    tile = (TILE = new Tile());
                }
                num = tile.TileType;
                if (num != 0 || tile.HasTile)
                {
                    if (num == num2)
                    {
                        num3 += num7;
                        continue;
                    }
                    NewTileCounts[num2] += num3;
                    num2 = num;
                    num3 = num7;
                }
            }
            NewTileCounts[num2] += num3;
            num3 = 0;
            num4++;
        }
        while (num4 < 2);
        WorldGen.AddUpAlignmentCounts();
    }

    public static List<int> ContagionCountCollection; //Our own TileID.Sets for the tiles that are counted towards world %
                                                      //Note: (MUST HAVE THE SAME TILES AS On_WorldGen_AddUpAlignmentCounts SOLID TILE ADD UP SECTION!!!)

    public override void PostSetupContent()
    {
        ContagionCountCollection = new List<int> {
                ModContent.TileType<Chunkstone>(),
                ModContent.TileType<Ickgrass>(),
                ModContent.TileType<ContagionJungleGrass>(),
                ModContent.TileType<Snotsand>(),
                ModContent.TileType<YellowIce>(),
                ModContent.TileType<Snotsandstone>(),
                ModContent.TileType<HardenedSnotsand>()
            };
    }
    #endregion

    #region WorldIconOverlay
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
            if ((_glitchFrame == 4 || _glitchFrame == 8 || _glitchFrame == 12) && Main.rand.NextBool(3))
            {
                _glitchVariation = Main.rand.Next(7);
            }
        }
        (affectedElement as UIImageFramed).SetFrame(7, 16, _glitchVariation, _glitchFrame, 0, 0);
    }
    #endregion
}
