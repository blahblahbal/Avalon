using Avalon.Common;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Walls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Passes;

internal class Contagion : GenPass
{
    public Contagion(string name, float loadWeight) : base(name, loadWeight)
    {
    }
    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Making the world gross";
        int num208 = 0;
        while (num208 < Main.maxTilesX * 0.00045)
        {
            float num209 = (float)(num208 / (Main.maxTilesX * 0.00045));
            bool flag12 = false;
            int num210 = 0;
            int num211 = 0;
            int num212 = 0;
            while (!flag12)
            {
                int num213 = 0;
                flag12 = true;
                int num214 = Main.maxTilesX / 2;
                int num215 = 200;
                if (GenVars.dungeonX < Main.maxTilesX * 0.5)
                {
                    num210 = WorldGen.genRand.Next(600, Main.maxTilesX - 320);
                }
                else
                {
                    num210 = WorldGen.genRand.Next(320, Main.maxTilesX - 600);
                }
                num211 = num210 - WorldGen.genRand.Next(200) - 100;
                num212 = num210 + WorldGen.genRand.Next(200) + 100;
                if (num211 < 285)
                {
                    num211 = 285;
                }
                if (num212 > Main.maxTilesX - 285)
                {
                    num212 = Main.maxTilesX - 285;
                }
                if (num211 < GenVars.evilBiomeBeachAvoidance)
                {
                    num211 = GenVars.evilBiomeBeachAvoidance;
                }
                if (num212 > Main.maxTilesX - GenVars.evilBiomeBeachAvoidance)
                {
                    num212 = Main.maxTilesX - GenVars.evilBiomeBeachAvoidance;
                }
                if (num210 < num211 + GenVars.evilBiomeAvoidanceMidFixer)
                {
                    num210 = num211 + GenVars.evilBiomeAvoidanceMidFixer;
                }
                if (num210 > num212 - GenVars.evilBiomeAvoidanceMidFixer)
                {
                    num210 = num212 - GenVars.evilBiomeAvoidanceMidFixer;
                }
                if (GenVars.dungeonSide < 0 && num211 < 400)
                {
                    num211 = 400;
                }
                else if (GenVars.dungeonSide > 0 && num211 > Main.maxTilesX - 400)
                {
                    num211 = Main.maxTilesX - 400;
                }
                if (num210 > num214 - num215 && num210 < num214 + num215)
                {
                    flag12 = false;
                }
                if (num211 > num214 - num215 && num211 < num214 + num215)
                {
                    flag12 = false;
                }
                if (num212 > num214 - num215 && num212 < num214 + num215)
                {
                    flag12 = false;
                }
                if (num210 > GenVars.UndergroundDesertLocation.X && num210 < GenVars.UndergroundDesertLocation.X + GenVars.UndergroundDesertLocation.Width)
                {
                    flag12 = false;
                }
                if (num211 > GenVars.UndergroundDesertLocation.X && num211 < GenVars.UndergroundDesertLocation.X + GenVars.UndergroundDesertLocation.Width)
                {
                    flag12 = false;
                }
                if (num212 > GenVars.UndergroundDesertLocation.X && num212 < GenVars.UndergroundDesertLocation.X + GenVars.UndergroundDesertLocation.Width)
                {
                    flag12 = false;
                }
                for (int num216 = num211; num216 < num212; num216++)
                {
                    for (int num217 = 0; num217 < (int)Main.worldSurface; num217 += 5)
                    {
                        if (Main.tile[num216, num217].HasTile && Main.tileDungeon[Main.tile[num216, num217].TileType])
                        {
                            flag12 = false;
                            break;
                        }
                        if (!flag12)
                        {
                            break;
                        }
                    }
                }
                if (num213 < 200 && GenVars.jungleOriginX > num211 && GenVars.jungleOriginX < num212)
                {
                    num213++;
                    flag12 = false;
                }
            }
            ContagionRunner(num210, (int)GenVars.worldSurfaceLow - 10 + (Main.maxTilesY / 8));
            for (int num218 = num211; num218 < num212; num218++)
            {
                int num219 = (int)GenVars.worldSurfaceLow;
                while (num219 < Main.worldSurface - 1.0)
                {
                    if (Main.tile[num218, num219].HasTile)
                    {
                        int num220 = num219 + WorldGen.genRand.Next(10, 14);
                        for (int num221 = num219; num221 < num220; num221++)
                        {
                            if ((Main.tile[num218, num221].TileType == TileID.Mud || Main.tile[num218, num221].TileType == TileID.JungleGrass) && num218 >= num211 + WorldGen.genRand.Next(5) && num218 < num212 - WorldGen.genRand.Next(5))
                            {
                                Main.tile[num218, num221].TileType = TileID.Dirt;
                            }
                        }
                        break;
                    }
                    num219++;
                }
            }
            double num222 = Main.worldSurface + 40.0;
            for (int num223 = num211; num223 < num212; num223++)
            {
                num222 += WorldGen.genRand.Next(-2, 3);
                if (num222 < Main.worldSurface + 30.0)
                {
                    num222 = Main.worldSurface + 30.0;
                }
                if (num222 > Main.worldSurface + 50.0)
                {
                    num222 = Main.worldSurface + 50.0;
                }
                int num57 = num223;
                bool flag13 = false;
                int num224 = (int)GenVars.worldSurfaceLow;
                while (num224 < num222)
                {
                    if (Main.tile[num57, num224].HasTile)
                    {
                        if (Main.tile[num57, num224].TileType == TileID.Sand && num57 >= num211 + WorldGen.genRand.Next(5) && num57 <= num212 - WorldGen.genRand.Next(5))
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsand>();
                        }
                        if (Main.tile[num57, num224].TileType == TileID.Dirt && num224 < Main.worldSurface - 1.0 && !flag13)
                        {
                            WorldGen.grassSpread = 0;
                            WorldGen.SpreadGrass(num57, num224, 0, ModContent.TileType<Ickgrass>(), true, default);
                        }
                        flag13 = true;
                        if (Main.tile[num57, num224].TileType == TileID.Stone && num57 >= num211 + WorldGen.genRand.Next(5) && num57 <= num212 - WorldGen.genRand.Next(5))
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Chunkstone>();
                        }
                        if (Main.tile[num57, num224].TileType == TileID.Grass)
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Ickgrass>();
                        }
                        if (Main.tile[num57, num224].TileType == TileID.IceBlock)
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<YellowIce>();
                        }
                        if (Main.tile[num57, num224].TileType == TileID.HardenedSand)
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                        }
                        if (Main.tile[num57, num224].TileType == TileID.Sandstone)
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                        }
                    }
                    num224++;
                }
            }
            int num225 = WorldGen.genRand.Next(10, 15);
            for (int num226 = 0; num226 < num225; num226++)
            {
                int num227 = 0;
                bool flag14 = false;
                int num228 = 0;
                while (!flag14)
                {
                    num227++;
                    int num229 = WorldGen.genRand.Next(num211 - num228, num212 + num228);
                    int num230 = WorldGen.genRand.Next((int)(Main.worldSurface - num228 / 2), (int)(Main.worldSurface + 100.0 + num228));
                    if (num227 > 100)
                    {
                        num228++;
                        num227 = 0;
                    }
                    if (!Main.tile[num229, num230].HasTile)
                    {
                        while (!Main.tile[num229, num230].HasTile)
                        {
                            num230++;
                        }
                        num230--;
                    }
                    else
                    {
                        while (Main.tile[num229, num230].HasTile && num230 > Main.worldSurface)
                        {
                            num230--;
                        }
                    }
                    if (num228 > 10 || (Main.tile[num229, num230 + 1].HasTile && Main.tile[num229, num230 + 1].TileType == TileID.Crimstone))
                    {
                        WorldGen.Place3x2(num229, num230, (ushort)ModContent.TileType<IckyAltar>());
                        if (Main.tile[num229, num230].TileType == (ushort)ModContent.TileType<IckyAltar>())
                        {
                            flag14 = true;
                        }
                    }
                    if (num228 > 100)
                    {
                        flag14 = true;
                    }
                }
            }
            num208++;
        }
    }

    /// <summary>
    /// Contagion generation method.
    /// </summary>
    /// <param name="i">The x coordinate to start the generation at.</param>
    /// <param name="j">The y coordinate to start the generation at.</param>
    private static void ContagionRunner(int i, int j)
    {
        int j2 = j;
        int radius = WorldGen.genRand.Next(50, 61);
        int rad2 = WorldGen.genRand.Next(20, 26);
        j = Utils.TileCheck(i) + radius + 50;
        Vector2 center = new Vector2(i, j);
        List<Vector2> points = new List<Vector2>();
        List<Vector2> pointsToGoTo = new List<Vector2>();
        List<double> angles = new List<double>();
        List<Vector2> outerCircles = new List<Vector2>(); // the circles at the ends of the first tunnels
        List<Vector2> secondaryCircles = new List<Vector2>(); // the circles at the ends of the outer circles
        List<Vector2> secondCircleStartPoints = new List<Vector2>();
        List<Vector2> secondCircleEndpoints = new List<Vector2>();
        List<double> secondCirclePointsAroundCircle = new List<double>();
        List<Vector2> exclusions = new List<Vector2>();
        List<Vector2> excludedPointsForOuterTunnels = new List<Vector2>();
        //new List<Vector2>();
        #region make the main circle
        for (int k = i - radius; k <= i + radius; k++)
        {
            for (int l = j - radius; l <= j + radius; l++)
            {
                float dist = Vector2.Distance(new Vector2(k, l), new Vector2(i, j));
                if (dist <= radius && dist >= (radius - 29))
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = false;
                }
                if (((dist <= radius && dist >= radius - 7) || (dist <= (float)(radius - 22) && dist >= (float)(radius - 29))) && Main.tile[k, l].TileType != (ushort)ModContent.TileType<SnotOrb>())
                {
                    Tile t = Main.tile[k, l];
                    t.HasTile = true;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    t.TileType = (ushort)ModContent.TileType<Chunkstone>();
                }
                if (dist <= radius - 6 && dist >= radius - 23)
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                }
            }
        }
        #endregion
        int radiusModifier = radius - 7; // makes the tunnels go deeper into the main circle (more subtracted means further in)
        Vector2 posToPlaceAnotherCircle = Vector2.Zero;
        #region find the points for making the tunnels to the outer circles
        for (int m = 0; m < 16; m++)
        {
            double positionAroundCircle = (WorldGen.genRand.Next(0, 62831852) / 10000000);
            Vector2 randPoint = new Vector2(center.X + ((int)Math.Round(radiusModifier * Math.Cos(positionAroundCircle))), center.Y + ((int)Math.Round(radiusModifier * Math.Sin(positionAroundCircle))));
            posToPlaceAnotherCircle = randPoint;
            Vector2 item2 = center;
            if (randPoint.X > center.X)
            {
                if (randPoint.X > center.X + radius / 2)
                {
                    if (randPoint.Y > center.Y)
                    {
                        if (randPoint.Y > center.Y + radius / 2)
                        {
                            item2 = new Vector2(randPoint.X + 50f, randPoint.Y + 50f);
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                outerCircles.Add(item2);
                                secondaryCircles.Add(item2);
                                excludedPointsForOuterTunnels.Add(randPoint);
                            }
                        }
                        else
                        {
                            item2 = new Vector2(randPoint.X + 50f, randPoint.Y + 25f);
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                outerCircles.Add(item2);
                                secondaryCircles.Add(item2);
                                excludedPointsForOuterTunnels.Add(randPoint);
                            }
                        }
                    }
                    else if (randPoint.Y < center.Y - radius / 2)
                    {
                        item2 = new Vector2(randPoint.X + 50f, randPoint.Y - 50f);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                    else
                    {
                        item2 = new Vector2(randPoint.X + 50f, randPoint.Y - 25f);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                }
                else if (randPoint.Y > center.Y)
                {
                    if (randPoint.Y > center.Y + radius / 2)
                    {
                        item2 = new Vector2(randPoint.X + 25f, randPoint.Y + 50f);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                    else
                    {
                        item2 = new Vector2(randPoint.X + 25f, randPoint.Y + 25f);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                }
                else if (randPoint.Y < center.Y - radius / 2)
                {
                    item2 = new Vector2(randPoint.X + 25f, randPoint.Y - 50f);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
                else
                {
                    item2 = new Vector2(randPoint.X + 25f, randPoint.Y - 25f);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
            }
            else if (randPoint.X < center.X - radius / 2)
            {
                if (randPoint.Y > center.Y)
                {
                    if (randPoint.Y > center.Y + radius / 2)
                    {
                        item2 = new Vector2(randPoint.X - 50f, randPoint.Y + 50f);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                    else
                    {
                        item2 = new Vector2(randPoint.X - 50f, randPoint.Y + 25f);
                        if (WorldGen.genRand.Next(2) == 0)
                        {
                            outerCircles.Add(item2);
                            secondaryCircles.Add(item2);
                            excludedPointsForOuterTunnels.Add(randPoint);
                        }
                    }
                }
                else if (randPoint.Y < center.Y - radius / 2)
                {
                    item2 = new Vector2(randPoint.X - 50f, randPoint.Y - 50f);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
                else
                {
                    item2 = new Vector2(randPoint.X - 50f, randPoint.Y - 25f);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
            }
            else if (randPoint.Y > center.Y)
            {
                if (randPoint.Y > center.Y + radius / 2)
                {
                    item2 = new Vector2(randPoint.X - 25f, randPoint.Y + 50f);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
                else
                {
                    item2 = new Vector2(randPoint.X - 25f, randPoint.Y + 25f);
                    if (WorldGen.genRand.Next(2) == 0)
                    {
                        outerCircles.Add(item2);
                        secondaryCircles.Add(item2);
                        excludedPointsForOuterTunnels.Add(randPoint);
                    }
                }
            }
            else if (randPoint.Y < center.Y - radius / 2)
            {
                item2 = new Vector2(randPoint.X - 25f, randPoint.Y - 50f);
                if (WorldGen.genRand.Next(2) == 0)
                {
                    outerCircles.Add(item2);
                    secondaryCircles.Add(item2);
                    excludedPointsForOuterTunnels.Add(randPoint);
                }
            }
            else
            {
                item2 = new Vector2(randPoint.X - 25f, randPoint.Y - 25f);
                if (WorldGen.genRand.Next(2) == 0)
                {
                    outerCircles.Add(item2);
                    secondaryCircles.Add(item2);
                    excludedPointsForOuterTunnels.Add(randPoint);
                }
            }
            points.Add(randPoint);
            pointsToGoTo.Add(item2);
            angles.Add(positionAroundCircle);
        }
        #endregion

        // make outer circles
        #region outer circles and tunnels
        if (secondaryCircles.Count != 0)
        {
            for (int z = 0; z < secondaryCircles.Count; z++)
            {
                if (secondaryCircles[z].Y < center.Y - 10) continue;
                int outerTunnelsRadiusMod = rad2 - 6;
                double pointsAroundCircle2 = (WorldGen.genRand.Next(0, 62831852) / 10000000);
                Vector2 randPointAroundCircle = new Vector2(outerCircles[z].X + ((int)Math.Round(outerTunnelsRadiusMod * Math.Cos(pointsAroundCircle2))), outerCircles[z].Y + ((int)Math.Round(outerTunnelsRadiusMod * Math.Sin(pointsAroundCircle2))));
                for (int m = 0; m < 16; m++)
                {
                    Vector2 endpoint = secondaryCircles[z];
                    #region endpoint calculation
                    if (randPointAroundCircle.X > outerCircles[z].X)
                    {
                        if (randPointAroundCircle.X > outerCircles[z].X + rad2 / 2)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y)
                            {
                                if (randPointAroundCircle.Y > outerCircles[z].Y + rad2 / 2)
                                {
                                    endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y + 15f);
                                }
                                else
                                {
                                    endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y + 7f);
                                }
                            }
                            else if (randPointAroundCircle.Y < outerCircles[z].Y - rad2 / 2)
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y - 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y - 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y > outerCircles[z].Y)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y + rad2 / 2)
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y + 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y + 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y < outerCircles[z].Y - rad2 / 2)
                        {
                            endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y - 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y - 7f);
                        }
                    }
                    else if (randPointAroundCircle.X < outerCircles[z].X - rad2 / 2)
                    {
                        if (randPointAroundCircle.Y > outerCircles[z].Y)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y + rad2 / 2)
                            {
                                endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y + 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y + 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y < outerCircles[z].Y - rad2 / 2)
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y - 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y - 7f);
                        }
                    }
                    else if (randPointAroundCircle.Y > outerCircles[z].Y)
                    {
                        if (randPointAroundCircle.Y > outerCircles[z].Y + rad2 / 2)
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y + 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y + 7f);
                        }
                    }
                    else if (randPointAroundCircle.Y < outerCircles[z].Y - rad2 / 2)
                    {
                        endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y - 15f);
                    }
                    else
                    {
                        endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y - 7f);
                    }
                    #endregion
                    secondCircleStartPoints.Add(randPointAroundCircle);
                    secondCircleEndpoints.Add(endpoint);
                    secondCirclePointsAroundCircle.Add(pointsAroundCircle2);
                }
            }
        }

        #endregion
        // make tunnels going outwards from the main circle
        for (int n = 0; n < points.Count; n++)
        {
            if (points[n].Y < center.Y - 10) continue;
            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 10f, (ushort)ModContent.TileType<Chunkstone>());
            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 5f, 65535);
            MakeEndingCircle((int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 13f, (ushort)ModContent.TileType<Chunkstone>());
            MakeCircle((int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 8f, 65535);
        }
        if (outerCircles.Count != 0)
        {
            for (int q = 0; q < outerCircles.Count; q++)
            {
                if (outerCircles[q].Y < center.Y - 10) continue;
                MakeEndingCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2, (ushort)ModContent.TileType<Chunkstone>());
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 6, 65535);
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 13, (ushort)ModContent.TileType<Chunkstone>());
                exclusions.Add(outerCircles[q]);
            }
        }
        int num8 = radius - 7;
        for (int num9 = 0; num9 < 20; num9++)
        {
            double d = WorldGen.genRand.Next(0, 62831852) / 10000000;
            Vector2 vector2 = new Vector2(center.X + ((int)Math.Round(num8 * Math.Cos(d))), center.Y + ((int)Math.Round(num8 * Math.Sin(d))));
            if (exclusions.Contains(vector2)) continue;
            MakeCircle((int)vector2.X, (int)vector2.Y, 4f, (ushort)ModContent.TileType<Chunkstone>());
        }

        // make tunnels going outwards from the outer circles
        for (int n = 0; n < secondCircleStartPoints.Count; n++)
        {
            if (excludedPointsForOuterTunnels.Count != 0 && n < excludedPointsForOuterTunnels.Count)
                if (Vector2.Distance(excludedPointsForOuterTunnels[n], secondCircleEndpoints[n]) < 55)
                    continue;
            BoreTunnel2((int)secondCircleStartPoints[n].X, (int)secondCircleStartPoints[n].Y, (int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 7f, (ushort)ModContent.TileType<Chunkstone>());
            BoreTunnel2((int)secondCircleStartPoints[n].X, (int)secondCircleStartPoints[n].Y, (int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 3f, 65535);
            // ending circles
            MakeCircle((int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 3f, 65535); // air
            MakeEndingCircle((int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 5f, (ushort)ModContent.TileType<Chunkstone>()); // chunkstone
        }
        // fill main tunnels with air
        for (int n = 0; n < points.Count; n++)
        {
            if (points[n].Y < center.Y - 10)
            {
                exclusions.Add(pointsToGoTo[n]);
                continue;
            }
            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 3f, 65535);
        }
        // make secondary circles inner area filled
        if (outerCircles.Count != 0)
        {
            for (int q = 0; q < outerCircles.Count; q++)
            {
                if (outerCircles[q].Y < center.Y - 10) continue;
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 6, 65535);
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 13, (ushort)ModContent.TileType<Chunkstone>());
            }
        }
        for (int num5 = i - radius; num5 <= i + radius; num5++)
        {
            for (int num6 = j - radius; num6 <= j + radius; num6++)
            {
                float num7 = Vector2.Distance(new Vector2(num5, num6), new Vector2(i, j));
                if (num7 < radius - 7 && num7 > radius - 22)
                {
                    Tile t = Main.tile[num5, num6];
                    t.HasTile = false;
                }
            }
        }
        for (int num10 = 0; num10 < pointsToGoTo.Count; num10++)
        {
            if (exclusions.Contains(pointsToGoTo[num10])) continue;
            AddSnotOrb((int)pointsToGoTo[num10].X, (int)pointsToGoTo[num10].Y);
        }
        for (int num10 = 0; num10 < secondCircleEndpoints.Count; num10++)
        {
            if (exclusions.Contains(secondCircleEndpoints[num10])) continue;
            AddSnotOrb((int)secondCircleEndpoints[num10].X, (int)secondCircleEndpoints[num10].Y);
        }
        BoreTunnel2(i, j - radius - 50, i, j - radius + 7, 5, ushort.MaxValue);
        for (int x = i - 12; x < i + 12; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                if (x >= i + 7 || x <= i - 7)
                {
                    Tile t = Main.tile[x, y];
                    t.HasTile = true;
                    t.IsHalfBlock = false;
                    t.Slope = SlopeType.Solid;
                    t.TileType = (ushort)ModContent.TileType<Chunkstone>();
                }
                if (x <= i + 7 && x >= i - 7)
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ChunkstoneWall>();
                    Tile t = Main.tile[x, y];
                    t.HasTile = false;
                }
            }
        }
        for (int x = i - 12; x < i + 12; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                if (x == i + 9 || x == i - 9)
                {
                    int rn = WorldGen.genRand.Next(13, 17);
                    if (y % rn == 0)
                    {
                        MakeCircle(x, y, 3, (ushort)ModContent.TileType<Chunkstone>());
                    }
                }
            }
        }
    }

    public void ContagionRunner2(int evilBiomePosition, int qq = 0)
    {
        int radius = WorldGen.genRand.Next(65, 70); //Radius for Main Circle
        int rad2 = WorldGen.genRand.Next(20, 26);

        int j = Utils.TileCheck(evilBiomePosition) + radius + 50;
        var center = new Vector2(evilBiomePosition, j);

        var points = new List<Vector2>(); //List of Points from where to Generate Main tunnels
        var pointsToGoTo = new List<Vector2>(); //List of Points where the Main Tunnels end


        var outerCircles = new List<Vector2>(); // the circles at the ends of the first tunnels
        var secondaryCircles = new List<Vector2>(); // the circles at the ends of the outer circles
        var secondCircleStartPoints = new List<Vector2>(); //Starts points for secondary Tunnels
        var secondCircleEndpoints = new List<Vector2>(); //Ends points for secondary Tunnels

        var secondCirclePointsAroundCircle = new List<double>();
        var exclusions = new List<Vector2>();
        var excludedPointsForOuterTunnels = new List<Vector2>();
        //new List<Vector2>();

        #region make the main circle

        for (int k = evilBiomePosition - radius; k <= evilBiomePosition + radius; k++)
        {

            for (int l = j - radius; l <= j + radius; l++)
            {

                float dist = Vector2.Distance(new Vector2(k, l), new Vector2(evilBiomePosition, j));
                if (dist <= radius && dist >= radius - 29)
                {
                    Main.tile[k, l].Active(false);
                }

                if (((dist <= radius && dist >= radius - 7) || (dist <= radius - 22 && dist >= radius - 29)) &&
                    Main.tile[k, l].TileType != (ushort)ModContent.TileType<SnotOrb>())
                {
                    Main.tile[k, l].Active(true);
                    Tile tile2 = Main.tile[k, l];
                    tile2.IsHalfBlock = false;
                    tile2.Slope = SlopeType.Solid;
                    Main.tile[k, l].TileType = (ushort)ModContent.TileType<Chunkstone>();
                }

                if (dist <= radius - 6 && dist >= radius - 23)
                {
                    Main.tile[k, l].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                }
            }
        }

        #endregion

        int radiusModifier =
            radius - 7; // makes the tunnels go deeper into the main circle (more subtracted means further in)
        Vector2 posToPlaceAnotherCircle = Vector2.Zero;

        #region find the points for making the main Tunnels

        // Variables for how far Tunnels go and how many spawn
        int MainSpawnRadius = WorldGen.genRand.Next(30, 70); //Length of Tunnel
        int InnerSpawnRadius = WorldGen.genRand.Next(-50, -35); //Length of Inwards going Tunnel

        int MainTunnelN = WorldGen.genRand.Next(3, 6); //How many Tunnels per Contagion Opening

        float RandAngle = MathHelper.ToRadians(-180); //Starting Angle for Tunnel spawn
        //

        for (int TunnelNumber = 0; TunnelNumber <= MainTunnelN; TunnelNumber++)
        {

            points.Add(new Vector2(center.X + radius * (float)Math.Sin((double)RandAngle), center.Y + radius * (float)Math.Cos((double)RandAngle)));
            pointsToGoTo.Add(new Vector2(center.X + radius * (float)Math.Sin((double)RandAngle) + MainSpawnRadius * (float)Math.Sin((double)RandAngle), center.Y + radius * (float)Math.Cos((double)RandAngle) + MainSpawnRadius * (float)Math.Cos((double)RandAngle)));

            //TemporaryPointStart is used for Tunnels that connect to the end of other tunnels
            Vector2 TemporaryPointStart = new Vector2(center.X + radius * (float)Math.Sin((double)RandAngle) + MainSpawnRadius * (float)Math.Sin((double)RandAngle), center.Y + radius * (float)Math.Cos((double)RandAngle) + MainSpawnRadius * (float)Math.Cos((double)RandAngle));

            RandAngle = MathHelper.ToRadians(-180 + ((360 / MainTunnelN) * TunnelNumber) + WorldGen.genRand.Next(-15, 16));
            MainSpawnRadius = WorldGen.genRand.Next(30, 70);

            if (WorldGen.genRand.Next(0, 2) == 1) //50% Chance to Create an extra branching path
            {
                points.Add(TemporaryPointStart);
                pointsToGoTo.Add(new Vector2(TemporaryPointStart.X + (float)Math.Sin((double)RandAngle) * MainSpawnRadius, TemporaryPointStart.Y + (float)Math.Cos((double)RandAngle) * MainSpawnRadius));
            }

        }

        for (int TunnelNumber = 0; TunnelNumber <= MainTunnelN; TunnelNumber++)
        {

            points.Add(new Vector2(center.X + radius * (float)Math.Sin((double)RandAngle), center.Y + radius * (float)Math.Cos((double)RandAngle)));
            pointsToGoTo.Add(new Vector2(center.X + radius * (float)Math.Sin((double)RandAngle) + InnerSpawnRadius * (float)Math.Sin((double)RandAngle), center.Y + radius * (float)Math.Cos((double)RandAngle) + InnerSpawnRadius * (float)Math.Cos((double)RandAngle)));

            RandAngle = MathHelper.ToRadians(-180 + ((360 / MainTunnelN) * TunnelNumber) + WorldGen.genRand.Next(-15, 16));
            InnerSpawnRadius = WorldGen.genRand.Next(-45, -30);

        }


        #region Old Point Find Code

        //int fiftyRand = WorldGen.genRand.Next(30, 60);
        //int tfRand = WorldGen.genRand.Next(15, 35);

        //for (int m = 0; m < 16; m++)
        //{
        //    double positionAroundCircle = WorldGen.genRand.Next(0, 6283) / 1000;
        //    var randPoint = new Vector2(center.X + (int)Math.Round(radiusModifier * Math.Cos(positionAroundCircle)),
        //        center.Y + (int)Math.Round(radiusModifier * Math.Sin(positionAroundCircle)));
        //    posToPlaceAnotherCircle = randPoint;
        //    Vector2 item2 = center;

        //    if (randPoint.X > center.X)
        //    {
        //        if (randPoint.X > center.X + (radius / 2))
        //        {
        //            if (randPoint.Y > center.Y)
        //            {
        //                if (randPoint.Y > center.Y + (radius / 2))
        //                {
        //                    item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y + fiftyRand);
        //                    if (WorldGen.genRand.Next(2) == 0)
        //                    {
        //                        outerCircles.Add(item2);
        //                        secondaryCircles.Add(item2);
        //                        excludedPointsForOuterTunnels.Add(randPoint);
        //                    }
        //                }
        //                else
        //                {
        //                    item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y + tfRand);
        //                    if (WorldGen.genRand.Next(2) == 0)
        //                    {
        //                        outerCircles.Add(item2);
        //                        secondaryCircles.Add(item2);
        //                        excludedPointsForOuterTunnels.Add(randPoint);
        //                    }
        //                }
        //            }
        //            else if (randPoint.Y < center.Y - (radius / 2))
        //            {
        //                item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y - fiftyRand);
        //                if (WorldGen.genRand.Next(2) == 0)
        //                {
        //                    outerCircles.Add(item2);
        //                    secondaryCircles.Add(item2);
        //                    excludedPointsForOuterTunnels.Add(randPoint);
        //                }
        //            }
        //            else
        //            {
        //                item2 = new Vector2(randPoint.X + fiftyRand, randPoint.Y - tfRand);
        //                if (WorldGen.genRand.Next(2) == 0)
        //                {
        //                    outerCircles.Add(item2);
        //                    secondaryCircles.Add(item2);
        //                    excludedPointsForOuterTunnels.Add(randPoint);
        //                }
        //            }
        //        }
        //        else if (randPoint.Y > center.Y)
        //        {
        //            if (randPoint.Y > center.Y + (radius / 2))
        //            {
        //                item2 = new Vector2(randPoint.X + tfRand, randPoint.Y + fiftyRand);
        //                if (WorldGen.genRand.Next(2) == 0)
        //                {
        //                    outerCircles.Add(item2);
        //                    secondaryCircles.Add(item2);
        //                    excludedPointsForOuterTunnels.Add(randPoint);
        //                }
        //            }
        //            else
        //            {
        //                item2 = new Vector2(randPoint.X + tfRand, randPoint.Y + tfRand);
        //                if (WorldGen.genRand.Next(2) == 0)
        //                {
        //                    outerCircles.Add(item2);
        //                    secondaryCircles.Add(item2);
        //                    excludedPointsForOuterTunnels.Add(randPoint);
        //                }
        //            }
        //        }
        //        else if (randPoint.Y < center.Y - (radius / 2))
        //        {
        //            item2 = new Vector2(randPoint.X + tfRand, randPoint.Y - fiftyRand);
        //            if (WorldGen.genRand.Next(2) == 0)
        //            {
        //                outerCircles.Add(item2);
        //                secondaryCircles.Add(item2);
        //                excludedPointsForOuterTunnels.Add(randPoint);
        //            }
        //        }
        //        else
        //        {
        //            item2 = new Vector2(randPoint.X + tfRand, randPoint.Y - tfRand);
        //            if (WorldGen.genRand.Next(2) == 0)
        //            {
        //                outerCircles.Add(item2);
        //                secondaryCircles.Add(item2);
        //                excludedPointsForOuterTunnels.Add(randPoint);
        //            }
        //        }
        //    }
        //    else if (randPoint.X < center.X - (radius / 2))
        //    {
        //        if (randPoint.Y > center.Y)
        //        {
        //            if (randPoint.Y > center.Y + (radius / 2))
        //            {
        //                item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y + fiftyRand);
        //                if (WorldGen.genRand.Next(2) == 0)
        //                {
        //                    outerCircles.Add(item2);
        //                    secondaryCircles.Add(item2);
        //                    excludedPointsForOuterTunnels.Add(randPoint);
        //                }
        //            }
        //            else
        //            {
        //                item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y + tfRand);
        //                if (WorldGen.genRand.Next(2) == 0)
        //                {
        //                    outerCircles.Add(item2);
        //                    secondaryCircles.Add(item2);
        //                    excludedPointsForOuterTunnels.Add(randPoint);
        //                }
        //            }
        //        }
        //        else if (randPoint.Y < center.Y - (radius / 2))
        //        {
        //            item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y - fiftyRand);
        //            if (WorldGen.genRand.Next(2) == 0)
        //            {
        //                outerCircles.Add(item2);
        //                secondaryCircles.Add(item2);
        //                excludedPointsForOuterTunnels.Add(randPoint);
        //            }
        //        }
        //        else
        //        {
        //            item2 = new Vector2(randPoint.X - fiftyRand, randPoint.Y - tfRand);
        //            if (WorldGen.genRand.Next(2) == 0)
        //            {
        //                outerCircles.Add(item2);
        //                secondaryCircles.Add(item2);
        //                excludedPointsForOuterTunnels.Add(randPoint);
        //            }
        //        }
        //    }
        //    else if (randPoint.Y > center.Y)
        //    {
        //        if (randPoint.Y > center.Y + (radius / 2))
        //        {
        //            item2 = new Vector2(randPoint.X - tfRand, randPoint.Y + fiftyRand);
        //            if (WorldGen.genRand.Next(2) == 0)
        //            {
        //                outerCircles.Add(item2);
        //                secondaryCircles.Add(item2);
        //                excludedPointsForOuterTunnels.Add(randPoint);
        //            }
        //        }
        //        else
        //        {
        //            item2 = new Vector2(randPoint.X - tfRand, randPoint.Y + tfRand);
        //            if (WorldGen.genRand.Next(2) == 0)
        //            {
        //                outerCircles.Add(item2);
        //                secondaryCircles.Add(item2);
        //                excludedPointsForOuterTunnels.Add(randPoint);
        //            }
        //        }
        //    }
        //    else if (randPoint.Y < center.Y - (radius / 2))
        //    {
        //        item2 = new Vector2(randPoint.X - tfRand, randPoint.Y - fiftyRand);
        //        if (WorldGen.genRand.Next(2) == 0)
        //        {
        //            outerCircles.Add(item2);
        //            secondaryCircles.Add(item2);
        //            excludedPointsForOuterTunnels.Add(randPoint);
        //        }
        //    }
        //    else
        //    {
        //        item2 = new Vector2(randPoint.X - tfRand, randPoint.Y - tfRand);
        //        if (WorldGen.genRand.Next(2) == 0)
        //        {
        //            outerCircles.Add(item2);
        //            secondaryCircles.Add(item2);
        //            excludedPointsForOuterTunnels.Add(randPoint);
        //        }
        //    }

        //    points.Add(randPoint);
        //    pointsToGoTo.Add(item2);
        //    angles.Add(positionAroundCircle);
        //}
        #endregion

        #endregion

        #region find the points for making the secondary Tunnels // BUGGY 

        //// Variables for how far Tunnels go and how many spawn
        int SecondarySpawnRadius = WorldGen.genRand.Next(15, 30); //Length of Tunnel

        int SecondaryTunnelN = 1; //Amount of secondary Tunnels

        float SetAngle = 0f; //Set to Angle of the Tunnel, used to prevent Secondary tunnels from spawning on top of Main tunnels

        float RandSecondaryTunnelAngle = 0f; //Starting Angle for Tunnel spawn
        //

        for (int n = 0; n < pointsToGoTo.Count; n++)
        {
            SetAngle = (float)Math.Atan2((double)points[n].Y - (double)pointsToGoTo[n].Y, (double)points[n].X - (double)pointsToGoTo[n].X);

            for (int SecondaryTunnelCounter = 0; SecondaryTunnelCounter <= SecondaryTunnelN; SecondaryTunnelCounter++)
            {
                RandSecondaryTunnelAngle = MathHelper.ToRadians(WorldGen.genRand.Next((int)SetAngle + 20, (int)SetAngle + 340));
                SecondarySpawnRadius = WorldGen.genRand.Next(15, 30);

                secondCircleStartPoints.Add(pointsToGoTo[n]);
                secondCircleEndpoints.Add(new Vector2(pointsToGoTo[n].X + SecondarySpawnRadius * (float)Math.Sin((double)RandSecondaryTunnelAngle), pointsToGoTo[n].Y + SecondarySpawnRadius * (float)Math.Cos((double)RandSecondaryTunnelAngle)));

            }

            SecondaryTunnelN = WorldGen.genRand.Next(1, 3);
        }

        #endregion

        #region outer circles and tunnels

        #region Outer Circle Code
        if (secondaryCircles.Count != 0)
        {
            for (int z = 0; z < secondaryCircles.Count; z++)
            {
                if (secondaryCircles[z].Y < center.Y - 10)
                {
                    continue;
                }

                int outerTunnelsRadiusMod = rad2 - 6;
                double pointsAroundCircle2 = WorldGen.genRand.Next(0, 62831852) / 10000000;
                var randPointAroundCircle =
                    new Vector2(
                        outerCircles[z].X + (int)Math.Round(outerTunnelsRadiusMod * Math.Cos(pointsAroundCircle2)),
                        outerCircles[z].Y + (int)Math.Round(outerTunnelsRadiusMod * Math.Sin(pointsAroundCircle2)));
                int fifteenRand = WorldGen.genRand.Next(-15, 15);
                int sevenRand = WorldGen.genRand.Next(-7, 7);
                for (int m = 0; m < 16; m++)
                {
                    Vector2 endpoint = secondaryCircles[z];

                    #region endpoint calculation

                    if (randPointAroundCircle.X > outerCircles[z].X)
                    {
                        if (randPointAroundCircle.X > outerCircles[z].X + (rad2 / 2))
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y)
                            {
                                if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                                {
                                    endpoint = new Vector2(randPointAroundCircle.X + 15f,
                                        randPointAroundCircle.Y + 15f);
                                }
                                else
                                {
                                    endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y + 7f);
                                }
                            }
                            else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y - 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 15f, randPointAroundCircle.Y - 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y > outerCircles[z].Y)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y + 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y + 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                        {
                            endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y - 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X + 7f, randPointAroundCircle.Y - 7f);
                        }
                    }
                    else if (randPointAroundCircle.X < outerCircles[z].X - (rad2 / 2))
                    {
                        if (randPointAroundCircle.Y > outerCircles[z].Y)
                        {
                            if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                            {
                                endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y + 15f);
                            }
                            else
                            {
                                endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y + 7f);
                            }
                        }
                        else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y - 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 15f, randPointAroundCircle.Y - 7f);
                        }
                    }
                    else if (randPointAroundCircle.Y > outerCircles[z].Y)
                    {
                        if (randPointAroundCircle.Y > outerCircles[z].Y + (rad2 / 2))
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y + 15f);
                        }
                        else
                        {
                            endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y + 7f);
                        }
                    }
                    else if (randPointAroundCircle.Y < outerCircles[z].Y - (rad2 / 2))
                    {
                        endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y - 15f);
                    }
                    else
                    {
                        endpoint = new Vector2(randPointAroundCircle.X - 7f, randPointAroundCircle.Y - 7f);
                    }

                    #endregion

                    secondCircleStartPoints.Add(randPointAroundCircle);
                    secondCircleEndpoints.Add(endpoint);
                    secondCirclePointsAroundCircle.Add(pointsAroundCircle2);
                }
            }
        }
        #endregion

        #endregion

        // make tunnels going outwards from the main circle
        for (int n = 0; n < points.Count; n++)
        {
            if (points[n].Y < center.Y - 10)
            {
                continue;
            }

            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 10f,
                (ushort)ModContent.TileType<Chunkstone>());
            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 5f, 65535);
            MakeEndingCircle((int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 13f,
                (ushort)ModContent.TileType<Chunkstone>());
            MakeCircle((int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 8f, 65535);

        }

        if (outerCircles.Count != 0)
        {
            for (int q = 0; q < outerCircles.Count; q++)
            {
                if (outerCircles[q].Y < center.Y - 10)
                {
                    continue;
                }

                MakeEndingCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2,
                    (ushort)ModContent.TileType<Chunkstone>());
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 6, 65535);
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 13,
                    (ushort)ModContent.TileType<Chunkstone>());
                exclusions.Add(outerCircles[q]);
            }
        }

        int num8 = radius - 7;
        for (int num9 = 0; num9 < 20; num9++)
        {
            double d = WorldGen.genRand.Next(0, 62831852) / 10000000;
            var vector2 = new Vector2(center.X + (int)Math.Round(num8 * Math.Cos(d)),
                center.Y + (int)Math.Round(num8 * Math.Sin(d)));
            if (exclusions.Contains(vector2))
            {
                continue;
            }

            MakeCircle((int)vector2.X, (int)vector2.Y, 4f, (ushort)ModContent.TileType<Chunkstone>());
        }

        // make tunnels going outwards from the outer circles
        for (int n = 0; n < secondCircleStartPoints.Count; n++)
        {
            if (excludedPointsForOuterTunnels.Count != 0 && n < excludedPointsForOuterTunnels.Count)
            {
                if (Vector2.Distance(excludedPointsForOuterTunnels[n], secondCircleEndpoints[n]) < 55)
                {
                    continue;
                }
            }

            BoreTunnel2((int)secondCircleStartPoints[n].X, (int)secondCircleStartPoints[n].Y,
                (int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 7f,
                (ushort)ModContent.TileType<Chunkstone>());
            BoreTunnel2((int)secondCircleStartPoints[n].X, (int)secondCircleStartPoints[n].Y,
                (int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 3f, 65535);
            // ending circles
            MakeCircle((int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 3f, 65535); // air
            MakeEndingCircle((int)secondCircleEndpoints[n].X, (int)secondCircleEndpoints[n].Y, 5f,
                (ushort)ModContent.TileType<Chunkstone>()); // chunkstone
        }

        // fill main tunnels with air
        for (int n = 0; n < points.Count; n++)
        {
            if (points[n].Y < center.Y - 10)
            {
                exclusions.Add(pointsToGoTo[n]);
                continue;
            }

            BoreTunnel2((int)points[n].X, (int)points[n].Y, (int)pointsToGoTo[n].X, (int)pointsToGoTo[n].Y, 3f, 65535);
        }

        // make secondary circles inner area filled
        if (outerCircles.Count != 0)
        {
            for (int q = 0; q < outerCircles.Count; q++)
            {
                if (outerCircles[q].Y < center.Y - 10)
                {
                    continue;
                }

                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 6, 65535);
                MakeCircle((int)outerCircles[q].X, (int)outerCircles[q].Y, rad2 - 13,
                    (ushort)ModContent.TileType<Chunkstone>());
            }
        }

        for (int num5 = evilBiomePosition - radius; num5 <= evilBiomePosition + radius; num5++)
        {
            for (int num6 = j - radius; num6 <= j + radius; num6++)
            {
                float num7 = Vector2.Distance(new Vector2(num5, num6), new Vector2(evilBiomePosition, j));
                if (num7 < radius - 7 && num7 > radius - 22)
                {
                    Main.tile[num5, num6].Active(false);
                }
            }
        }

        for (int num10 = 0; num10 < pointsToGoTo.Count; num10++)
        {
            if (exclusions.Contains(pointsToGoTo[num10]))
            {
                continue;
            }

            AddSnotOrb((int)pointsToGoTo[num10].X, (int)pointsToGoTo[num10].Y);
        }

        for (int num10 = 0; num10 < secondCircleEndpoints.Count; num10++)
        {
            if (exclusions.Contains(secondCircleEndpoints[num10]))
            {
                continue;
            }

            AddSnotOrb((int)secondCircleEndpoints[num10].X, (int)secondCircleEndpoints[num10].Y);
        }

        BoreTunnel2(evilBiomePosition, j - radius - 50, evilBiomePosition, j - radius + 7, 5, ushort.MaxValue);
        for (int x = evilBiomePosition - 12; x < evilBiomePosition + 12; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                if (x >= evilBiomePosition + 7 || x <= evilBiomePosition - 7)
                {
                    Main.tile[x, y].Active(true);
                    Tile tile3 = Main.tile[x, y];
                    tile3.IsHalfBlock = false;
                    tile3.Slope = SlopeType.Solid;
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<Chunkstone>();
                }

                if (x <= evilBiomePosition + 7 && x >= evilBiomePosition - 7)
                {
                    Main.tile[x, y].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    Main.tile[x, y].Active(false);
                }
            }
        }

        for (int x = evilBiomePosition - 12; x < evilBiomePosition + 12; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                if (x == evilBiomePosition + 9 || x == evilBiomePosition - 9)
                {
                    int rn = WorldGen.genRand.Next(13, 17);
                    if (y % rn == 0)
                    {
                        MakeCircle(x, y, 3, (ushort)ModContent.TileType<Chunkstone>());
                    }
                }
            }
        }

        /*double num22 = Main.worldSurface + 40.0;
        for (int l = evilBiomePositionWestBound; l < evilBiomePositionEastBound; l++)
        {
            num22 += WorldGen.genRand.Next(-2, 3);
            if (num22 < Main.worldSurface + 30.0)
            {
                num22 = Main.worldSurface + 30.0;
            }
            if (num22 > Main.worldSurface + 50.0)
            {
                num22 = Main.worldSurface + 50.0;
            }
            int i2 = l;
            bool flag4 = false;
            int num23 = (int)GenVars.worldSurfaceLow;
            while (num23 < num22)
            {
                if (Main.tile[i2, num23].HasTile)
                {
                    if (Main.tile[i2, num23].TileType == TileID.Sand && i2 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i2 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Snotsand>();
                    }
                    if (Main.tile[i2, num23].TileType == TileID.Dirt && num23 < Main.worldSurface - 1.0 && !flag4)
                    {
                        //ALReflection.WorldGen_GrassSpread = 0;
                        WorldGen.SpreadGrass(i2, num23, 0, (ushort)ModContent.TileType<Ickgrass>(), true, 0);
                    }
                    flag4 = true;
                    if (Main.tile[i2, num23].WallType == WallID.HardenedSand)
                    {
                        Main.tile[i2, num23].WallType = (ushort)ModContent.WallType<HardenedSnotsandWall>();
                    }
                    else if (Main.tile[i2, num23].WallType == WallID.Sandstone)
                    {
                        Main.tile[i2, num23].WallType = (ushort)ModContent.WallType<SnotsandstoneWall>();
                    }
                    if (Main.tile[i2, num23].TileType == TileID.Stone)
                    {
                        if (i2 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i2 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
                        {
                            Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Chunkstone>();
                        }
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.Grass)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Ickgrass>();
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.IceBlock)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<YellowIce>();
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.Sandstone)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<Snotsandstone>();
                    }
                    else if (Main.tile[i2, num23].TileType == TileID.HardenedSand)
                    {
                        Main.tile[i2, num23].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
                    }
                }
                else
                {
                    if (Main.tile[i2, num23].WallType == WallID.DirtUnsafe)
                    {
                        Main.tile[i2, num23].WallType = (ushort)ModContent.WallType<Walls.ContagionGrassWall>();
                    }
                }
                num23++;
            }
        }
        int num24 = WorldGen.genRand.Next(10, 15);
        for (int m = 0; m < num24; m++)
        {
            int num25 = 0;
            bool flag5 = false;
            int num26 = 0;
            while (!flag5)
            {
                num25++;
                int x = WorldGen.genRand.Next(evilBiomePositionWestBound - num26, evilBiomePositionEastBound + num26);
                int num27 = WorldGen.genRand.Next((int)(Main.worldSurface - num26 / 2), (int)(Main.worldSurface + 100.0 + num26));
                while (WorldGen.oceanDepths(x, num27))
                {
                    x = WorldGen.genRand.Next(evilBiomePositionWestBound - num26, evilBiomePositionEastBound + num26);
                    num27 = WorldGen.genRand.Next((int)(Main.worldSurface - num26 / 2), (int)(Main.worldSurface + 100.0 + num26));
                }
                if (num25 > 100)
                {
                    num26++;
                    num25 = 0;
                }
                if (!Main.tile[x, num27].HasTile)
                {
                    while (!Main.tile[x, num27].HasTile)
                    {
                        num27++;
                    }
                    num27--;
                }
                else
                {
                    while (Main.tile[x, num27].HasTile && num27 > Main.worldSurface)
                    {
                        num27--;
                    }
                }
                if ((num26 > 10 || Main.tile[x, num27 + 1].HasTile && Main.tile[x, num27 + 1].TileType == 203) && !WorldGen.IsTileNearby(x, num27, ModContent.TileType<IckyAltar>(), 3))
                {
                    WorldGen.Place3x2(x, num27, (ushort)ModContent.TileType<IckyAltar>());
                    if (Main.tile[x, num27].TileType == ModContent.TileType<IckyAltar>())
                    {
                        flag5 = true;
                    }
                }
                if (num26 > 100)
                {
                    flag5 = true;
                }
            }
        }*/
    }
    /// <summary>
    ///     A helper method to generate a tunnel using MakeCircle().
    /// </summary>
    /// <param name="x0">Starting x coordinate.</param>
    /// <param name="y0">Starting y coordinate.</param>
    /// <param name="x1">Ending x coordinate.</param>
    /// <param name="y1">Ending y coordinate.</param>
    /// <param name="r">Radius.</param>
    /// <param name="type">Type to generate.</param>
    public static void BoreTunnel2(int x0, int y0, int x1, int y1, float r, ushort type) // Code for making tunnels.. crazy
    {
        bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (flag)
        {
            Utils.Swap(ref x0, ref y0);
            Utils.Swap(ref x1, ref y1);
        }

        if (x0 > x1)
        {
            Utils.Swap(ref x0, ref x1);
            Utils.Swap(ref y0, ref y1);
        }

        int XDifference = x1 - x0;
        int AbsoluteXLegnth = Math.Abs(y1 - y0);
        int XDifferenceHalved = XDifference / 2;
        int IsY0Smaller = y0 < y1 ? 1 : -1;
        int OriginalY = y0;

        for (int i = x0; i <= x1; i++)

        {
            if (flag)
            {
                MakeCircle(OriginalY + WorldGen.genRand.Next(-5, 5), i + WorldGen.genRand.Next(-5, 5), r + (int)(4 * Math.Sin((double)i / 10)), type);

                //if (WorldGen.genRand.Next(0, 16) == 15)
                //{
                //    MakeCircle(OriginalY + WorldGen.genRand.Next(-8, 9), i + WorldGen.genRand.Next(-8, 9), WorldGen.genRand.Next(6, 13), type);
                //}
            }
            else
            {
                MakeCircle(i + WorldGen.genRand.Next(-5, 5), OriginalY + WorldGen.genRand.Next(-5, 5), r + (int)(4 * Math.Sin((double)i / 10)), type);

                //if (WorldGen.genRand.Next(0, 16) == 15)
                //{
                //    MakeCircle(i + WorldGen.genRand.Next(-8, 9), OriginalY + WorldGen.genRand.Next(-8, 9), WorldGen.genRand.Next(6, 13), type);
                //}
            }



            XDifferenceHalved -= AbsoluteXLegnth;
            if (XDifferenceHalved < 0)
            {
                OriginalY += IsY0Smaller;
                XDifferenceHalved += XDifference;
            }


        }
    }

    /// <summary>
    ///     Makes a circle for the Contagion generation. Fills all tiles with Chunkstone Walls.
    /// </summary>
    /// <param name="x">The x coordinate of the center of the circle.</param>
    /// <param name="y">The y coordinate of the center of the circle.</param>
    /// <param name="r">The radius of the circle.</param>
    /// <param name="type">The type to generate - if ushort.MaxValue, will generate air.</param>
    public static void MakeCircle(int x, int y, float r, ushort type)
    {
        int num = (int)(x - r);
        int num2 = (int)(y - r);
        int num3 = (int)(x + r);
        int num4 = (int)(y + r);
        for (int i = num; i < num3 + 1; i++)
        {
            for (int j = num2; j < num4 + 1; j++)
            {
                if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r &&
                    Main.tile[i, j].TileType != TileID.ShadowOrbs)
                {
                    if (type == 65535)
                    {
                        Main.tile[i, j].Active(false);
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else
                    {
                        Main.tile[i, j].Active(true);
                        Main.tile[i, j].TileType = type;
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
                //else if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) == r - 1)
                //{
                //    Main.tile[i, j].wall = 0;
                //}
            }
        }
    }

    /// <summary>
    ///     Makes an ending circle for the Contagion generation.
    /// </summary>
    /// <param name="x">The x coordinate of the center of the circle.</param>
    /// <param name="y">The y coordinate of the center of the circle.</param>
    /// <param name="r">The radius of the circle.</param>
    /// <param name="type">The type to generate - if ushort.MaxValue, will generate air.</param>
    public static void MakeEndingCircle(int x, int y, float r, ushort type)
    {
        int num = (int)(x - r);
        int num2 = (int)(y - r);
        int num3 = (int)(x + r);
        int num4 = (int)(y + r);
        for (int i = num; i < num3 + 1; i++)
        {
            for (int j = num2; j < num4 + 1; j++)
            {
                if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r &&
                    Main.tile[i, j].TileType != TileID.ShadowOrbs)
                {
                    if (type == 65535)
                    {
                        Main.tile[i, j].Active(false);
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else
                    {
                        Main.tile[i, j].Active(true);
                        Main.tile[i, j].TileType = type;
                        //Main.tile[i, j].wall = (ushort)ModContent.WallType<Walls.ChunkstoneWall>();
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
                else if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) == r - 1)
                {
                    Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                }
            }
        }
    }

    /// <summary>
    ///     Adds a Snot Orb at the given coordinates. For the Contagion.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="style">Unused.</param>
    public static void AddSnotOrb(int x, int y, int style = 0)
    {
        if (x < 10 || x > Main.maxTilesX - 10)
        {
            return;
        }

        if (y < 10 || y > Main.maxTilesY - 10)
        {
            return;
        }

        for (int i = x - 1; i < x + 1; i++)
        {
            for (int j = y - 1; j < y + 1; j++)
            {
                if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == (ushort)ModContent.TileType<SnotOrb>())
                {
                    return;
                }
            }
        }

        short num = 0;
        Main.tile[x - 1, y - 1].Active(true);
        Main.tile[x - 1, y - 1].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x - 1, y - 1].TileFrameX = num;
        Main.tile[x - 1, y - 1].TileFrameY = 0;
        Main.tile[x, y - 1].Active(true);
        Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x, y - 1].TileFrameX = (short)(18 + num);
        Main.tile[x, y - 1].TileFrameY = 0;
        Main.tile[x - 1, y].Active(true);
        Main.tile[x - 1, y].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x - 1, y].TileFrameX = num;
        Main.tile[x - 1, y].TileFrameY = 18;
        Main.tile[x, y].Active(true);
        Main.tile[x, y].TileType = (ushort)ModContent.TileType<SnotOrb>();
        Main.tile[x, y].TileFrameX = (short)(18 + num);
        Main.tile[x, y].TileFrameY = 18;
    }
}
