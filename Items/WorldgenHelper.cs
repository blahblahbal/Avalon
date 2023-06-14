using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

class WorldgenHelper : ModItem
{
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

    public override bool? UseItem(Player player)
    {
        int x = (int)player.position.X / 16;
        int y = (int)player.position.Y / 16;

        //WorldGeneration.Structures.Hellcastle.GenerateHellcastle(x, y);
        //y = WorldGeneration.Utils.CaesiumTileCheck(x, y);
        //MakeSpike(x, y, 20, 10, 1);
        WorldGeneration.Passes.Contagion.ContagionRunner3(x, y);

        return true;
        //int xStored = x;
        //List<int> l = new List<int>()
        //{
        //    ModContent.TileType<Tiles.Nest>(),
        //    ModContent.TileType<Tiles.Loamstone>(),
        //    ModContent.TileType<Tiles.BismuthBrick>(),
        //    TileID.IridescentBrick,
        //};
        //World.Utils.GetSkyFortressXCoord(x, y, 5, 5, ref xStored);
        //World.Utils.GetXCoordGeneric(x, y, 5, 5, ref xStored, l, true, true);
        //World.Utils.MakeSquareTemp(x, y);
        //Main.hardMode = false;
        //World.Passes.HallowedAltars.Generate();
        //NPC.SetEventFlagCleared(ref ModContent.GetInstance<DownedBossSystem>().DownedArmageddon, -1);
        //Task.Run(AvalonWorld.GenerateSkyFortress);
        //ModContent.GetInstance<AvalonWorld>().GenerateCrystalMines();
        //World.Tests.MakeZigZag(x, y, TileID.Titanium, WallID.Wood);
        //World.Structures.CaesiumSpike.CreateSpikeUp((int)player.position.X / 16, (int)player.position.Y / 16, (ushort)ModContent.TileType<Tiles.Ores.CaesiumOre>());
        //World.Structures.IceShrine.Generate((int)player.position.X / 16, (int)player.position.Y / 16);
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
