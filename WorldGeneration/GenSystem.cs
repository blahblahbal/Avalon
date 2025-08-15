using Avalon.Common;
using Avalon.ModSupport;
using Avalon.World.Passes;
using Avalon.WorldGeneration.Enums;
using Avalon.WorldGeneration.Passes;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration;

public class GenSystem : ModSystem
{
    public static int HellfireItemCount;
	public static int HellfireItemResult;
	public static int SavannaItemCount;

	public static int CaesiumSide;

    public override void PostWorldGen()
    {
        AvalonWorld.JungleLocationX = GenVars.JungleX;
    }
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
		if (!AvalonWorld.retroWorld || (WorldGen.drunkWorldGen && WorldGen.getGoodWorldGen && WorldGen.noTrapsWorldGen))
		{
			GenPass currentPass;

			int index = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));
			if (index != -1)
			{
				tasks.Insert(index + 1, new AvalonReset("Reset", 1000f));
			}


			//index = tasks.FindIndex(genpass => genpass.Name.Equals("Generate Ice Biome"));
			//if (index != -1)
			//{
			//    tasks.Insert(index + 1, new SkyClouds());
			//}

			int evil = WorldGen.WorldGenParam_Evil;
			if (!AltLibrarySupport.Enabled)
			{
				if (evil == (int)WorldEvil.Contagion && !Main.zenithWorld)
				{
					index = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
					if (index != -1)
					{
						// Replace corruption task with contagion task
						tasks[index] = new Contagion("Corruption", 80f); //DONT RENAME THE PASS YOU'RE REPLACING BRUH
					}
					index = tasks.FindIndex(genpass => genpass.Name.Equals("Altars"));
					if (index != -1)
					{
						tasks[index] = new IckyAltars();
					}
				}

				if (ModContent.GetInstance<AvalonWorld>().WorldJungle == WorldJungle.Savanna)
				{
					int jungleIndex = tasks.FindIndex(i => i.Name.Equals("Wet Jungle"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Wet Jungle", new WorldGenLegacyMethod(Savanna.JunglesWetTask));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Ice"));
					if (jungleIndex != -1)
					{
						tasks.Insert(jungleIndex + 1, new PassLegacy("Tuhrtl Brick Unsolid", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration config)
						{
							Main.tileSolid[ModContent.TileType<Tiles.Savanna.TuhrtlBrick>()] = false;
							Main.tileSolid[ModContent.TileType<Tiles.Savanna.BrambleSpikes>()] = false;
						})));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Mud Caves To Grass"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Mud Caves To Grass", new WorldGenLegacyMethod(Savanna.JunglesGrassTask));
						tasks.Insert(jungleIndex, new PassLegacy("Loam", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration configuration)
						{
							int tile = ModContent.TileType<Tiles.Savanna.Loam>();
							for (int i = 0; i < Main.maxTilesX; i++)
							{
								for (int j = 0; j < Main.maxTilesY; j++)
								{
									if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == TileID.Mud)
									{
										Main.tile[i, j].TileType = (ushort)tile;
									}
								}
							}
						})));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Webs And Honey"));
					if (jungleIndex != -1)
					{
						tasks.Insert(jungleIndex + 1, new PassLegacy("Replacing lava in nests with blood", new WorldGenLegacyMethod(Savanna.ReplaceLavaWithBlood)));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Jungle Temple"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Jungle Temple", new WorldGenLegacyMethod(Savanna.TuhrtlOutpostTask));
						tasks.Insert(jungleIndex + 1, new PassLegacy("Outpost Traps", new WorldGenLegacyMethod(Savanna.TuhrtlOutpostReplaceTraps)));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Hives"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Hives", new WorldGenLegacyMethod(Savanna.WaspNests));
					}

					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Jungle Chests"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Jungle Chests", new WorldGenLegacyMethod(Savanna.SavannaSanctumTask));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Muds Walls In Jungle"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Muds Walls In Jungle", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration passConfig)
						{
							progress.Set(1.0);
							int num171 = 0;
							int num172 = 0;
							bool flag4 = false;
							for (int num173 = 5; num173 < Main.maxTilesX - 5; num173++)
							{
								for (int num174 = 0; num174 < Main.worldSurface + 20.0; num174++)
								{
									if (Main.tile[num173, num174].HasTile && Main.tile[num173, num174].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>())
									{
										num171 = num173;
										flag4 = true;
										break;
									}
								}

								if (flag4)
									break;
							}

							flag4 = false;
							for (int num175 = Main.maxTilesX - 5; num175 > 5; num175--)
							{
								for (int num176 = 0; num176 < Main.worldSurface + 20.0; num176++)
								{
									if (Main.tile[num175, num176].HasTile && Main.tile[num175, num176].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>())
									{
										num172 = num175;
										flag4 = true;
										break;
									}
								}

								if (flag4)
									break;
							}
							GenVars.jungleMinX = num171;
							GenVars.jungleMaxX = num172;
							for (int num177 = num171; num177 <= num172; num177++)
							{
								for (int num178 = 0; (double)num178 < Main.maxTilesY - 200; num178++)
								{
									if (((num177 >= num171 + 2 && num177 <= num172 - 2) || !WorldGen.genRand.NextBool(2)) &&
										((num177 >= num171 + 3 && num177 <= num172 - 3) || !WorldGen.genRand.NextBool(3)) &&
										(Main.tile[num177, num178].WallType == WallID.DirtUnsafe || Main.tile[num177, num178].WallType == WallID.Cave6Unsafe ||
										Main.tile[num177, num178].WallType == WallID.MudUnsafe))
									{
										Main.tile[num177, num178].WallType = (ushort)ModContent.WallType<Walls.LoamWall>();
									}
								}
							}
							for (int q = GenVars.jungleMinX; q <= GenVars.jungleMaxX; q++)
							{
								for (int z = 0; (double)z < Main.maxTilesY - 200; z++)
								{
									if ((q < GenVars.jungleMinX + 75 && q >= GenVars.jungleMinX + 50) ||
										(q > GenVars.jungleMaxX - 75 && q <= GenVars.jungleMaxX - 50) &&
										z < Main.rockLayer && z > 250)
									{
										if (Main.tile[q, z].HasTile && WorldGen.genRand.NextBool(10))
										{
											if (Main.tile[q, z].TileType == TileID.Grass)
											{
												Main.tile[q, z].TileType = (ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>();
											}
											if (Main.tile[q, z].TileType == TileID.Dirt)
											{
												Main.tile[q, z].TileType = (ushort)ModContent.TileType<Tiles.Savanna.Loam>();
											}
										}
									}

									if (q >= GenVars.jungleMinX + 75 && q <= GenVars.jungleMaxX - 75 && z < Main.rockLayer && z > 250)
									{
										if (Main.tile[q, z].HasTile)
										{
											if (Main.tile[q, z].TileType == TileID.Grass)
											{
												Main.tile[q, z].TileType = (ushort)ModContent.TileType<Tiles.Savanna.SavannaGrass>();
											}
											if (Main.tile[q, z].TileType == TileID.Dirt)
											{
												Main.tile[q, z].TileType = (ushort)ModContent.TileType<Tiles.Savanna.Loam>();
											}
											if (Main.tile[q, z].TileType == TileID.Plants)
											{
												Main.tile[q, z].TileType = (ushort)ModContent.TileType<Tiles.Savanna.SavannaShortGrass>();
											}
											if (Main.tile[q, z].TileType == TileID.Plants2)
											{
												Main.tile[q, z].TileType = (ushort)ModContent.TileType<Tiles.Savanna.SavannaLongGrass>();
											}
										}
									}
								}
							}
						}));
					}

					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Temple"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Temple", new WorldGenLegacyMethod(Savanna.LihzahrdBrickReSolidTask));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Glowing Mushrooms and Jungle Plants"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Glowing Mushrooms and Jungle Plants", new WorldGenLegacyMethod(Savanna.GlowingMushroomsandJunglePlantsTask));
					}
					jungleIndex = tasks.FindIndex(i => i.Name.Equals("Jungle Plants"));
					if (jungleIndex != -1)
					{
						tasks[jungleIndex] = new PassLegacy("Jungle Plants", new WorldGenLegacyMethod(Savanna.JungleBushesTask));
					}
				}
			}
			else
			{
				AltLibrarySupport.UpdateBiomeFields();
				evil = (int)ModContent.GetInstance<AvalonWorld>().WorldEvil;
			}

			index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

			if (index != -1)
			{
				tasks.Insert(index + 1, new OreGenPreHardmode("Adding Avalon Ores", 237.4298f));
			}

			index = tasks.FindIndex(genpass => genpass.Name.Equals("Weeds"));
			if (index != -1)
			{
				tasks.Insert(index + 1, new ShortGrass("Contagion Weeds", 50f));
				tasks.Insert(index + 2, new ReplaceChestItems());
			}

			//int iceWalls = tasks.FindIndex(genPass => genPass.Name == "Cave Walls");
			//if (iceWalls != -1)
			//{
			//    //currentPass = new Shrines();
			//    //tasks.Insert(iceWalls + 1, currentPass);
			//    //totalWeight += currentPass.Weight;
			//}

			int stalac = tasks.FindIndex(genPass => genPass.Name == "Remove Broken Traps");
			if (stalac != -1)
			{
				currentPass = new GemTreePass();
				tasks.Insert(stalac + 2, currentPass);
				totalWeight += currentPass.Weight;

				currentPass = new GemStashes();
				tasks.Insert(stalac + 2, currentPass);
				totalWeight += currentPass.Weight;

				currentPass = new AvalonStalac();
				tasks.Insert(stalac + 1, currentPass);
				totalWeight += currentPass.Weight;

				currentPass = new Shrines();
				tasks.Insert(stalac + 1, currentPass);
				totalWeight += currentPass.Weight;
			}
			int dungeon = tasks.FindIndex(genPass => genPass.Name == "Dungeon");
			if (dungeon != -1)
			{
				currentPass = new MoreDungeonChests();
				tasks.Insert(dungeon + 1, currentPass);
				totalWeight += currentPass.Weight;

				currentPass = new ManaCrystals("Mana Crystals in the Dungeon", 5f);
				tasks.Insert(dungeon + 2, currentPass);
				totalWeight += currentPass.Weight;
			}
			// uncomment when hm update releases
			int underworld = tasks.FindIndex(genPass => genPass.Name == "Micro Biomes");
			if (underworld != -1)
			{
				if (ModContent.GetInstance<AvalonClientConfig>().UnimplementedStructureGen)
				{
					// uncomment when sky fortress becomes a thing
					tasks.Insert(underworld + 4, new SkyFortress());
				}

				currentPass = new Underworld();
				tasks.Insert(underworld + 1, currentPass);
				totalWeight += currentPass.Weight;

				currentPass = new Ectovines();
				tasks.Insert(underworld + 2, currentPass);
				totalWeight += currentPass.Weight;

				currentPass = new ReplacePass("Replacing any improper ores", 25f);
				tasks.Insert(underworld + 1, currentPass);
				totalWeight += currentPass.Weight;
			}

			index = tasks.FindIndex(genPass => genPass.Name == "Slush");
			if (index != -1)
			{
				currentPass = new Hooks.DungeonRemoveCrackedBricks();
				tasks.Insert(index + 1, currentPass);
				totalWeight += currentPass.Weight;
			}

			index = tasks.FindIndex(genPass => genPass.Name == "Vines");
			if (index != -1)
			{
				if (evil == (int)WorldEvil.Contagion && !Main.zenithWorld)
				{
					tasks.Insert(index + 2, new ContagionVines("Contagion Vines", 25f));
				}
				if (ModContent.GetInstance<AvalonWorld>().WorldJungle == WorldJungle.Savanna)
				{
					currentPass = new SavannaVines();
					tasks.Insert(index + 3, currentPass);
					totalWeight += currentPass.Weight;

					currentPass = new PassLegacy("Savanna Traps", new WorldGenLegacyMethod(Savanna.PlatformLeafTrapTask));
					tasks.Insert(index + 4, currentPass);
					totalWeight += currentPass.Weight;
				}
				if (ModContent.GetInstance<AvalonClientConfig>().SuperhardmodeStuff)
				{
					currentPass = new CrystalMinesPass();
					tasks.Insert(index + 4, currentPass);
					totalWeight += currentPass.Weight;
				}
			}
		}
    }

    public static int GetNextHellfireChestItem()
    {
		//int result = ModContent.ItemType<Items.Accessories.PreHardmode.OilBottle>();
		//switch (HellfireItemCount % 2)
		//{
		//	case 0:
		//		result = ModContent.ItemType<Items.Accessories.PreHardmode.OilBottle>();
		//		break;
		//	case 1:
		//		result = ModContent.ItemType<Items.Tools.PreHardmode.EruptionHook>();
		//		break;
		//}

		//HellfireItemCount++;
		//return result;
		List<int> items = new()
		{
			ModContent.ItemType<Items.Accessories.PreHardmode.OilBottle>(),
			ModContent.ItemType<Items.Tools.PreHardmode.EruptionHook>(),
			ModContent.ItemType<Items.Placeable.Furniture.BasaltObelisk>(),
		};
		if (HellfireItemCount % 2 == 0)
		{
			HellfireItemResult = WorldGen.genRand.Next(items.Count);
			HellfireItemCount++;
			return items[HellfireItemResult];
		}
		else
		{
			List<int> excludeFirstItem = items;
			excludeFirstItem.Remove(excludeFirstItem[HellfireItemResult]);
			int result2 = WorldGen.genRand.Next(excludeFirstItem.Count);
			HellfireItemCount++;
			return excludeFirstItem[result2];
		}
	}

	public static int GetNextSavannaChestItem()
    {
        int result = ModContent.ItemType<Items.Accessories.PreHardmode.RubberGloves>();
        switch (SavannaItemCount % 4)
        {
            case 0:
                result = ModContent.ItemType<Items.Accessories.PreHardmode.RubberGloves>();
                break;
            case 1:
                result = ModContent.ItemType<Items.Weapons.Ranged.PreHardmode.Thompson>();
                break;
            case 2:
                result = ModContent.ItemType<Items.Accessories.PreHardmode.AnkletofAcceleration>();
                break;
            case 3:
                result = ItemID.StaffofRegrowth;
                break;
        }

        if (WorldGen.genRand.NextBool(50))
            result = ItemID.Seaweed;
        else if (WorldGen.genRand.NextBool(15))
            result = ItemID.FiberglassFishingPole;
        else if (WorldGen.genRand.NextBool(20))
            result = ItemID.FlowerBoots;

        SavannaItemCount++;
        return result;
    }
}
