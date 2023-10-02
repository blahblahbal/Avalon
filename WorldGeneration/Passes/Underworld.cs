using Avalon.Tiles;
using Avalon.Tiles.Ores;
using Avalon.WorldGeneration.Structures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class Underworld : GenPass
{
    public Underworld() : base("Avalon Underworld", 120f) { }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Generating Caesium Blastplains";
        #region caesium blastplains
        int caesiumXPosLeft = Main.maxTilesX - (Main.maxTilesX / 5) - 15;
        int caesiumXPosRight = Main.maxTilesX - (Main.maxTilesX / 5);
        int caesiumMaxRight = Main.maxTilesX - 20;
        if (Main.drunkWorld)
        {
            caesiumXPosLeft = Main.maxTilesX - (Main.maxTilesX / 3) - 15;
            caesiumXPosRight = Main.maxTilesX - (Main.maxTilesX / 3);
            caesiumMaxRight = Main.maxTilesX - (Main.maxTilesX / 5) + 50;
        }
        if (GenVars.dungeonSide < 0 && !Main.drunkWorld)
        {
            int caesiumLeftSidePosXLeft  = Main.maxTilesX / 5;
            int caesiumLeftSidePosXRight = Main.maxTilesX / 5 + 20;
            int caesiumMaxLeft = 20;

            // make little blobs on the edge
            for (int q = caesiumLeftSidePosXLeft; q < caesiumLeftSidePosXRight; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if (q > caesiumLeftSidePosXLeft - 5)
                    {
                        if (WorldGen.genRand.NextBool(5) && Main.tile[q, z].HasTile &&
                            Main.tile[q, z].TileType == TileID.Ash)
                        {
                            WorldGen.TileRunner(q, z, WorldGen.genRand.Next(12, 19), WorldGen.genRand.Next(12, 18),
                                ModContent.TileType<BlastedStone>());
                        }
                    }

                }
            }

            // shave some stuff off the bottom layer of hell
            for (int q = caesiumMaxLeft; q < caesiumLeftSidePosXLeft; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if (q % 6 == 0 && z > Main.maxTilesY - 140 && z < Main.maxTilesY - 120)
                    {
                        Utils.TileRunnerSpecial(q, z, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(20, 30), -2);
                    }
                    if (z > Main.maxTilesY - 115 && z < Main.maxTilesY - 95)
                    {
                        Main.tile[q, z].LiquidAmount = 0;
                    }
                }
            }
            // make the majority of the blastplains
            for (int q = caesiumMaxLeft; q < caesiumLeftSidePosXLeft; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if ((Main.tile[q, z].TileType == TileID.Ash || Main.tile[q, z].TileType == TileID.Hellstone || Main.tile[q, z].TileType == TileID.AshGrass) &&
                        Main.tile[q, z].HasTile)
                    {
                        Main.tile[q, z].TileType = (ushort)ModContent.TileType<BlastedStone>();
                    }
                    if (Main.tile[q, z].TileType == TileID.AshVines || Main.tile[q, z].TileType == TileID.AshPlants || Main.tile[q, z].TileType == TileID.TreeAsh)
                    {
                        WorldGen.KillTile(q, z);
                    }
                    // up spikes
                    if (z < Main.maxTilesY - 100 && z > Main.maxTilesY - 110)
                    {
                        if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                        {
                            if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                            {
                                if (q % WorldGen.genRand.Next(30, 45) == 0)
                                {
                                    z = Utils.CaesiumTileCheck(q, z, -1);
                                    z += WorldGen.genRand.Next(12);
                                    MakeSpike(q, z, WorldGen.genRand.Next(15, 22), WorldGen.genRand.Next(8, 13), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), -1);
                                    //if (WorldGen.genRand.NextBool(3))
                                    //{
                                    //    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), -1);
                                    //}
                                }
                            }
                        }
                    }
                    // down spikes
                    if (z < Main.maxTilesY - 185 && z > Main.maxTilesY - 195)
                    {
                        if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                        {
                            if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                            {
                                if (q % WorldGen.genRand.Next(10, 15) == 0)
                                {
                                    z = Utils.CaesiumTileCheck(q, z, 1);
                                    MakeSpike(q, z, WorldGen.genRand.Next(15, 22), WorldGen.genRand.Next(8, 13), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), 1);
                                    //if (WorldGen.genRand.NextBool(3))
                                    //{
                                    //    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), 1);
                                    //}
                                }
                            }
                        }
                    }
                    if (WorldGen.genRand.NextBool(50))
                        Utils.OreRunner(q, z, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(5, 8), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>());
                }
            }

            for (int q = caesiumMaxLeft; q < caesiumLeftSidePosXLeft; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if (q % 100 < 33 && z > Main.maxTilesY - 175)
                    {
                        if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                        {
                            if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                            {
                                Main.tile[q, z].TileType = (ushort)ModContent.TileType<LaziteGrass>();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // make little blobs on the edge
            for (int q = caesiumXPosLeft; q < caesiumXPosRight; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if (q > caesiumXPosLeft + 5)
                    {
                        if (WorldGen.genRand.NextBool(5) && Main.tile[q, z].HasTile &&
                            Main.tile[q, z].TileType == TileID.Ash)
                        {
                            WorldGen.TileRunner(q, z, WorldGen.genRand.Next(12, 19), WorldGen.genRand.Next(12, 18),
                                ModContent.TileType<BlastedStone>());
                        }
                    }

                }
            }

            // shave some stuff off the bottom layer of hell
            for (int q = caesiumXPosRight; q < caesiumMaxRight; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if (q % 6 == 0 && z > Main.maxTilesY - 140 && z < Main.maxTilesY - 120)
                    {
                        Utils.TileRunnerSpecial(q, z, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(20, 30), -2);
                    }
                    if (z > Main.maxTilesY - 115 && z < Main.maxTilesY - 95)
                    {
                        Main.tile[q, z].LiquidAmount = 0;
                    }
                }
            }

            // make the majority of the blastplains
            for (int q = caesiumXPosRight; q < caesiumMaxRight; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if ((Main.tile[q, z].TileType == TileID.Ash || Main.tile[q, z].TileType == TileID.Hellstone || Main.tile[q, z].TileType == TileID.AshGrass) &&
                        Main.tile[q, z].HasTile)
                    {
                        Main.tile[q, z].TileType = (ushort)ModContent.TileType<BlastedStone>();
                    }
                    if (Main.tile[q, z].TileType == TileID.AshVines || Main.tile[q, z].TileType == TileID.AshPlants)
                    {
                        WorldGen.KillTile(q, z);
                    }
                    // up spikes
                    if (z < Main.maxTilesY - 100 && z > Main.maxTilesY - 110)
                    {
                        if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                        {
                            if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                            {
                                if (q % WorldGen.genRand.Next(30, 45) == 0)
                                {
                                    z = Utils.CaesiumTileCheck(q, z, -1);
                                    z += WorldGen.genRand.Next(12);
                                    MakeSpike(q, z, WorldGen.genRand.Next(15, 22), WorldGen.genRand.Next(8, 13), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), -1);
                                    //if (WorldGen.genRand.NextBool(3))
                                    //{
                                    //    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), -1);
                                    //}
                                }
                            }
                        }
                    }
                    // down spikes
                    if (z < Main.maxTilesY - 185 && z > Main.maxTilesY - 195)
                    {
                        if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                        {
                            if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                            {
                                if (q % WorldGen.genRand.Next(10, 15) == 0)
                                {
                                    z = Utils.CaesiumTileCheck(q, z, 1);
                                    // 15, 22 / 8, 13
                                    MakeSpike(q, z, WorldGen.genRand.Next(15, 22), WorldGen.genRand.Next(8, 13), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), 1);
                                    //if (WorldGen.genRand.NextBool(3))
                                    //{
                                    //    MakeSpike(q, z, WorldGen.genRand.Next(20, 25), WorldGen.genRand.Next(6, 11), (ushort)ModContent.TileType<CaesiumCrystal>(), (ushort)ModContent.TileType<CaesiumOre>(), 1);
                                    //}
                                }
                            }
                        }
                    }
                    if (WorldGen.genRand.NextBool(50))
                        Utils.OreRunner(q, z, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(5, 8), (ushort)ModContent.TileType<CaesiumOre>(), (ushort)ModContent.TileType<CaesiumCrystal>());
                }
            }

            for (int q = caesiumXPosLeft; q < caesiumXPosRight; q++)
            {
                for (int z = Main.maxTilesY - 250; z < Main.maxTilesY - 20; z++)
                {
                    if (q % 100 < 33 && z > Main.maxTilesY - 175)
                    {
                        if ((Main.tile[q, z].HasTile && !Main.tile[q, z - 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q, z + 1].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q - 1, z].HasTile) ||
                            (Main.tile[q, z].HasTile && !Main.tile[q + 1, z].HasTile))
                        {
                            if (Main.tile[q, z].TileType == ModContent.TileType<BlastedStone>())
                            {
                                Main.tile[q, z].TileType = (ushort)ModContent.TileType<LaziteGrass>();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        progress.Message = "Generating Hellcastle and the Phantom Garden";
        int hellcastleOriginX = (Main.maxTilesX / 2) - 200;
        int ashenLeft = hellcastleOriginX - 125;
        int ashenRight = hellcastleOriginX + 525;


        //if (Main.drunkWorld)
        //{
        //    hellcastleOriginX = (Main.maxTilesX / 3) - 210;
        //    ashenLeft = (Main.maxTilesX / 3) - 450;
        //    ashenRight = (Main.maxTilesX / 3) + 500;
        //}

        Hellcastle.GenerateHellcastle(hellcastleOriginX, Main.maxTilesY - 330);
        for (int hbx = ashenLeft; hbx < ashenRight; hbx++)
        {
            for (int hby = Main.maxTilesY - 200; hby < Main.maxTilesY - 50; hby++)
            {
                //if (Main.tile[hbx, hby].HasTile &&
                //    (Main.tile[hbx, hby].TileType == TileID.ObsidianBrick ||
                //     Main.tile[hbx, hby].TileType == TileID.HellstoneBrick))
                //{
                //    Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<ImperviousBrick>();
                //    Tile t = Main.tile[hbx, hby];
                //    t.HasTile = true;
                //}
                //if (Main.tile[hbx, hby].WallType == WallID.ObsidianBrickUnsafe ||
                //     Main.tile[hbx, hby].WallType == WallID.HellstoneBrickUnsafe)
                //{
                //    Main.tile[hbx, hby].TileType = (ushort)ModContent.TileType<ImperviousBrick>();
                //    Tile t = Main.tile[hbx, hby];
                //    t.HasTile = true;
                //    WorldGen.KillWall(hbx, hby);
                //}
                if ((Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby - 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx, hby + 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby - 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx - 1, hby + 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby - 1].HasTile) ||
                    (Main.tile[hbx, hby].HasTile && !Main.tile[hbx + 1, hby + 1].HasTile))
                {
                    if (Main.tile[hbx, hby].TileType == TileID.Ash)
                    {
                        SlopeType s = Main.tile[hbx, hby].Slope;
                        Tile t = Main.tile[hbx, hby];
                        t.TileType = (ushort)ModContent.TileType<Ectograss>();
                        t.Slope = s;
                        if (WorldGen.genRand.NextBool(1))
                        {
                            WorldGen.GrowTree(hbx, hby - 1);
                        }
                    }
                }
                if (WorldGen.genRand.NextBool(100))
                {
                    WorldGen.OreRunner(hbx, hby, 4, 4, (ushort)ModContent.TileType<BrimstoneBlock>());
                }
                if (WorldGen.genRand.NextBool(50))
                {
                    WorldGen.OreRunner(hbx, hby, 2, 3, TileID.Hellstone);
                }
            }
        }
    }

    /// <summary>
    /// Makes a spike at the given coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="length">The height/tallness of the spike to generate.</param>
    /// <param name="width">The width/thickness of the spike to generate.</param>
    /// <param name="direction">The vertical direction of the spike; 1 is down, -1 is up.</param>
    public static void MakeSpike(int x, int y, int length, int width, ushort centerType, ushort borderType, int direction = 1)
    {
        // Store the x and y in new vars
        int startX = x;
        int startY = y;

        // Define variables to determine how many tiles to travel in one direction before changing direction
        int howManyTimes = 0;
        int maxTimes = WorldGen.genRand.Next(4) + 2;
        int modifier = 1;

        // Change direction (left/right) of the spike before generating
        if (WorldGen.genRand.NextBool())
        {
            modifier *= -1;
        }

        // Initial assignment of last position
        Vector2 lastPos = new(x, y);

        // Loop until length
        for (int q = 1; q <= length; q++)
        {
            // Grab the distance between the start and the current position
            float distFromStart = Vector2.Distance(new Vector2(startX, startY), new Vector2(x, y));

            // If the distance is divisible by 4 and the width is less than 5, reduce the width
            int w = width;
            if ((int)distFromStart % 4 == 0 || w < 5)
            {
                width--;
            }

            // Put a circle between the last point and the current
            int betweenXPos = (int)(lastPos.X + x) / 2;
            int betweenYPos = (int)(lastPos.Y + y) / 2;

            // weird, centerType and borderType are swapped
            Utils.MakeCircle2(betweenXPos, betweenYPos, (int)((length - q) * 0.4f), centerType, borderType);

            // Make a square/circle of the tile
            Utils.MakeCircle2(x, y, (int)((length - q) * 0.4f), centerType, borderType);

            // Assign the last position to the current position
            lastPos = new(x, y);

            // Make the spike go in the opposite direction after a certain amount of tiles
            howManyTimes++;
            if (howManyTimes % maxTimes == 0)
            {
                modifier *= -1;
                howManyTimes = 0;
                maxTimes = WorldGen.genRand.Next(4) + 2;
            }

            // Add to the x to make the spike turn/curve in a direction
            x += (WorldGen.genRand.Next(2) + 1) * modifier; // * (maxTimes / (WorldGen.genRand.Next(2) + 1));

            // Add a bit to the y coord to make it go up or down depending on the parameter
            if (w > 3)
            {
                y += (WorldGen.genRand.Next(3) + 2) * direction;
            }
            else
            {
                y += 1 * direction;
            }
        }
    }
}
