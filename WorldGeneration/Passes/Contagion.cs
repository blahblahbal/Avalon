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
using Terraria.Localization;
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
        int jungleXMax = Main.maxTilesX;
        int jungleXMin = 0;
        int iceXMax = Main.maxTilesX;
        int iceXMin = 0;
        for (int tileXCoord = 0; tileXCoord < Main.maxTilesX; tileXCoord++)
        {
            for (int tileYCoord = 0; (double)tileYCoord < Main.worldSurface; tileYCoord++)
            {
                if (Main.tile[tileXCoord, tileYCoord].HasTile)
                {
                    if (Main.tile[tileXCoord, tileYCoord].TileType == TileID.JungleGrass || Main.tile[tileXCoord, tileYCoord].TileType == ModContent.TileType<Tiles.Tropics.TropicalGrass>())
                    {
                        if (tileXCoord < jungleXMax)
                        {
                            jungleXMax = tileXCoord;
                        }
                        if (tileXCoord > jungleXMin)
                        {
                            jungleXMin = tileXCoord;
                        }
                    }
                    else if (Main.tile[tileXCoord, tileYCoord].TileType == TileID.SnowBlock || Main.tile[tileXCoord, tileYCoord].TileType == TileID.IceBlock)
                    {
                        if (tileXCoord < iceXMax)
                        {
                            iceXMax = tileXCoord;
                        }
                        if (tileXCoord > iceXMin)
                        {
                            iceXMin = tileXCoord;
                        }
                    }
                }
            }
        }
        int buffer = 10;
        jungleXMax -= buffer;
        jungleXMin += buffer;
        iceXMax -= buffer;
        iceXMin += buffer;

        progress.Message = Language.GetTextValue("Mods.Avalon.World.Generation.Contagion.Message");
        double num766 = Main.maxTilesX * 0.00045;
        if (WorldGen.remixWorldGen)
        {
            num766 *= 2.0;
        }
        int[] positions = new int[4];
        for (int num767 = 0; num767 < num766; num767++)
        {
            int num768 = iceXMax;
            int num769 = iceXMin;
            int num770 = jungleXMax;
            int num771 = jungleXMin;

            float num209 = (float)(num767 / num766);
            bool flag12 = false;
            int xCoordEvil = 0;
            int evilLeftCoord = 0;
            int evilRightCoord = 0;
            while (!flag12)
            {
                flag12 = true;
                int num214 = Main.maxTilesX / 2;
                int num215 = 200;
                if (GenVars.dungeonX < Main.maxTilesX * 0.5)
                {
                    xCoordEvil = WorldGen.genRand.Next(600, Main.maxTilesX - 320);
                }
                else
                {
                    xCoordEvil = WorldGen.genRand.Next(320, Main.maxTilesX - 600);
                }
                evilLeftCoord = xCoordEvil - WorldGen.genRand.Next(200) - 100;
                evilRightCoord = xCoordEvil + WorldGen.genRand.Next(200) + 100;
                if (evilLeftCoord < 285)
                {
                    evilLeftCoord = 285;
                }
                if (evilRightCoord > Main.maxTilesX - 285)
                {
                    evilRightCoord = Main.maxTilesX - 285;
                }
                if (evilLeftCoord < GenVars.evilBiomeBeachAvoidance)
                {
                    evilLeftCoord = GenVars.evilBiomeBeachAvoidance;
                }
                if (evilRightCoord > Main.maxTilesX - GenVars.evilBiomeBeachAvoidance)
                {
                    evilRightCoord = Main.maxTilesX - GenVars.evilBiomeBeachAvoidance;
                }
                if (xCoordEvil < evilLeftCoord + GenVars.evilBiomeAvoidanceMidFixer)
                {
                    xCoordEvil = evilLeftCoord + GenVars.evilBiomeAvoidanceMidFixer;
                }
                if (xCoordEvil > evilRightCoord - GenVars.evilBiomeAvoidanceMidFixer)
                {
                    xCoordEvil = evilRightCoord - GenVars.evilBiomeAvoidanceMidFixer;
                }
                if (GenVars.dungeonSide < 0 && evilLeftCoord < 400)
                {
                    evilLeftCoord = 400;
                }
                else if (GenVars.dungeonSide > 0 && evilLeftCoord > Main.maxTilesX - 400)
                {
                    evilLeftCoord = Main.maxTilesX - 400;
                }
                if (evilLeftCoord < GenVars.dungeonLocation + 150 && evilRightCoord > GenVars.dungeonLocation - 150)
                {
                    flag12 = false;
                }
                if (xCoordEvil > num214 - num215 && xCoordEvil < num214 + num215)
                {
                    flag12 = false;
                }
                if (evilLeftCoord > num214 - num215 && evilLeftCoord < num214 + num215)
                {
                    flag12 = false;
                }
                if (evilRightCoord > num214 - num215 && evilRightCoord < num214 + num215)
                {
                    flag12 = false;
                }
                if (num767 > 0)
                {
                    if (Math.Abs(xCoordEvil - positions[num767 - 1]) < 150)
                    {
                        flag12 = false;
                    }
                }
                #region ug desert avoidance
                if (!WorldGen.remixWorldGen)
                {
                    if (!WorldGen.tenthAnniversaryWorldGen)
                    {
                        if (xCoordEvil > num214 - num215 && xCoordEvil < num214 + num215)
                        {
                            flag12 = false;
                        }
                        if (evilLeftCoord > num214 - num215 && evilLeftCoord < num214 + num215)
                        {
                            flag12 = false;
                        }
                        if (evilRightCoord > num214 - num215 && evilRightCoord < num214 + num215)
                        {
                            flag12 = false;
                        }
                    }
                    if (xCoordEvil > GenVars.UndergroundDesertLocation.X && xCoordEvil < GenVars.UndergroundDesertLocation.X + GenVars.UndergroundDesertLocation.Width)
                    {
                        flag12 = false;
                    }
                    if (evilLeftCoord > GenVars.UndergroundDesertLocation.X && evilLeftCoord < GenVars.UndergroundDesertLocation.X + GenVars.UndergroundDesertLocation.Width)
                    {
                        flag12 = false;
                    }
                    if (evilRightCoord > GenVars.UndergroundDesertLocation.X && evilRightCoord < GenVars.UndergroundDesertLocation.X + GenVars.UndergroundDesertLocation.Width)
                    {
                        flag12 = false;
                    }
                }
                #endregion
                for (int num216 = evilLeftCoord; num216 < evilRightCoord; num216++)
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
                #region jungle avoidance
                if (evilLeftCoord < num769 && evilRightCoord > num768)
                {
                    num768++;
                    num769--;
                    flag12 = false;
                }
                if (evilLeftCoord < num771 && evilRightCoord > num770)
                {
                    num770++;
                    num771--;
                    flag12 = false;
                }
                #endregion
            }
            ContagionRunner(xCoordEvil, (int)GenVars.worldSurfaceLow - 10 + (Main.maxTilesY / 8));
            positions[num767] = xCoordEvil;
            for (int num218 = evilLeftCoord; num218 < evilRightCoord; num218++)
            {
                int num219 = (int)GenVars.worldSurfaceLow;
                while (num219 < Main.worldSurface - 1.0)
                {
                    if (Main.tile[num218, num219].HasTile)
                    {
                        int num220 = num219 + WorldGen.genRand.Next(10, 14);
                        for (int num221 = num219; num221 < num220; num221++)
                        {
                            if (Main.tile[num218, num221].TileType == TileID.JungleGrass && num218 >= evilLeftCoord + WorldGen.genRand.Next(5) && num218 < evilRightCoord - WorldGen.genRand.Next(5))
                            {
                                Main.tile[num218, num221].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
                            }
                        }
                        break;
                    }
                    num219++;
                }
            }
            double num222 = Main.worldSurface + 40.0;
            for (int num223 = evilLeftCoord; num223 < evilRightCoord; num223++)
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
                        if (Main.tile[num57, num224].TileType == TileID.Sand && num57 >= evilLeftCoord + WorldGen.genRand.Next(5) && num57 <= evilRightCoord - WorldGen.genRand.Next(5))
                        {
                            Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsand>();
                        }
                        if (Main.tile[num57, num224].TileType == TileID.Dirt && num224 < Main.worldSurface - 1.0 && !flag13)
                        {
                            WorldGen.grassSpread = 0;
                            WorldGen.SpreadGrass(num57, num224, 0, ModContent.TileType<Ickgrass>(), true, default);
                        }
                        flag13 = true;
                        if (Main.tile[num57, num224].TileType == TileID.Stone && num57 >= evilLeftCoord + WorldGen.genRand.Next(5) && num57 <= evilRightCoord - WorldGen.genRand.Next(5))
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
                    int num229 = WorldGen.genRand.Next(evilLeftCoord - num228, evilRightCoord + num228);
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
        }
    }

    /// <summary>
    /// A helper method that checks if the angle is too close to a List of angles.
    /// </summary>
    /// <param name="angle1">The current angle.</param>
    /// <param name="angle2">The List of angles to check against.</param>
    /// <param name="minDistance">The minimum distance to check for.</param>
    /// <returns>True if the angle is too close, false otherwise.</returns>
    private static bool IsAngleTooClose(int angle1, List<int> angle2, int minDistance = 45)
    {
        foreach (int q in angle2)
        {
            if (q + minDistance > angle1 && q - minDistance < angle1)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsBetween(float a, float b, float targetAngle, float maxDist = (float)Math.PI * 0.5f)
    {
        //const float maxDistance = (float)Math.PI * 0.5f; // 90 degrees

        return Math.Abs(MathHelper.WrapAngle(a - targetAngle)) < maxDist
            && Math.Abs(MathHelper.WrapAngle(b - targetAngle)) < maxDist;
    }

    /// <summary>
    /// Contagion generation method.
    /// </summary>
    /// <param name="i">The x coordinate to start the generation at.</param>
    /// <param name="j">The y coordinate to start the generation at.</param>
    public static void ContagionRunner(int i, int j)
    {
        int j2 = j;
        int radius = WorldGen.genRand.Next(70, 75);
        int radMod = radius - 10;
        int rad2 = WorldGen.genRand.Next(20, 26);
        int rad3 = WorldGen.genRand.Next(115, 131);

        ushort chunkstone = (ushort)ModContent.TileType<Chunkstone>();

        // Shift the Y coord down to the world surface
        j = Utils.TileCheck(i) + radius + 50;

        Vector2 center = new(i, j);
        List<Vector2> points = new();
        List<Vector2> pointsForHollow = new();
        List<float[]> endpoints = new();

        #region secondary tunnel vars
        List<Vector3> secondaryTunnelCenters = new();
        List<Vector3> secondaryTunnelEnds = new();
        List<Vector2> secondTunnelStarts = new();
        List<Vector2> secondaryTunnelHollows = new();
        int radiusForSecondaryTunnels = WorldGen.genRand.Next(10, 14);
        int radModForSecondaryTunnels = radiusForSecondaryTunnels - 10;
        int rad3ForSecondTunnels = WorldGen.genRand.Next(55, 77);
        #endregion

        #region inner orb vars
        int radiusInner = WorldGen.genRand.Next(5) + 5;
        List<Vector4> innerCircleStarts = new();
        List<Vector2> innerCircleEnds = new();
        List<Vector3> orbs = new();
        #endregion

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

        #region inner tunnels with orbs
        // A List of ints to store the angles; used to prevent the current angle from being to close to the previous
        List<int> prevAnglesInner = new List<int>();
        for (int m = 0; m < 4; m++)
        {
            int angleDegrees = WorldGen.genRand.Next(360);

            // Loop 10 times to check if the angle is too close; if it isn't, break out of the loop immediately
            for (int z = 0; z < 10; z++)
            {
                if (IsAngleTooClose(angleDegrees, prevAnglesInner))
                {
                    angleDegrees = WorldGen.genRand.Next(360);
                }
                else break;
            }

            // Convert the angle to radians
            float angle = (float)(Math.PI / 180) * angleDegrees;

            // Endpoint calc
            float posX = (float)(center.X + radius * Math.Cos(angle));
            float posY = (float)(center.Y + radius * Math.Sin(angle));

            // Start point calc
            float posX2 = (float)(center.X + radiusInner * Math.Cos(angle));
            float posY2 = (float)(center.Y + radiusInner * Math.Sin(angle));

            // Add the start and endpoints to their respective Lists
            innerCircleStarts.Add(new Vector4(posX2, posY2, WorldGen.genRand.NextBool(2) ? 1 : 0, angle));
            innerCircleEnds.Add(new Vector2(posX, posY));

            // Add the current angle in degrees to the List of previous angles
            prevAnglesInner.Add(angleDegrees);
        }

        // Make the tunnels
        for (int n = 0; n < innerCircleEnds.Count; n++)
        {
            BoreWavyTunnel((int)innerCircleStarts[n].X, (int)innerCircleStarts[n].Y, (int)innerCircleEnds[n].X, (int)innerCircleEnds[n].Y, 50, 4, 6, chunkstone);
            BoreWavyTunnel((int)innerCircleStarts[n].X, (int)innerCircleStarts[n].Y, (int)innerCircleEnds[n].X, (int)innerCircleEnds[n].Y, 50, 4, 2, 65535);

            // Check if the Z field is 1, and if it is, make this a point to add a secondary tunnel to
            if (innerCircleStarts[n].Z == 1)
            {
                orbs.Add(new Vector3(innerCircleStarts[n].X, innerCircleStarts[n].Y, innerCircleStarts[n].W));
            }
            // Otherwise, add a Sepsis Cell to the endpoint
            else
            {
                AddSepsisCell((int)innerCircleStarts[n].X, (int)innerCircleStarts[n].Y);
            }
        }

        List<int> prevAnglesInnerOrbs = new();
        foreach (Vector3 v in orbs)
        {
            for (int k = 0; k < 3; k++)
            {
                int ang = (int)MathHelper.ToDegrees(v.Z) + 180;
                if (ang >= 360) ang -= 360;

                int angleDegrees = WorldGen.genRand.Next(ang) + ang + 60;

                // Loop 10 times to check if the angle is too close; if it isn't, break out of the loop immediately
                for (int z = 0; z < 10; z++)
                {
                    if (IsAngleTooClose(angleDegrees, prevAnglesInnerOrbs))
                    {
                        angleDegrees = WorldGen.genRand.Next(ang) + ang + 60;
                    }
                    else break;
                }

                // Convert the angle to radians
                float angle = (float)(Math.PI / 180) * angleDegrees;

                float posX = (float)(v.X + 2 * Math.Cos(angle));
                float posY = (float)(v.Y + 2 * Math.Sin(angle));

                MakeCircle((int)posX, (int)posY, 10, chunkstone);
                MakeCircle((int)posX, (int)posY, 5, 65535);
                AddSepsisCell((int)posX, (int)posY);

                if (WorldGen.genRand.NextBool(2)) break;
            }
        }

        MakeHollowCircle(i, j, 15, 4, chunkstone);
        #endregion

        #region add endpoints to the paths
        List<int> prevAngles = new List<int>();
        for (int m = 0; m < 4; m++)
        {
            // Randomize the angle, disallowing any angles between 225 and 315
            int angleDegrees = WorldGen.genRand.Next(270) + 315;

            for (int z = 0; z < 10; z++)
            {
                if (IsAngleTooClose(angleDegrees, prevAngles))
                {
                    angleDegrees = WorldGen.genRand.Next(270) + 315;
                }
                else break;
            }

            // Convert the angle to radians
            float angle = (float)(Math.PI / 180) * angleDegrees;

            // Endpoint calc
            float posX = (float)(center.X + (rad3 + WorldGen.genRand.Next(-7, 8)) * Math.Cos(angle));
            float posY = (float)(center.Y + (rad3 + WorldGen.genRand.Next(-7, 8)) * Math.Sin(angle));

            // Start point calc
            float posX2 = (float)(center.X + (radMod + WorldGen.genRand.Next(-7, 8)) * Math.Cos(angle));
            float posY2 = (float)(center.Y + (radMod + WorldGen.genRand.Next(-7, 8)) * Math.Sin(angle));

            // Hollow tunnel calc 
            float posXHollow = (float)(center.X + (radius + WorldGen.genRand.Next(-7, 8)) * Math.Cos(angle));
            float posYHollow = (float)(center.Y + (radius + WorldGen.genRand.Next(-7, 8)) * Math.Sin(angle));

            // The size of the end circle
            float size;

            // Whether the endpoint has a secondary tunnel branching out from it
            bool pointHasSecondTunnels = WorldGen.genRand.NextBool(2);

            // If the endpoint has a branching tunnel, set the size to a larger number;
            // otherwise, set it smaller
            if (pointHasSecondTunnels) size = WorldGen.genRand.Next(18, 25);
            else size = WorldGen.genRand.Next(11, 16);

            // Use a float array to store the position, whether the endpoint will have a secondary
            // tunnel, the angle of this tunnel, and the size of the end circle
            float[] data = new float[5]
            {
                posX, posY, pointHasSecondTunnels ? 1 : 0, angle, size
            };

            // Add the data to the lists
            endpoints.Add(data);
            points.Add(new Vector2(posX2, posY2));
            pointsForHollow.Add(new Vector2(posXHollow, posYHollow));
            prevAngles.Add(angleDegrees);
        }
        #endregion

        #region tunnels from main ring to outer rings
        for (int n = 0; n < points.Count; n++)
        {
            BoreWavyTunnel((int)points[n].X, (int)points[n].Y, (int)endpoints[n][0], (int)endpoints[n][1], 50, 4, 10, chunkstone);
            BoreWavyTunnel((int)points[n].X, (int)points[n].Y, (int)endpoints[n][0], (int)endpoints[n][1], 50, 4, 4, 65535);

            // if the endpoint will be the start to a secondary tunnel
            if (endpoints[n][2] == 1)
            {
                MakeEndingCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4], chunkstone);
                MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 5, 65535);
                secondaryTunnelCenters.Add(new Vector3(endpoints[n][0], endpoints[n][1], endpoints[n][3]));

                if (endpoints[n][4] >= 19 && endpoints[n][4] <= 20)
                {
                    MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 12, chunkstone, true);
                }
                else if (endpoints[n][4] < 23)
                {
                    MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 13, chunkstone, true);
                    MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 18, 65535);
                }
            }
            else
            {
                MakeEndingCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4], chunkstone);
                MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 5, 65535);

                AddSepsisCell((int)endpoints[n][0], (int)endpoints[n][1]);
            }
            BoreWavyTunnel((int)points[n].X, (int)points[n].Y, (int)endpoints[n][0], (int)endpoints[n][1], 50, 4, 4, 65535);
        }
        #endregion

        #region tunnels from secondary circles outwards
        List<int> prevAnglesSecondary = new();
        foreach (Vector3 v in secondaryTunnelCenters)
        {
            for (int k = 0; k < 2; k++)
            {
                // Add 180 to the current tunnels' angle from the the center of the entire structure
                // to invert it; if the angle is greater than 360, subtract 360 from it
                int ang = (int)MathHelper.ToDegrees(v.Z) + 180;
                if (ang >= 360) ang -= 360;

                // Limit the angle so it can't be within 45 degrees of the tunnel
                int angleDegrees = WorldGen.genRand.Next(ang) + ang + 45;

                // Loop 10 times to check if the angle is too close; if it isn't, break out of the loop immediately
                for (int z = 0; z < 10; z++)
                {
                    if (IsAngleTooClose(angleDegrees, prevAnglesSecondary))
                    {
                        angleDegrees = WorldGen.genRand.Next(ang) + ang + 45;
                    }
                    else break;
                }

                // Convert the angle to radians
                float angle = (float)(Math.PI / 180) * angleDegrees;

                float posX = (float)(v.X + (rad3ForSecondTunnels + WorldGen.genRand.Next(-7, 8)) * Math.Cos(angle));
                float posY = (float)(v.Y + (rad3ForSecondTunnels + WorldGen.genRand.Next(-7, 8)) * Math.Sin(angle));
                float posX2 = (float)(v.X + (radiusForSecondaryTunnels + WorldGen.genRand.Next(-7, 8)) * Math.Cos(angle));
                float posY2 = (float)(v.Y + (radiusForSecondaryTunnels + WorldGen.genRand.Next(-7, 8)) * Math.Sin(angle));

                float posXHollow = (float)(v.X + (radModForSecondaryTunnels + WorldGen.genRand.Next(-7, 8)) * Math.Cos(angle));
                float posYHollow = (float)(v.Y + (radModForSecondaryTunnels + WorldGen.genRand.Next(-7, 8)) * Math.Sin(angle));
                float size = WorldGen.genRand.Next(10, 17);
                secondaryTunnelEnds.Add(new Vector3(posX, posY, size));
                secondTunnelStarts.Add(new Vector2(posX2, posY2));
                secondaryTunnelHollows.Add(new Vector2(posXHollow, posYHollow));
                prevAnglesSecondary.Add(angleDegrees);
            }
        }

        for (int q = 0; q < secondTunnelStarts.Count; q++)
        {
            if (secondaryTunnelEnds[q].Y > j)
            {
                BoreWavyTunnel((int)secondTunnelStarts[q].X, (int)secondTunnelStarts[q].Y, (int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, 50, 4, 9, chunkstone);
                BoreWavyTunnel((int)secondTunnelStarts[q].X, (int)secondTunnelStarts[q].Y, (int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, 50, 4, 3, 65535);
                MakeEndingCircle((int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, secondaryTunnelEnds[q].Z, chunkstone);
                if (secondaryTunnelEnds[q].Z >= 10)
                {
                    MakeCircle((int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, 8f, 65535);
                }
                else if (secondaryTunnelEnds[q].Z > 12)
                {
                    MakeCircle((int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, 6f, chunkstone, true);
                }
                else if (secondaryTunnelEnds[q].Z > 15)
                {
                    MakeCircle((int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, 7f, chunkstone, true);
                    MakeCircle((int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y, 4f, 65535);
                }
                AddSepsisCell((int)secondaryTunnelEnds[q].X, (int)secondaryTunnelEnds[q].Y);
            }
        }
        //for (int n = 0; n < secondTunnelStarts.Count; n++)
        //{
        //    if (secondaryTunnelEnds[n].Y > j)
        //    {
        //        BoreTunnelOriginal((int)secondaryTunnelHollows[n].X, (int)secondaryTunnelHollows[n].Y, (int)secondaryTunnelEnds[n].X, (int)secondaryTunnelEnds[n].Y, 4f, 65535);
        //    }
        //}
        #endregion
        for (int n = 0; n < points.Count; n++)
        {
            //BoreWavyTunnel((int)pointsForHollow[n].X, (int)pointsForHollow[n].Y, (int)endpoints[n][0], (int)endpoints[n][1], 50, 4, 3, 65535);
        }

        for (int n = 0; n < points.Count; n++)
        {
            if (endpoints[n][2] == 1)
            {
                if (endpoints[n][4] >= 19 && endpoints[n][4] <= 20)
                {
                    MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 12, chunkstone, true);
                }
                else if (endpoints[n][4] < 23)
                {
                    MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 13, chunkstone, true);
                    MakeCircle((int)endpoints[n][0], (int)endpoints[n][1], endpoints[n][4] - 18, 65535);
                    AddSepsisCell((int)endpoints[n][0], (int)endpoints[n][1]);
                }
            }
        }

        // Make the tunnel from the surface to the main circle
        for (int x = i - 14; x < i + 14; x++)
        {
            for (int y = j - radius - 50; y < j - radius + 8; y++)
            {
                int min = WorldGen.genRand.Next(6, 10);
                int max = WorldGen.genRand.Next(6, 10);
                int circleSize = WorldGen.genRand.Next(2, 5);
                int offsetX = WorldGen.genRand.Next(-2, 3);
                if (x >= i + 7 || x <= i - 7)
                {
                    MakeCircle(x + offsetX, y + 3, circleSize, (ushort)ModContent.TileType<Chunkstone>());
                }
                if (x <= i + min && x >= i - max)
                {
                    Tile t = Main.tile[x, y];
                    t.HasTile = false;
                    t.WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                }
                // Make the walls at the top of the entrance randomly jut out
                if (Main.tile[x, y - 1].WallType == 0 && Main.tile[x, y].WallType == (ushort)ModContent.WallType<ChunkstoneWall>() && (y < j - radius - 45))
                {
                    int doubleWide = (WorldGen.genRand.NextBool() ? -1 : 1);
                    Main.tile[x, y - 1].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    Main.tile[x + doubleWide, y - 1].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    if (Main.tile[x, y].TileType != (ushort)ModContent.TileType<Chunkstone>())
                    {
                        Main.tile[x, y - 2].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        Main.tile[x + doubleWide, y - 2].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        Main.tile[x + doubleWide + 1, y - 1].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        Main.tile[x + doubleWide - 1, y - 1].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        if (WorldGen.genRand.NextBool(2))
                        {
                            Main.tile[x, y - 3].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        }
                    }
                }
            }
        }
    }

    public static void MakeHollowCircle(int x, int y, int r, int thickness, ushort type)
    {
        int num = x - r;
        int num2 = y - r;
        int num3 = x + r;
        int num4 = y + r;
        for (int i = num; i < num3 + 1; i++)
        {
            for (int j = num2; j < num4 + 1; j++)
            {
                float dist = Vector2.Distance(new Vector2(i, j), new Vector2(x, y));
                if (dist <= r && dist > r - thickness)
                {
                    Main.tile[i, j].Active(true);
                    Main.tile[i, j].TileType = type;
                    if (dist <= r - 1 && dist > r - thickness + 1)
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    WorldGen.SquareTileFrame(i, j);
                }
            }
        }
    }

    public static void MakeOval(int x, int y, int xRadius, int yRadius, int type)
    {
        int xmin = x - xRadius;
        int ymin = y - yRadius;
        int xmax = x + xRadius;
        int ymax = y + yRadius;
        for (int i = xmin; i < xmax + 1; i++)
        {
            for (int j = ymin; j < ymax + 1; j++)
            {
                if (Utils.IsInsideEllipse(i, j, new Vector2(x, y), xRadius, yRadius) &&
                    Main.tile[i, j].TileType != TileID.ShadowOrbs && Main.tile[i, j].TileType != ModContent.TileType<SnotOrb>())
                {
                    if (type == 65535)
                    {
                        Tile t = Main.tile[i, j];
                        t.HasTile = false;
                        t.WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else
                    {
                        if (Main.tile[i, j].WallType != ModContent.WallType<ChunkstoneWall>())
                        {
                            Tile t = Main.tile[i, j];
                            t.HasTile = true;
                            t.TileType = (ushort)type;
                            if (Utils.IsInsideEllipse(i, j, new Vector2(x, y), xRadius - 1, yRadius - 1))
                            {
                                t.WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                            }
                            
                            WorldGen.SquareTileFrame(i, j);
                        }
                        //else if (center)
                        //{
                        //    Main.tile[i, j].Active(true);
                        //    Main.tile[i, j].TileType = type;
                        //    Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                        //    WorldGen.SquareTileFrame(i, j);
                        //}
                    }
                }
            }
        }
    }

    /// <summary>
    /// Helper method to generate a sinewave-like tunnel between 2 points. 
    /// Uses the MakeCircle method to generate the tunnel.
    /// </summary>
    /// <param name="startX">The starting X coordinate.</param>
    /// <param name="startY">The starting Y coordinate.</param>
    /// <param name="endX">The ending X coordinate.</param>
    /// <param name="endY">The ending Y coordinate.</param>
    /// <param name="wavelength">The wavelength of the wave.</param>
    /// <param name="amplitude">The amiplitude of the wave.</param>
    /// <param name="radius">The radius of the tunnel.</param>
    /// <param name="type">The tile type to place.</param>
    public static void BoreWavyTunnel(int startX, int startY, int endX, int endY, int wavelength, float amplitude, int radius, ushort type)
    {
        float length = Vector2.Distance(new Vector2(startX, startY), new Vector2(endX, endY));
        float direction = (float)Math.Atan2(endY - startY, endX - startX);
        float t = 0f;

        while (t <= 1f)
        {
            int x = (int)MathHelper.Lerp(startX, endX, t);
            int y = (int)MathHelper.Lerp(startY, endY, t) + (int)(Math.Sin(t * length / wavelength * MathHelper.TwoPi) * amplitude);

            // Place the desired tile or perform any other action
            MakeCircle(x, y, radius, type);

            t += 1f / length;
        }
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
    public static void BoreTunnelFred(int x0, int y0, int x1, int y1, float r, ushort type, bool center = false) // Code for making tunnels.. crazy
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
                MakeCircle(OriginalY + WorldGen.genRand.Next(-5, 5), i + WorldGen.genRand.Next(-5, 5), r + (int)(4 * Math.Sin((double)i / 10)), type, center);

                //if (WorldGen.genRand.Next(0, 16) == 15)
                //{
                //    MakeCircle(OriginalY + WorldGen.genRand.Next(-8, 9), i + WorldGen.genRand.Next(-8, 9), WorldGen.genRand.Next(6, 13), type);
                //}
            }
            else
            {
                MakeCircle(i + WorldGen.genRand.Next(-5, 5), OriginalY + WorldGen.genRand.Next(-5, 5), r + (int)(4 * Math.Sin((double)i / 10)), type, center);

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

    public static void BoreTunnelOriginal(int x0, int y0, int x1, int y1, float r, ushort type, bool center = false)
    {
        bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (flag)
        {
            Utils.Swap<int>(ref x0, ref y0);
            Utils.Swap<int>(ref x1, ref y1);
        }
        if (x0 > x1)
        {
            Utils.Swap<int>(ref x0, ref x1);
            Utils.Swap<int>(ref y0, ref y1);
        }
        int num = x1 - x0;
        int num2 = Math.Abs(y1 - y0);
        int num3 = num / 2;
        int num4 = (y0 < y1) ? 1 : -1;
        int num5 = y0;
        for (int i = x0; i <= x1; i++)
        {
            if (flag)
            {
                MakeCircle(num5, i, r, type, center);
            }
            else
            {
                MakeCircle(i, num5, r, type, center);
            }
            num3 -= num2;
            if (num3 < 0)
            {
                num5 += num4;
                num3 += num;
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
    public static void MakeCircle(int x, int y, float r, ushort type, bool center = false)
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
                    Main.tile[i, j].TileType != TileID.ShadowOrbs && Main.tile[i, j].TileType != ModContent.TileType<SnotOrb>())
                {
                    if (type == 65535)
                    {
                        Main.tile[i, j].Active(false);
                        Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                    }
                    else
                    {
                        if (Main.tile[i, j].WallType != ModContent.WallType<ChunkstoneWall>())
                        {
                            Main.tile[i, j].Active(true);
                            Main.tile[i, j].TileType = type;
                            if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r - 2)
                            {
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                            }
                            WorldGen.SquareTileFrame(i, j);
                        }
                        else if (center)
                        {
                            Main.tile[i, j].Active(true);
                            Main.tile[i, j].TileType = type;
                            //if (Vector2.Distance(new Vector2(i, j), new Vector2(x, y)) <= r - 1)
                            //{
                                Main.tile[i, j].WallType = (ushort)ModContent.WallType<ChunkstoneWall>();
                            //}
                            WorldGen.SquareTileFrame(i, j);
                        }
                        
                        //if (!Main.tile[i, j].HasTile && Main.tile[i, j].WallType != ModContent.WallType<ChunkstoneWall>())
                        //{
                            
                        //}
                    }
                }
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
    ///     Places a Sepsis Cell at the given coordinates. For the Contagion.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    /// <param name="style">Unused.</param>
    public static void AddSepsisCell(int x, int y, int style = 0)
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
