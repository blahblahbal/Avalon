using Avalon.Common;
using Avalon.Items.Tools.PreHardmode;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Avalon.WorldGeneration.secretSeeds
{
	public class secretSeedSystem : ModSystem
	{
		private static readonly string[] vanillaGenpasses = new string[] { "Reset", "Terrain", "Dunes", "Ocean Sand", "Sand Patches", "Tunnels", "Mount Caves", "Dirt Wall Backgrounds", "Rocks In Dirt", "Dirt In Rocks", "Clay", "Small Holes", "Dirt Layer Caves", "Rock Layer Caves", "Surface Caves",
				"Wavy Caves", "Generate Ice Biome", "Grass", "Jungle", "Mud Caves To Grass", "Full Desert", "Floating Islands", "Mushroom Patches", "Marble", "Granite", "Dirt To Mud", "Silt", "Shinies", "Webs", "Underworld", "Corruption", "Lakes", "Dungeon", "Slush", "Mountain Caves",
				"Beaches", "Gems", "Gravitating Sand", "Create Ocean Caves", "Shimmer", "Clean Up Dirt", "Pyramids", "Dirt Rock Wall Runner", "Living Trees", "Wood Tree Walls", "Altars", "Wet Jungle", "Jungle Temple", "Hives", "Jungle Chests", "Settle Liquids", "Remove Water From Sand",
				"Oasis", "Shell Piles", "Smooth World", "Waterfalls", "Ice", "Wall Variety", "Life Crystals", "Statues", "Buried Chests", "Surface Chests", "Jungle Chests Placement", "Water Chests", "Spider Caves", "Gem Caves", "Moss", "Temple", "Cave Walls", "Jungle Trees",
				"Floating Island Houses", "Quick Cleanup", "Pots", "Hellforge", "Spreading Grass", "Surface Ore and Stone", "Place Fallen Log", "Traps", "Piles", "Spawn Point", "Grass Wall", "Guide", "Sunflowers", "Planting Trees", "Herbs", "Dye Plants", "Webs And Honey", "Weeds",
				"Glowing Mushrooms and Jungle Plants", "Jungle Plants", "Vines", "Flowers", "Mushrooms", "Gems In Ice Biome", "Random Gems", "Moss Grass", "Muds Walls In Jungle", "Larva", "Settle Liquids Again", "Cactus, Palm Trees, & Coral", "Tile Cleanup", "Lihzahrd Altars", "Micro Biomes",
				"Water Plants", "Stalac", "Remove Broken Traps", "Final Cleanup" };

		public static bool GetRetroWorldGen = false;

		public static bool GetCavesWorldGen = false; //unfinished, will be announced and made known when finished

		public override void Load()
		{
			On_WorldGen.GenerateWorld += CheckSeedNumber;
			On_UIWorldCreation.ProcessSpecialWorldSeeds += SetGetSecretSeed;
		}

		internal void SetGetSecretSeed(On_UIWorldCreation.orig_ProcessSpecialWorldSeeds orig, string processedSeed)
		{
			GetRetroWorldGen = false;
			GetCavesWorldGen = false;
			if (processedSeed.ToLower() == "oh so retro" || processedSeed.ToLower() == "1.1gen")
			{
				GetRetroWorldGen = true;
			}
			if (processedSeed.ToLower() == "oopsallcaves" || processedSeed.ToLower() == "oops all caves" || processedSeed.ToLower() == "oops! all caves" || processedSeed.ToLower() == "oops!allcaves")
			{
				GetCavesWorldGen = true;
			}
			if (processedSeed.ToLower() == "get fixed boi" || processedSeed.ToLower() == "getfixedboi")
			{
				GetRetroWorldGen = true;
				//GetCavesWorldGen = true; //support isnt added yet, once added, uncomment this
			}
			orig.Invoke(processedSeed);
		}

		internal void CheckSeedNumber(On_WorldGen.orig_GenerateWorld orig, int seed, GenerationProgress customProgressObject)
		{
			if (GetRetroWorldGen || seed == 12012011 || seed == 01312024 || seed == 05232012)
			{
				AvalonWorld.retroWorld = true;
				Main.rand = new UnifiedRandom();
				seed = Main.rand.Next(999999999);
			}
			else
			{
				AvalonWorld.retroWorld = false;
			}
			if (GetCavesWorldGen)
			{
				AvalonWorld.cavesWorld = true;
				Main.rand = new UnifiedRandom();
				seed = Main.rand.Next(999999999);
			}
			else
			{
				AvalonWorld.cavesWorld = false;
			}
			orig.Invoke(seed, customProgressObject);
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			if (AvalonWorld.retroWorld)
			{
				if (!(WorldGen.drunkWorldGen && WorldGen.getGoodWorldGen && WorldGen.noTrapsWorldGen)) //is not zenith
				{
					string[] blockedGenpass = new string[32];

					blockedGenpass[0] = "Corruption";
					blockedGenpass[1] = "Lakes";
					blockedGenpass[2] = "Dungeon";
					blockedGenpass[3] = "Settle Liquids";
					blockedGenpass[4] = "Remove Water From Sand";
					blockedGenpass[5] = "Pots";
					blockedGenpass[6] = "Hellforge";
					blockedGenpass[7] = "Spreading Grass";
					blockedGenpass[8] = "Surface Ore and Stone";
					blockedGenpass[9] = "Place Fallen Log";
					blockedGenpass[10] = "Traps";
					blockedGenpass[11] = "Spawn Point";
					blockedGenpass[12] = "Grass Wall";
					blockedGenpass[13] = "Guide";
					blockedGenpass[14] = "Sunflowers";
					blockedGenpass[15] = "Planting Trees";
					blockedGenpass[16] = "Herbs";
					blockedGenpass[17] = "Dye Plants";
					blockedGenpass[18] = "Webs And Honey";
					blockedGenpass[19] = "Weeds";
					blockedGenpass[20] = "Glowing Mushrooms and Jungle Plants";
					blockedGenpass[21] = "Jungle Plants";
					blockedGenpass[22] = "Vines";
					blockedGenpass[23] = "Flowers";
					blockedGenpass[24] = "Mushrooms";
					blockedGenpass[25] = "Gems In Ice Biome";
					blockedGenpass[26] = "Random Gems";
					blockedGenpass[27] = "Moss Grass";
					blockedGenpass[28] = "Muds Walls In Jungle";
					blockedGenpass[29] = "Larva";
					blockedGenpass[30] = "Settle Liquids Again";
					blockedGenpass[31] = "Cactus, Palm Trees, & Coral";

					for (int i = 0; i < vanillaGenpasses.Length; i++)
					{
						string str = vanillaGenpasses[i];
						for (int j = 0; j < blockedGenpass.Length; j++)
						{
							if (str == blockedGenpass[j])
							{
								continue;
							}
						}
						int index = tasks.FindIndex(genpass => genpass.Name.Equals(str));
						if (index != -1)
						{
							WorldGenLegacyMethod editedGenpass;
							switch (str)
							{
								case "Reset":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Reset);
									break;
								case "Terrain":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Terrain);
									break;
								case "Dunes":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Dunes);
									break;
								case "Mount Caves":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.MountCaves);
									break;
								case "Dirt Wall Backgrounds":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.DirtWallBackgrounds);
									break;
								case "Rocks In Dirt":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.RocksInDirt);
									break;
								case "Dirt In Rocks":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.DirtInRocks);
									break;
								case "Clay":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Clay);
									break;
								case "Small Holes":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.SmallHoles);
									break;
								case "Dirt Layer Caves":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.DirtLayerCaves);
									break;
								case "Rock Layer Caves":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.RockLayerCaves);
									break;
								case "Surface Caves":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.SurfaceCaves);
									break;
								case "Jungle":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Jungle); //added hives to wg to provide hive tiles/items
									break;
								case "Floating Islands":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.FloatingIslands);
									break;
								case "Mushroom Patches":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.MushroomPatches);
									break;
								case "Dirt To Mud":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.DirtToMud);
									break;
								case "Silt":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Silt);
									break;
								case "Shinies":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Shinies);
									break;
								case "Webs":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Webs);
									break;
								case "Underworld":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Underworld);
									break;
								case "Mountain Caves":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.MountainCaves);
									break;
								case "Beaches":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Beaches);
									break;
								case "Gems":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Gems);
									break;
								case "Gravitating Sand":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.GravitatingSand);
									break;
								case "Clean Up Dirt":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.CleanUpDirt);
									break;
								case "Altars":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Altars);
									break;
								case "Wet Jungle":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.WetJungle);
									break;
								case "Life Crystals":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.LifeCrystals);
									break;
								case "Statues":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.Statues);
									break;
								case "Buried Chests":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.BuriedChests);
									break;
								case "Surface Chests":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.SurfaceChests);
									break;
								case "Jungle Chests Placement":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.JungleChestsPlacement);
									break;
								case "Water Chests":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.WaterChests);
									break;
								case "Floating Island Houses":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.FloatingIslandHouses);
									break;
								case "Jungle Temple":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.JungleTemple);
									break;
								case "Lihzahrd Altars":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.LihzahrdAltars);
									break;
								case "Final Cleanup":
									editedGenpass = new WorldGenLegacyMethod(retroWorldGen.FinalCleanup);
									break;
								default:
									editedGenpass = new WorldGenLegacyMethod(EmptyPass); //includes added shimmer wg
									break;
							}
							tasks.Insert(index + 1, new PassLegacy(str, editedGenpass));
							tasks.RemoveAt(index);
						}
					}

					int index2 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[0])); //for some reason corruption was moved down before lakes in later updates
					int index3 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[1]));
					int index4 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[2]));
					ReplaceGenpass(ref tasks, index2, "Lakes", new WorldGenLegacyMethod(retroWorldGen.Lakes));
					ReplaceGenpass(ref tasks, index3, "Dungeon", new WorldGenLegacyMethod(retroWorldGen.Dungeon));
					ReplaceGenpass(ref tasks, index4, "Corruption", new WorldGenLegacyMethod(retroWorldGen.Corruption));

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[3])); //swapped sand water removal and settle liquids
					index3 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[4]));
					ReplaceGenpass(ref tasks, index2, "Remove Water From Sand", new WorldGenLegacyMethod(retroWorldGen.RemoveWaterFromSand));
					ReplaceGenpass(ref tasks, index3, "Settle Liquids", new WorldGenLegacyMethod(retroWorldGen.SettleLiquids));

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[5])); //Traps was also moved from pre-pots to post fallen log/speading grass
					index3 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[6]));
					index4 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[7]));
					int index5 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[8]));
					int index6 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[9]));
					int index7 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[10]));
					ReplaceGenpass(ref tasks, index2, "Traps", new WorldGenLegacyMethod(retroWorldGen.Traps));
					ReplaceGenpass(ref tasks, index3, "Pots", new WorldGenLegacyMethod(retroWorldGen.Pots));
					ReplaceGenpass(ref tasks, index4, "Hellforge", new WorldGenLegacyMethod(retroWorldGen.Hellforge));
					ReplaceGenpass(ref tasks, index5, "Spreading Grass", new WorldGenLegacyMethod(retroWorldGen.SpreadingGrass));
					ReplaceGenpass(ref tasks, index6, "Surface Ore and Stone", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index7, "Place Fallen Log", new WorldGenLegacyMethod(EmptyPass));

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[11])); //Cactus growth was also moved from pre-spawnpoint to post-settling liquids for a second time
					index3 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[12]));
					index4 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[13]));
					index5 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[14]));
					index6 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[15]));
					index7 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[16]));
					int index8 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[17]));
					int index9 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[18]));
					int index10 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[19]));
					int index11 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[20]));
					int index12 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[21]));
					int index13 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[22]));
					int index14 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[23]));
					int index15 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[24]));
					int index16 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[25]));
					int index17 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[26]));
					int index18 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[27]));
					int index19 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[28]));
					int index20 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[29]));
					int index21 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[30]));
					int index22 = tasks.FindIndex(genpass => genpass.Name.Equals(blockedGenpass[31]));
					ReplaceGenpass(ref tasks, index2, "Cactus, Palm Trees, & Coral", new WorldGenLegacyMethod(retroWorldGen.CactusPalmTreesCoral));
					ReplaceGenpass(ref tasks, index3, "Spawn Point", new WorldGenLegacyMethod(retroWorldGen.SpawnPoint));
					ReplaceGenpass(ref tasks, index4, "Grass Wall", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index5, "Guide", new WorldGenLegacyMethod(retroWorldGen.Guide)); //Added forced name all worlds created to give the name brian
					ReplaceGenpass(ref tasks, index6, "Sunflowers", new WorldGenLegacyMethod(retroWorldGen.Sunflowers));
					ReplaceGenpass(ref tasks, index7, "Planting Trees", new WorldGenLegacyMethod(retroWorldGen.PlantingTrees));
					ReplaceGenpass(ref tasks, index8, "Herbs", new WorldGenLegacyMethod(retroWorldGen.Herbs));
					ReplaceGenpass(ref tasks, index9, "Dye Plants", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index10, "Webs And Honey", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index11, "Weeds", new WorldGenLegacyMethod(retroWorldGen.Weeds));
					ReplaceGenpass(ref tasks, index12, "Glowing Mushrooms and Jungle Plants", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index13, "Jungle Plants", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index14, "Vines", new WorldGenLegacyMethod(retroWorldGen.Vines));
					ReplaceGenpass(ref tasks, index15, "Flowers", new WorldGenLegacyMethod(retroWorldGen.Flowers));
					ReplaceGenpass(ref tasks, index16, "Mushrooms", new WorldGenLegacyMethod(retroWorldGen.Mushrooms));
					ReplaceGenpass(ref tasks, index17, "Gems In Ice Biome", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index18, "Random Gems", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index19, "Moss Grass", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index20, "Muds Walls In Jungle", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index21, "Larva", new WorldGenLegacyMethod(EmptyPass));
					ReplaceGenpass(ref tasks, index22, "Settle Liquids Again", new WorldGenLegacyMethod(EmptyPass));

					int indexMod = tasks.FindIndex(genpass => genpass.Name.Equals("Remove Broken Traps")); //modded WG
					if (indexMod != -1)
					{
						tasks.Insert(indexMod + 1, new PassLegacy("Make Clouds", new WorldGenLegacyMethod(retroWorldGen.MakeCloudPass)));
						tasks.Insert(indexMod + 2, new PassLegacy("Titanium Ore", new WorldGenLegacyMethod(retroWorldGen.TitaniumOrePass)));
						tasks.Insert(indexMod + 3, new PassLegacy("Add Motherloads", new WorldGenLegacyMethod(retroWorldGen.AddMotherloadsPass)));
						//tasks.Insert(indexMod + 1, new PassLegacy("Add Coal", new WorldGenLegacyMethod(retroWorldGen.AddCoalPass)));
						//tasks.Insert(indexMod + 1, new PassLegacy("Add Darkstone", new WorldGenLegacyMethod(retroWorldGen.AddDarkStonePass)));
						//tasks.Insert(indexMod + 1, new PassLegacy("Add Salt", new WorldGenLegacyMethod(retroWorldGen.AddSaltPass)));
						//tasks.Insert(indexMod + 1, new PassLegacy("Add Hardsalt", new WorldGenLegacyMethod(retroWorldGen.AddHardSaltPass)));
						//tasks.Insert(indexMod + 1, new PassLegacy("Add Jungle Ore", new WorldGenLegacyMethod(retroWorldGen.AddJungleOrePass)));
						tasks.Insert(indexMod + 4, new PassLegacy("Ice Shrine", new WorldGenLegacyMethod(retroWorldGen.IceShrinePass)));
						tasks.Insert(indexMod + 5, new PassLegacy("Caesium Ore", new WorldGenLegacyMethod(retroWorldGen.CaesiumOrePass)));
						tasks.Insert(indexMod + 6, new PassLegacy("HellCastle", new WorldGenLegacyMethod(retroWorldGen.HellCastlePass)));
						//tasks.Insert(indexMod + 1, new PassLegacy("Hallowed Altars", new WorldGenLegacyMethod(retroWorldGen.HallowedAltarPass)));
						tasks.Insert(indexMod + 7, new PassLegacy("Heartstone Patch", new WorldGenLegacyMethod(retroWorldGen.HeartStonePatchPass)));
						tasks.Insert(indexMod + 8, new PassLegacy("Make Ice Cave", new WorldGenLegacyMethod(retroWorldGen.MakeIceCavePass)));
						tasks.Insert(indexMod + 9, new PassLegacy("Replace Chest Contents", new WorldGenLegacyMethod(retroWorldGen.ReplaceChestContentsPass)));
						tasks.Insert(indexMod + 10, new PassLegacy("WG Replace Chests", new WorldGenLegacyMethod(retroWorldGen.WGReplaceChestsPass)));
						tasks.Insert(indexMod + 11, new PassLegacy("WG Place Custom Statues", new WorldGenLegacyMethod(retroWorldGen.WGPlaceCustomStatuesPass)));
					}
				}
				else
				{
					//get fixed boi worldgen
					int index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Avalon: Reset Secret Seed stuff", new WorldGenLegacyMethod(retroWorldGen.ResetZenith)));
					}

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Planting Trees"));
					ReplaceGenpass(ref tasks, index2, "Planting Trees", new WorldGenLegacyMethod(retroWorldGen.PlantingTreesZenith));

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Remove Broken Traps"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Avalon: Unhammer hammered tiles", new WorldGenLegacyMethod(retroWorldGen.RemoveSlopesSlabsZenith)));
					}

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Floating Islands"));
					ReplaceGenpass(ref tasks, index2, "Floating Islands", new WorldGenLegacyMethod(retroWorldGen.FloatingIslandsZenith));

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Avalon: Angel-ify chests", new WorldGenLegacyMethod(retroWorldGen.ReplaceGoldChestsWithStatues)));
					}

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Floating Island Houses"));
					ReplaceGenpass(ref tasks, index2, "Floating Island Houses", new WorldGenLegacyMethod(retroWorldGen.FloatingIslandHousesZenith));
				}
			}
			else if (AvalonWorld.cavesWorld)
			{
				if (!(WorldGen.drunkWorldGen && WorldGen.getGoodWorldGen && WorldGen.noTrapsWorldGen))
				{
					for (int i = 0; i < vanillaGenpasses.Length; i++)
					{
						string str = vanillaGenpasses[i];
						int index = tasks.FindIndex(genpass => genpass.Name.Equals(str));
						if (index != -1)
						{
							WorldGenLegacyMethod? editedGenpass = null;
							switch (str)
							{
								case "Mount Caves":
								case "Dirt Layer Caves":
								case "Surface Caves":
								case "Grass":
								case "Floating Islands": // TODO: make loot found elsewhere
								case "Mountain Caves":
								case "Living Trees":
								case "Wood Tree Walls":
								case "Oasis":
								case "Shell Piles":
								case "Surface Chests":
								case "Floating Island Houses":
								case "Planting Trees":
								case "Sunflowers":
								case "Granite":
								case "Cactus, Palm Trees, & Coral":
									editedGenpass = new WorldGenLegacyMethod(EmptyPass);
									break;
							}
							if (editedGenpass != null)
							{
								tasks.Insert(index + 1, new PassLegacy(str, editedGenpass));
								tasks.RemoveAt(index);
							}
						}
					}

					int index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Dunes"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Cave-ing caves", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration config)
						{
							Main.worldSurface = 30;
							Main.rockLayer = 50;
						})));
						//tasks.RemoveAt(index2);
					}
					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Spawn Point"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Adding Underground Spawnpoint", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration config)
						{
							Main.spawnTileY += 300;
							cavesWorldGen.GenerateSpawnArea(Main.spawnTileX - 5, Main.spawnTileY - 6);
						})));
					}

					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Adding Cave Lakes", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration config)
						{
							GenVars.worldSurfaceLow = 150;
						})));
					}
					index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Lakes"));
					if (index2 != -1)
					{
						tasks.Insert(index2 + 1, new PassLegacy("Adding Lake Caves", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration config)
						{
							GenVars.worldSurfaceLow = 30;
						})));
					}

				}
			}
		}

		private static void ReplaceGenpass(ref List<GenPass> tasks, int index, string passName, WorldGenLegacyMethod newGenPass)
		{
			if (index != -1)
			{
				tasks.Insert(index + 1, new PassLegacy(passName, newGenPass));
				tasks.RemoveAt(index);
			}
		}

		//all genpasses that didnt exist in 1.1.2 use a blank genpass to still exist so mod confict happen less, although the old worldgen is quite agressive and may/will still break with other mod worldgen
		private static void EmptyPass(GenerationProgress progres, GameConfiguration configurations) { }
	}
}
