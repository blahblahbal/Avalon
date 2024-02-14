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

        #region tunnels
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

        int angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        if (!leftOrRight) angleDegrees = WorldGen.genRand.Next(-5, 6);
        float angle = (float)(Math.PI / 180) * angleDegrees;
        float posX = (float)(endpoints[0].X + (tunnelLength + WorldGen.genRand.Next(8, 10)) * Math.Cos(angle));
        float posY = (float)(endpoints[0].Y + (tunnelLength + WorldGen.genRand.Next(8, 10)) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[1].X, (int)endpoints[1].Y, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));

        // third tunnel/room (down)
        angleDegrees = 80 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[2].X + (tunnelLength - 10) * Math.Cos(angle));
        posY = (float)(endpoints[2].Y + (tunnelLength - 10) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[2].X, (int)endpoints[2].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));

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

        // fifth tunnel/room
        angleDegrees = WorldGen.genRand.Next(-5, 6);
        if (!leftOrRight) angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[4].X + (tunnelLength + difference) * Math.Cos(angle));
        posY = (float)(endpoints[4].Y + (tunnelLength + difference) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[4].X, (int)endpoints[4].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));

        // sixth tunnel/room
        angleDegrees = 80 + WorldGen.genRand.Next(-5, 6);
        //if (!leftOrRight) angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[5].X + (tunnelLength - 20) * Math.Cos(angle));
        posY = (float)(endpoints[5].Y + (tunnelLength - 20) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[5].X, (int)endpoints[5].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));

        // seventh tunnel/room
        angleDegrees = 180 + WorldGen.genRand.Next(-5, 6);
        if (!leftOrRight) angleDegrees = WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[6].X + (tunnelLength + difference) * Math.Cos(angle));
        posY = (float)(endpoints[6].Y + (tunnelLength + difference) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[6].X, (int)endpoints[6].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 0, 15);
        endpoints.Add(MakeBox((int)posX, (int)posY + 7, WorldGen.genRand.Next(20, 30), WorldGen.genRand.Next(15, 20), ushort.MaxValue, 15, 1));
        #endregion

        #region boss room
        angleDegrees = 80 + WorldGen.genRand.Next(-5, 6);
        angle = (float)(Math.PI / 180) * angleDegrees;
        posX = (float)(endpoints[7].X + (tunnelLength) * Math.Cos(angle));
        posY = (float)(endpoints[7].Y + (tunnelLength) * Math.Sin(angle));

        Hellcastle.BoreTunnel((int)endpoints[7].X, (int)endpoints[7].Y - 8, (int)posX, (int)posY, heightOfStartTunnel, ushort.MaxValue, 0);

        MakeBoxFromCenter((int)posX + 25, (int)posY, WorldGen.genRand.Next(80, 90), WorldGen.genRand.Next(25, 35), ushort.MaxValue, 0, 30);
        endpoints.Add(MakeBox((int)posX + 25, (int)posY, WorldGen.genRand.Next(80, 90), WorldGen.genRand.Next(25, 35), ushort.MaxValue, 30, 1));
        #endregion

        #region door, spikes, etc
        PlaceDoor((int)midpointStartTunnel.X, (int)midpointStartTunnel.Y);
        AddSpikes(x - wide / 2, y, 90, heightOfBox + high + high / 2);
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
