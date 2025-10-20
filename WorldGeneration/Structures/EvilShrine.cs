using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Accessories.Vanity;
using Avalon.Items.Fish;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Ores;
using Avalon.Tiles;
using Avalon.Tiles.Contagion.ChunkstoneBrick;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures;

class EvilShrine
{
    public static bool AddEvilChest(int i, int j, int contain = 0, bool notNearOtherChests = false, int Style = -1)
    {
        //if (WorldGen.genRand == null)
        //    WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
        int k = j;
        while (k < Main.maxTilesY)
        {
            if (Main.tile[i, k].HasTile && Main.tileSolid[(int)Main.tile[i, k].TileType])
            {
                int num = k;
                int num2 = WorldGen.PlaceChest(i - 1, num - 1, (ushort)ModContent.TileType<Tiles.Furniture.Coughwood.CoughwoodChest>(), notNearOtherChests, 0);
                if (num2 >= 0)
                {
                    int num3 = 0;
                    while (num3 == 0)
                    {
                        int rN = WorldGen.genRand.Next(42);
                        if (rN >= 0 && rN <= 20)
                        {
                            int q = WorldGen.genRand.Next(3);
                            if (q == 0) q = ItemID.DemoniteOre;
                            if (q == 1) q = ItemID.CrimtaneOre;
                            if (q == 2) q = ModContent.ItemType<BacciliteOre>();
                            Main.chest[num2].item[0].SetDefaults(q, false);
                            Main.chest[num2].item[0].stack = WorldGen.genRand.Next(41, 68);
                        }
                        else if (rN >= 21 && rN <= 41)
                        {
                            int q = WorldGen.genRand.Next(3);
                            if (q == 0) q = ItemID.DemoniteBar;
                            if (q == 1) q = ItemID.CrimtaneBar;
                            if (q == 2) q = ModContent.ItemType<BacciliteBar>();
                            Main.chest[num2].item[0].SetDefaults(q, false);
                            Main.chest[num2].item[0].stack = WorldGen.genRand.Next(2, 7);
                        }
                        int rand = WorldGen.genRand.Next(51);
                        if (rand >= 0 && rand <= 20)
                        {
                            Main.chest[num2].item[1].SetDefaults(ItemID.MusketBall, false);
                            Main.chest[num2].item[1].stack = WorldGen.genRand.Next(50, 101);
                        }
                        else if (rand >= 21 && rand <= 30)
                        {
                            Main.chest[num2].item[1].SetDefaults(ItemID.PoisonedKnife, false);
                            Main.chest[num2].item[1].stack = WorldGen.genRand.Next(77, 125);
                        }
                        else if (rand >= 31 && rand <= 40)
                        {
                            int q = WorldGen.genRand.Next(3);
                            if (q == 0) q = ItemID.Ebonkoi;
                            if (q == 1) q = ItemID.Hemopiranha;
                            if (q == 2) q = ModContent.ItemType<Ickfish>();
                            Main.chest[num2].item[1].SetDefaults(q, false);
                            Main.chest[num2].item[1].stack = WorldGen.genRand.Next(4, 8);
                        }
                        else if (rand >= 41 && rand <= 50)
                        {
                            Main.chest[num2].item[1].SetDefaults(ItemID.RecallPotion, false);
                            Main.chest[num2].item[1].stack = WorldGen.genRand.Next(3) + 1;
                        }
                        int rand2 = WorldGen.genRand.Next(27);
                        if (rand2 >= 0 && rand2 <= 20)
                        {
                            Main.chest[num2].item[2].SetDefaults(73, false);
                            Main.chest[num2].item[2].stack = WorldGen.genRand.Next(5, 11);
                        }
                        else if (rand2 >= 21 && rand2 <= 26)
                        {
                            int q = WorldGen.genRand.Next(3);
                            if (q == 0) q = ItemID.BandofStarpower;
                            if (q == 1) q = ItemID.PanicNecklace;
                            if (q == 2) q = ModContent.ItemType<NerveNumbNecklace>();
                            Main.chest[num2].item[2].SetDefaults(q, false);
                            Main.chest[num2].item[2].Prefix(-1);
                        }
                        int rand3 = WorldGen.genRand.Next(27);
                        if (rand3 >= 0 && rand2 <= 25)
                        {
                            int q = WorldGen.genRand.Next(3);
                            if (q == 0) q = ModContent.ItemType<BagofShadows>();
                            if (q == 1) q = ModContent.ItemType<BagofBlood>();
                            if (q == 2) q = ModContent.ItemType<BagofIck>();
                            Main.chest[num2].item[3].SetDefaults(q, false);
                        }
                        else if (rand3 == 26)
                        {
                            Main.chest[num2].item[3].SetDefaults(73, false);
                            Main.chest[num2].item[3].stack = WorldGen.genRand.Next(7, 14);
                        }
                        num3++;
                    }
                    return true;
                }
                return false;
            }
            else k++;
        }
        return false;
    }

    public static void GenerateEvilShrine(int x, int y)
    {
        int[,] _structure = {
                {0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,0,0},
                {0,0,0,0,1,1,0,1,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,1,0,1,1,0,0,0,0},
                {0,0,0,0,1,1,1,1,1,1,1,1,1,2,0,2,0,0,0,0,0,0,0,0,0,2,0,2,1,1,1,1,1,1,1,1,1,0,0,0,0},
                {0,0,0,0,1,1,1,3,3,3,1,1,1,2,2,2,2,0,0,0,0,0,0,0,2,2,2,2,1,1,1,3,3,3,1,1,1,0,0,0,0},
                {0,0,0,0,0,1,1,1,1,1,1,1,2,4,4,4,2,2,0,2,0,2,0,2,2,4,4,4,2,1,1,1,1,1,1,1,0,0,0,0,0},
                {0,0,0,0,0,5,1,1,1,1,1,5,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,5,1,1,1,1,1,5,0,0,0,0,0},
                {0,0,0,0,0,5,0,6,6,6,7,5,7,7,7,2,2,2,2,2,2,2,2,2,2,2,7,7,7,5,7,6,6,6,0,5,0,0,0,0,0},
                {0,0,0,0,0,5,6,6,6,7,7,5,7,7,7,7,2,2,2,7,7,7,2,2,2,7,7,7,7,5,7,7,6,6,6,5,0,0,0,0,0},
                {0,0,0,0,0,6,6,6,7,7,7,5,7,7,7,7,2,2,7,7,7,7,7,2,2,7,7,7,7,5,7,7,7,6,6,6,0,0,0,0,0},
                {0,0,0,0,6,6,6,7,7,7,7,5,7,7,7,7,8,7,7,7,7,7,7,7,8,7,7,7,7,5,7,7,7,7,6,6,6,0,0,0,0},
                {0,0,6,6,6,6,7,7,7,7,7,5,7,7,7,7,8,9,7,7,7,7,7,9,8,7,7,7,7,5,7,7,7,7,7,6,6,6,6,0,0},
                {0,6,6,6,6,10,7,7,7,7,7,5,7,7,7,7,8,7,7,7,7,7,7,7,8,7,7,7,7,5,7,7,7,7,7,10,6,6,6,6,0},
                {6,6,6,7,7,10,7,7,7,7,7,5,7,12,12,7,8,7,7,7,7,7,7,7,8,7,12,12,7,5,7,7,7,7,7,10,7,7,6,6,6},
                {6,6,7,7,7,10,7,7,7,7,7,5,7,11,12,6,6,6,6,6,6,6,6,6,6,6,11,12,7,5,7,7,7,7,7,10,7,7,7,6,6},
                {6,6,7,7,7,10,7,7,7,7,7,5,7,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,7,5,7,7,7,7,7,10,7,7,7,6,6},
                {7,12,7,7,9,10,9,7,7,7,7,5,6,6,6,6,14,14,14,6,10,6,14,14,14,6,6,6,6,5,7,7,7,7,9,10,9,7,7,12,7},
                {7,12,7,7,7,10,7,7,7,7,7,6,6,6,10,6,6,6,6,6,10,6,6,6,6,6,10,6,6,6,7,7,7,7,7,10,7,7,7,12,7},
                {7,13,7,7,7,10,7,7,7,7,6,6,6,7,10,7,7,7,7,7,10,7,7,7,7,7,10,7,6,6,6,7,7,7,7,10,7,7,7,13,7},
                {6,6,7,7,7,10,7,7,7,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,7,7,7,10,7,7,7,6,6},
                {6,6,6,6,7,10,7,6,6,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,6,6,6,7,10,7,6,6,6,6},
                {0,6,6,6,6,6,6,6,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,6,6,6,6,6,6,6,6,0},
                {0,0,0,6,6,6,6,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,6,6,6,6,0,0,0}
            };

        int[,] walls = {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,1,1,1,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0},
                {0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0},
                {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                {0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
                {0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0},
                {0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,0,1,0,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0},
                {0,0,1,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,1,1,1,1,1,1,1,1,0,0},
                {0,0,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1,1,1,1,1,0,0},
                {0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0},
                {0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };
        int PosX = x;
        int PosY = y;
        //i = vertical, j = horizontal
        for (int confirmPlatforms = 0; confirmPlatforms < 2; confirmPlatforms++)    //Increase the iterations on this outermost for loop if tabletop-objects are not properly spawning
        {
            for (int i = 0; i < _structure.GetLength(0); i++)
            {
                for (int j = _structure.GetLength(1) - 1; j >= 0; j--)
                {
                    int k = PosX + j;
                    int l = PosY + i;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_structure[i, j])
                        {
                            case 0:
                                break;
                            case 1:
                                tile.HasTile = true;
                                tile.TileType = TileID.CrimstoneBrick;
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 2:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<ChunkstoneBrickTile>();
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 3:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.Ores.BacciliteOre>();
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 4:
                                tile.HasTile = true;
                                tile.TileType = TileID.Demonite;
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 5:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.CrimstoneColumn>();
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 6:
                                tile.HasTile = true;
                                tile.TileType = TileID.EbonstoneBrick;
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 7:
                                tile.HasTile = false;
                                break;
                            case 8:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.ChunkstoneColumn>();
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 9:
                                if (confirmPlatforms == 0)
                                    tile.HasTile = false;
                                WorldGen.PlaceTile(k, l, TileID.Torches, true, true, -1, 13);
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 10:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.EbonstoneColumn>();
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                            case 11:
                                if (confirmPlatforms == 1)
                                {
                                    tile.HasTile = false;
                                    tile.Slope = 0;
                                    tile.IsHalfBlock = false;
                                    AddEvilChest(k + 1, l);
                                }
                                break;
                            case 12:
                                if (confirmPlatforms == 0)
                                {
                                    tile.HasTile = false;
                                    tile.IsHalfBlock = false;
                                    tile.Slope = 0;
                                }
                                break;
                            case 13:
                                if (confirmPlatforms == 1)
                                {
                                    tile.HasTile = false;
                                    tile.Slope = 0;
                                    tile.IsHalfBlock = false;
                                    WorldGen.PlaceTile(k, l, 10, true, true, -1, 8);
                                }
                                break;
                            case 14:
                                tile.HasTile = true;
                                tile.TileType = TileID.Crimtane;
                                tile.Slope = 0;
                                tile.IsHalfBlock = false;
                                break;
                        }
                    }
                }
            }
        }
        //i = vertical, j = horizontal
        for (int i = 0; i < walls.GetLength(0); i++)
        {
            for (int j = walls.GetLength(1) - 1; j >= 0; j--)
            {
                int k = PosX + j;
                int l = PosY + i;
                if (WorldGen.InWorld(k, l, 30))
                {
                    Tile tile = Framing.GetTileSafely(k, l);
                    switch (walls[i, j])
                    {
                        case 0:
                            tile.WallType = 0;
                            break;
                        case 1:
                            tile.WallType = WallID.ObsidianBackEcho;
                            break;
                    }
                }
            }
        }

    }

    public static void GenerateEvilShrineOld(int x, int y)
    {
        int[,] _structure = {
            {0,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,0},
            {0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,1,1,1,1,2,2,2,1,1,1,1,3,3,3,3,3,3,3,3,3,1,1,1,1,2,2,2,1,1,1,1,0,0},
            {0,0,0,1,1,1,1,1,1,1,15,15,15,3,3,3,4,4,4,3,3,3,15,15,15,1,1,1,1,1,1,1,0,0,0},
            {0,0,0,0,1,1,1,1,1,1,1,3,3,3,3,3,4,4,4,3,3,3,3,3,1,1,1,1,1,1,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,3,3,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,3,3,0,0,0,0,0,0,0},
            {0,0,0,0,0,3,3,3,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,3,3,3,0,0,0,0,0},
            {0,0,0,3,3,3,6,6,6,5,6,6,6,6,7,6,6,8,6,6,7,6,6,6,6,5,6,6,6,3,3,3,0,0,0},
            {0,0,3,3,9,9,6,6,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,6,6,9,9,3,3,0,0},
            {3,3,3,9,9,9,8,6,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,6,8,9,9,9,3,3,3},
            {3,9,9,9,9,9,6,6,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,6,6,9,9,9,9,9,3},
            {3,9,9,9,9,9,10,10,10,5,10,10,10,10,7,10,10,10,10,10,7,10,10,10,10,5,10,10,10,9,9,9,9,9,3},
            {3,9,9,9,9,9,6,6,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,6,6,9,9,9,9,9,3},
            {3,6,6,11,6,6,6,6,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,6,6,6,6,11,6,6,3},
            {6,6,6,11,6,6,6,6,6,5,6,6,6,6,7,6,6,6,6,6,7,6,6,6,6,5,6,6,6,6,6,11,6,6,6},
            {6,6,6,11,6,6,6,6,6,5,6,6,12,6,7,6,6,6,6,6,7,6,12,6,6,5,6,6,6,6,6,11,6,6,6},
            {13,6,6,11,6,6,6,6,6,5,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,5,6,6,6,6,6,11,6,6,13},
            {3,3,3,3,6,14,6,6,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,6,14,6,6,3,3,3,3},
            {0,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,0,0,0}
        };
        int PosX = x;   //spawnX and spawnY is where you want the anchor to be when this generates
        int PosY = y;
        //i = vertical, j = horizontal
        for (int confirmPlatforms = 0; confirmPlatforms < 2; confirmPlatforms++)    //Increase the iterations on this outermost for loop if tabletop-objects are not properly spawning
        {
            for (int i = 0; i < _structure.GetLength(0); i++)
            {
                for (int j = _structure.GetLength(1) - 1; j >= 0; j--)
                {
                    int k = PosX + j;
                    int l = PosY + i;
                    if (WorldGen.InWorld(k, l, 30))
                    {
                        Tile tile = Framing.GetTileSafely(k, l);
                        switch (_structure[i, j])
                        {
                            case 0:
                                break;
                            case 1:
                                tile.HasTile = true;
                                tile.TileType = TileID.CrimtaneBrick;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 2:
                                tile.HasTile = true;
                                tile.TileType = TileID.Demonite;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 3:
                                tile.HasTile = true;
                                tile.TileType = TileID.DemoniteBrick;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 4:
                                tile.HasTile = true;
                                tile.TileType = TileID.Crimtane;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 5:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.EbonstoneColumn>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.ObsidianBackEcho;
                                break;
                            case 6:
                                if (confirmPlatforms == 0)
                                {
                                    tile.HasTile = false;
                                    tile.IsHalfBlock = false;
                                    tile.Slope = SlopeType.Solid;
                                    tile.WallType = WallID.ObsidianBackEcho;
                                }
                                break;
                            case 7:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.ChunkstoneColumn>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.ObsidianBackEcho;
                                break;
                            case 8:
                                tile.HasTile = true;
                                tile.TileType = 4;
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.ObsidianBackEcho;
                                break;
                            case 9:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<ChunkstoneBrickTile>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                            case 10:
                                if (confirmPlatforms == 0)
                                    tile.HasTile = false;
                                WorldGen.PlaceTile(k, l, 19, true, true, -1, 9);
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.ObsidianBackEcho;
                                break;
                            case 11:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.CrimstoneColumn>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                tile.WallType = WallID.ObsidianBackEcho;
                                break;
                            case 12:
                                if (confirmPlatforms == 1)
                                {
                                    tile.HasTile = false;
                                    tile.Slope = SlopeType.Solid;
                                    tile.IsHalfBlock = false;
                                    WorldGen.PlaceTile(k, l, 93, true, true, -1, 10);
                                    tile.WallType = WallID.ObsidianBackEcho;
                                }
                                break;
                            case 13:
                                if (confirmPlatforms == 1)
                                {
                                    tile.HasTile = false;
                                    tile.Slope = SlopeType.Solid;
                                    tile.IsHalfBlock = false;
                                    WorldGen.PlaceTile(k, l, 10, true, true, -1, 0);
                                }
                                break;
                            case 14: // chest placement
                                if (confirmPlatforms == 1)
                                {
                                    tile.HasTile = false;
                                    tile.Slope = SlopeType.Solid;
                                    tile.IsHalfBlock = false;
                                    tile.WallType = WallID.ObsidianBackEcho;
                                    AddEvilChest(k + 1, l);
                                }
                                break;
                            case 15:
                                tile.HasTile = true;
                                tile.TileType = (ushort)ModContent.TileType<Tiles.Ores.BacciliteOre>();
                                tile.Slope = SlopeType.Solid;
                                tile.IsHalfBlock = false;
                                break;
                        }
                    }
                }
            }
        }
    }
}
