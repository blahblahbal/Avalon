using Avalon.Tiles.Tropics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.Structures;

internal class TuhrtlOutpost
{
    public static void Outpost(int x, int y)
    {
        // height of the vertical box to generate
        int heightOfBox = 0;
        int wallHeightOfBox = 0;

        ushort brick = (ushort)ModContent.TileType<TuhrtlBrick>();
        ushort brickWall = (ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>();

        #region tiles
        int wide = 90;
        int high = 60;

        int totalWidth = wide * 2;
        // add high to height of box, offsetting the diagonal part by high
        heightOfBox += high;

        // set the pyramid step width to 90
        int pstep = 90;
        int yStartSlope = y + high;
        heightOfBox += high;

        for (int pyY = yStartSlope; pyY <= yStartSlope + high + 4; pyY += 2)
        {
            for (int pyX = x - pstep + 1; pyX <= x + pstep + 1; pyX++)
            {
                Main.tile[pyX, pyY].Active(true);
                Main.tile[pyX, pyY].TileType = brick;
                Main.tile[pyX, pyY - 1].Active(true);
                Main.tile[pyX, pyY - 1].TileType = brick;
                Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
                Utils.SquareTileFrame(pyX, pyY - 1, resetSlope: true);
            }
            pstep++;
            totalWidth++;
        }

        int yStartInnerSlope = yStartSlope + high + 4;
        heightOfBox += high + 4;

        for (int pyY = yStartInnerSlope; pyY <= yStartInnerSlope + high + 4; pyY += 2)
        {
            for (int pyX = x - pstep + 2; pyX <= x + pstep; pyX++)
            {
                Main.tile[pyX, pyY].Active(true);
                Main.tile[pyX, pyY].TileType = brick;
                Main.tile[pyX, pyY - 1].Active(true);
                Main.tile[pyX, pyY - 1].TileType = brick;
                Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
                Utils.SquareTileFrame(pyX, pyY - 1, resetSlope: true);
            }
            pstep--;
            totalWidth++;
        }

        // make the main box
        for (int i = x - wide + 1; i < x + wide + 1; i++)
        {
            for (int j = y; j < y + heightOfBox + high + (high / 2); j++)
            {
                Main.tile[i, j].Active(true);
                Main.tile[i, j].TileType = brick;
                Utils.SquareTileFrame(i, j, resetSlope: true);
                Main.tile[i, j].LiquidAmount = 0;
            }
        }
        #endregion

        #region walls
        int wallWidth = wide - 2;
        int wallHeight = high - 2;

        wallHeightOfBox += wallHeight;

        // set the pyramid step width to 90
        int pstepWall = 90;
        int yStartSlopeWall = y + high;
        wallHeightOfBox += high;

        for (int pyY = yStartSlopeWall; pyY <= yStartSlopeWall + high + 4; pyY += 2)
        {
            for (int pyX = x - pstepWall + 2; pyX <= x + pstepWall; pyX++)
            {
                Main.tile[pyX, pyY].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY);
                Main.tile[pyX, pyY - 1].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY - 1);
            }
            pstepWall++;
        }

        int yStartInnerSlopeWall = yStartSlopeWall + high + 4;
        wallHeightOfBox += high + 4;

        for (int pyY = yStartInnerSlopeWall; pyY <= yStartInnerSlopeWall + high + 4; pyY += 2)
        {
            for (int pyX = x - pstepWall + 3; pyX <= x + pstepWall - 1; pyX++)
            {
                Main.tile[pyX, pyY].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY);
                Main.tile[pyX, pyY - 1].WallType = brickWall;
                WorldGen.SquareWallFrame(pyX, pyY - 1);
            }
            pstepWall--;
        }

        // make the main box walls
        for (int i = x - wallWidth; i < x + wallWidth; i++)
        {
            for (int j = y + 1; j < y + wallHeightOfBox + high + (high / 2); j++)
            {
                Main.tile[i, j].WallType = brickWall;
                WorldGen.SquareWallFrame(i, j);
            }
        }

        #endregion

        Vector2 midpointStartTunnel;
        int rooms = 0;

        #region tunnels and rooms
        List<Vector2> endpoints = new List<Vector2>();

        // true: right, false: left (Start side)
        bool leftOrRight = WorldGen.genRand.NextBool(2);
        int xStartTunnel = x + (leftOrRight ? wide : -wide);
        int yTunnel = y + high / 2 + WorldGen.genRand.Next(-10, 15);
        int heightOfStartTunnel = WorldGen.genRand.Next(4, 7);

        Vector2 tunnelEndpoint = new Vector2(x, yTunnel);
        // add endpoint to list
        endpoints.Add(tunnelEndpoint);
        // length of the tunnel
        int tunnelLength = WorldGen.genRand.Next(40, 55);

        Hellcastle.BoreTunnel(xStartTunnel, y + high / 2, (int)tunnelEndpoint.X, (int)tunnelEndpoint.Y, heightOfStartTunnel, ushort.MaxValue, 0);

        // midpoint calc
        if (leftOrRight)
        {
            midpointStartTunnel = new Vector2(xStartTunnel - ((xStartTunnel - tunnelEndpoint.X) / 2), (y + high / 2) - (((y + high / 2) - tunnelEndpoint.Y) / 2));
        }
        else
        {
            midpointStartTunnel = new Vector2(tunnelEndpoint.X - ((tunnelEndpoint.X - xStartTunnel) / 2), tunnelEndpoint.Y - ((tunnelEndpoint.Y - (y + high / 2)) / 2));
        }
        

        MakeBoxFromCenter((int)endpoints[0].X, (int)endpoints[0].Y + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)endpoints[0].X, (int)endpoints[0].Y + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;

        int angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        if (!leftOrRight) angleDegrees = WorldGen.genRand.Next(-5, 6);
        float angle = (float)(Math.PI / 180) * angleDegrees;
        float posX = (float)(endpoints[0].X + (tunnelLength + WorldGen.genRand.Next(8, 10)) * Math.Cos(angle));
        float posY = (float)(endpoints[0].Y + (tunnelLength + WorldGen.genRand.Next(8, 10)) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[1].X, (int)endpoints[1].Y, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;

        // third tunnel/room (down)
        angleDegrees = 80 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[2].X + (tunnelLength - 10) * Math.Cos(angle));
        posY = (float)(endpoints[2].Y + (tunnelLength - 10) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[2].X, (int)endpoints[2].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;

        // fourth tunnel/room
        angleDegrees = WorldGen.genRand.Next(-5, 6);
        int difference = 20;
        if (!leftOrRight)
        {
            angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
            difference = -10;
        }
        else
        {
            
        }
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[3].X + (tunnelLength + difference) * Math.Cos(angle));
        posY = (float)(endpoints[3].Y + (tunnelLength + difference) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[3].X, (int)endpoints[3].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;

        // fifth tunnel/room
        angleDegrees = WorldGen.genRand.Next(-5, 6);
        if (!leftOrRight) angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[4].X + (tunnelLength + difference) * Math.Cos(angle));
        posY = (float)(endpoints[4].Y + (tunnelLength + difference) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[4].X, (int)endpoints[4].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;

        // sixth tunnel/room
        angleDegrees = 80 + WorldGen.genRand.Next(-5, 6);
        //if (!leftOrRight) angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[5].X + (tunnelLength - 20) * Math.Cos(angle));
        posY = (float)(endpoints[5].Y + (tunnelLength - 20) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[5].X, (int)endpoints[5].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;

        // seventh tunnel/room
        angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        if (!leftOrRight) angleDegrees = WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[6].X + (tunnelLength + difference) * Math.Cos(angle));
        posY = (float)(endpoints[6].Y + (tunnelLength + difference) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[6].X, (int)endpoints[6].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        rooms++;
        #endregion

        #region boss room
        angleDegrees = 80 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[7].X + (tunnelLength) * Math.Cos(angle));
        posY = (float)(endpoints[7].Y + (tunnelLength) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[7].X, (int)endpoints[7].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX + 25, (int)posY, WorldGen.genRand.Next(80, 90), WorldGen.genRand.Next(25, 29), ushort.MaxValue, 0, 30);
        endpoints.Add(MakeBox((int)posX + 25, (int)posY, WorldGen.genRand.Next(80, 90), WorldGen.genRand.Next(25, 29), ushort.MaxValue, 30, 1));
        #endregion

        #region door, spikes, chests, etc
        PlaceDoor((int)midpointStartTunnel.X, (int)midpointStartTunnel.Y);
        //AddChests(x - wide / 2, y, 90, heightOfBox + high + high / 2);
        
        AddSpikes(x - wide / 2, y, 90, heightOfBox + high + high / 2);
        

        GenVars.tLeft = x - wide;
        GenVars.tRight = x + wide;
        GenVars.tTop = y;
        GenVars.tBottom = y + heightOfBox + high + high / 2;
        GenVars.tRooms = rooms;
        OutpostPart2();

        GenVars.structures.AddProtectedStructure(new Rectangle(x - (totalWidth / 2), y, totalWidth, heightOfBox + high + high / 2), 10);
        #endregion
    }

    public static void AddSpikes(int x, int y, int width, int height)
    {
        int counter = 0;
        int countTo = 10;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {

                }
                else
                {
                    if (Main.tile[x + i, y + j].HasTile && !Main.tileSolidTop[Main.tile[x + i, y + j].TileType] && Main.tileSolid[Main.tile[x + i, y + j].TileType])
                    {
                        if (!Main.tile[x + i, y + j - 1].HasTile && Main.tile[x + i + 1, y + j].HasTile && Main.tile[x + i - 1, y + j].HasTile)
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                Hellcastle.GenerateSpikeTrap(x + i, y + j, WorldGen.genRand.Next(15, 21));
                                counter = 0;
                                countTo = 10;
                            }
                        }
                        if (!Main.tile[x + i, y + j + 1].HasTile && Main.tile[x + i + 1, y + j].HasTile && Main.tile[x + i - 1, y + j].HasTile)
                        {
                            counter++;
                            if (counter > countTo)
                            {
                                Hellcastle.GenerateSpikeTrap(x + i, y + j, WorldGen.genRand.Next(15, 21));
                                counter = 0;
                                countTo = 10;
                            }
                        }
                    }
                }
            }
        }
    }
    public static void AddChests(int x, int y, int width, int height)
    {
        int maxAmount = WorldGen.genRand.Next(15, 18);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                }
                else
                {
                    if (Main.tile[x + i, y + j].HasTile && !Main.tile[x + i, y + j - 1].HasTile && !Main.tile[x + i + 1, y + j - 1].HasTile)
                    {
                        if (WorldGen.genRand.NextBool(80) && maxAmount > 0)
                        {
                            maxAmount--;
                            WorldGen.AddBuriedChest(x + i + 1, y + j, 1293, true, 16);
                            
                            //AddTuhrtlChest(x + i + 1, y + j);
                            //WorldGen.PlaceChest(x + i, y + j - 1, (ushort)ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodChest>());
                        }
                    }
                }
            }
        }
    }

    public static void OutpostPart2()
    {
        int tLeft = GenVars.tLeft;
        int tRight = GenVars.tRight;
        int tTop = GenVars.tTop;
        int tBottom = GenVars.tBottom;
        int tRooms = GenVars.tRooms;
        double num = tRooms * 1.9;
        num *= 1.0 + WorldGen.genRand.Next(-15, 16) * 0.01;
        int num2 = 0;
        while (num > 0.0)
        {
            int num3 = WorldGen.genRand.Next(tLeft, tRight);
            int num4 = WorldGen.genRand.Next(tTop, tBottom);
            if (Main.tile[num3, num4].WallType == ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>() && !Main.tile[num3, num4].HasTile)
            {
                if (TuhrtlTrap(num3, num4))
                {
                    num -= 1.0;
                    num2 = 0;
                }
                else
                {
                    num2++;
                }
            }
            else
            {
                num2++;
            }

            if (num2 > 100)
            {
                num2 = 0;
                num -= 1.0;
            }
        }

        Main.tileSolid[ModContent.TileType<Tiles.VenomSpike>()] = false;
        double num5 = tRooms * 0.35;
        num5 *= 1.0 + WorldGen.genRand.Next(-15, 16) * 0.01;
        int contain = 1293;
        num2 = 0;
        while (num5 > 0.0)
        {
            int num6 = WorldGen.genRand.Next(tLeft, tRight);
            int num7 = WorldGen.genRand.Next(tTop, tBottom);
            if (Main.tile[num6, num7].WallType == ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>() && !Main.tile[num6, num7].HasTile && WorldGen.AddBuriedChest(num6, num7, contain, notNearOtherChests: true, 16, trySlope: false, 0))
            {
                num5 -= 1.0;
                num2 = 0;
            }

            num2++;
            if (num2 > 10000)
                break;
        }

        double num8 = tRooms * 1.25;
        num8 *= 1.0 + WorldGen.genRand.Next(-25, 36) * 0.01;
        num2 = 0;
        while (num8 > 0.0)
        {
            num2++;
            int num9 = WorldGen.genRand.Next(tLeft, tRight);
            int num10 = WorldGen.genRand.Next(tTop, tBottom);
            if (Main.tile[num9, num10].WallType != ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>() || Main.tile[num9, num10].HasTile)
                continue;

            int num11 = num9;
            int num12 = num10;
            while (!Main.tile[num11, num12].HasTile)
            {
                num12++;
                if (num12 > tBottom)
                    break;
            }

            num12--;
            if (num12 <= tBottom)
            {
                WorldGen.PlaceTile(num11, num12, ModContent.TileType<Tiles.Statues>(), mute: true, forced: false, -1, 15); // other types later
                if (Main.tile[num11, num12].TileType == ModContent.TileType<Tiles.Statues>())
                    num8 -= 1.0;
            }
        }

        double num13 = tRooms * 1.35;
        num13 *= 1.0 + WorldGen.genRand.Next(-15, 26) * 0.01;
        num2 = 0;
        while (num13 > 0.0)
        {
            num2++;
            int num14 = WorldGen.genRand.Next(tLeft, tRight);
            int num15 = WorldGen.genRand.Next(tTop, tBottom);
            if (Main.tile[num14, num15].WallType == ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>() && !Main.tile[num14, num15].HasTile)
            {
                int num16 = num14;
                int num17 = num15;
                while (!Main.tile[num16, num17].HasTile)
                {
                    num17++;
                    if (num17 > tBottom)
                        break;
                }

                num17--;
                if (num17 <= tBottom)
                {
                    switch (WorldGen.genRand.Next(3))
                    {
                        case 0:
                            WorldGen.PlaceTile(num16, num17, ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlWorkBench>(), mute: true);
                            if (Main.tile[num16, num17].TileType == ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlWorkBench>())
                                num13 -= 1.0;
                            break;
                        case 1:
                            WorldGen.PlaceTile(num16, num17, ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlChair>(), mute: true);
                            if (Main.tile[num16, num17].TileType == ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlChair>())
                                num13 -= 1.0;
                            break;
                        case 2:
                            WorldGen.PlaceTile(num16, num17, ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlTable>(), mute: true);
                            if (Main.tile[num16, num17].TileType == ModContent.TileType<Tiles.Furniture.Tuhrtl.TuhrtlTable>())
                                num13 -= 1.0;
                            break;
                    }
                }
            }

            if (num2 > 10000)
                break;
        }

        int num18 = 1;
        if (Main.maxTilesX > 4200)
            num18++;

        if (Main.maxTilesX > 6400)
            num18 += WorldGen.genRand.Next(2);

        num2 = 0;
        while (num18 > 0)
        {
            num2++;
            int num19 = WorldGen.genRand.Next(tLeft, tRight);
            int num20 = WorldGen.genRand.Next(tTop, tBottom);
            if (Main.tile[num19, num20].WallType == ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>() && !Main.tile[num19, num20].HasTile)
            {
                bool flag = false;
                for (int i = -70; i <= 70; i++)
                {
                    for (int j = -70; j <= 70; j++)
                    {
                        int num21 = i + num19;
                        int num22 = j + num20;
                        if (!WorldGen.InWorld(num21, num22, 5))
                            continue;

                        Tile tile = Main.tile[num21, num22];
                        if (tile.HasTile)
                        {
                            if (tile.TileType == 240)
                            {
                                flag = true;
                                break;
                            }

                            if (i >= -4 && i <= 4 && j >= -4 && j <= 4 && tile.TileType == ModContent.TileType<TuhrtlBrick>())
                            {
                                flag = true;
                                break;
                            }
                        }
                    }

                    if (flag)
                        break;
                }

                if (flag)
                    continue;

                if (WorldGen.PlaceTile(num19, num20, ModContent.TileType<Tiles.Paintings3x3>(), mute: true, forced: false, -1, 5))
                    num18--;
            }

            if (num2 > 10000)
                break;
        }

        Main.tileSolid[ModContent.TileType<Tiles.VenomSpike>()] = true;
    }
    public static bool TuhrtlTrap(int x2, int y2)
    {
        int num = 1;
        if (WorldGen.genRand.NextBool(3))
            num = 0;

        int num2 = y2;
        while (!WorldGen.SolidOrSlopedTile(x2, num2))
        {
            num2++;
            if (num2 >= Main.maxTilesY - 300)
                return false;
        }

        if (Main.tile[x2, num2].TileType == 232 || Main.tile[x2, num2].TileType == 10)
            return false;

        num2--;
        if (Main.tile[x2, num2].LiquidAmount > 0 && Main.tile[x2, num2].LiquidType == LiquidID.Lava)
            return false;

        if (num == -1 && WorldGen.genRand.NextBool(20))
            num = 2;
        else if (num == -1)
            num = WorldGen.genRand.Next(2);

        if (Main.tile[x2, num2].HasUnactuatedTile || Main.tile[x2 - 1, num2].HasUnactuatedTile || Main.tile[x2 + 1, num2].HasUnactuatedTile || Main.tile[x2, num2 - 1].HasUnactuatedTile || Main.tile[x2 - 1, num2 - 1].HasUnactuatedTile || Main.tile[x2 + 1, num2 - 1].HasUnactuatedTile || Main.tile[x2, num2 - 2].HasUnactuatedTile || Main.tile[x2 - 1, num2 - 2].HasUnactuatedTile || Main.tile[x2 + 1, num2 - 2].HasUnactuatedTile)
            return false;

        if (Main.tile[x2, num2 + 1].TileType == 10)
            return false;

        if (Main.tile[x2, num2 + 1].TileType == 48)
            return false;

        if (Main.tile[x2, num2 + 1].TileType == 232)
            return false;

        switch (num)
        {
            case 0:
                {
                    int num12 = x2;
                    int num13 = num2;
                    num13 -= WorldGen.genRand.Next(3);
                    while (WorldGen.InWorld(num12, num13, 5) && !WorldGen.SolidOrSlopedTile(num12, num13))
                    {
                        num12--;
                    }

                    int num14 = num12;
                    for (num12 = x2; WorldGen.InWorld(num12, num13, 5) && !WorldGen.SolidOrSlopedTile(num12, num13); num12++)
                    {
                    }

                    int num15 = num12;
                    int num16 = x2 - num14;
                    int num17 = num15 - x2;
                    bool flag = false;
                    bool flag2 = false;
                    if (num16 > 5 && num16 < 50)
                        flag = true;

                    if (num17 > 5 && num17 < 50)
                        flag2 = true;

                    if (flag && !WorldGen.SolidOrSlopedTile(num14, num13 + 1))
                        flag = false;

                    if (flag2 && !WorldGen.SolidOrSlopedTile(num15, num13 + 1))
                        flag2 = false;

                    if (flag && (Main.tile[num14, num13].TileType == 10 || Main.tile[num14, num13].TileType == 48 || Main.tile[num14, num13 + 1].TileType == 10 || Main.tile[num14, num13 + 1].TileType == 48))
                        flag = false;

                    if (flag2 && (Main.tile[num15, num13].TileType == 10 || Main.tile[num15, num13].TileType == 48 || Main.tile[num15, num13 + 1].TileType == 10 || Main.tile[num15, num13 + 1].TileType == 48))
                        flag2 = false;

                    int num18 = 0;
                    if (flag && flag2)
                    {
                        num18 = 1;
                        num12 = num14;
                        if (WorldGen.genRand.NextBool(2))
                        {
                            num12 = num15;
                            num18 = -1;
                        }
                    }
                    else if (flag2)
                    {
                        num12 = num15;
                        num18 = -1;
                    }
                    else
                    {
                        if (!flag)
                            return false;

                        num12 = num14;
                        num18 = 1;
                    }

                    if (Main.tile[num12, num13].WallType != ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>())
                        return false;

                    if (Main.tile[num12, num13].TileType == 135)
                        return false;

                    if (Main.tile[num12, num13].TileType == 137)
                        return false;

                    if (Main.tile[num12, num13].TileType == ModContent.TileType<Tiles.VenomSpike>())
                        return false;

                    if (Main.tile[num12, num13].TileType == 237) // altar
                        return false;

                    if (Main.tile[num12, num13].TileType == ModContent.TileType<Tiles.Furniture.Tuhrtl.LockedTuhrtlDoor>())
                        return false;

                    WorldGen.PlaceTile(x2, num2, 135, mute: true, forced: true, -1, 6);
                    WorldGen.KillTile(num12, num13);
                    int num19 = WorldGen.genRand.Next(3);
                    if (Main.tile[x2, num2].RedWire)
                        num19 = 0;

                    if (Main.tile[x2, num2].BlueWire)
                        num19 = 1;

                    if (Main.tile[x2, num2].GreenWire)
                        num19 = 2;

                    int num20 = Math.Abs(num12 - x2);
                    int style2 = 1;
                    if (num20 < 10 && !WorldGen.genRand.NextBool(3))
                        style2 = 2;

                    WorldGen.PlaceTile(num12, num13, 137, mute: true, forced: true, -1, style2);
                    if (num18 == 1)
                        Main.tile[num12, num13].TileFrameX += 18;

                    int num21 = WorldGen.genRand.Next(5);
                    int num22 = num13;
                    while (num21 > 0)
                    {
                        num21--;
                        num22--;
                        if (!WorldGen.SolidTile(num12, num22) || !WorldGen.SolidTile(num12 - num18, num22) || WorldGen.SolidOrSlopedTile(num12 + num18, num22))
                            break;

                        WorldGen.PlaceTile(num12, num22, 137, mute: true, forced: true, -1, style2);
                        if (num18 == 1)
                            Main.tile[num12, num22].TileFrameX += 18;

                        Tile wireTile = Main.tile[num12, num22];
                        switch (num19)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }
                    }

                    int num23 = x2;
                    int num24 = num2;
                    while (num23 != num12 || num24 != num13)
                    {
                        Tile wireTile = Main.tile[num23, num24];
                        switch (num19)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }

                        if (num23 > num12)
                            num23--;

                        if (num23 < num12)
                            num23++;

                        wireTile = Main.tile[num23, num24];

                        switch (num19)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }

                        if (num24 > num13)
                            num24--;

                        if (num24 < num13)
                            num24++;

                        wireTile = Main.tile[num23, num24];

                        switch (num19)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }
                    }

                    return true;
                }
            case 1:
                {
                    int num3 = x2;
                    int num4 = num2;
                    while (!WorldGen.SolidOrSlopedTile(num3, num4))
                    {
                        num4--;
                        if ((double)num4 < Main.worldSurface)
                            return false;
                    }

                    int num5 = Math.Abs(num4 - num2);
                    if (num5 < 3)
                        return false;

                    int num6 = WorldGen.genRand.Next(3);
                    if (Main.tile[x2, num2].RedWire)
                        num6 = 0;

                    if (Main.tile[x2, num2].BlueWire)
                        num6 = 1;

                    if (Main.tile[x2, num2].GreenWire)
                        num6 = 2;

                    int style = 3;
                    if (num5 < 16 && !WorldGen.genRand.NextBool(3))
                        style = 4;

                    if (Main.tile[num3, num4].TileType == 135)
                        return false;

                    if (Main.tile[num3, num4].TileType == 137)
                        return false;

                    if (Main.tile[num3, num4].TileType == ModContent.TileType<Tiles.VenomSpike>()) // change to bramble variant later
                        return false;

                    if (Main.tile[num3, num4].TileType == 237) // altar
                        return false;

                    if (Main.tile[num3, num4].TileType == ModContent.TileType<Tiles.Furniture.Tuhrtl.LockedTuhrtlDoor>()) // door
                        return false;

                    if (Main.tile[num3, num4].WallType != ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>())
                        return false;

                    WorldGen.PlaceTile(x2, num2, 135, mute: true, forced: true, -1, 6);
                    WorldGen.PlaceTile(num3, num4, 137, mute: true, forced: true, -1, style);
                    for (int i = 0; i < 2; i++)
                    {
                        int num7 = WorldGen.genRand.Next(1, 5);
                        int num8 = num3;
                        int num9 = -1;
                        if (i == 1)
                            num9 = 1;

                        while (num7 > 0)
                        {
                            num7--;
                            num8 += num9;
                            if (!WorldGen.SolidTile(num8, num4 - 1) || WorldGen.SolidOrSlopedTile(num8, num4 + 1))
                                break;

                            WorldGen.PlaceTile(num8, num4, 137, mute: true, forced: true, -1, style);
                            Tile wireTile = Main.tile[num8, num4];
                            switch (num6)
                            {
                                case 0:
                                    wireTile.RedWire = true;
                                    break;
                                case 1:
                                    wireTile.BlueWire = true;
                                    break;
                                case 2:
                                    wireTile.GreenWire = true;
                                    break;
                            }
                        }
                    }

                    int num10 = x2;
                    int num11 = num2;
                    while (num10 != num3 || num11 != num4)
                    {
                        Tile wireTile = Main.tile[num10, num11];
                        switch (num6)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }

                        if (num10 > num3)
                            num10--;

                        if (num10 < num3)
                            num10++;

                        wireTile = Main.tile[num10, num11];
                        
                        switch (num6)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }

                        if (num11 > num4)
                            num11--;

                        if (num11 < num4)
                            num11++;

                        wireTile = Main.tile[num10, num11];

                        switch (num6)
                        {
                            case 0:
                                wireTile.RedWire = true;
                                break;
                            case 1:
                                wireTile.BlueWire = true;
                                break;
                            case 2:
                                wireTile.GreenWire = true;
                                break;
                        }
                    }

                    return true;
                }
            default:
                return false;
        }
    }
    public static void AddTuhrtlChest(int i, int j, bool notNearOtherChests = false)
    {
        int k = j;
        while (k < Main.maxTilesY)
        {
            if (Main.tile[i, k].HasTile && Main.tileSolid[Main.tile[i, k].TileType])
            {
                int num = k;
                int num2 = WorldGen.PlaceChest(i - 1, num - 1, (ushort)ModContent.TileType<Tiles.Furniture.ResistantWood.ResistantWoodChest>(), notNearOtherChests);
                if (num2 >= 0)
                {
                    //Main.chest[num2].item[0].SetDefaults(ModContent.ItemType<TuhrtlRelic>());

                    if (WorldGen.genRand.NextBool(5))
                    {
                        Main.chest[num2].item[1].SetDefaults(2767);
                    }
                    else
                    {
                        Main.chest[num2].item[1].SetDefaults(2766);
                        Main.chest[num2].item[1].stack = WorldGen.genRand.Next(3, 8);
                    }


                }
            }
        }
    }

    public static void PlaceDoor(int x, int y)
    {
        int pstep = 1; // width of pyramid at the top
        for (int pyY = y + 2; pyY < y + 6; pyY++)
        {
            for (int pyX = x - pstep; pyX <= x + pstep; pyX++)
            {
                Main.tile[pyX, pyY].Active(true);
                Main.tile[pyX, pyY].TileType = (ushort)ModContent.TileType<TuhrtlBrick>();
                Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
            }
            pstep++;
        }

        for (int pyY = y - 6; pyY <= y - 2; pyY++)
        {
            for (int pyX = x - pstep; pyX <= x + pstep; pyX++)
            {
                Main.tile[pyX, pyY].Active(true);
                Main.tile[pyX, pyY].TileType = (ushort)ModContent.TileType<TuhrtlBrick>();
                Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
            }
            pstep--;
        }
        PlaceDoorActually(x, y, ModContent.TileType<Tiles.Furniture.Tuhrtl.LockedTuhrtlDoor>(), 0);
    }

    public static bool PlaceDoorActually(int i, int j, int type, int style = 0)
    {
        int num = style / 36;
        int num2 = style % 36;
        int num3 = 54 * num;
        int num4 = 54 * num2;
        try
        {
            if (Main.tile[i, j - 2].HasUnactuatedTile && Main.tileSolid[Main.tile[i, j - 2].TileType] && WorldGen.SolidTile(i, j + 2))
            {
                Tile top = Main.tile[i, j - 1];
                top.HasTile = true;
                top.TileType = (ushort)type;
                top.TileFrameY = (short)num4;
                top.TileFrameX = (short)(num3 + WorldGen.genRand.Next(3) * 18);

                Tile middle = Main.tile[i, j];
                middle.HasTile = true;
                middle.TileType = (ushort)type;
                middle.TileFrameY = (short)(num4 + 18);
                middle.TileFrameX = (short)(num3 + WorldGen.genRand.Next(3) * 18);

                Tile bottom = Main.tile[i, j + 1];
                bottom.HasTile = true;
                bottom.TileType = (ushort)type;
                bottom.TileFrameY = (short)(num4 + 36);
                bottom.TileFrameX = (short)(num3 + WorldGen.genRand.Next(3) * 18);
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public static Vector2 MakeBox(int x, int y, int width, int height, int type, int length, int direction)
    {
        int a = (width / 2);
        int b = (height / 2);

        if (direction == 0)
        {
            Hellcastle.BoreTunnel(x - a, y - b + WorldGen.genRand.Next(-6, 7), x + a, y + b, length, (ushort)type, 0);
            return new Vector2(x + a, y + b - 2);
        }
        else
        {
            Hellcastle.BoreTunnel(x - a, y + b, x + a, y - b + WorldGen.genRand.Next(-6, 7), length, (ushort)type, 0);
            return new Vector2(x - a, y + b - 2);
        }
    }
    public static void MakeBoxFromCenter(int x, int y, int width, int height, int type, float angle, int length)
    {
        int a = (width / 2);
        int b = (height / 2);

        if (angle == 0)
        {
            Hellcastle.BoreTunnel(x - a, y - b + WorldGen.genRand.Next(-6, 7), x + a, y + b, length, (ushort)type, 0);
        }
        else if (angle == 1)
        {
            Hellcastle.BoreTunnel(x - a, y + b, x + a, y - b + WorldGen.genRand.Next(-6, 7), length, (ushort)type, 0);
        }
    }
}
