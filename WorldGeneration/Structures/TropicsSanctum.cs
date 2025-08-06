using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures
{
    public class TropicsSanctum
    {
        public static void MakeSanctum2(int x, int y)
        {
            ushort t = (ushort)WorldGen.genRand.Next(3);
            if (t == 0)
                t = TileID.IridescentBrick;
            else if (t == 1)
                t = (ushort)ModContent.TileType<Tiles.BismuthBrick>();
            else if (t == 2)
                t = (ushort)ModContent.TileType<Tiles.Savanna.Loamstone>();


        }

        public static void MakeSanctum(int x, int y)
        {
            int[,] _structure = {
                {0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                {0,0,1,1,6,1,1,1,1,1,1,1,1,1,1,6,1,1,0,0},
                {0,1,1,6,1,2,2,2,2,2,2,2,2,2,2,1,6,1,1,0},
                {1,1,6,1,2,2,2,2,2,2,2,2,2,2,2,2,1,6,1,1},
                {1,6,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,6,1},
                {1,6,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,6,1},
                {1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1},
                {1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
                {0,2,2,4,2,2,3,2,2,2,5,2,2,3,2,2,4,2,2,0},
                {0,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2,2,2,2,0},
                {0,2,2,2,1,1,6,1,1,1,1,1,1,6,1,1,2,2,2,0},
                {0,2,1,1,1,6,6,6,1,1,1,1,6,6,6,1,1,1,2,0},
                {1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
            int PosX = x;   //spawnX and spawnY is where you want the anchor to be when this generates
            int PosY = y;
            
            ushort t = (ushort)WorldGen.genRand.Next(3);
            ushort t2 = 0;
            if (t == 0)
            {
                t = TileID.IridescentBrick;
                t2 = (ushort)ModContent.TileType<Tiles.TwiliplateBlock>();
            }
            else if (t == 1)
            {
                t = TileID.PlatinumBrick;
                t2 = (ushort)ModContent.TileType<Tiles.MoonplateBlock>();
            }
            else if (t == 2)
            {
                t = (ushort)ModContent.TileType<Tiles.Savanna.Loamstone>();
                t2 = TileID.Sunplate;
            }
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
                            //if (tile.TileType != (ushort)ModContent.TileType<Tiles.TuhrtlBrick>() || tile.wall != (ushort)ModContent.WallType<Walls.TuhrtlBrickWallUnsafe>()) continue;
                            switch (_structure[i, j])
                            {
                                case 0:
                                    break;
                                case 1:
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                    {
                                        tile.HasTile = true;
                                        tile.TileType = t;
                                        tile.Slope = SlopeType.Solid;
                                        tile.IsHalfBlock = false;
                                    }
                                    break;
                                case 2:
                                    if (confirmPlatforms == 0)
                                    {
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.HasTile = false;
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.IsHalfBlock = false;
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.Slope = SlopeType.Solid;
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.WallType = (ushort)ModContent.WallType<Walls.TropicalGrassWall>();
                                    }
                                    break;
                                case 3:
                                    if (confirmPlatforms == 1)
                                    {
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.HasTile = false;
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.Slope = SlopeType.Solid;
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.IsHalfBlock = false;
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            WorldGen.PlaceTile(k, l, 93, true, true, -1, 0);
                                        if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                            tile.WallType = (ushort)ModContent.WallType<Walls.TropicalGrassWall>();
                                    }
                                    break;
                                case 4:
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.WallType = (ushort)ModContent.WallType<Walls.TropicalGrassWall>();
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.HasTile = true;
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.TileType = 4;
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.Slope = SlopeType.Solid;
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.IsHalfBlock = false;
                                    break;
                                case 5:
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.HasTile = false;
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.Slope = SlopeType.Solid;
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.IsHalfBlock = false;
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>() && confirmPlatforms == 1)
                                        WorldGen.AddBuriedChest(k, l, contain: GenSystem.GetNextSavannaChestItem(), Style: 0, chestTileType: (ushort)ModContent.TileType<Tiles.PlatinumChest>());
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                        tile.WallType = (ushort)ModContent.WallType<Walls.TropicalGrassWall>();
                                    break;
                                case 6:
                                    if (tile.TileType != (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
                                    {
                                        tile.HasTile = true;
                                        tile.TileType = t2;
                                        tile.Slope = SlopeType.Solid;
                                        tile.IsHalfBlock = false;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
