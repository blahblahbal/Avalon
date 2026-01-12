using Avalon.Tiles.Bricks;
using Avalon.Tiles.Furniture.Functional;
using Avalon.Tiles.Savanna;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.WorldGeneration.Structures
{
    public class SavannaSanctum
    {
        public static void MakeSanctum2(int x, int y)
        {
            ushort t = (ushort)WorldGen.genRand.Next(3);
            if (t == 0)
                t = TileID.IridescentBrick;
            else if (t == 1)
                t = (ushort)ModContent.TileType<BismuthBrick>();
            else if (t == 2)
                t = (ushort)ModContent.TileType<Loamstone>();


        }

        public static void MakeSanctum(int x, int y)
        {
            int[,] _structure = {
				{ 0,0,0,1,1,1,1,0,0,0 },
				{ 0,1,1,2,1,1,2,1,1,0 },
				{ 1,1,2,2,2,2,2,2,1,1 },
				{ 1,2,2,2,2,2,2,2,2,1 },
				{ 1,2,1,1,1,1,1,1,2,1 },
				{ 1,1,5,5,5,5,5,5,1,1 },
				{ 0,5,3,5,5,5,5,3,5,0 },
				{ 0,5,5,5,5,5,5,5,5,0 },
				{ 0,5,5,5,5,5,5,5,5,0 },
				{ 0,5,5,5,5,5,5,5,5,0 },
				{ 1,1,5,5,5,4,5,5,1,1 },
				{ 1,2,1,1,1,1,1,1,2,1 },
				{ 1,2,2,2,2,2,2,2,2,1 },
				{ 1,1,1,1,1,1,1,1,1,1 }
                //{0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
                //{0,0,1,1,6,1,1,1,1,1,1,1,1,1,1,6,1,1,0,0},
                //{0,1,1,6,1,2,2,2,2,2,2,2,2,2,2,1,6,1,1,0},
                //{1,1,6,1,2,2,2,2,2,2,2,2,2,2,2,2,1,6,1,1},
                //{1,6,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,6,1},
                //{1,6,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,6,1},
                //{1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1},
                //{1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1},
                //{0,2,2,4,2,2,3,2,2,2,5,2,2,3,2,2,4,2,2,0},
                //{0,2,2,2,2,1,1,1,1,1,1,1,1,1,1,2,2,2,2,0},
                //{0,2,2,2,1,1,6,1,1,1,1,1,1,6,1,1,2,2,2,0},
                //{0,2,1,1,1,6,6,6,1,1,1,1,6,6,6,1,1,1,2,0},
                //{1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1},
                //{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };
            int PosX = x;   //spawnX and spawnY is where you want the anchor to be when this generates
            int PosY = y;
            
            ushort t = (ushort)WorldGen.genRand.Next(3);
            ushort t2 = 0;
			ushort wallType = 0;
            if (t == 0)
            {
                t = TileID.IridescentBrick;
                t2 = (ushort)ModContent.TileType<Tiles.TwiliplateBlock>();
				wallType = WallID.IridescentBrick;
            }
            else if (t == 1)
            {
                t = TileID.PlatinumBrick;
                t2 = TileID.Sunplate;
				wallType = WallID.PlatinumBrick;
			}
            else if (t == 2)
            {
                t = (ushort)ModContent.TileType<Loamstone>();
                t2 = (ushort)ModContent.TileType<MoonplateBlock>();
				wallType = (ushort)ModContent.WallType<Walls.MoonWall>();
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
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
                                    {
                                        tile.HasTile = true;
                                        tile.TileType = t;
                                        tile.Slope = SlopeType.Solid;
                                        tile.IsHalfBlock = false;
                                    }
                                    break;
                                case 5:
									if (confirmPlatforms == 0)
									{
										if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
										{
											tile.HasTile = false;
											tile.IsHalfBlock = false;
											tile.Slope = SlopeType.Solid;
											tile.WallType = wallType;
										}
									}
                                    break;
								case 3:
									if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
									{
										tile.HasTile = false;
										tile.IsHalfBlock = false;
										tile.Slope = SlopeType.Solid;
										tile.WallType = wallType;
										WorldGen.PlaceTile(k, l, ModContent.TileType<SavannaTorch>());
									}
									break;
                                case 4:
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
                                        tile.HasTile = false;
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
                                        tile.Slope = SlopeType.Solid;
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
                                        tile.IsHalfBlock = false;
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>() && confirmPlatforms == 1)
                                        WorldGen.AddBuriedChest(k, l, contain: GenSystem.GetNextSavannaChestItem(), Style: 0, chestTileType: (ushort)ModContent.TileType<PlatinumChest>());
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
                                        tile.WallType = wallType;
                                    break;
                                case 2:
                                    if (tile.TileType != (ushort)ModContent.TileType<TuhrtlBrick>())
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
