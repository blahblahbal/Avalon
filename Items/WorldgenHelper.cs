using Avalon.Common;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.CrystalMines;
using Avalon.Tiles.Tropics;
using Avalon.WorldGeneration.Passes;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

class WorldgenHelper : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Purple;
        Item.width = dims.Width;
        Item.maxStack = 1;
        Item.useAnimation = Item.useTime = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 0;
        Item.height = dims.Height;
        //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Scroll");
    }
    public static float Distance(Vector2 a, Vector2 b)
    {
        float diff_x = a.X - b.X;
        float diff_y = a.Y - b.Y;
        return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y);
    }
    public override bool? UseItem(Player player)
    {
        int x = (int)Main.MouseWorld.X / 16;
        int y = (int)Main.MouseWorld.Y / 16;

        if (player.ItemAnimationJustStarted)
        {
            //WorldGeneration.Structures.TuhrtlOutpost.Outpost(x, y);
            //CreateLeafTrap(x, y);
            //Crystals(x, y);
            //World.Biomes.CrystalMines.Place(new Point(x, y));

            //WorldGeneration.Structures.Nest.CreateWaspNest(x, y);

            //WorldGeneration.Structures.LavaOcean.MakeLavaLake(x, y);
            //WorldGeneration.Structures.LavaShrine.NewLavaShrine(x - 29, y - 10);


            //int rift = Item.NewItem(Item.GetSource_TileInteraction(x, y), x * 16, y * 16, 8, 8, ModContent.ItemType<Accessories.Hardmode.OreRift>());
            //Main.item[rift].velocity *= 0f;
            //Main.item[rift].GetGlobalItem<AvalonGlobalItemInstance>().RiftTimeLeft = 300;
        }
        return false;
    }
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
                WorldGeneration.Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
                WorldGeneration.Utils.SquareTileFrame(pyX, pyY - 1, resetSlope: true);
            }
            pstep++;
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
                WorldGeneration.Utils.SquareTileFrame(pyX, pyY, resetSlope: true);
                WorldGeneration.Utils.SquareTileFrame(pyX, pyY - 1, resetSlope: true);
            }
            pstep--;
        }

        // make the main box
        for (int i = x - wide + 1; i < x + wide + 1; i++)
        {
            for (int j = y; j < y + heightOfBox + high; j++)
            {
                Main.tile[i, j].Active(true);
                Main.tile[i, j].TileType = brick;
                WorldGeneration.Utils.SquareTileFrame(i, j, resetSlope: true);
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
        for (int i = x - wallWidth ; i < x + wallWidth; i++)
        {
            for (int j = y + 1; j < y + wallHeightOfBox + high - 1; j++)
            {
                Main.tile[i, j].WallType = brickWall;
                WorldGen.SquareWallFrame(i, j);
            }
        }

        #endregion

        int xStartTunnel = x + (WorldGen.genRand.NextBool(2) ? wide : -wide);
        int yTunnel = y + high / 2;
        int heightOfStartTunnel = WorldGen.genRand.Next(5, 7);

        Vector2 tunnelEndpoint = new Vector2(x, yTunnel);

        //WorldGeneration.Structures.Hellcastle.MakeTunnel()
    }
    public static void CreateLeafTrap(int x, int y)
    {
        ushort loam = (ushort)ModContent.TileType<Loam>();
        ushort grass = (ushort)ModContent.TileType<TropicalGrass>();
        ushort spike = (ushort)ModContent.TileType<WoodenSpikes>();
        for (int i = x - 2; i <= x + 2; i++)
        {
            for (int j = y - 3; j < y + 6; j++)
            {
                WorldGen.KillTile(i, j, noItem: true);
                Tile t = Main.tile[i, j];
                t.Slope = SlopeType.Solid;
            }
        }
        for (int i = x - 2; i <= x + 2; i++)
        {
            for (int j = y; j < y + 6; j++)
            {
                if (i == x - 2 || i == x + 2)
                {
                    if (j == y)
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = grass;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                    else
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = loam;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
                
                if (j >= y + 4)
                {
                    if (j == y + 4 && i <= x + 1 && i >= x - 1)
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = spike;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                    else
                    {
                        Tile t = Main.tile[i, j];
                        t.TileType = loam;
                        t.HasTile = true;
                        WorldGen.SquareTileFrame(i, j);
                    }
                }
            }
        }
        Place3x4(x, y + 3, (ushort)ModContent.TileType<PlatformLeaf>(), 0);
    }

    public static void Place3x4(int x, int y, ushort type, int style)
    {
        if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
            return;

        bool flag = true;
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 3; j < y + 1; j++)
            {
                if (Main.tile[i, j].HasTile)
                    flag = false;
            }

            if (!WorldGen.SolidTile2(i, y + 1))
                flag = false;
        }

        if (flag)
        {
            int num = 0;
            for (int k = -3; k <= 0; k++)
            {
                short frameY = (short)((3 + k) * 18);
                Tile t = Main.tile[x - 1, y + k];
                Main.tile[x - 1, y + k].Active(true);
                Main.tile[x - 1, y + k].TileFrameY = frameY;
                Main.tile[x - 1, y + k].TileFrameX = (short)num;
                Main.tile[x - 1, y + k].TileType = type;
                Main.tile[x, y + k].Active(true);
                Main.tile[x, y + k].TileFrameY = frameY;
                Main.tile[x, y + k].TileFrameX = (short)(num + 26);
                Main.tile[x, y + k].TileType = type;
                Main.tile[x + 1, y + k].Active(true);
                Main.tile[x + 1, y + k].TileFrameY = frameY;
                Main.tile[x + 1, y + k].TileFrameX = (short)(num + 52);
                Main.tile[x + 1, y + k].TileType = type;
            }
        }
    }


    public static void Crystals(int x, int y)
    {
        int mult = WorldGen.genRand.Next(2, 4);
        int pstep = 0;

        for (int pyY = y; pyY <= y + mult + WorldGen.genRand.Next(1, 6); pyY++)
        {
            for (int pyX = x - pstep + WorldGen.genRand.Next(1, 4); pyX <= x + pstep + WorldGen.genRand.Next(1, 4); pyX++)
            {
                WorldGen.PlaceTile(pyX, pyY, ModContent.TileType<CrystalStoneCrystals>());
            }
            pstep++;
        }
    }







    public static void GrowCloud(int x, int y)
    {
        GrowFragile(x, y, TileID.Cloud, 40);
        for (int i = x; i < x + 40; i++)
        {
            for (int j = y; j < y + 40; j++)
            {
                if (Main.tile[i, j].TileType == TileID.Cloud &&
                    !Main.tile[i, j + 1].HasTile)
                {
                    Main.tile[i, j].TileType = TileID.RainCloud;
                }
            }
        }
    }

    public static void GrowFragile(int x, int y, ushort type, int rounds)
    {
        int growth = WorldGen.genRand.Next(256);

        for (int i = 0; i < 18; i++)
        {
            int j = i % 3;
            int tgro = 1 << i;
            if ((tgro & growth) == tgro)
            {
                int tx = (x + j - 1);
                int ty = y + (i / 3) - 1;

                if (!Main.tile[tx, ty].HasTile)
                {
                    Tile t = Main.tile[tx, ty];
                    t.HasTile = true;
                    t.TileType = type;
                    WorldGen.SquareTileFrame(tx, ty);
                    if (rounds > 0)
                    {
                        GrowFragile(tx, ty, type, rounds - 1);
                    }
                }
            }
        }
        WorldGen.SquareTileFrame(x, y);
    }


    //public static void GetXCoord(int x, int y, int ylength, ref int xCoord)
    //{
    //    bool leftSideActive = false;
    //    bool rightSideActive = false;
    //    for (int i = y; i < y + ylength; i++)
    //    {
    //        if (Main.tile[x, i].HasTile || Main.tile[x, i].LiquidAmount > 0 || Main.tile[x, i].WallType > 0)
    //        {
    //            leftSideActive = true;
    //            break;
    //        }
    //    }
    //    for (int i = y; i < y + ylength; i++)
    //    {
    //        if (Main.tile[x + 4, i].HasTile || Main.tile[x + 4, i].LiquidAmount > 0 || Main.tile[x + 4, i].WallType > 0)
    //        {
    //            rightSideActive = true;
    //            break;
    //        }
    //    }
    //    if (leftSideActive || rightSideActive)
    //    {
    //        xCoord--;
    //        GetXCoord(xCoord, y, ylength, ref xCoord);
    //    }
    //}
    //public static int GetXCoord(int x, int y, ref int xStored)
    //{
    //    if (Main.tile[x, y].HasTile || Main.tile[x, y].liquid > 0 || Main.tile[x, y].WallType > 0)
    //    {
    //        xStored--;
    //        GetXCoord(xStored, y, ref xStored);
    //    }
    //    return xStored;
    //}



    /// <summary>
    /// Makes a spike at the given coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="length">The height/tallness of the spike to generate.</param>
    /// <param name="width">The width/thickness of the spike to generate.</param>
    /// <param name="direction">The vertical direction of the spike; 1 is down, -1 is up.</param>
    public static void MakeSpike(int x, int y, int length, int width, int direction = 1)
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
            WorldGeneration.Utils.MakeCircle2(betweenXPos, betweenYPos, (int)((length - q) * 0.4f), ModContent.TileType<Tiles.CaesiumCrystal>(), ModContent.TileType<Tiles.Ores.CaesiumOre>());

            // Make a square/circle of the tile
            WorldGeneration.Utils.MakeCircle2(x, y, (int)((length - q) * 0.4f), ModContent.TileType<Tiles.CaesiumCrystal>(), ModContent.TileType<Tiles.Ores.CaesiumOre>());

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
