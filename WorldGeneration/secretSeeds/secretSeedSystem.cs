using Avalon.Common;
using Avalon.Items.Material;
using Avalon.Items.Tools.PreHardmode;
using Avalon.NPCs.PreHardmode;
using Avalon.WorldGeneration.Passes;
using Humanizer;
using Microsoft.Build.Tasks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Projectiles;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Tiles;

namespace Avalon.WorldGeneration.secretSeeds
{
	public class secretSeedSystem : ModSystem
	{
		public static bool GetRetroWorldGen = false;

		public override void Load()
		{
			On_WorldGen.GenerateWorld += CheckSeedNumber;
			On_UIWorldCreation.ProcessSpecialWorldSeeds += SetGetRetroSeed;
		}

		private void SetGetRetroSeed(On_UIWorldCreation.orig_ProcessSpecialWorldSeeds orig, string processedSeed)
		{
			GetRetroWorldGen = false;
			if (processedSeed.ToLower() == "test-icles")
			{
				GetRetroWorldGen = true;
			}
			if (processedSeed.ToLower() == "get fixed boi" || processedSeed.ToLower() == "getfixedboi")
			{
				GetRetroWorldGen = true;
			}
			orig.Invoke(processedSeed);
		}

		private void CheckSeedNumber(On_WorldGen.orig_GenerateWorld orig, int seed, Terraria.WorldBuilding.GenerationProgress customProgressObject)
		{
			if (GetRetroWorldGen || seed == 12012011)
			{
				AvalonWorld.retroWorld = true;
				Main.rand = new UnifiedRandom();
				seed = Main.rand.Next(999999999);
			}
			else
			{
				AvalonWorld.retroWorld = false;
			}
			orig.Invoke(seed, customProgressObject);
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			if (AvalonWorld.retroWorld)
			{
				int index = tasks.FindIndex(genpass => genpass.Name.Equals("Reset"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Reset", new WorldGenLegacyMethod(Reset)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Terrain"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Terrain", new WorldGenLegacyMethod(Terrain)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Dunes"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Dunes", new WorldGenLegacyMethod(Dunes)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Ocean Sand"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Ocean Sand", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Sand Patches"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Sand Patches", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Tunnels"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Tunnels", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Mount Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Mount Caves", new WorldGenLegacyMethod(MountCaves)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Dirt Wall Backgrounds"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Dirt Wall Backgrounds", new WorldGenLegacyMethod(DirtWallBackgrounds)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Rocks In Dirt"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Rocks In Dirt", new WorldGenLegacyMethod(RocksInDirt)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Dirt In Rocks"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Dirt In Rocks", new WorldGenLegacyMethod(DirtInRocks)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Clay"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Clay", new WorldGenLegacyMethod(Clay)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Small Holes"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Small Holes", new WorldGenLegacyMethod(SmallHoles)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Dirt Layer Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Dirt Layer Caves", new WorldGenLegacyMethod(DirtLayerCaves)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Rock Layer Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Rock Layer Caves", new WorldGenLegacyMethod(RockLayerCaves)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Surface Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Surface Caves", new WorldGenLegacyMethod(SurfaceCaves)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Wavy Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Wavy Caves", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Generate Ice Biome"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Generate Ice Biome", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Grass"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Grass", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Jungle", new WorldGenLegacyMethod(Jungle)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Mud Caves To Grass"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Mud Caves To Grass", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Full Desert"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Full Desert", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Floating Islands"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Floating Islands", new WorldGenLegacyMethod(FloatingIslands)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Mushroom Patches"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Mushroom Patches", new WorldGenLegacyMethod(MushroomPatches)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Marble"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Marble", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Granite"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Granite", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Dirt To Mud"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Dirt To Mud", new WorldGenLegacyMethod(DirtToMud)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Silt"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Silt", new WorldGenLegacyMethod(Silt)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Shinies", new WorldGenLegacyMethod(Shinies)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Webs"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Webs", new WorldGenLegacyMethod(Webs)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Underworld"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Underworld", new WorldGenLegacyMethod(Underworld)));
					tasks.RemoveAt(index);
				}



				int index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Corruption")); //for some reason corruption was moved down before lakes in later updates
				int index3 = tasks.FindIndex(genpass => genpass.Name.Equals("Lakes"));
				int index4 = tasks.FindIndex(genpass => genpass.Name.Equals("Dungeon"));
				if (index2 != -1)
				{
					tasks.Insert(index2 + 1, new PassLegacy("Lakes", new WorldGenLegacyMethod(Lakes)));
					tasks.RemoveAt(index2);
				}
				if (index3 != -1)
				{
					tasks.Insert(index3 + 1, new PassLegacy("Dungeon", new WorldGenLegacyMethod(Dungeon)));
					tasks.RemoveAt(index3);
				}
				if (index != -1)
				{
					tasks.Insert(index4 + 1, new PassLegacy("Corruption", new WorldGenLegacyMethod(Corruption)));
					tasks.RemoveAt(index4);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Slush"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Slush", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Mountain Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Mountain Caves", new WorldGenLegacyMethod(MountainCaves)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Beaches"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Beaches", new WorldGenLegacyMethod(Beaches)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Gems"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Gems", new WorldGenLegacyMethod(Gems)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Gravitating Sand"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Gravitating Sand", new WorldGenLegacyMethod(GravitatingSand)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Create Ocean Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Create Ocean Caves", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Shimmer"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Shimmer", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Clean Up Dirt"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Clean Up Dirt", new WorldGenLegacyMethod(CleanUpDirt)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Pyramids"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Pyramids", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Dirt Rock Wall Runner"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Dirt Rock Wall Runner", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Living Trees", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Wood Tree Walls"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Wood Tree Walls", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Altars"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Altars", new WorldGenLegacyMethod(Altars)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Wet Jungle"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Wet Jungle", new WorldGenLegacyMethod(WetJungle)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Temple"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Jungle Temple", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Hives"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Hives", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Chests")); //90% sure this isnt implemented until later in the chests method but idk
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Jungle Chests", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals(""));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Settle Liquids", new WorldGenLegacyMethod(SettleLiquids)));
					tasks.RemoveAt(index);
				}
				


				index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Settle Liquids"));
				index3 = tasks.FindIndex(genpass => genpass.Name.Equals("Remove Water From Sand"));
				if (index2 != -1)
				{
					tasks.Insert(index2 + 1, new PassLegacy("Remove Water From Sand", new WorldGenLegacyMethod(RemoveWaterFromSand)));
					tasks.RemoveAt(index2);
				}
				if (index3 != -1)
				{
					tasks.Insert(index3 + 1, new PassLegacy("Settle Liquids", new WorldGenLegacyMethod(SettleLiquids)));
					tasks.RemoveAt(index3);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Oasis"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Oasis", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Shell Piles"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Shell Piles", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Smooth World"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Smooth World", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Waterfalls"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Waterfalls", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Ice"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Ice", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Wall Variety"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Wall Variety", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Life Crystals"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Life Crystals", new WorldGenLegacyMethod(LifeCrystals)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Statues"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Statues", new WorldGenLegacyMethod(Statues)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Buried Chests"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Buried Chests", new WorldGenLegacyMethod(BuriedChests)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Surface Chests"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Surface Chests", new WorldGenLegacyMethod(SurfaceChests)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Chests Placement"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Jungle Chests Placement", new WorldGenLegacyMethod(JungleChestsPlacement)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Water Chests"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Water Chests", new WorldGenLegacyMethod(WaterChests)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Spider Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Spider Caves", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Gem Caves"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Gem Caves", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Moss"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Moss", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Temple"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Temple", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Cave Walls"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Cave Walls", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Trees"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Jungle Trees", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Floating Island Houses"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Floating Island Houses", new WorldGenLegacyMethod(FloatingIslandHouses)));
					tasks.RemoveAt(index);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Quick Cleanup"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Quick Cleanup", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Pots")); //Traps was also moved from pre-pots to post fallen log/speading grass
				index3 = tasks.FindIndex(genpass => genpass.Name.Equals("Hellforge"));
				index4 = tasks.FindIndex(genpass => genpass.Name.Equals("Spreading Grass"));
				int index5 = tasks.FindIndex(genpass => genpass.Name.Equals("Surface Ore and Stone"));
				int index6 = tasks.FindIndex(genpass => genpass.Name.Equals("Place Fallen Log"));
				int index7 = tasks.FindIndex(genpass => genpass.Name.Equals("Traps"));
				if (index2 != -1)
				{
					tasks.Insert(index2 + 1, new PassLegacy("Traps", new WorldGenLegacyMethod(Traps)));
					tasks.RemoveAt(index2);
				}
				if (index3 != -1)
				{
					tasks.Insert(index3 + 1, new PassLegacy("Pots", new WorldGenLegacyMethod(Pots)));
					tasks.RemoveAt(index3);
				}
				if (index4 != -1)
				{
					tasks.Insert(index4 + 1, new PassLegacy("Hellforge", new WorldGenLegacyMethod(Hellforge)));
					tasks.RemoveAt(index4);
				}
				if (index5 != -1)
				{
					tasks.Insert(index5 + 1, new PassLegacy("Spreading Grass", new WorldGenLegacyMethod(SpreadingGrass)));
					tasks.RemoveAt(index5);
				}
				if (index6 != -1)
				{
					tasks.Insert(index6 + 1, new PassLegacy("Surface Ore and Stone", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index6);
				}
				if (index7 != -1)
				{
					tasks.Insert(index7 + 1, new PassLegacy("Place Fallen Log", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index7);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Piles"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Piles", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}



				index2 = tasks.FindIndex(genpass => genpass.Name.Equals("Spawn Point")); //Cactus growth was also moved from pre-spawnpoint to post BLANK
				index3 = tasks.FindIndex(genpass => genpass.Name.Equals("Grass Wall"));
				index4 = tasks.FindIndex(genpass => genpass.Name.Equals("Guide"));
				index5 = tasks.FindIndex(genpass => genpass.Name.Equals("Sunflowers"));
				index6 = tasks.FindIndex(genpass => genpass.Name.Equals("Planting Trees"));
				index7 = tasks.FindIndex(genpass => genpass.Name.Equals("Herbs"));
				int index8 = tasks.FindIndex(genpass => genpass.Name.Equals("Dye Plants"));
				int index9 = tasks.FindIndex(genpass => genpass.Name.Equals("Webs And Honey"));
				int index10 = tasks.FindIndex(genpass => genpass.Name.Equals("Weeds"));
				int index11 = tasks.FindIndex(genpass => genpass.Name.Equals("Glowing Mushrooms and Jungle Plants"));
				int index12 = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle Plants"));
				int index13 = tasks.FindIndex(genpass => genpass.Name.Equals("Vines"));
				int index14 = tasks.FindIndex(genpass => genpass.Name.Equals("Flowers"));
				int index15 = tasks.FindIndex(genpass => genpass.Name.Equals("Mushrooms"));
				int index16 = tasks.FindIndex(genpass => genpass.Name.Equals("Gems In Ice Biome"));
				int index17 = tasks.FindIndex(genpass => genpass.Name.Equals("Random Gems"));
				int index18 = tasks.FindIndex(genpass => genpass.Name.Equals("Moss Grass"));
				int index19 = tasks.FindIndex(genpass => genpass.Name.Equals("Muds Walls In Jungle"));
				int index20 = tasks.FindIndex(genpass => genpass.Name.Equals("Larva"));
				int index21 = tasks.FindIndex(genpass => genpass.Name.Equals("Settle Liquids Again"));
				int index22 = tasks.FindIndex(genpass => genpass.Name.Equals("Cactus, Palm Trees, & Coral"));
				if (index2 != -1)
				{
					tasks.Insert(index2 + 1, new PassLegacy("Cactus, Palm Trees, & Coral", new WorldGenLegacyMethod(CactusPalmTreesCoral)));
					tasks.RemoveAt(index2);
				}
				if (index3 != -1)
				{
					tasks.Insert(index3 + 1, new PassLegacy("Spawn Point", new WorldGenLegacyMethod(SpawnPoint)));
					tasks.RemoveAt(index3);
				}
				if (index4 != -1)
				{
					tasks.Insert(index4 + 1, new PassLegacy("Grass Wall", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index4);
				}
				if (index5 != -1)
				{
					tasks.Insert(index5 + 1, new PassLegacy("Guide", new WorldGenLegacyMethod(Guide)));
					tasks.RemoveAt(index5);
				}
				if (index6 != -1)
				{
					tasks.Insert(index6 + 1, new PassLegacy("Sunflowers", new WorldGenLegacyMethod(Sunflowers)));
					tasks.RemoveAt(index6);
				}
				if (index7 != -1)
				{
					tasks.Insert(index7 + 1, new PassLegacy("Planting Trees", new WorldGenLegacyMethod(PlantingTrees)));
					tasks.RemoveAt(index7);
				}
				if (index8 != -1)
				{
					tasks.Insert(index8 + 1, new PassLegacy("Herbs", new WorldGenLegacyMethod(Herbs)));
					tasks.RemoveAt(index8);
				}
				if (index9 != -1)
				{
					tasks.Insert(index9 + 1, new PassLegacy("Dye Plants", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index9);
				}
				if (index10 != -1)
				{
					tasks.Insert(index10 + 1, new PassLegacy("Webs And Honey", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index10);
				}
				if (index11 != -1)
				{
					tasks.Insert(index11 + 1, new PassLegacy("Weeds", new WorldGenLegacyMethod(Weeds)));
					tasks.RemoveAt(index11);
				}
				if (index12 != -1)
				{
					tasks.Insert(index12 + 1, new PassLegacy("Glowing Mushrooms and Jungle Plants", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index12);
				}
				if (index13 != -1)
				{
					tasks.Insert(index13 + 1, new PassLegacy("Jungle Plants", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index13);
				}
				if (index14 != -1)
				{
					tasks.Insert(index14 + 1, new PassLegacy("Vines", new WorldGenLegacyMethod(Vines)));
					tasks.RemoveAt(index14);
				}
				if (index15 != -1)
				{
					tasks.Insert(index15 + 1, new PassLegacy("Flowers", new WorldGenLegacyMethod(Flowers)));
					tasks.RemoveAt(index15);
				}
				if (index16 != -1)
				{
					tasks.Insert(index16 + 1, new PassLegacy("Mushrooms", new WorldGenLegacyMethod(Mushrooms)));
					tasks.RemoveAt(index16);
				}
				if (index17 != -1)
				{
					tasks.Insert(index17 + 1, new PassLegacy("Gems In Ice Biome", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index17);
				}
				if (index18 != -1)
				{
					tasks.Insert(index18 + 1, new PassLegacy("Random Gems", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index18);
				}
				if (index19 != -1)
				{
					tasks.Insert(index19 + 1, new PassLegacy("Moss Grass", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index19);
				}
				if (index20 != -1)
				{
					tasks.Insert(index20 + 1, new PassLegacy("Muds Walls In Jungle", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index20);
				}
				if (index21 != -1)
				{
					tasks.Insert(index21 + 1, new PassLegacy("Larva", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index21);
				}
				if (index22 != -1)
				{
					tasks.Insert(index22 + 1, new PassLegacy("Settle Liquids Again", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index22);
				}



				index = tasks.FindIndex(genpass => genpass.Name.Equals("Tile Cleanup"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Tile Cleanup", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Lihzahrd Altars"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Lihzahrd Altars", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Micro Biomes", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Water Plants"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Water Plants", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Stalac"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Stalac", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Remove Broken Traps"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Remove Broken Traps", new WorldGenLegacyMethod(EmptyPass)));
					tasks.RemoveAt(index);
				}
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
				if (index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("Final Cleanup", new WorldGenLegacyMethod(FinalCleanup)));
					tasks.RemoveAt(index);
				}
			}
		}

		private static void EmptyPass(GenerationProgress progres, GameConfiguration configurations) { }

		//readded old feilds, have a look into GenVars for ones moved there
		private static int houseCount = 0;
		private static int numDPlats;
		private static int[] fihX = new int[300];
		private static int[] fihY = new int[300];
		private static int[] DPlatX = new int[300];
		private static int[] DPlatY = new int[300];
		private static int lastMaxTilesX = 0;
		private static int lastMaxTilesY = 0;
		private static Vector2 lastDungeonHall = default(Vector2);

		private static int localVar1; //due to 1.1 worldgeneration being in 1 big method, local variables are shared constantly
		private static int localVar2; //as worldgen is added, the names for the ones used a lot (ie ones here) are given
		private static double localVar3;
		private static double localVar4;
		private static double localVar5;
		private static double localVar6;
		private static double localVar7;
		private static double localVar8;
		private static int localVar9;
		private static int localVar12;
		private static int localVar197;
		private static int localVar198;
		private static int localVar199;
		private static int localVar200;
		private static float localVar252;

		public static void clearWorld() //maybe detour clear world to add this?
		{
			lastMaxTilesX = Main.maxTilesX;
			lastMaxTilesY = Main.maxTilesY;
		}

		private static void resetGen()
		{
			GenVars.mudWall = false;
			GenVars.hellChest = 0;
			GenVars.JungleX = 0;
			GenVars.numMCaves = 0;
			GenVars.numIslandHouses = 0;
			houseCount = 0;
			GenVars.dEnteranceX = 0;
			GenVars.numDRooms = 0;
			GenVars.numDDoors = 0;
			numDPlats = 0;
			GenVars.numJChests = 0;
		}

		private static void Reset(GenerationProgress progres, GameConfiguration configurations) 
		{
			clearWorld(); //added since clearworld is just before this method
			Main.checkXMas();
			//NPC.clrNames(); //legacy NPC name code, does not to be reinstalled
			//NPC.setNames();
			SetBackgroundNormal();
			WorldGen.gen = true;
			resetGen();
			/*if (seed > 0) //seed is done before any genpass now, adding this line may cause unknown changes
			{
				genRand = new Random(seed);
			}
			else
			{
				genRand = new Random((int)DateTime.Now.Ticks);
			}*/
			Main.worldID = WorldGen.genRand.Next(int.MaxValue);
			int num = 0;
			int num2 = 0;
			double num3 = (double)Main.maxTilesY * 0.3;
			num3 *= (double)WorldGen.genRand.Next(90, 110) * 0.005;
			double num4 = num3 + (double)Main.maxTilesY * 0.2;
			num4 *= (double)WorldGen.genRand.Next(90, 110) * 0.01;
			double num5 = num3;
			double num6 = num3;
			double num7 = num4;
			double num8 = num4;
			int num9 = 0;
			num9 = ((!WorldGen.genRand.NextBool(2)) ? 1 : (-1));

			localVar1 = num;
			localVar2 = num2;
			localVar3 = num3;
			localVar4 = num4;
			localVar5 = num5;
			localVar6 = num6;
			localVar7 = num7;
			localVar8 = num8;
			localVar9 = num9;
		}

		private static void Terrain(GenerationProgress progres, GameConfiguration configurations)
		{
			int num = localVar1;
			int num2 = localVar2;
			double num3 = localVar3;
			double num4 = localVar4;
			double num5 = localVar5;
			double num6 = localVar6;
			double num7 = localVar7;
			double num8 = localVar8;

			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num10 = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[0] + " " + (int)(num10 * 100f + 1f) + "%";
				if (num3 < num5)
				{
					num5 = num3;
				}
				if (num3 > num6)
				{
					num6 = num3;
				}
				if (num4 < num7)
				{
					num7 = num4;
				}
				if (num4 > num8)
				{
					num8 = num4;
				}
				if (num2 <= 0)
				{
					num = WorldGen.genRand.Next(0, 5);
					num2 = WorldGen.genRand.Next(5, 40);
					if (num == 0)
					{
						num2 *= (int)((double)WorldGen.genRand.Next(5, 30) * 0.2);
					}
				}
				num2--;
				if (num == 0)
				{
					while (WorldGen.genRand.Next(0, 7) == 0)
					{
						num3 += (double)WorldGen.genRand.Next(-1, 2);
					}
				}
				else if (num == 1)
				{
					while (WorldGen.genRand.Next(0, 4) == 0)
					{
						num3 -= 1.0;
					}
					while (WorldGen.genRand.Next(0, 10) == 0)
					{
						num3 += 1.0;
					}
				}
				else if (num == 2)
				{
					while (WorldGen.genRand.Next(0, 4) == 0)
					{
						num3 += 1.0;
					}
					while (WorldGen.genRand.Next(0, 10) == 0)
					{
						num3 -= 1.0;
					}
				}
				else if (num == 3)
				{
					while (WorldGen.genRand.Next(0, 2) == 0)
					{
						num3 -= 1.0;
					}
					while (WorldGen.genRand.Next(0, 6) == 0)
					{
						num3 += 1.0;
					}
				}
				else if (num == 4)
				{
					while (WorldGen.genRand.Next(0, 2) == 0)
					{
						num3 += 1.0;
					}
					while (WorldGen.genRand.Next(0, 5) == 0)
					{
						num3 -= 1.0;
					}
				}
				if (num3 < (double)Main.maxTilesY * 0.17)
				{
					num3 = (double)Main.maxTilesY * 0.17;
					num2 = 0;
				}
				else if (num3 > (double)Main.maxTilesY * 0.3)
				{
					num3 = (double)Main.maxTilesY * 0.3;
					num2 = 0;
				}
				if ((i < 275 || i > Main.maxTilesX - 275) && num3 > (double)Main.maxTilesY * 0.25)
				{
					num3 = (double)Main.maxTilesY * 0.25;
					num2 = 1;
				}
				while (WorldGen.genRand.Next(0, 3) == 0)
				{
					num4 += (double)WorldGen.genRand.Next(-2, 3);
				}
				if (num4 < num3 + (double)Main.maxTilesY * 0.05)
				{
					num4 += 1.0;
				}
				if (num4 > num3 + (double)Main.maxTilesY * 0.35)
				{
					num4 -= 1.0;
				}
				for (int j = 0; (double)j < num3; j++)
				{
					Tile tile = Main.tile[i, j];
					tile.HasTile = false;
					tile.TileFrameX = -1;
					tile.TileFrameY = -1;
				}
				for (int k = (int)num3; k < Main.maxTilesY; k++)
				{
					if ((double)k < num4)
					{
						Tile tile = Main.tile[i, k];
						tile.HasTile = true;
						tile.TileType = 0;
						tile.TileFrameX = -1;
						tile.TileFrameY = -1;
					}
					else
					{
						Tile tile = Main.tile[i, k];
						tile.HasTile = true;
						tile.TileType = 1;
						tile.TileFrameX = -1;
						tile.TileFrameY = -1;
					}
				}
			}
			Main.worldSurface = num6 + 25.0;
			Main.rockLayer = num8;
			double num11 = (int)((Main.rockLayer - Main.worldSurface) / 6.0) * 6;
			Main.rockLayer = Main.worldSurface + num11;
			GenVars.waterLine = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2;
			GenVars.waterLine += WorldGen.genRand.Next(-100, 20);
			GenVars.lavaLine = GenVars.waterLine + WorldGen.genRand.Next(50, 80);
			int num12 = 0;
			for (int l = 0; l < (int)((double)Main.maxTilesX * 0.0015); l++)
			{
				int[] array = new int[10];
				int[] array2 = new int[10];
				int num13 = WorldGen.genRand.Next(450, Main.maxTilesX - 450);
				int m = 0;
				for (int n = 0; n < 10; n++)
				{
					for (; !Main.tile[num13, m].HasTile; m++)
					{
					}
					array[n] = num13;
					array2[n] = m - WorldGen.genRand.Next(11, 16);
					num13 += WorldGen.genRand.Next(5, 11);
				}
				for (int num14 = 0; num14 < 10; num14++)
				{
					WorldGen.TileRunner(array[num14], array2[num14], WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(6, 9), 0, addTile: true, -2f, -0.3f);
					WorldGen.TileRunner(array[num14], array2[num14], WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(6, 9), 0, addTile: true, 2f, -0.3f);
				}
			}

			localVar1 = num;
			localVar2 = num2;
			localVar3 = num3;
			localVar4 = num4;
			localVar5 = num5;
			localVar6 = num6;
			localVar7 = num7;
			localVar8 = num8;
			localVar12 = num12;
		}

		private static void Dunes(GenerationProgress progres, GameConfiguration configurations)
		{
			int num9 = localVar9;

			Main.statusText = (string)Lang.gen[1];
			int num15 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.0008), (int)((double)Main.maxTilesX * 0.0025));
			num15 += 2;
			for (int num16 = 0; num16 < num15; num16++)
			{
				int num17 = WorldGen.genRand.Next(Main.maxTilesX);
				while ((float)num17 > (float)Main.maxTilesX * 0.4f && (float)num17 < (float)Main.maxTilesX * 0.6f)
				{
					num17 = WorldGen.genRand.Next(Main.maxTilesX);
				}
				int num18 = WorldGen.genRand.Next(35, 90);
				if (num16 == 1)
				{
					float num19 = Main.maxTilesX / 4200;
					num18 += (int)((float)WorldGen.genRand.Next(20, 40) * num19);
				}
				if (WorldGen.genRand.NextBool(3))
				{
					num18 *= 2;
				}
				if (num16 == 1)
				{
					num18 *= 2;
				}
				int num20 = num17 - num18;
				num18 = WorldGen.genRand.Next(35, 90);
				if (WorldGen.genRand.NextBool(3))
				{
					num18 *= 2;
				}
				if (num16 == 1)
				{
					num18 *= 2;
				}
				int num21 = num17 + num18;
				if (num20 < 0)
				{
					num20 = 0;
				}
				if (num21 > Main.maxTilesX)
				{
					num21 = Main.maxTilesX;
				}
				switch (num16)
				{
					case 0:
						num20 = 0;
						num21 = WorldGen.genRand.Next(260, 300);
						if (num9 == 1)
						{
							num21 += 40;
						}
						break;
					case 2:
						num20 = Main.maxTilesX - WorldGen.genRand.Next(260, 300);
						num21 = Main.maxTilesX;
						if (num9 == -1)
						{
							num20 -= 40;
						}
						break;
				}
				int num22 = WorldGen.genRand.Next(50, 100);
				for (int num23 = num20; num23 < num21; num23++)
				{
					if (WorldGen.genRand.NextBool(2))
					{
						num22 += WorldGen.genRand.Next(-1, 2);
						if (num22 < 50)
						{
							num22 = 50;
						}
						if (num22 > 100)
						{
							num22 = 100;
						}
					}
					for (int num24 = 0; (double)num24 < Main.worldSurface; num24++)
					{
						if (!Main.tile[num23, num24].HasTile)
						{
							continue;
						}
						int num25 = num22;
						if (num23 - num20 < num25)
						{
							num25 = num23 - num20;
						}
						if (num21 - num23 < num25)
						{
							num25 = num21 - num23;
						}
						num25 += WorldGen.genRand.Next(5);
						for (int num26 = num24; num26 < num24 + num25; num26++)
						{
							if (num23 > num20 + WorldGen.genRand.Next(5) && num23 < num21 - WorldGen.genRand.Next(5))
							{
								Main.tile[num23, num26].TileType = 53;
							}
						}
						break;
					}
				}
			}
			for (int num27 = 0; num27 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 8E-06); num27++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.worldSurface, (int)Main.rockLayer), WorldGen.genRand.Next(15, 70), WorldGen.genRand.Next(20, 130), 53);
			}
		}

		private static void MountCaves(GenerationProgress progres, GameConfiguration configurations)
		{
			GenVars.numMCaves = 0;
			Main.statusText = (string)Lang.gen[2];
			for (int num28 = 0; num28 < (int)((double)Main.maxTilesX * 0.0008); num28++)
			{
				int num29 = 0;
				bool flag = false;
				bool flag2 = false;
				int num30 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.25), (int)((double)Main.maxTilesX * 0.75));
				while (!flag2)
				{
					flag2 = true;
					while (num30 > Main.maxTilesX / 2 - 100 && num30 < Main.maxTilesX / 2 + 100)
					{
						num30 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.25), (int)((double)Main.maxTilesX * 0.75));
					}
					for (int num31 = 0; num31 < GenVars.numMCaves; num31++)
					{
						if (num30 > GenVars.mCaveX[num31] - 50 && num30 < GenVars.mCaveX[num31] + 50)
						{
							num29++;
							flag2 = false;
							break;
						}
					}
					if (num29 >= 200)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					continue;
				}
				for (int num32 = 0; (double)num32 < Main.worldSurface; num32++)
				{
					if (Main.tile[num30, num32].HasTile)
					{
						WorldGen.Mountinater(num30, num32); //surprisingly, this method has barely changed since 2011 with only a remix addition to local vars in 1.4.4
						GenVars.mCaveX[GenVars.numMCaves] = num30;
						GenVars.mCaveY[GenVars.numMCaves] = num32;
						GenVars.numMCaves++;
						break;
					}
				}
			}
			bool flag3 = false;
			if (Main.xMas)
			{
				flag3 = true;
			}
			else if (WorldGen.genRand.NextBool(3))
			{
				flag3 = true;
			}
			if (flag3)
			{
				Main.statusText = (string)Lang.gen[56];
				int num33 = WorldGen.genRand.Next(Main.maxTilesX);
				while ((float)num33 < (float)Main.maxTilesX * 0.35f || (float)num33 > (float)Main.maxTilesX * 0.65f)
				{
					num33 = WorldGen.genRand.Next(Main.maxTilesX);
				}
				int num34 = WorldGen.genRand.Next(35, 90);
				float num35 = Main.maxTilesX / 4200;
				num34 += (int)((float)WorldGen.genRand.Next(20, 40) * num35);
				num34 += (int)((float)WorldGen.genRand.Next(20, 40) * num35);
				int num36 = num33 - num34;
				num34 = WorldGen.genRand.Next(35, 90);
				num34 += (int)((float)WorldGen.genRand.Next(20, 40) * num35);
				num34 += (int)((float)WorldGen.genRand.Next(20, 40) * num35);
				int num37 = num33 + num34;
				if (num36 < 0)
				{
					num36 = 0;
				}
				if (num37 > Main.maxTilesX)
				{
					num37 = Main.maxTilesX;
				}
				int num38 = WorldGen.genRand.Next(50, 100);
				for (int num39 = num36; num39 < num37; num39++)
				{
					if (WorldGen.genRand.NextBool(2))
					{
						num38 += WorldGen.genRand.Next(-1, 2);
						if (num38 < 50)
						{
							num38 = 50;
						}
						if (num38 > 100)
						{
							num38 = 100;
						}
					}
					for (int num40 = 0; (double)num40 < Main.worldSurface; num40++)
					{
						if (!Main.tile[num39, num40].HasTile)
						{
							continue;
						}
						int num41 = num38;
						if (num39 - num36 < num41)
						{
							num41 = num39 - num36;
						}
						if (num37 - num39 < num41)
						{
							num41 = num37 - num39;
						}
						num41 += WorldGen.genRand.Next(5);
						for (int num42 = num40; num42 < num40 + num41; num42++)
						{
							if (num39 > num36 + WorldGen.genRand.Next(5) && num39 < num37 - WorldGen.genRand.Next(5))
							{
								Main.tile[num39, num42].TileType = 147;
							}
						}
						break;
					}
				}
			}
		}

		private static void DirtWallBackgrounds(GenerationProgress progres, GameConfiguration configurations)
		{
			int num12 = localVar12;

			for (int num43 = 1; num43 < Main.maxTilesX - 1; num43++)
			{
				float num44 = (float)num43 / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[3] + " " + (int)(num44 * 100f + 1f) + "%";
				bool flag4 = false;
				num12 += WorldGen.genRand.Next(-1, 2);
				if (num12 < 0)
				{
					num12 = 0;
				}
				if (num12 > 10)
				{
					num12 = 10;
				}
				for (int num45 = 0; (double)num45 < Main.worldSurface + 10.0 && !((double)num45 > Main.worldSurface + (double)num12); num45++)
				{
					if (flag4)
					{
						Main.tile[num43, num45].WallType = 2;
					}
					if (Main.tile[num43, num45].HasTile && Main.tile[num43 - 1, num45].HasTile && Main.tile[num43 + 1, num45].HasTile && Main.tile[num43, num45 + 1].HasTile && Main.tile[num43 - 1, num45 + 1].HasTile && Main.tile[num43 + 1, num45 + 1].HasTile)
					{
						flag4 = true;
					}
				}
			}

			localVar12 = num12;
		}

		private static void RocksInDirt(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			double num6 = localVar6;
			double num8 = localVar8;

			Main.statusText = (string)Lang.gen[4];
			for (int num46 = 0; num46 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num46++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5 + 1), WorldGen.genRand.Next(4, 15), WorldGen.genRand.Next(5, 40), 1);
			}
			for (int num47 = 0; num47 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num47++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6 + 1), WorldGen.genRand.Next(4, 10), WorldGen.genRand.Next(5, 30), 1);
			}
			for (int num48 = 0; num48 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0045); num48++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8 + 1), WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(2, 23), 1);
			}
		}

		private static void DirtInRocks(GenerationProgress progres, GameConfiguration configurations)
		{
			double num7 = localVar7;

			Main.statusText = (string)Lang.gen[5];
			for (int num49 = 0; num49 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.005); num49++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 40), 0);
			}
		}

		private static void Clay(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			double num6 = localVar6;
			double num8 = localVar8;

			Main.statusText = (string)Lang.gen[6];
			for (int num50 = 0; num50 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num50++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5), WorldGen.genRand.Next(4, 14), WorldGen.genRand.Next(10, 50), 40);
			}
			for (int num51 = 0; num51 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 5E-05); num51++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6 + 1), WorldGen.genRand.Next(8, 14), WorldGen.genRand.Next(15, 45), 40);
			}
			for (int num52 = 0; num52 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num52++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8 + 1), WorldGen.genRand.Next(8, 15), WorldGen.genRand.Next(5, 50), 40);
			}
			for (int num53 = 5; num53 < Main.maxTilesX - 5; num53++)
			{
				for (int num54 = 1; (double)num54 < Main.worldSurface - 1.0; num54++)
				{
					if (!Main.tile[num53, num54].HasTile)
					{
						continue;
					}
					for (int num55 = num54; num55 < num54 + 5; num55++)
					{
						if (Main.tile[num53, num55].TileType == 40)
						{
							Main.tile[num53, num55].TileType = 0;
						}
					}
					break;
				}
			}
		}

		private static void SmallHoles(GenerationProgress progres, GameConfiguration configurations)
		{
			double num6 = localVar6;

			for (int num57 = 0; num57 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0015); num57++)
			{
				float num58 = (float)((double)num57 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.0015));
				Main.statusText = Lang.gen[7] + " " + (int)(num58 * 100f + 1f) + "%";
				int type = -1;
				if (WorldGen.genRand.NextBool(5))
				{
					type = -2;
				}
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, Main.maxTilesY), WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 20), type);
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, Main.maxTilesY), WorldGen.genRand.Next(8, 15), WorldGen.genRand.Next(7, 30), type);
			}
		}

		private static void DirtLayerCaves(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			double num8 = localVar8;

			for (int num59 = 0; num59 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); num59++)
			{
				float num60 = (float)((double)num59 / ((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05));
				Main.statusText = Lang.gen[8] + " " + (int)(num60 * 100f + 1f) + "%";
				if (num8 <= (double)Main.maxTilesY)
				{
					int type2 = -1;
					if (WorldGen.genRand.NextBool(6))
					{
						type2 = -2;
					}
					WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num8 + 1), WorldGen.genRand.Next(5, 15), WorldGen.genRand.Next(30, 200), type2);
				}
			}
		}

		private static void RockLayerCaves(GenerationProgress progres, GameConfiguration configurations)
		{
			double num8 = localVar8;

			for (int num61 = 0; num61 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00013); num61++)
			{
				float num62 = (float)((double)num61 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.00013));
				Main.statusText = Lang.gen[9] + " " + (int)(num62 * 100f + 1f) + "%";
				if (num8 <= (double)Main.maxTilesY)
				{
					int type3 = -1;
					if (WorldGen.genRand.NextBool(10))
					{
						type3 = -2;
					}
					WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num8, Main.maxTilesY), WorldGen.genRand.Next(6, 20), WorldGen.genRand.Next(50, 300), type3);
				}
			}
		}

		private static void SurfaceCaves(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			double num6 = localVar6;

			int num56 = 0;
			Main.statusText = (string)Lang.gen[10];
			for (int num63 = 0; num63 < (int)((double)Main.maxTilesX * 0.0025); num63++)
			{
				num56 = WorldGen.genRand.Next(0, Main.maxTilesX);
				for (int num64 = 0; (double)num64 < num6; num64++)
				{
					if (Main.tile[num56, num64].HasTile)
					{
						WorldGen.TileRunner(num56, num64, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(5, 50), -1, addTile: false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 1f);
						break;
					}
				}
			}
			for (int num65 = 0; num65 < (int)((double)Main.maxTilesX * 0.0007); num65++)
			{
				num56 = WorldGen.genRand.Next(0, Main.maxTilesX);
				for (int num66 = 0; (double)num66 < num6; num66++)
				{
					if (Main.tile[num56, num66].HasTile)
					{
						WorldGen.TileRunner(num56, num66, WorldGen.genRand.Next(10, 15), WorldGen.genRand.Next(50, 130), -1, addTile: false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 2f);
						break;
					}
				}
			}
			for (int num67 = 0; num67 < (int)((double)Main.maxTilesX * 0.0003); num67++)
			{
				num56 = WorldGen.genRand.Next(0, Main.maxTilesX);
				for (int num68 = 0; (double)num68 < num6; num68++)
				{
					if (Main.tile[num56, num68].HasTile)
					{
						WorldGen.TileRunner(num56, num68, WorldGen.genRand.Next(12, 25), WorldGen.genRand.Next(150, 500), -1, addTile: false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 4f);
						WorldGen.TileRunner(num56, num68, WorldGen.genRand.Next(8, 17), WorldGen.genRand.Next(60, 200), -1, addTile: false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 2f);
						WorldGen.TileRunner(num56, num68, WorldGen.genRand.Next(5, 13), WorldGen.genRand.Next(40, 170), -1, addTile: false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 2f);
						break;
					}
				}
			}
			for (int num69 = 0; num69 < (int)((double)Main.maxTilesX * 0.0004); num69++)
			{
				num56 = WorldGen.genRand.Next(0, Main.maxTilesX);
				for (int num70 = 0; (double)num70 < num6; num70++)
				{
					if (Main.tile[num56, num70].HasTile)
					{
						WorldGen.TileRunner(num56, num70, WorldGen.genRand.Next(7, 12), WorldGen.genRand.Next(150, 250), -1, addTile: false, 0f, 1f, noYChange: true);
						break;
					}
				}
			}
			float num71 = Main.maxTilesX / 4200;
			for (int num72 = 0; (float)num72 < 5f * num71; num72++)
			{
				try
				{
					WorldGen.Caverer(WorldGen.genRand.Next(100, Main.maxTilesX - 100), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 400));
				}
				catch
				{
				}
			}
			for (int num73 = 0; num73 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.002); num73++)
			{
				int num74 = WorldGen.genRand.Next(1, Main.maxTilesX - 1);
				int num75 = WorldGen.genRand.Next((int)num5, (int)num6);
				if (num75 >= Main.maxTilesY)
				{
					num75 = Main.maxTilesY - 2;
				}
				if (Main.tile[num74 - 1, num75].HasTile && Main.tile[num74 - 1, num75].TileType == 0 && Main.tile[num74 + 1, num75].HasTile && Main.tile[num74 + 1, num75].TileType == 0 && Main.tile[num74, num75 - 1].HasTile && Main.tile[num74, num75 - 1].TileType == 0 && Main.tile[num74, num75 + 1].HasTile && Main.tile[num74, num75 + 1].TileType == 0)
				{
					Tile tile = Main.tile[num74, num75];
					tile.HasTile = true;
					tile.TileType = 2;
				}
				num74 = WorldGen.genRand.Next(1, Main.maxTilesX - 1);
				num75 = WorldGen.genRand.Next(0, (int)num5);
				if (num75 >= Main.maxTilesY)
				{
					num75 = Main.maxTilesY - 2;
				}
				if (Main.tile[num74 - 1, num75].HasTile && Main.tile[num74 - 1, num75].TileType == 0 && Main.tile[num74 + 1, num75].HasTile && Main.tile[num74 + 1, num75].TileType == 0 && Main.tile[num74, num75 - 1].HasTile && Main.tile[num74, num75 - 1].TileType == 0 && Main.tile[num74, num75 + 1].HasTile && Main.tile[num74, num75 + 1].TileType == 0)
				{
					Tile tile = Main.tile[num74, num75];
					tile.HasTile = true;
					tile.TileType = 2;
				}
			}
		}

		private static void Jungle(GenerationProgress progres, GameConfiguration configurations)
		{
			int num9 = localVar9;

			Main.statusText = Lang.gen[11] + " 0%";
			float num76 = Main.maxTilesX / 4200;
			num76 *= 1.5f;
			int num77 = 0;
			float num78 = (float)WorldGen.genRand.Next(15, 30) * 0.01f;
			if (num9 == -1)
			{
				num78 = 1f - num78;
				num77 = (int)((float)Main.maxTilesX * num78);
			}
			else
			{
				num77 = (int)((float)Main.maxTilesX * num78);
			}
			int num79 = (int)((double)Main.maxTilesY + Main.rockLayer) / 2;
			num77 += WorldGen.genRand.Next((int)(-100f * num76), (int)(101f * num76));
			num79 += WorldGen.genRand.Next((int)(-100f * num76), (int)(101f * num76));
			int num80 = num77;
			int num81 = num79;
			WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next((int)(250f * num76), (int)(500f * num76)), WorldGen.genRand.Next(50, 150), 59, addTile: false, num9 * 3);
			for (int num82 = 0; (float)num82 < 6f * num76; num82++)
			{
				WorldGen.TileRunner(num77 + WorldGen.genRand.Next(-(int)(125f * num76), (int)(125f * num76)), num79 + WorldGen.genRand.Next(-(int)(125f * num76), (int)(125f * num76)), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(63, 65));
			}
			GenVars.mudWall = true;
			Main.statusText = Lang.gen[11] + " 15%";
			num77 += WorldGen.genRand.Next((int)(-250f * num76), (int)(251f * num76));
			num79 += WorldGen.genRand.Next((int)(-150f * num76), (int)(151f * num76));
			int num83 = num77;
			int num84 = num79;
			int num85 = num77;
			int num86 = num79;
			WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next((int)(250f * num76), (int)(500f * num76)), WorldGen.genRand.Next(50, 150), 59);
			GenVars.mudWall = false;
			for (int num87 = 0; (float)num87 < 6f * num76; num87++)
			{
				WorldGen.TileRunner(num77 + WorldGen.genRand.Next(-(int)(125f * num76), (int)(125f * num76)), num79 + WorldGen.genRand.Next(-(int)(125f * num76), (int)(125f * num76)), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(65, 67));
			}
			GenVars.mudWall = true;
			Main.statusText = Lang.gen[11] + " 30%";
			num77 += WorldGen.genRand.Next((int)(-400f * num76), (int)(401f * num76));
			num79 += WorldGen.genRand.Next((int)(-150f * num76), (int)(151f * num76));
			int num88 = num77;
			int num89 = num79;
			WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next((int)(250f * num76), (int)(500f * num76)), WorldGen.genRand.Next(50, 150), 59, addTile: false, num9 * -3);
			GenVars.mudWall = false;
			for (int num90 = 0; (float)num90 < 6f * num76; num90++)
			{
				WorldGen.TileRunner(num77 + WorldGen.genRand.Next(-(int)(125f * num76), (int)(125f * num76)), num79 + WorldGen.genRand.Next(-(int)(125f * num76), (int)(125f * num76)), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(67, 69));
			}
			GenVars.mudWall = true;
			Main.statusText = Lang.gen[11] + " 45%";
			num77 = (num80 + num83 + num88) / 3;
			num79 = (num81 + num84 + num89) / 3;
			WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next((int)(400f * num76), (int)(600f * num76)), 10000, 59, addTile: false, 0f, -20f, noYChange: true);
			JungleRunner(num77, num79);
			Main.statusText = Lang.gen[11] + " 60%";
			GenVars.mudWall = false;
			for (int num91 = 0; num91 < Main.maxTilesX / 10; num91++)
			{
				num77 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				num79 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
				while (Main.tile[num77, num79].WallType != 15)
				{
					num77 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					num79 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
				}
				WorldGen.MudWallRunner(num77, num79);
			}
			num77 = num85;
			num79 = num86;
			for (int num92 = 0; (float)num92 <= 20f * num76; num92++)
			{
				Main.statusText = Lang.gen[11] + " " + (int)(60f + (float)num92 / num76) + "%";
				num77 += WorldGen.genRand.Next((int)(-5f * num76), (int)(6f * num76));
				num79 += WorldGen.genRand.Next((int)(-5f * num76), (int)(6f * num76));
				WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next(40, 100), WorldGen.genRand.Next(300, 500), 59);
			}
			for (int num93 = 0; (float)num93 <= 10f * num76; num93++)
			{
				Main.statusText = Lang.gen[11] + " " + (int)(80f + (float)num93 / num76 * 2f) + "%";
				num77 = num85 + WorldGen.genRand.Next((int)(-600f * num76), (int)(600f * num76));
				num79 = num86 + WorldGen.genRand.Next((int)(-200f * num76), (int)(200f * num76));
				while (num77 < 1 || num77 >= Main.maxTilesX - 1 || num79 < 1 || num79 >= Main.maxTilesY - 1 || Main.tile[num77, num79].TileType != 59)
				{
					num77 = num85 + WorldGen.genRand.Next((int)(-600f * num76), (int)(600f * num76));
					num79 = num86 + WorldGen.genRand.Next((int)(-200f * num76), (int)(200f * num76));
				}
				for (int num94 = 0; (float)num94 < 8f * num76; num94++)
				{
					num77 += WorldGen.genRand.Next(-30, 31);
					num79 += WorldGen.genRand.Next(-30, 31);
					int type4 = -1;
					if (WorldGen.genRand.NextBool(7))
					{
						type4 = -2;
					}
					WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next(10, 20), WorldGen.genRand.Next(30, 70), type4);
				}
			}
			for (int num95 = 0; (float)num95 <= 300f * num76; num95++)
			{
				num77 = num85 + WorldGen.genRand.Next((int)(-600f * num76), (int)(600f * num76));
				num79 = num86 + WorldGen.genRand.Next((int)(-200f * num76), (int)(200f * num76));
				while (num77 < 1 || num77 >= Main.maxTilesX - 1 || num79 < 1 || num79 >= Main.maxTilesY - 1 || Main.tile[num77, num79].TileType != 59)
				{
					num77 = num85 + WorldGen.genRand.Next((int)(-600f * num76), (int)(600f * num76));
					num79 = num86 + WorldGen.genRand.Next((int)(-200f * num76), (int)(200f * num76));
				}
				WorldGen.TileRunner(num77, num79, WorldGen.genRand.Next(4, 10), WorldGen.genRand.Next(5, 30), 1);
				if (WorldGen.genRand.NextBool(4))
				{
					int type5 = WorldGen.genRand.Next(63, 69);
					WorldGen.TileRunner(num77 + WorldGen.genRand.Next(-1, 2), num79 + WorldGen.genRand.Next(-1, 2), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(4, 8), type5);
				}
			}
			num77 = num85;
			num79 = num86;
			float num96 = WorldGen.genRand.Next(6, 10);
			float num97 = Main.maxTilesX / 4200;
			num96 *= num97;
			for (int num98 = 0; (float)num98 < num96; num98++)
			{
				bool flag5 = true;
				while (flag5)
				{
					num77 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					num79 = WorldGen.genRand.Next((int)(Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 300);
					if (Main.tile[num77, num79].TileType != 59)
					{
						continue;
					}
					flag5 = false;
					int num99 = WorldGen.genRand.Next(2, 4);
					int num100 = WorldGen.genRand.Next(2, 4);
					for (int num101 = num77 - num99 - 1; num101 <= num77 + num99 + 1; num101++)
					{
						for (int num102 = num79 - num100 - 1; num102 <= num79 + num100 + 1; num102++)
						{
							Tile tile = Main.tile[num101, num102];
							tile.HasTile = true;
							tile.TileType = 45;
							tile.LiquidAmount = 0;
							tile.LiquidType = 0;
						}
					}
					for (int num103 = num77 - num99; num103 <= num77 + num99; num103++)
					{
						for (int num104 = num79 - num100; num104 <= num79 + num100; num104++)
						{
							Tile tile = Main.tile[num103, num104];
							tile.HasTile = false;
						}
					}
					bool flag6 = false;
					int num105 = 0;
					while (!flag6 && num105 < 100)
					{
						num105++;
						int num106 = WorldGen.genRand.Next(num77 - num99, num77 + num99 + 1);
						int num107 = WorldGen.genRand.Next(num79 - num100, num79 + num100 - 2);
						WorldGen.PlaceTile(num106, num107, 4, mute: true);
						if (Main.tile[num106, num107].TileType == 4)
						{
							flag6 = true;
						}
					}
					for (int num108 = num77 - num99 - 1; num108 <= num77 + num99 + 1; num108++)
					{
						for (int num109 = num79 + num100 - 2; num109 <= num79 + num100; num109++)
						{
							Tile tile = Main.tile[num108, num109];
							tile.HasTile = false;
						}
					}
					for (int num110 = num77 - num99 - 1; num110 <= num77 + num99 + 1; num110++)
					{
						for (int num111 = num79 + num100 - 2; num111 <= num79 + num100 - 1; num111++)
						{
							Tile tile = Main.tile[num110, num111];
							tile.HasTile = false;
						}
					}
					for (int num112 = num77 - num99 - 1; num112 <= num77 + num99 + 1; num112++)
					{
						int num113 = 4;
						int num114 = num79 + num100 + 2;
						while (!Main.tile[num112, num114].HasTile && num114 < Main.maxTilesY && num113 > 0)
						{
							Tile tile = Main.tile[num112, num114];
							tile.HasTile = true;
							tile.TileType = 59;
							num114++;
							num113--;
						}
					}
					num99 -= WorldGen.genRand.Next(1, 3);
					int num115 = num79 - num100 - 2;
					while (num99 > -1)
					{
						for (int num116 = num77 - num99 - 1; num116 <= num77 + num99 + 1; num116++)
						{
							Tile tile = Main.tile[num116, num115];
							tile.HasTile = true;
							tile.TileType = 45;
						}
						num99 -= WorldGen.genRand.Next(1, 3);
						num115--;
					}
					GenVars.JChestX[GenVars.numJChests] = num77;
					GenVars.JChestY[GenVars.numJChests] = num79;
					GenVars.numJChests++;
				}
			}
			for (int num117 = 0; num117 < Main.maxTilesX; num117++)
			{
				for (int num118 = 0; num118 < Main.maxTilesY; num118++)
				{
					if (Main.tile[num117, num118].HasTile)
					{
						try
						{
							WorldGen.grassSpread = 0;
							WorldGen.SpreadGrass(num117, num118, 59, 60);
						}
						catch
						{
							WorldGen.grassSpread = 0;
							WorldGen.SpreadGrass(num117, num118, 59, 60, repeat: false);
						}
					}
				}
			}
		}

		private static void FloatingIslands(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;

			GenVars.numIslandHouses = 0;
			houseCount = 0;
			Main.statusText = (string)Lang.gen[12];
			for (int num119 = 0; num119 < (int)((double)Main.maxTilesX * 0.0008); num119++)
			{
				int num120 = 0;
				bool flag7 = false;
				int num121 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.1), (int)((double)Main.maxTilesX * 0.9));
				bool flag8 = false;
				while (!flag8)
				{
					flag8 = true;
					while (num121 > Main.maxTilesX / 2 - 80 && num121 < Main.maxTilesX / 2 + 80)
					{
						num121 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.1), (int)((double)Main.maxTilesX * 0.9));
					}
					for (int num122 = 0; num122 < GenVars.numIslandHouses; num122++)
					{
						if (num121 > fihX[num122] - 80 && num121 < fihX[num122] + 80)
						{
							num120++;
							flag8 = false;
							break;
						}
					}
					if (num120 >= 200)
					{
						flag7 = true;
						break;
					}
				}
				if (flag7)
				{
					continue;
				}
				for (int num123 = 200; (double)num123 < Main.worldSurface; num123++)
				{
					if (Main.tile[num121, num123].HasTile)
					{
						int num124 = num121;
						int num125 = WorldGen.genRand.Next(90, num123 - 100);
						while ((double)num125 > num5 - 50.0)
						{
							num125--;
						}
						FloatingIsland(num124, num125);
						fihX[GenVars.numIslandHouses] = num124;
						fihY[GenVars.numIslandHouses] = num125;
						GenVars.numIslandHouses++;
						break;
					}
				}
			}
		}

		private static void MushroomPatches(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[13];
			for (int num126 = 0; num126 < Main.maxTilesX / 500; num126++)
			{
				int i2 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.3), (int)((double)Main.maxTilesX * 0.7));
				int j2 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 350);
				ShroomPatch(i2, j2);
			}
			for (int num127 = 0; num127 < Main.maxTilesX; num127++)
			{
				for (int num128 = (int)Main.worldSurface; num128 < Main.maxTilesY; num128++)
				{
					if (Main.tile[num127, num128].HasTile)
					{
						WorldGen.grassSpread = 0;
						WorldGen.SpreadGrass(num127, num128, 59, 70, repeat: false);
					}
				}
			}
		}

		private static void DirtToMud(GenerationProgress progres, GameConfiguration configurations)
		{
			double num7 = localVar7;

			Main.statusText = (string)Lang.gen[14];
			for (int num129 = 0; num129 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.001); num129++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 40), 59);
			}
		}

		private static void Silt(GenerationProgress progres, GameConfiguration configurations)
		{
			double num8 = localVar8;

			Main.statusText = (string)Lang.gen[15];
			for (int num130 = 0; num130 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0001); num130++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num8, Main.maxTilesY), WorldGen.genRand.Next(5, 12), WorldGen.genRand.Next(15, 50), 123);
			}
			for (int num131 = 0; num131 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0005); num131++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num8, Main.maxTilesY), WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 5), 123);
			}
		}

		private static void Shinies(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			double num6 = localVar6;
			double num7 = localVar7;
			double num8 = localVar8;

			Main.statusText = (string)Lang.gen[16];
			for (int num132 = 0; num132 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); num132++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6), WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), 7);
			}
			for (int num133 = 0; num133 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 8E-05); num133++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 7), 7);
			}
			for (int num134 = 0; num134 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num134++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 7);
			}
			for (int num135 = 0; num135 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); num135++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6), WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(2, 5), 6);
			}
			for (int num136 = 0; num136 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 8E-05); num136++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8), WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), 6);
			}
			for (int num137 = 0; num137 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num137++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 6);
			}
			for (int num138 = 0; num138 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2.6E-05); num138++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8), WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), 9);
			}
			for (int num139 = 0; num139 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00015); num139++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 9);
			}
			for (int num140 = 0; num140 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00017); num140++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5), WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 9);
			}
			for (int num141 = 0; num141 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00012); num141++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), 8);
			}
			for (int num142 = 0; num142 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00012); num142++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5 - 20), WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), 8);
			}
			for (int num143 = 0; num143 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num143++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), 22);
			}
		}

		private static void Webs(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;

			Main.statusText = (string)Lang.gen[17];
			for (int num144 = 0; num144 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0006); num144++)
			{
				int num145 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				int num146 = WorldGen.genRand.Next((int)num5, Main.maxTilesY - 20);
				if (num144 < GenVars.numMCaves)
				{
					num145 = GenVars.mCaveX[num144];
					num146 = GenVars.mCaveY[num144];
				}
				if (!Main.tile[num145, num146].HasTile && ((double)num146 > Main.worldSurface || Main.tile[num145, num146].WallType > 0))
				{
					while (!Main.tile[num145, num146].HasTile && num146 > (int)num5)
					{
						num146--;
					}
					num146++;
					int num147 = 1;
					if (WorldGen.genRand.NextBool(2))
					{
						num147 = -1;
					}
					for (; !Main.tile[num145, num146].HasTile && num145 > 10 && num145 < Main.maxTilesX - 10; num145 += num147)
					{
					}
					num145 -= num147;
					if ((double)num146 > Main.worldSurface || Main.tile[num145, num146].WallType > 0)
					{
						WorldGen.TileRunner(num145, num146, WorldGen.genRand.Next(4, 11), WorldGen.genRand.Next(2, 4), 51, addTile: true, num147, -1f, noYChange: false, overRide: false);
					}
				}
			}
		}

		private static void Underworld(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = Lang.gen[18] + " 0%";
			int num148 = Main.maxTilesY - WorldGen.genRand.Next(150, 190);
			for (int num149 = 0; num149 < Main.maxTilesX; num149++)
			{
				num148 += WorldGen.genRand.Next(-3, 4);
				if (num148 < Main.maxTilesY - 190)
				{
					num148 = Main.maxTilesY - 190;
				}
				if (num148 > Main.maxTilesY - 160)
				{
					num148 = Main.maxTilesY - 160;
				}
				for (int num150 = num148 - 20 - WorldGen.genRand.Next(3); num150 < Main.maxTilesY; num150++)
				{
					if (num150 >= num148)
					{
						Tile tile = Main.tile[num149, num150];
						tile.HasTile = false;
						tile.LiquidType = LiquidID.Lava;
						tile.LiquidAmount = 0;
					}
					else
					{
						Main.tile[num149, num150].TileType = 57;
					}
				}
			}
			int num151 = Main.maxTilesY - WorldGen.genRand.Next(40, 70);
			for (int num152 = 10; num152 < Main.maxTilesX - 10; num152++)
			{
				num151 += WorldGen.genRand.Next(-10, 11);
				if (num151 > Main.maxTilesY - 60)
				{
					num151 = Main.maxTilesY - 60;
				}
				if (num151 < Main.maxTilesY - 100)
				{
					num151 = Main.maxTilesY - 120;
				}
				for (int num153 = num151; num153 < Main.maxTilesY - 10; num153++)
				{
					if (!Main.tile[num152, num153].HasTile)
					{
						Tile tile = Main.tile[num152, num153];
						tile.LiquidType = LiquidID.Lava;
						tile.LiquidAmount = byte.MaxValue;
					}
				}
			}
			for (int num154 = 0; num154 < Main.maxTilesX; num154++)
			{
				if (WorldGen.genRand.NextBool(50))
				{
					int num155 = Main.maxTilesY - 65;
					while (!Main.tile[num154, num155].HasTile && num155 > Main.maxTilesY - 135)
					{
						num155--;
					}
					WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), num155 + WorldGen.genRand.Next(20, 50), WorldGen.genRand.Next(15, 20), 1000, 57, addTile: true, 0f, WorldGen.genRand.Next(1, 3), noYChange: true);
				}
			}
			Liquid.QuickWater(-2);
			for (int num156 = 0; num156 < Main.maxTilesX; num156++)
			{
				float num157 = (float)num156 / (float)(Main.maxTilesX - 1);
				Main.statusText = Lang.gen[18] + " " + (int)(num157 * 100f / 2f + 50f) + "%";
				if (WorldGen.genRand.NextBool(13))
				{
					int num158 = Main.maxTilesY - 65;
					while ((Main.tile[num156, num158].LiquidAmount > 0 || Main.tile[num156, num158].HasTile) && num158 > Main.maxTilesY - 140)
					{
						num158--;
					}
					WorldGen.TileRunner(num156, num158 - WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(5, 30), 1000, 57, addTile: true, 0f, WorldGen.genRand.Next(1, 3), noYChange: true);
					float num159 = WorldGen.genRand.Next(1, 3);
					if (WorldGen.genRand.NextBool(3))
					{
						num159 *= 0.5f;
					}
					if (WorldGen.genRand.NextBool(2))
					{
						WorldGen.TileRunner(num156, num158 - WorldGen.genRand.Next(2, 5), (int)((float)WorldGen.genRand.Next(5, 15) * num159), (int)((float)WorldGen.genRand.Next(10, 15) * num159), 57, addTile: true, 1f, 0.3f);
					}
					if (WorldGen.genRand.NextBool(2))
					{
						num159 = WorldGen.genRand.Next(1, 3);
						WorldGen.TileRunner(num156, num158 - WorldGen.genRand.Next(2, 5), (int)((float)WorldGen.genRand.Next(5, 15) * num159), (int)((float)WorldGen.genRand.Next(10, 15) * num159), 57, addTile: true, -1f, 0.3f);
					}
					WorldGen.TileRunner(num156 + WorldGen.genRand.Next(-10, 10), num158 + WorldGen.genRand.Next(-10, 10), WorldGen.genRand.Next(5, 15), WorldGen.genRand.Next(5, 10), -2, addTile: false, WorldGen.genRand.Next(-1, 3), WorldGen.genRand.Next(-1, 3));
					if (WorldGen.genRand.NextBool(3))
					{
						WorldGen.TileRunner(num156 + WorldGen.genRand.Next(-10, 10), num158 + WorldGen.genRand.Next(-10, 10), WorldGen.genRand.Next(10, 30), WorldGen.genRand.Next(10, 20), -2, addTile: false, WorldGen.genRand.Next(-1, 3), WorldGen.genRand.Next(-1, 3));
					}
					if (WorldGen.genRand.NextBool(5))
					{
						WorldGen.TileRunner(num156 + WorldGen.genRand.Next(-15, 15), num158 + WorldGen.genRand.Next(-15, 10), WorldGen.genRand.Next(15, 30), WorldGen.genRand.Next(5, 20), -2, addTile: false, WorldGen.genRand.Next(-1, 3), WorldGen.genRand.Next(-1, 3));
					}
				}
			}
			for (int num160 = 0; num160 < Main.maxTilesX; num160++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(20, Main.maxTilesX - 20), WorldGen.genRand.Next(Main.maxTilesY - 180, Main.maxTilesY - 10), WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(2, 7), -2);
			}
			for (int num161 = 0; num161 < Main.maxTilesX; num161++)
			{
				if (!Main.tile[num161, Main.maxTilesY - 145].HasTile)
				{
					Tile tile = Main.tile[num161, Main.maxTilesY - 145];
					tile.LiquidAmount = byte.MaxValue;
					tile.LiquidType = LiquidID.Lava;
				}
				if (!Main.tile[num161, Main.maxTilesY - 144].HasTile)
				{
					Tile tile = Main.tile[num161, Main.maxTilesY - 144];
					tile.LiquidAmount = byte.MaxValue;
					tile.LiquidType = LiquidID.Lava;
				}
			}
			for (int num162 = 0; num162 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008); num162++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY - 140, Main.maxTilesY), WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(3, 7), 58);
			}
			AddHellHouses();
		}

		private static void Lakes(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;

			int num163 = WorldGen.genRand.Next(2, (int)((double)Main.maxTilesX * 0.005));
			for (int num164 = 0; num164 < num163; num164++)
			{
				float num165 = (float)num164 / (float)num163;
				Main.statusText = Lang.gen[19] + " " + (int)(num165 * 100f) + "%";
				int num166 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
				while (num166 > Main.maxTilesX / 2 - 50 && num166 < Main.maxTilesX / 2 + 50)
				{
					num166 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
				}
				int num167;
				for (num167 = (int)num5 - 20; !Main.tile[num166, num167].HasTile; num167++)
				{
				}
				WorldGen.Lakinater(num166, num167); //unchanged
			}
		}

		private static void Dungeon(GenerationProgress progres, GameConfiguration configurations)
		{
			int num9 = localVar9;

			int num168 = 0;
			if (num9 == -1)
			{
				num168 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.05), (int)((double)Main.maxTilesX * 0.2));
				num9 = -1;
			}
			else
			{
				num168 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.8), (int)((double)Main.maxTilesX * 0.95));
				num9 = 1;
			}
			int y = (int)((Main.rockLayer + (double)Main.maxTilesY) / 2.0) + WorldGen.genRand.Next(-200, 200);
			MakeDungeon(num168, y);

			localVar9 = num9;
		}

		private static void Corruption(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			int num56 = 0;

			for (int num169 = 0; (double)num169 < (double)Main.maxTilesX * 0.00045; num169++)
			{
				float num170 = (float)((double)num169 / ((double)Main.maxTilesX * 0.00045));
				Main.statusText = Lang.gen[20] + " " + (int)(num170 * 100f) + "%";
				bool flag9 = false;
				int num171 = 0;
				int num172 = 0;
				int num173 = 0;
				while (!flag9)
				{
					int num174 = 0;
					flag9 = true;
					int num175 = Main.maxTilesX / 2;
					int num176 = 200;
					num171 = WorldGen.genRand.Next(320, Main.maxTilesX - 320);
					num172 = num171 - WorldGen.genRand.Next(200) - 100;
					num173 = num171 + WorldGen.genRand.Next(200) + 100;
					if (num172 < 285)
					{
						num172 = 285;
					}
					if (num173 > Main.maxTilesX - 285)
					{
						num173 = Main.maxTilesX - 285;
					}
					if (num171 > num175 - num176 && num171 < num175 + num176)
					{
						flag9 = false;
					}
					if (num172 > num175 - num176 && num172 < num175 + num176)
					{
						flag9 = false;
					}
					if (num173 > num175 - num176 && num173 < num175 + num176)
					{
						flag9 = false;
					}
					for (int num177 = num172; num177 < num173; num177++)
					{
						for (int num178 = 0; num178 < (int)Main.worldSurface; num178 += 5)
						{
							if (Main.tile[num177, num178].HasTile && Main.tileDungeon[Main.tile[num177, num178].TileType])
							{
								flag9 = false;
								break;
							}
							if (!flag9)
							{
								break;
							}
						}
					}
					if (num174 < 200 && GenVars.JungleX > num172 && GenVars.JungleX < num173)
					{
						num174++;
						flag9 = false;
					}
				}
				int num179 = 0;
				for (int num180 = num172; num180 < num173; num180++)
				{
					if (num179 > 0)
					{
						num179--;
					}
					if (num180 == num171 || num179 == 0)
					{
						for (int num181 = (int)num5; (double)num181 < Main.worldSurface - 1.0; num181++)
						{
							if (Main.tile[num180, num181].HasTile || Main.tile[num180, num181].WallType > 0)
							{
								if (num180 == num171)
								{
									num179 = 20;
									ChasmRunner(num180, num181, WorldGen.genRand.Next(150) + 150, makeOrb: true);
								}
								else if (WorldGen.genRand.NextBool(35) && num179 == 0)
								{
									num179 = 30;
									bool makeOrb = true;
									ChasmRunner(num180, num181, WorldGen.genRand.Next(50) + 50, makeOrb);
								}
								break;
							}
						}
					}
					for (int num182 = (int)num5; (double)num182 < Main.worldSurface - 1.0; num182++)
					{
						if (!Main.tile[num180, num182].HasTile)
						{
							continue;
						}
						int num183 = num182 + WorldGen.genRand.Next(10, 14);
						for (int num184 = num182; num184 < num183; num184++)
						{
							if ((Main.tile[num180, num184].TileType == 59 || Main.tile[num180, num184].TileType == 60) && num180 >= num172 + WorldGen.genRand.Next(5) && num180 < num173 - WorldGen.genRand.Next(5))
							{
								Main.tile[num180, num184].TileType = 0;
							}
						}
						break;
					}
				}
				double num185 = Main.worldSurface + 40.0;
				for (int num186 = num172; num186 < num173; num186++)
				{
					num185 += (double)WorldGen.genRand.Next(-2, 3);
					if (num185 < Main.worldSurface + 30.0)
					{
						num185 = Main.worldSurface + 30.0;
					}
					if (num185 > Main.worldSurface + 50.0)
					{
						num185 = Main.worldSurface + 50.0;
					}
					num56 = num186;
					bool flag10 = false;
					for (int num187 = (int)num5; (double)num187 < num185; num187++)
					{
						if (Main.tile[num56, num187].HasTile)
						{
							if (Main.tile[num56, num187].TileType == 53 && num56 >= num172 + WorldGen.genRand.Next(5) && num56 <= num173 - WorldGen.genRand.Next(5))
							{
								Main.tile[num56, num187].TileType = 0;
							}
							if (Main.tile[num56, num187].TileType == 0 && (double)num187 < Main.worldSurface - 1.0 && !flag10)
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(num56, num187, 0, 23);
							}
							flag10 = true;
							if (Main.tile[num56, num187].TileType == 1 && num56 >= num172 + WorldGen.genRand.Next(5) && num56 <= num173 - WorldGen.genRand.Next(5))
							{
								Main.tile[num56, num187].TileType = 25;
							}
							if (Main.tile[num56, num187].TileType == 2)
							{
								Main.tile[num56, num187].TileType = 23;
							}
						}
					}
				}
				for (int num188 = num172; num188 < num173; num188++)
				{
					for (int num189 = 0; num189 < Main.maxTilesY - 50; num189++)
					{
						if (!Main.tile[num188, num189].HasTile || Main.tile[num188, num189].TileType != 31)
						{
							continue;
						}
						int num190 = num188 - 13;
						int num191 = num188 + 13;
						int num192 = num189 - 13;
						int num193 = num189 + 13;
						for (int num194 = num190; num194 < num191; num194++)
						{
							if (num194 <= 10 || num194 >= Main.maxTilesX - 10)
							{
								continue;
							}
							for (int num195 = num192; num195 < num193; num195++)
							{
								if (Math.Abs(num194 - num188) + Math.Abs(num195 - num189) < 9 + WorldGen.genRand.Next(11) && !WorldGen.genRand.NextBool(3) && Main.tile[num194, num195].TileType != 31)
								{
									Tile tile = Main.tile[num194, num195];
									tile.HasTile = true;
									tile.TileType = 25;
									if (Math.Abs(num194 - num188) <= 1 && Math.Abs(num195 - num189) <= 1)
									{
										Tile tile2 = Main.tile[num194, num195];
										tile2.HasTile = false;
									}
								}
								if (Main.tile[num194, num195].TileType != 31 && Math.Abs(num194 - num188) <= 2 + WorldGen.genRand.Next(3) && Math.Abs(num195 - num189) <= 2 + WorldGen.genRand.Next(3))
								{
									Tile tile = Main.tile[num194, num195];
									tile.HasTile = false;
								}
							}
						}
					}
				}
			}
		}

		private static void MountainCaves(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[21];
			for (int num196 = 0; num196 < GenVars.numMCaves; num196++)
			{
				int i3 = GenVars.mCaveX[num196];
				int j3 = GenVars.mCaveY[num196];
				CaveOpenater(i3, j3);
				Cavinator(i3, j3, WorldGen.genRand.Next(40, 50));
			}
		}

		private static void Beaches(GenerationProgress progres, GameConfiguration configurations)
		{
			int num9 = localVar9;

			int num197 = 0;
			int num198 = 0;
			int num199 = 20;
			int num200 = Main.maxTilesX - 20;
			Main.statusText = (string)Lang.gen[22];
			for (int num201 = 0; num201 < 2; num201++)
			{
				int num202 = 0;
				int num203 = 0;
				if (num201 == 0)
				{
					num202 = 0;
					num203 = WorldGen.genRand.Next(125, 200) + 50;
					if (num9 == 1)
					{
						num203 = 275;
					}
					int num204 = 0;
					float num205 = 1f;
					int num206;
					for (num206 = 0; !Main.tile[num203 - 1, num206].HasTile; num206++)
					{
					}
					num197 = num206;
					num206 += WorldGen.genRand.Next(1, 5);
					for (int num207 = num203 - 1; num207 >= num202; num207--)
					{
						num204++;
						if (num204 < 3)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.2f;
						}
						else if (num204 < 6)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.15f;
						}
						else if (num204 < 9)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
						}
						else if (num204 < 15)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.07f;
						}
						else if (num204 < 50)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
						}
						else if (num204 < 75)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.04f;
						}
						else if (num204 < 100)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.03f;
						}
						else if (num204 < 125)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.02f;
						}
						else if (num204 < 150)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
						}
						else if (num204 < 175)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.005f;
						}
						else if (num204 < 200)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.001f;
						}
						else if (num204 < 230)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
						}
						else if (num204 < 235)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
						}
						else if (num204 < 240)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
						}
						else if (num204 < 245)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
						}
						else if (num204 < 255)
						{
							num205 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
						}
						if (num204 == 235)
						{
							num200 = num207;
						}
						if (num204 == 235)
						{
							num199 = num207;
						}
						int num208 = WorldGen.genRand.Next(15, 20);
						for (int num209 = 0; (float)num209 < (float)num206 + num205 + (float)num208; num209++)
						{
							if ((float)num209 < (float)num206 + num205 * 0.75f - 3f)
							{
								Tile tile = Main.tile[num207, num209];
								tile.HasTile = false;
								if (num209 > num206)
								{
									Main.tile[num207, num209].LiquidAmount = byte.MaxValue;
								}
								else if (num209 == num206)
								{
									Main.tile[num207, num209].LiquidAmount = 127;
								}
							}
							else if (num209 > num206)
							{
								Tile tile = Main.tile[num207, num209];
								tile.TileType = 53;
								tile.HasTile = true;
							}
							Main.tile[num207, num209].WallType = 0;
						}
					}
					continue;
				}
				num202 = Main.maxTilesX - WorldGen.genRand.Next(125, 200) - 50;
				num203 = Main.maxTilesX;
				if (num9 == -1)
				{
					num202 = Main.maxTilesX - 275;
				}
				float num210 = 1f;
				int num211 = 0;
				int num212;
				for (num212 = 0; !Main.tile[num202, num212].HasTile; num212++)
				{
				}
				num198 = num212;
				num212 += WorldGen.genRand.Next(1, 5);
				for (int num213 = num202; num213 < num203; num213++)
				{
					num211++;
					if (num211 < 3)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.2f;
					}
					else if (num211 < 6)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.15f;
					}
					else if (num211 < 9)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
					}
					else if (num211 < 15)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.07f;
					}
					else if (num211 < 50)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
					}
					else if (num211 < 75)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.04f;
					}
					else if (num211 < 100)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.03f;
					}
					else if (num211 < 125)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.02f;
					}
					else if (num211 < 150)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
					}
					else if (num211 < 175)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.005f;
					}
					else if (num211 < 200)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.001f;
					}
					else if (num211 < 230)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
					}
					else if (num211 < 235)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
					}
					else if (num211 < 240)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
					}
					else if (num211 < 245)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
					}
					else if (num211 < 255)
					{
						num210 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
					}
					if (num211 == 235)
					{
						num200 = num213;
					}
					int num214 = WorldGen.genRand.Next(15, 20);
					for (int num215 = 0; (float)num215 < (float)num212 + num210 + (float)num214; num215++)
					{
						if ((float)num215 < (float)num212 + num210 * 0.75f - 3f && (double)num215 < Main.worldSurface - 2.0)
						{
							Tile tile = Main.tile[num213, num215];
							tile.HasTile = false;
							if (num215 > num212)
							{
								Main.tile[num213, num215].LiquidAmount = byte.MaxValue;
							}
							else if (num215 == num212)
							{
								Main.tile[num213, num215].LiquidAmount = 127;
							}
						}
						else if (num215 > num212)
						{
							Tile tile = Main.tile[num213, num215];
							tile.TileType = 53;
							tile.HasTile = true;
						}
						Main.tile[num213, num215].WallType = 0;
					}
				}
			}

			localVar197 = num197;
			localVar198 = num198;
			localVar199 = num199;
			localVar200 = num200;
		}

		private static void Gems(GenerationProgress progres, GameConfiguration configurations)
		{
			int num197 = localVar197;
			int num198 = localVar198;
			int num199 = localVar199;
			int num200 = localVar200;

			for (; !Main.tile[num199, num197].HasTile; num197++)
			{
			}
			num197++;
			for (; !Main.tile[num200, num198].HasTile; num198++)
			{
			}
			num198++;
			Main.statusText = (string)Lang.gen[23];
			for (int num216 = 63; num216 <= 68; num216++)
			{
				float num217 = 0f;
				switch (num216)
				{
					case 67:
						num217 = (float)Main.maxTilesX * 0.5f;
						break;
					case 66:
						num217 = (float)Main.maxTilesX * 0.45f;
						break;
					case 63:
						num217 = (float)Main.maxTilesX * 0.3f;
						break;
					case 65:
						num217 = (float)Main.maxTilesX * 0.25f;
						break;
					case 64:
						num217 = (float)Main.maxTilesX * 0.1f;
						break;
					case 68:
						num217 = (float)Main.maxTilesX * 0.05f;
						break;
				}
				num217 *= 0.2f;
				for (int num218 = 0; (float)num218 < num217; num218++)
				{
					int num219 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int num220 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
					while (Main.tile[num219, num220].TileType != 1)
					{
						num219 = WorldGen.genRand.Next(0, Main.maxTilesX);
						num220 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
					}
					WorldGen.TileRunner(num219, num220, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), num216);
				}
			}
			for (int num221 = 0; num221 < 2; num221++)
			{
				int num222 = 1;
				int num223 = 5;
				int num224 = Main.maxTilesX - 5;
				if (num221 == 1)
				{
					num222 = -1;
					num223 = Main.maxTilesX - 5;
					num224 = 5;
				}
				for (int num225 = num223; num225 != num224; num225 += num222)
				{
					for (int num226 = 10; num226 < Main.maxTilesY - 10; num226++)
					{
						if (!Main.tile[num225, num226].HasTile || Main.tile[num225, num226].TileType != 53 || !Main.tile[num225, num226 + 1].HasTile || Main.tile[num225, num226 + 1].TileType != 53)
						{
							continue;
						}
						int num227 = num225 + num222;
						int num228 = num226 + 1;
						if (!Main.tile[num227, num226].HasTile && !Main.tile[num227, num226 + 1].HasTile)
						{
							for (; !Main.tile[num227, num228].HasTile; num228++)
							{
							}
							num228--;
							Tile tile = Main.tile[num225, num226];
							tile.HasTile = false;
							Tile tile2 = Main.tile[num227, num228];
							tile2.HasTile = true;
							tile2.TileType = 53;
						}
					}
				}
			}
		}

		private static void GravitatingSand(GenerationProgress progres, GameConfiguration configurations)
		{
			for (int num229 = 0; num229 < Main.maxTilesX; num229++)
			{
				float num230 = (float)num229 / (float)(Main.maxTilesX - 1);
				Main.statusText = Lang.gen[24] + " " + (int)(num230 * 100f) + "%";
				for (int num231 = Main.maxTilesY - 5; num231 > 0; num231--)
				{
					if (Main.tile[num229, num231].HasTile)
					{
						if (Main.tile[num229, num231].TileType == 53)
						{
							for (int num232 = num231; !Main.tile[num229, num232 + 1].HasTile && num232 < Main.maxTilesY - 5; num232++)
							{
								Tile tile = Main.tile[num229, num232 + 1];
								tile.HasTile = true;
								tile.TileType = 53;
							}
						}
						else if (Main.tile[num229, num231].TileType == 123)
						{
							for (int num233 = num231; !Main.tile[num229, num233 + 1].HasTile && num233 < Main.maxTilesY - 5; num233++)
							{
								Tile tile = Main.tile[num229, num233 + 1];
								tile.HasTile = true;
								tile.TileType = 123;
								Tile tile2 = Main.tile[num229, num233];
								tile2.HasTile = false;
							}
						}
					}
				}
			}
		}

		private static void CleanUpDirt(GenerationProgress progres, GameConfiguration configurations)
		{
			for (int num234 = 3; num234 < Main.maxTilesX - 3; num234++)
			{
				float num235 = (float)num234 / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[25] + " " + (int)(num235 * 100f + 1f) + "%";
				bool flag11 = true;
				for (int num236 = 0; (double)num236 < Main.worldSurface; num236++)
				{
					if (flag11)
					{
						if (Main.tile[num234, num236].WallType == 2)
						{
							Main.tile[num234, num236].WallType = 0;
						}
						if (Main.tile[num234, num236].TileType != 53)
						{
							if (Main.tile[num234 - 1, num236].WallType == 2)
							{
								Main.tile[num234 - 1, num236].WallType = 0;
							}
							if (Main.tile[num234 - 2, num236].WallType == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num234 - 2, num236].WallType = 0;
							}
							if (Main.tile[num234 - 3, num236].WallType == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num234 - 3, num236].WallType = 0;
							}
							if (Main.tile[num234 + 1, num236].WallType == 2)
							{
								Main.tile[num234 + 1, num236].WallType = 0;
							}
							if (Main.tile[num234 + 2, num236].WallType == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num234 + 2, num236].WallType = 0;
							}
							if (Main.tile[num234 + 3, num236].WallType == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num234 + 3, num236].WallType = 0;
							}
							if (Main.tile[num234, num236].HasTile)
							{
								flag11 = false;
							}
						}
					}
					else if (Main.tile[num234, num236].WallType == 0 && Main.tile[num234, num236 + 1].WallType == 0 && Main.tile[num234, num236 + 2].WallType == 0 && Main.tile[num234, num236 + 3].WallType == 0 && Main.tile[num234, num236 + 4].WallType == 0 && Main.tile[num234 - 1, num236].WallType == 0 && Main.tile[num234 + 1, num236].WallType == 0 && Main.tile[num234 - 2, num236].WallType == 0 && Main.tile[num234 + 2, num236].WallType == 0 && !Main.tile[num234, num236].HasTile && !Main.tile[num234, num236 + 1].HasTile && !Main.tile[num234, num236 + 2].HasTile && !Main.tile[num234, num236 + 3].HasTile)
					{
						flag11 = true;
					}
				}
			}
		}

		private static void Altars(GenerationProgress progres, GameConfiguration configurations)
		{
			double num6 = localVar6;

			for (int num237 = 0; num237 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num237++)
			{
				float num238 = (float)((double)num237 / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				Main.statusText = Lang.gen[26] + " " + (int)(num238 * 100f + 1f) + "%";
				bool flag12 = false;
				int num239 = 0;
				while (!flag12)
				{
					int num240 = WorldGen.genRand.Next(1, Main.maxTilesX);
					int num241 = (int)(num6 + 20.0);
					WorldGen.Place3x2(num240, num241, 26);
					if (Main.tile[num240, num241].TileType == 26)
					{
						flag12 = true;
						continue;
					}
					num239++;
					if (num239 >= 10000)
					{
						flag12 = true;
					}
				}
			}
		}

		private static void WetJungle(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			int num56 = 0;

			for (int num242 = 0; num242 < Main.maxTilesX; num242++)
			{
				num56 = num242;
				for (int num243 = (int)num5; (double)num243 < Main.worldSurface - 1.0; num243++)
				{
					if (Main.tile[num56, num243].HasTile)
					{
						if (Main.tile[num56, num243].TileType == 60)
						{
							Main.tile[num56, num243 - 1].LiquidAmount = byte.MaxValue;
							Main.tile[num56, num243 - 2].LiquidAmount = byte.MaxValue;
						}
						break;
					}
				}
			}
		}

		private static void RemoveWaterFromSand(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			int num56 = 0;

			for (int num244 = 400; num244 < Main.maxTilesX - 400; num244++)
			{
				num56 = num244;
				for (int num245 = (int)num5; (double)num245 < Main.worldSurface - 1.0; num245++)
				{
					if (!Main.tile[num56, num245].HasTile)
					{
						continue;
					}
					if (Main.tile[num56, num245].TileType == 53)
					{
						int num246 = num245;
						while ((double)num246 > num5)
						{
							num246--;
							Main.tile[num56, num246].LiquidAmount = 0;
						}
					}
					break;
				}
			}
		}

		private static void SettleLiquids(GenerationProgress progres, GameConfiguration configurations)
		{
			Liquid.QuickWater(3);
			WorldGen.WaterCheck();
			int num247 = 0;
			Liquid.quickSettle = true;
			while (num247 < 10)
			{
				int num248 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				num247++;
				float num249 = 0f;
				while (Liquid.numLiquid > 0)
				{
					float num250 = (float)(num248 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num248;
					if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num248)
					{
						num248 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
					}
					if (num250 > num249)
					{
						num249 = num250;
					}
					else
					{
						num250 = num249;
					}
					if (num247 == 1)
					{
						Main.statusText = Lang.gen[27] + " " + (int)(num250 * 100f / 3f + 33f) + "%";
					}
					int num251 = 10;
					if (num247 > num251)
					{
						num251 = num247;
					}
					Liquid.UpdateLiquid();
				}
				WorldGen.WaterCheck();
				Main.statusText = Lang.gen[27] + " " + (int)((float)num247 * 10f / 3f + 66f) + "%";
			}
			Liquid.quickSettle = false;
		}

		private static void LifeCrystals(GenerationProgress progres, GameConfiguration configurations)
		{
			double num6 = localVar6;

			float num252 = Main.maxTilesX / 4200;
			for (int num253 = 0; num253 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num253++)
			{
				float num254 = (float)((double)num253 / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				Main.statusText = Lang.gen[28] + " " + (int)(num254 * 100f + 1f) + "%";
				bool flag13 = false;
				int num255 = 0;
				while (!flag13)
				{
					if (WorldGen.AddLifeCrystal(WorldGen.genRand.Next(1, Main.maxTilesX), WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY)))
					{
						flag13 = true;
						continue;
					}
					num255++;
					if (num255 >= 10000)
					{
						flag13 = true;
					}
				}
			}

			localVar252 = num252;
		}

		private static void Statues(GenerationProgress progres, GameConfiguration configurations)
		{
			float num252 = localVar252;
			double num6 = localVar6;

			int num256 = 0;
			for (int num257 = 0; (float)num257 < 82f * num252; num257++)
			{
				if (num256 > 41)
				{
					num256 = 0;
				}
				float num258 = (float)num257 / (200f * num252);
				Main.statusText = Lang.gen[29] + " " + (int)(num258 * 100f + 1f) + "%";
				bool flag14 = false;
				int num259 = 0;
				while (!flag14)
				{
					int num260 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					int num261;
					for (num261 = WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY - 300); !Main.tile[num260, num261].HasTile; num261++)
					{
					}
					num261--;
					PlaceTile_Old(num260, num261, 105, mute: true, forced: true, -1, num256);
					if (Main.tile[num260, num261].HasTile && Main.tile[num260, num261].TileType == 105)
					{
						flag14 = true;
						num256++;
						continue;
					}
					num259++;
					if (num259 >= 10000)
					{
						flag14 = true;
					}
				}
			}
		}

		private static void BuriedChests(GenerationProgress progres, GameConfiguration configurations)
		{
			double num6 = localVar6;
			float num252 = localVar252;

			for (int num262 = 0; num262 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05); num262++)
			{
				float num263 = (float)((double)num262 / ((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05));
				Main.statusText = Lang.gen[30] + " " + (int)(num263 * 100f + 1f) + "%";
				bool flag15 = false;
				int num264 = 0;
				while (!flag15)
				{
					int num265 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					int num266 = WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY - 230);
					if ((float)num262 <= 3f * num252)
					{
						num266 = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY - 50);
					}
					while (Main.tile[num265, num266].WallType == 7 || Main.tile[num265, num266].WallType == 8 || Main.tile[num265, num266].WallType == 9)
					{
						num265 = WorldGen.genRand.Next(1, Main.maxTilesX);
						num266 = WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY - 230);
						if (num262 <= 3)
						{
							num266 = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY - 50);
						}
					}
					if (AddBuriedChest(num265, num266))
					{
						flag15 = true;
						if (WorldGen.genRand.NextBool(2))
						{
							int num267;
							for (num267 = num266; Main.tile[num265, num267].TileType != 21 && num267 < Main.maxTilesY - 300; num267++)
							{
							}
							if (num266 < Main.maxTilesY - 300)
							{
								MineHouse(num265, num267);
							}
						}
					}
					else
					{
						num264++;
						if (num264 >= 5000)
						{
							flag15 = true;
						}
					}
				}
			}
		}

		private static void SurfaceChests(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;

			for (int num268 = 0; num268 < (int)((double)Main.maxTilesX * 0.005); num268++)
			{
				float num269 = (float)((double)num268 / ((double)Main.maxTilesX * 0.005));
				Main.statusText = Lang.gen[31] + " " + (int)(num269 * 100f + 1f) + "%";
				bool flag16 = false;
				int num270 = 0;
				while (!flag16)
				{
					int num271 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
					int num272 = WorldGen.genRand.Next((int)num5, (int)Main.worldSurface);
					bool flag17 = false;
					if (Main.tile[num271, num272].WallType == 2 && !Main.tile[num271, num272].HasTile)
					{
						flag17 = true;
					}
					if (flag17 && AddBuriedChest(num271, num272, 0, notNearOtherChests: true))
					{
						flag16 = true;
						continue;
					}
					num270++;
					if (num270 >= 2000)
					{
						flag16 = true;
					}
				}
			}
		}

		private static void JungleChestsPlacement(GenerationProgress progres, GameConfiguration configurations)
		{
			int num273 = 0;
			for (int num274 = 0; num274 < GenVars.numJChests; num274++)
			{
				float num275 = num274 / GenVars.numJChests;
				Main.statusText = Lang.gen[32] + " " + (int)(num275 * 100f + 1f) + "%";
				num273++;
				int contain = 211;
				switch (num273)
				{
					case 1:
						contain = 211;
						break;
					case 2:
						contain = 212;
						break;
					case 3:
						contain = 213;
						break;
				}
				if (num273 > 3)
				{
					num273 = 0;
				}
				if (AddBuriedChest(GenVars.JChestX[num274] + WorldGen.genRand.Next(2), GenVars.JChestY[num274], contain))
				{
					continue;
				}
				for (int num276 = GenVars.JChestX[num274]; num276 <= GenVars.JChestX[num274] + 1; num276++)
				{
					for (int num277 = GenVars.JChestY[num274]; num277 <= GenVars.JChestY[num274] + 1; num277++)
					{
						WorldGen.KillTile(num276, num277);
					}
				}
				AddBuriedChest(GenVars.JChestX[num274], GenVars.JChestY[num274], contain);
			}
		}

		private static void WaterChests(GenerationProgress progres, GameConfiguration configurations)
		{
			float num252 = localVar252;

			int num278 = 0;
			for (int num279 = 0; (float)num279 < 9f * num252; num279++)
			{
				float num280 = (float)num279 / (9f * num252);
				Main.statusText = Lang.gen[33] + " " + (int)(num280 * 100f + 1f) + "%";
				int num281 = 0;
				num278++;
				switch (num278)
				{
					case 1:
						num281 = 186;
						break;
					case 2:
						num281 = 277;
						break;
					default:
						num281 = 187;
						num278 = 0;
						break;
				}
				bool flag18 = false;
				while (!flag18)
				{
					int num282 = WorldGen.genRand.Next(1, Main.maxTilesX);
					int num283 = WorldGen.genRand.Next(1, Main.maxTilesY - 200);
					while (Main.tile[num282, num283].LiquidAmount < 200 || Main.tile[num282, num283].LiquidType == LiquidID.Lava)
					{
						num282 = WorldGen.genRand.Next(1, Main.maxTilesX);
						num283 = WorldGen.genRand.Next(1, Main.maxTilesY - 200);
					}
					flag18 = AddBuriedChest(num282, num283, num281);
				}
			}
		}

		private static void FloatingIslandHouses(GenerationProgress progres, GameConfiguration configurations)
		{
			for (int num284 = 0; num284 < GenVars.numIslandHouses; num284++)
			{
				IslandHouse(fihX[num284], fihY[num284]);
			}
		}

		private static void Traps(GenerationProgress progres, GameConfiguration configurations)
		{
			for (int num285 = 0; num285 < (int)((double)Main.maxTilesX * 0.05); num285++)
			{
				float num286 = (float)((double)num285 / ((double)Main.maxTilesX * 0.05));
				Main.statusText = Lang.gen[34] + " " + (int)(num286 * 100f + 1f) + "%";
				for (int num287 = 0; num287 < 1000; num287++)
				{
					int num288 = Main.rand.Next(200, Main.maxTilesX - 200);
					int num289 = Main.rand.Next((int)Main.worldSurface, Main.maxTilesY - 300);
					if (Main.tile[num288, num289].WallType == 0 && WorldGen.placeTrap(num288, num289))
					{
						break;
					}
				}
			}
		}

		private static void Pots(GenerationProgress progres, GameConfiguration configurations)
		{
			double num5 = localVar5;
			double num6 = localVar6;

			for (int num290 = 0; num290 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008); num290++)
			{
				float num291 = (float)((double)num290 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008));
				Main.statusText = Lang.gen[35] + " " + (int)(num291 * 100f + 1f) + "%";
				bool flag19 = false;
				int num292 = 0;
				while (!flag19)
				{
					int num293 = WorldGen.genRand.Next((int)num6, Main.maxTilesY - 10);
					if ((double)num291 > 0.93)
					{
						num293 = Main.maxTilesY - 150;
					}
					else if ((double)num291 > 0.75)
					{
						num293 = (int)num5;
					}
					int num294 = WorldGen.genRand.Next(1, Main.maxTilesX);
					bool flag20 = false;
					for (int num295 = num293; num295 < Main.maxTilesY; num295++)
					{
						if (!flag20)
						{
							if (Main.tile[num294, num295].HasTile && Main.tileSolid[Main.tile[num294, num295].TileType] && Main.tile[num294, num295 - 1].LiquidType != LiquidID.Lava)
							{
								flag20 = true;
							}
							continue;
						}
						if (PlacePot(num294, num295))
						{
							flag19 = true;
							break;
						}
						num292++;
						if (num292 >= 10000)
						{
							flag19 = true;
							break;
						}
					}
				}
			}
		}

		private static void Hellforge(GenerationProgress progres, GameConfiguration configurations)
		{
			for (int num296 = 0; num296 < Main.maxTilesX / 200; num296++)
			{
				float num297 = num296 / (Main.maxTilesX / 200);
				Main.statusText = Lang.gen[36] + " " + (int)(num297 * 100f + 1f) + "%";
				bool flag21 = false;
				int num298 = 0;
				while (!flag21)
				{
					int num299 = WorldGen.genRand.Next(1, Main.maxTilesX);
					int num300 = WorldGen.genRand.Next(Main.maxTilesY - 250, Main.maxTilesY - 5);
					try
					{
						if (Main.tile[num299, num300].WallType != 13 && Main.tile[num299, num300].WallType != 14)
						{
							continue;
						}
						for (; !Main.tile[num299, num300].HasTile; num300++)
						{
						}
						num300--;
						WorldGen.PlaceTile(num299, num300, 77);
						if (Main.tile[num299, num300].TileType == 77)
						{
							flag21 = true;
							continue;
						}
						num298++;
						if (num298 >= 10000)
						{
							flag21 = true;
						}
					}
					catch
					{
					}
				}
			}
		}

		private static void SpreadingGrass(GenerationProgress progres, GameConfiguration configurations)
		{
			double num6 = localVar6;
			int num56 = 0;

			Main.statusText = (string)Lang.gen[37];
			for (int num301 = 0; num301 < Main.maxTilesX; num301++)
			{
				num56 = num301;
				bool flag22 = true;
				for (int num302 = 0; (double)num302 < Main.worldSurface - 1.0; num302++)
				{
					if (Main.tile[num56, num302].HasTile)
					{
						if (flag22 && Main.tile[num56, num302].TileType == 0)
						{
							try
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(num56, num302);
							}
							catch
							{
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(num56, num302, 0, 2, repeat: false);
							}
						}
						if ((double)num302 > num6)
						{
							break;
						}
						flag22 = false;
					}
					else if (Main.tile[num56, num302].WallType == 0)
					{
						flag22 = true;
					}
				}
			}
		}

		private static void CactusPalmTreesCoral(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[38];
			for (int num303 = 5; num303 < Main.maxTilesX - 5; num303++)
			{
				if (!WorldGen.genRand.NextBool(8))
				{
					continue;
				}
				for (int num304 = 0; (double)num304 < Main.worldSurface - 1.0; num304++)
				{
					if (!Main.tile[num303, num304].HasTile || Main.tile[num303, num304].TileType != 53 || Main.tile[num303, num304 - 1].HasTile || Main.tile[num303, num304 - 1].WallType != 0)
					{
						continue;
					}
					if (num303 < 250 || num303 > Main.maxTilesX - 250)
					{
						if (Main.tile[num303, num304 - 2].LiquidAmount == byte.MaxValue && Main.tile[num303, num304 - 3].LiquidAmount == byte.MaxValue && Main.tile[num303, num304 - 4].LiquidAmount == byte.MaxValue)
						{
							WorldGen.PlaceTile(num303, num304 - 1, 81, mute: true);
						}
					}
					else if (num303 > 400 && num303 < Main.maxTilesX - 400)
					{
						WorldGen.PlantCactus(num303, num304);
					}
				}
			}
		}

		private static void SpawnPoint(GenerationProgress progres, GameConfiguration configurations)
		{
			int num305 = 5;
			bool flag23 = true;
			while (flag23)
			{
				int num306 = Main.maxTilesX / 2 + WorldGen.genRand.Next(-num305, num305 + 1);
				for (int num307 = 0; num307 < Main.maxTilesY; num307++)
				{
					if (Main.tile[num306, num307].HasTile)
					{
						Main.spawnTileX = num306;
						Main.spawnTileY = num307;
						break;
					}
				}
				flag23 = false;
				num305++;
				if ((double)Main.spawnTileY > Main.worldSurface)
				{
					flag23 = true;
				}
				if (Main.tile[Main.spawnTileX, Main.spawnTileY - 1].LiquidAmount > 0)
				{
					flag23 = true;
				}
			}
			int num308 = 10;
			while ((double)Main.spawnTileY > Main.worldSurface)
			{
				int num309 = WorldGen.genRand.Next(Main.maxTilesX / 2 - num308, Main.maxTilesX / 2 + num308);
				for (int num310 = 0; num310 < Main.maxTilesY; num310++)
				{
					if (Main.tile[num309, num310].HasTile)
					{
						Main.spawnTileX = num309;
						Main.spawnTileY = num310;
						break;
					}
				}
				num308++;
			}
		}

		private static void Guide(GenerationProgress progres, GameConfiguration configurations)
		{
			int num311 = NPC.NewNPC(new EntitySource_WorldGen(), Main.spawnTileX * 16, Main.spawnTileY * 16, 22);
			Main.npc[num311].homeTileX = Main.spawnTileX;
			Main.npc[num311].homeTileY = Main.spawnTileY;
			Main.npc[num311].direction = 1;
			Main.npc[num311].homeless = true;
		}
		private static void Sunflowers(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[39];
			for (int num312 = 0; (double)num312 < (double)Main.maxTilesX * 0.002; num312++)
			{
				int num313 = 0;
				int num314 = 0;
				_ = Main.maxTilesX / 2;
				int num315 = WorldGen.genRand.Next(Main.maxTilesX);
				num313 = num315 - WorldGen.genRand.Next(10) - 7;
				num314 = num315 + WorldGen.genRand.Next(10) + 7;
				if (num313 < 0)
				{
					num313 = 0;
				}
				if (num314 > Main.maxTilesX - 1)
				{
					num314 = Main.maxTilesX - 1;
				}
				for (int num316 = num313; num316 < num314; num316++)
				{
					for (int num317 = 1; (double)num317 < Main.worldSurface - 1.0; num317++)
					{
						if (Main.tile[num316, num317].TileType == 2 && Main.tile[num316, num317].HasTile && !Main.tile[num316, num317 - 1].HasTile)
						{
							WorldGen.PlaceTile(num316, num317 - 1, 27, mute: true);
						}
						if (Main.tile[num316, num317].HasTile)
						{
							break;
						}
					}
				}
			}
		}
		
		private static void PlantingTrees(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[40];
			for (int num318 = 0; (double)num318 < (double)Main.maxTilesX * 0.003; num318++)
			{
				int num319 = WorldGen.genRand.Next(50, Main.maxTilesX - 50);
				int num320 = WorldGen.genRand.Next(25, 50);
				for (int num321 = num319 - num320; num321 < num319 + num320; num321++)
				{
					for (int num322 = 20; (double)num322 < Main.worldSurface; num322++)
					{
						GrowEpicTree(num321, num322);
					}
				}
			}
			AddTrees();
		}

		private static void Herbs(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[41];
			for (int num323 = 0; (double)num323 < (double)Main.maxTilesX * 1.7; num323++)
			{
				PlantAlch();
			}
		}

		private static void Weeds(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[42];
			AddPlants();
			for (int num324 = 0; num324 < Main.maxTilesX; num324++)
			{
				for (int num325 = 0; num325 < Main.maxTilesY; num325++)
				{
					if (!Main.tile[num324, num325].HasTile)
					{
						continue;
					}
					if (num325 >= (int)Main.worldSurface && Main.tile[num324, num325].TileType == 70 && !Main.tile[num324, num325 - 1].HasTile)
					{
						GrowShroom(num324, num325);
						if (!Main.tile[num324, num325 - 1].HasTile)
						{
							WorldGen.PlaceTile(num324, num325 - 1, 71, mute: true);
						}
					}
					if (Main.tile[num324, num325].TileType == 60 && !Main.tile[num324, num325 - 1].HasTile)
					{
						WorldGen.PlaceTile(num324, num325 - 1, 61, mute: true);
					}
				}
			}
		}

		private static void Vines(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[43];
			for (int num326 = 0; num326 < Main.maxTilesX; num326++)
			{
				int num327 = 0;
				for (int num328 = 0; (double)num328 < Main.worldSurface; num328++)
				{
					if (num327 > 0 && !Main.tile[num326, num328].HasTile)
					{
						Tile tile = Main.tile[num326, num328];
						tile.HasTile = true;
						tile.TileType = 52;
						num327--;
					}
					else
					{
						num327 = 0;
					}
					if (Main.tile[num326, num328].HasTile && Main.tile[num326, num328].TileType == 2 && WorldGen.genRand.Next(5) < 3)
					{
						num327 = WorldGen.genRand.Next(1, 10);
					}
				}
				num327 = 0;
				for (int num329 = 0; num329 < Main.maxTilesY; num329++)
				{
					if (num327 > 0 && !Main.tile[num326, num329].HasTile)
					{
						Tile tile = Main.tile[num326, num329];
						tile.HasTile = true;
						tile.TileType = 62;
						num327--;
					}
					else
					{
						num327 = 0;
					}
					if (Main.tile[num326, num329].HasTile && Main.tile[num326, num329].TileType == 60 && WorldGen.genRand.Next(5) < 3)
					{
						num327 = WorldGen.genRand.Next(1, 10);
					}
				}
			}
		}

		private static void Flowers(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[44];
			for (int num330 = 0; (double)num330 < (double)Main.maxTilesX * 0.005; num330++)
			{
				int num331 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				int num332 = WorldGen.genRand.Next(5, 15);
				int num333 = WorldGen.genRand.Next(15, 30);
				for (int num334 = 1; (double)num334 < Main.worldSurface - 1.0; num334++)
				{
					if (!Main.tile[num331, num334].HasTile)
					{
						continue;
					}
					for (int num335 = num331 - num332; num335 < num331 + num332; num335++)
					{
						for (int num336 = num334 - num333; num336 < num334 + num333; num336++)
						{
							if (Main.tile[num335, num336].TileType == 3 || Main.tile[num335, num336].TileType == 24)
							{
								Main.tile[num335, num336].TileFrameX = (short)(WorldGen.genRand.Next(6, 8) * 18);
							}
						}
					}
					break;
				}
			}
		}

		private static void Mushrooms(GenerationProgress progres, GameConfiguration configurations)
		{
			Main.statusText = (string)Lang.gen[45];
			for (int num337 = 0; (double)num337 < (double)Main.maxTilesX * 0.002; num337++)
			{
				int num338 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				int num339 = WorldGen.genRand.Next(4, 10);
				int num340 = WorldGen.genRand.Next(15, 30);
				for (int num341 = 1; (double)num341 < Main.worldSurface - 1.0; num341++)
				{
					if (!Main.tile[num338, num341].HasTile)
					{
						continue;
					}
					for (int num342 = num338 - num339; num342 < num338 + num339; num342++)
					{
						for (int num343 = num341 - num340; num343 < num341 + num340; num343++)
						{
							if (Main.tile[num342, num343].TileType == 3 || Main.tile[num342, num343].TileType == 24)
							{
								Main.tile[num342, num343].TileFrameX = 144;
							}
						}
					}
					break;
				}
			}
		}

		private static void FinalCleanup(GenerationProgress progres, GameConfiguration configurations)
		{
			WorldGen.gen = false;
		}

		//Readded legacy generation methods
		public static void JungleRunner(int i, int j)
		{
			double num = WorldGen.genRand.Next(5, 11);
			Vector2 vector;
			vector.X = i;
			vector.Y = j;
			Vector2 vector2;
			vector2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)WorldGen.genRand.Next(10, 20) * 0.1f;
			int num2 = 0;
			bool flag = true;
			while (flag)
			{
				if ((double)vector.Y < Main.worldSurface)
				{
					int num3 = (int)vector.X;
					int num4 = (int)vector.Y;
					if (Main.tile[num3, num4].WallType == 0 && !Main.tile[num3, num4].HasTile && Main.tile[num3, num4 - 3].WallType == 0 && !Main.tile[num3, num4 - 3].HasTile && Main.tile[num3, num4 - 1].WallType == 0 && !Main.tile[num3, num4 - 1].HasTile && Main.tile[num3, num4 - 4].WallType == 0 && !Main.tile[num3, num4 - 4].HasTile && Main.tile[num3, num4 - 2].WallType == 0 && !Main.tile[num3, num4 - 2].HasTile && Main.tile[num3, num4 - 5].WallType == 0 && !Main.tile[num3, num4 - 5].HasTile)
					{
						flag = false;
					}
				}
				GenVars.JungleX = (int)vector.X;
				num += (double)((float)WorldGen.genRand.Next(-20, 21) * 0.1f);
				if (num < 5.0)
				{
					num = 5.0;
				}
				if (num > 10.0)
				{
					num = 10.0;
				}
				int num5 = (int)((double)vector.X - num * 0.5);
				int num6 = (int)((double)vector.X + num * 0.5);
				int num7 = (int)((double)vector.Y - num * 0.5);
				int num8 = (int)((double)vector.Y + num * 0.5);
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesX)
				{
					num6 = Main.maxTilesX;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesY)
				{
					num8 = Main.maxTilesY;
				}
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < num * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							WorldGen.KillTile(k, l);
						}
					}
				}
				num2++;
				if (num2 > 10 && WorldGen.genRand.Next(50) < num2)
				{
					num2 = 0;
					int num9 = -2;
					if (WorldGen.genRand.NextBool(2))
					{
						num9 = 2;
					}
					WorldGen.TileRunner((int)vector.X, (int)vector.Y, WorldGen.genRand.Next(3, 20), WorldGen.genRand.Next(10, 100), -1, addTile: false, num9);
				}
				vector += vector2;
				vector2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
				if (vector2.Y > 0f)
				{
					vector2.Y = 0f;
				}
				if (vector2.Y < -2f)
				{
					vector2.Y = -2f;
				}
				vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
				if (vector.X < (float)(i - 200))
				{
					vector2.X += (float)WorldGen.genRand.Next(5, 21) * 0.1f;
				}
				if (vector.X > (float)(i + 200))
				{
					vector2.X -= (float)WorldGen.genRand.Next(5, 21) * 0.1f;
				}
				if ((double)vector2.X > 1.5)
				{
					vector2.X = 1.5f;
				}
				if ((double)vector2.X < -1.5)
				{
					vector2.X = -1.5f;
				}
			}
		}

		public static void FloatingIsland(int i, int j)
		{
			double num = WorldGen.genRand.Next(80, 120);
			double num2 = num;
			float num3 = WorldGen.genRand.Next(20, 25);
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
			while (vector2.X > -2f && vector2.X < 2f)
			{
				vector2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
			}
			vector2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
			while (num > 0.0 && num3 > 0f)
			{
				num -= (double)WorldGen.genRand.Next(4);
				num3 -= 1f;
				int num4 = (int)((double)vector.X - num * 0.5);
				int num5 = (int)((double)vector.X + num * 0.5);
				int num6 = (int)((double)vector.Y - num * 0.5);
				int num7 = (int)((double)vector.Y + num * 0.5);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				float num8 = vector.Y + 1f;
				for (int k = num4; k < num5; k++)
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						num8 += (float)WorldGen.genRand.Next(-1, 2);
					}
					if (num8 < vector.Y)
					{
						num8 = vector.Y;
					}
					if (num8 > vector.Y + 2f)
					{
						num8 = vector.Y + 2f;
					}
					for (int l = num6; l < num7; l++)
					{
						if (!((float)l > num8))
						{
							continue;
						}
						float num9 = Math.Abs((float)k - vector.X);
						float num10 = Math.Abs((float)l - vector.Y) * 2f;
						if (Math.Sqrt(num9 * num9 + num10 * num10) < num2 * 0.4)
						{
							Tile tile = Main.tile[k, l];
							tile.HasTile = true;
							if (Main.tile[k, l].TileType == 59)
							{
								Main.tile[k, l].TileType = 0;
							}
						}
					}
				}
				WorldGen.TileRunner(WorldGen.genRand.Next(num4 + 10, num5 - 10), (int)((double)vector.Y + num2 * 0.1 + 5.0), WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(10, 15), 0, addTile: true, 0f, 2f, noYChange: true);
				num4 = (int)((double)vector.X - num * 0.4);
				num5 = (int)((double)vector.X + num * 0.4);
				num6 = (int)((double)vector.Y - num * 0.4);
				num7 = (int)((double)vector.Y + num * 0.4);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int m = num4; m < num5; m++)
				{
					for (int n = num6; n < num7; n++)
					{
						if ((float)n > vector.Y + 2f)
						{
							float num11 = Math.Abs((float)m - vector.X);
							float num12 = Math.Abs((float)n - vector.Y) * 2f;
							if (Math.Sqrt(num11 * num11 + num12 * num12) < num2 * 0.4)
							{
								Main.tile[m, n].WallType = 2;
							}
						}
					}
				}
				vector += vector2;
				vector2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 1f)
				{
					vector2.X = 1f;
				}
				if (vector2.X < -1f)
				{
					vector2.X = -1f;
				}
				if ((double)vector2.Y > 0.2)
				{
					vector2.Y = -0.2f;
				}
				if ((double)vector2.Y < -0.2)
				{
					vector2.Y = -0.2f;
				}
			}
		}

		public static void ShroomPatch(int i, int j)
		{
			double num = WorldGen.genRand.Next(40, 70);
			double num2 = num;
			float num3 = WorldGen.genRand.Next(20, 30);
			if (WorldGen.genRand.NextBool(5))
			{
				num *= 1.5;
				num2 *= 1.5;
				num3 *= 1.2f;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = (float)j - num3 * 0.3f;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
			while (num > 0.0 && num3 > 0f)
			{
				num -= (double)WorldGen.genRand.Next(3);
				num3 -= 1f;
				int num4 = (int)((double)vector.X - num * 0.5);
				int num5 = (int)((double)vector.X + num * 0.5);
				int num6 = (int)((double)vector.Y - num * 0.5);
				int num7 = (int)((double)vector.Y + num * 0.5);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num8 = Math.Abs((float)k - vector.X);
						float num9 = Math.Abs(((float)l - vector.Y) * 2.3f);
						if (!(Math.Sqrt(num8 * num8 + num9 * num9) < num2 * 0.4))
						{
							continue;
						}
						if ((double)l < (double)vector.Y + num2 * 0.02)
						{
							if (Main.tile[k, l].TileType != 59)
							{
								Tile tile2 = Main.tile[k, l];
								tile2.HasTile = false;
							}
						}
						else
						{
							Main.tile[k, l].TileType = 59;
						}
						Tile tile = Main.tile[k, l];
						tile.LiquidAmount = 0;
						tile.LiquidType = 0;
					}
				}
				vector += vector2;
				vector.X += vector2.X;
				vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				vector2.Y -= (float)WorldGen.genRand.Next(11) * 0.05f;
				if ((double)vector2.X > -0.5 && (double)vector2.X < 0.5)
				{
					if (vector2.X < 0f)
					{
						vector2.X = -0.5f;
					}
					else
					{
						vector2.X = 0.5f;
					}
				}
				if (vector2.X > 2f)
				{
					vector2.X = 1f;
				}
				if (vector2.X < -2f)
				{
					vector2.X = -1f;
				}
				if (vector2.Y > 1f)
				{
					vector2.Y = 1f;
				}
				if (vector2.Y < -1f)
				{
					vector2.Y = -1f;
				}
				for (int m = 0; m < 2; m++)
				{
					int num10 = (int)vector.X + WorldGen.genRand.Next(-20, 20);
					int num11 = (int)vector.Y + WorldGen.genRand.Next(0, 20);
					while (!Main.tile[num10, num11].HasTile && Main.tile[num10, num11].TileType != 59)
					{
						num10 = (int)vector.X + WorldGen.genRand.Next(-20, 20);
						num11 = (int)vector.Y + WorldGen.genRand.Next(0, 20);
					}
					int num12 = WorldGen.genRand.Next(7, 10);
					int num13 = WorldGen.genRand.Next(7, 10);
					WorldGen.TileRunner(num10, num11, num12, num13, 59, addTile: false, 0f, 2f, noYChange: true);
					if (WorldGen.genRand.NextBool(3))
					{
						WorldGen.TileRunner(num10, num11, num12 - 3, num13 - 3, -1, addTile: false, 0f, 2f, noYChange: true);
					}
				}
			}
		}

		public static void AddHellHouses()
		{
			int num = (int)((double)Main.maxTilesX * 0.25);
			for (int i = num; i < Main.maxTilesX - num; i++)
			{
				int num2 = Main.maxTilesY - 40;
				while (Main.tile[i, num2].HasTile || Main.tile[i, num2].LiquidAmount > 0)
				{
					num2--;
				}
				if (Main.tile[i, num2 + 1].HasTile)
				{
					byte b = (byte)WorldGen.genRand.Next(75, 77);
					byte wall = 13;
					if (WorldGen.genRand.Next(5) > 0)
					{
						b = 75;
					}
					if (b == 75)
					{
						wall = 14;
					}
					HellHouse(i, num2, b, wall);
					i += WorldGen.genRand.Next(15, 80);
				}
			}
			float num3 = Main.maxTilesX / 4200;
			for (int j = 0; (float)j < 200f * num3; j++)
			{
				int num4 = 0;
				bool flag = false;
				while (!flag)
				{
					num4++;
					int num5 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.2), (int)((double)Main.maxTilesX * 0.8));
					int num6 = WorldGen.genRand.Next(Main.maxTilesY - 300, Main.maxTilesY - 20);
					if (Main.tile[num5, num6].HasTile && (Main.tile[num5, num6].TileType == 75 || Main.tile[num5, num6].TileType == 76))
					{
						int num7 = 0;
						if (Main.tile[num5 - 1, num6].WallType > 0)
						{
							num7 = -1;
						}
						else if (Main.tile[num5 + 1, num6].WallType > 0)
						{
							num7 = 1;
						}
						if (!Main.tile[num5 + num7, num6].HasTile && !Main.tile[num5 + num7, num6 + 1].HasTile)
						{
							bool flag2 = false;
							for (int k = num5 - 8; k < num5 + 8; k++)
							{
								for (int l = num6 - 8; l < num6 + 8; l++)
								{
									if (Main.tile[k, l].HasTile && Main.tile[k, l].TileType == 4)
									{
										flag2 = true;
										break;
									}
								}
							}
							if (!flag2)
							{
								WorldGen.PlaceTile(num5 + num7, num6, 4, mute: true, forced: true, -1, 7);
								flag = true;
							}
						}
					}
					if (num4 > 1000)
					{
						flag = true;
					}
				}
			}
		}

		public static void HellHouse(int i, int j, byte type = 76, byte wall = 13)
		{
			int num = WorldGen.genRand.Next(8, 20);
			int num2 = WorldGen.genRand.Next(1, 3);
			int num3 = WorldGen.genRand.Next(4, 13);
			int num4 = j;
			for (int k = 0; k < num2; k++)
			{
				int num5 = WorldGen.genRand.Next(5, 9);
				HellRoom(i, num4, num, num5, type, wall);
				num4 -= num5;
			}
			num4 = j;
			for (int l = 0; l < num3; l++)
			{
				int num6 = WorldGen.genRand.Next(5, 9);
				num4 += num6;
				HellRoom(i, num4, num, num6, type, wall);
			}
			for (int m = i - num / 2; m <= i + num / 2; m++)
			{
				for (num4 = j; num4 < Main.maxTilesY && ((Main.tile[m, num4].HasTile && (Main.tile[m, num4].TileType == 76 || Main.tile[m, num4].TileType == 75)) || Main.tile[i, num4].WallType == 13 || Main.tile[i, num4].WallType == 14); num4++)
				{
				}
				int num7 = 6 + WorldGen.genRand.Next(3);
				while (num4 < Main.maxTilesY && !Main.tile[m, num4].HasTile)
				{
					num7--;
					Tile tile = Main.tile[m, num4];
					tile.HasTile = true;
					tile.TileType = 57;
					num4++;
					if (num7 <= 0)
					{
						break;
					}
				}
			}
			int num8 = 0;
			int num9 = 0;
			for (num4 = j; num4 < Main.maxTilesY && ((Main.tile[i, num4].HasTile && (Main.tile[i, num4].TileType == 76 || Main.tile[i, num4].TileType == 75)) || Main.tile[i, num4].WallType == 13 || Main.tile[i, num4].WallType == 14); num4++)
			{
			}
			num4--;
			num9 = num4;
			while ((Main.tile[i, num4].HasTile && (Main.tile[i, num4].TileType == 76 || Main.tile[i, num4].TileType == 75)) || Main.tile[i, num4].WallType == 13 || Main.tile[i, num4].WallType == 14)
			{
				num4--;
				if (!Main.tile[i, num4].HasTile || (Main.tile[i, num4].TileType != 76 && Main.tile[i, num4].TileType != 75))
				{
					continue;
				}
				int num10 = WorldGen.genRand.Next(i - num / 2 + 1, i + num / 2 - 1);
				int num11 = WorldGen.genRand.Next(i - num / 2 + 1, i + num / 2 - 1);
				if (num10 > num11)
				{
					int num12 = num10;
					num10 = num11;
					num11 = num12;
				}
				if (num10 == num11)
				{
					if (num10 < i)
					{
						num11++;
					}
					else
					{
						num10--;
					}
				}
				for (int n = num10; n <= num11; n++)
				{
					if (Main.tile[n, num4 - 1].WallType == 13)
					{
						Main.tile[n, num4].WallType = 13;
					}
					if (Main.tile[n, num4 - 1].WallType == 14)
					{
						Main.tile[n, num4].WallType = 14;
					}
					Tile tile = Main.tile[n, num4];
					tile.TileType = 19;
					tile.HasTile = true;
				}
				num4--;
			}
			num8 = num4;
			float num13 = (float)((num9 - num8) * num) * 0.02f;
			for (int num14 = 0; (float)num14 < num13; num14++)
			{
				int num15 = WorldGen.genRand.Next(i - num / 2, i + num / 2 + 1);
				int num16 = WorldGen.genRand.Next(num8, num9);
				int num17 = WorldGen.genRand.Next(3, 8);
				for (int num18 = num15 - num17; num18 <= num15 + num17; num18++)
				{
					for (int num19 = num16 - num17; num19 <= num16 + num17; num19++)
					{
						float num20 = Math.Abs(num18 - num15);
						float num21 = Math.Abs(num19 - num16);
						if (!(Math.Sqrt(num20 * num20 + num21 * num21) < (double)num17 * 0.4))
						{
							continue;
						}
						try
						{
							if (Main.tile[num18, num19].TileType == 76 || Main.tile[num18, num19].TileType == 19)
							{
								Tile tile = Main.tile[num18, num19];
								tile.HasTile = false;
							}
							Main.tile[num18, num19].WallType = 0;
						}
						catch
						{
						}
					}
				}
			}
		}

		public static void HellRoom(int i, int j, int width, int height, byte type = 76, byte wall = 13)
		{
			if (j > Main.maxTilesY - 40)
			{
				return;
			}
			for (int k = i - width / 2; k <= i + width / 2; k++)
			{
				for (int l = j - height; l <= j; l++)
				{
					try
					{
						Tile tile = Main.tile[k, l];
						tile.HasTile = true;
						tile.TileType = type;
						tile.LiquidAmount = 0;
						tile.LiquidType = 0;
					}
					catch
					{
					}
				}
			}
			for (int m = i - width / 2 + 1; m <= i + width / 2 - 1; m++)
			{
				for (int n = j - height + 1; n <= j - 1; n++)
				{
					try
					{
						Tile tile = Main.tile[m, n];
						tile.HasTile = false;
						tile.WallType = wall;
						tile.LiquidAmount = 0;
						tile.LiquidType = 0;
					}
					catch
					{
					}
				}
			}
		}

		public static void MakeDungeon(int x, int y, int tileType = 41, int wallType = 7)
		{
			int num = WorldGen.genRand.Next(3);
			int num2 = WorldGen.genRand.Next(3);
			switch (num)
			{
				case 1:
					tileType = 43;
					break;
				case 2:
					tileType = 44;
					break;
			}
			switch (num2)
			{
				case 1:
					wallType = 8;
					break;
				case 2:
					wallType = 9;
					break;
			}
			GenVars.numDDoors = 0;
			numDPlats = 0;
			GenVars.numDRooms = 0;
			GenVars.dungeonX = x;
			GenVars.dungeonY = y;
			GenVars.dMinX = x;
			GenVars.dMaxX = x;
			GenVars.dMinY = y;
			GenVars.dMaxY = y;
			GenVars.dxStrength1 = WorldGen.genRand.Next(25, 30);
			GenVars.dyStrength1 = WorldGen.genRand.Next(20, 25);
			GenVars.dxStrength2 = WorldGen.genRand.Next(35, 50);
			GenVars.dyStrength2 = WorldGen.genRand.Next(10, 15);
			float num3 = Main.maxTilesX / 60;
			num3 += (float)WorldGen.genRand.Next(0, (int)(num3 / 3f));
			float num4 = num3;
			int num5 = 5;
			DungeonRoom(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
			while (num3 > 0f)
			{
				if (GenVars.dungeonX < GenVars.dMinX)
				{
					GenVars.dMinX = GenVars.dungeonX;
				}
				if (GenVars.dungeonX > GenVars.dMaxX)
				{
					GenVars.dMaxX = GenVars.dungeonX;
				}
				if (GenVars.dungeonY > GenVars.dMaxY)
				{
					GenVars.dMaxY = GenVars.dungeonY;
				}
				num3 -= 1f;
				Main.statusText = Lang.gen[58] + " " + (int)((num4 - num3) / num4 * 60f) + "%";
				if (num5 > 0)
				{
					num5--;
				}
				if ((num5 == 0) & (WorldGen.genRand.NextBool(3)))
				{
					num5 = 5;
					if (WorldGen.genRand.NextBool(2))
					{
						int num6 = GenVars.dungeonX;
						int num7 = GenVars.dungeonY;
						DungeonHalls(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
						if (WorldGen.genRand.Next(2) == 0)
						{
							DungeonHalls(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
						}
						DungeonRoom(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
						GenVars.dungeonX = num6;
						GenVars.dungeonY = num7;
					}
					else
					{
						DungeonRoom(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
					}
				}
				else
				{
					DungeonHalls(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
				}
			}
			DungeonRoom(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
			int num8 = GenVars.dRoomX[0];
			int num9 = GenVars.dRoomY[0];
			for (int i = 0; i < GenVars.numDRooms; i++)
			{
				if (GenVars.dRoomY[i] < num9)
				{
					num8 = GenVars.dRoomX[i];
					num9 = GenVars.dRoomY[i];
				}
			}
			GenVars.dungeonX = num8;
			GenVars.dungeonY = num9;
			GenVars.dEnteranceX = num8;
			GenVars.dSurface = false;
			num5 = 5;
			while (!GenVars.dSurface)
			{
				if (num5 > 0)
				{
					num5--;
				}
				if (((num5 == 0) & (WorldGen.genRand.NextBool(5))) && (double)GenVars.dungeonY > Main.worldSurface + 50.0)
				{
					num5 = 10;
					int num10 = GenVars.dungeonX;
					int num11 = GenVars.dungeonY;
					DungeonHalls(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType, forceX: true);
					DungeonRoom(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
					GenVars.dungeonX = num10;
					GenVars.dungeonY = num11;
				}
				DungeonStairs(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
			}
			DungeonEnt(GenVars.dungeonX, GenVars.dungeonY, tileType, wallType);
			Main.statusText = Lang.gen[58] + " 65%";
			for (int j = 0; j < GenVars.numDRooms; j++)
			{
				for (int k = GenVars.dRoomL[j]; k <= GenVars.dRoomR[j]; k++)
				{
					if (!Main.tile[k, GenVars.dRoomT[j] - 1].HasTile)
					{
						DPlatX[numDPlats] = k;
						DPlatY[numDPlats] = GenVars.dRoomT[j] - 1;
						numDPlats++;
						break;
					}
				}
				for (int l = GenVars.dRoomL[j]; l <= GenVars.dRoomR[j]; l++)
				{
					if (!Main.tile[l, GenVars.dRoomB[j] + 1].HasTile)
					{
						DPlatX[numDPlats] = l;
						DPlatY[numDPlats] = GenVars.dRoomB[j] + 1;
						numDPlats++;
						break;
					}
				}
				for (int m = GenVars.dRoomT[j]; m <= GenVars.dRoomB[j]; m++)
				{
					if (!Main.tile[GenVars.dRoomL[j] - 1, m].HasTile)
					{
						GenVars.DDoorX[GenVars.numDDoors] = GenVars.dRoomL[j] - 1;
						GenVars.DDoorY[GenVars.numDDoors] = m;
						GenVars.DDoorPos[GenVars.numDDoors] = -1;
						GenVars.numDDoors++;
						break;
					}
				}
				for (int n = GenVars.dRoomT[j]; n <= GenVars.dRoomB[j]; n++)
				{
					if (!Main.tile[GenVars.dRoomR[j] + 1, n].HasTile)
					{
						GenVars.DDoorX[GenVars.numDDoors] = GenVars.dRoomR[j] + 1;
						GenVars.DDoorY[GenVars.numDDoors] = n;
						GenVars.DDoorPos[GenVars.numDDoors] = 1;
						GenVars.numDDoors++;
						break;
					}
				}
			}
			Main.statusText = Lang.gen[58] + " 70%";
			int num12 = 0;
			int num13 = 1000;
			int num14 = 0;
			while (num14 < Main.maxTilesX / 100)
			{
				num12++;
				int num15 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
				int num16 = WorldGen.genRand.Next((int)Main.worldSurface + 25, GenVars.dMaxY);
				int num17 = num15;
				if (Main.tile[num15, num16].WallType == wallType && !Main.tile[num15, num16].HasTile)
				{
					int num18 = 1;
					if (WorldGen.genRand.NextBool(2))
					{
						num18 = -1;
					}
					for (; !Main.tile[num15, num16].HasTile; num16 += num18)
					{
					}
					if (Main.tile[num15 - 1, num16].HasTile && Main.tile[num15 + 1, num16].HasTile && !Main.tile[num15 - 1, num16 - num18].HasTile && !Main.tile[num15 + 1, num16 - num18].HasTile)
					{
						num14++;
						int num19 = WorldGen.genRand.Next(5, 13);
						while (Main.tile[num15 - 1, num16].HasTile && Main.tile[num15, num16 + num18].HasTile && Main.tile[num15, num16].HasTile && !Main.tile[num15, num16 - num18].HasTile && num19 > 0)
						{
							Main.tile[num15, num16].TileType = 48;
							if (!Main.tile[num15 - 1, num16 - num18].HasTile && !Main.tile[num15 + 1, num16 - num18].HasTile)
							{
								Tile tile = Main.tile[num15, num16 - num18];
								tile.TileType = 48;
								tile.HasTile = true;
							}
							num15--;
							num19--;
						}
						num19 = WorldGen.genRand.Next(5, 13);
						num15 = num17 + 1;
						while (Main.tile[num15 + 1, num16].HasTile && Main.tile[num15, num16 + num18].HasTile && Main.tile[num15, num16].HasTile && !Main.tile[num15, num16 - num18].HasTile && num19 > 0)
						{
							Main.tile[num15, num16].TileType = 48;
							if (!Main.tile[num15 - 1, num16 - num18].HasTile && !Main.tile[num15 + 1, num16 - num18].HasTile)
							{
								Tile tile = Main.tile[num15, num16 - num18];
								tile.TileType = 48;
								tile.HasTile = true;
							}
							num15++;
							num19--;
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			Main.statusText = Lang.gen[58] + " 75%";
			while (num14 < Main.maxTilesX / 100)
			{
				num12++;
				int num20 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
				int num21 = WorldGen.genRand.Next((int)Main.worldSurface + 25, GenVars.dMaxY);
				int num22 = num21;
				if (Main.tile[num20, num21].WallType == wallType && !Main.tile[num20, num21].HasTile)
				{
					int num23 = 1;
					if (WorldGen.genRand.NextBool(2))
					{
						num23 = -1;
					}
					for (; num20 > 5 && num20 < Main.maxTilesX - 5 && !Main.tile[num20, num21].HasTile; num20 += num23)
					{
					}
					if (Main.tile[num20, num21 - 1].HasTile && Main.tile[num20, num21 + 1].HasTile && !Main.tile[num20 - num23, num21 - 1].HasTile && !Main.tile[num20 - num23, num21 + 1].HasTile)
					{
						num14++;
						int num24 = WorldGen.genRand.Next(5, 13);
						while (Main.tile[num20, num21 - 1].HasTile && Main.tile[num20 + num23, num21].HasTile && Main.tile[num20, num21].HasTile && !Main.tile[num20 - num23, num21].HasTile && num24 > 0)
						{
							Main.tile[num20, num21].TileType = 48;
							if (!Main.tile[num20 - num23, num21 - 1].HasTile && !Main.tile[num20 - num23, num21 + 1].HasTile)
							{
								Tile tile = Main.tile[num20 - num23, num21];
								tile.TileType = 48;
								tile.HasTile = true;
							}
							num21--;
							num24--;
						}
						num24 = WorldGen.genRand.Next(5, 13);
						num21 = num22 + 1;
						while (Main.tile[num20, num21 + 1].HasTile && Main.tile[num20 + num23, num21].HasTile && Main.tile[num20, num21].HasTile && !Main.tile[num20 - num23, num21].HasTile && num24 > 0)
						{
							Main.tile[num20, num21].TileType = 48;
							if (!Main.tile[num20 - num23, num21 - 1].HasTile && !Main.tile[num20 - num23, num21 + 1].HasTile)
							{
								Tile tile = Main.tile[num20 - num23, num21];
								tile.TileType = 48;
								tile.HasTile = true;
							}
							num21++;
							num24--;
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			Main.statusText = Lang.gen[58] + " 80%";
			for (int num25 = 0; num25 < GenVars.numDDoors; num25++)
			{
				int num26 = GenVars.DDoorX[num25] - 10;
				int num27 = GenVars.DDoorX[num25] + 10;
				int num28 = 100;
				int num29 = 0;
				int num30 = 0;
				int num31 = 0;
				for (int num32 = num26; num32 < num27; num32++)
				{
					bool flag = true;
					int num33 = GenVars.DDoorY[num25];
					while (!Main.tile[num32, num33].HasTile)
					{
						num33--;
					}
					if (!Main.tileDungeon[Main.tile[num32, num33].TileType])
					{
						flag = false;
					}
					num30 = num33;
					for (num33 = GenVars.DDoorY[num25]; !Main.tile[num32, num33].HasTile; num33++)
					{
					}
					if (!Main.tileDungeon[Main.tile[num32, num33].TileType])
					{
						flag = false;
					}
					num31 = num33;
					if (num31 - num30 < 3)
					{
						continue;
					}
					int num34 = num32 - 20;
					int num35 = num32 + 20;
					int num36 = num31 - 10;
					int num37 = num31 + 10;
					for (int num38 = num34; num38 < num35; num38++)
					{
						for (int num39 = num36; num39 < num37; num39++)
						{
							if (Main.tile[num38, num39].HasTile && Main.tile[num38, num39].TileType == 10)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						for (int num40 = num31 - 3; num40 < num31; num40++)
						{
							for (int num41 = num32 - 3; num41 <= num32 + 3; num41++)
							{
								if (Main.tile[num41, num40].HasTile)
								{
									flag = false;
									break;
								}
							}
						}
					}
					if (flag && num31 - num30 < 20)
					{
						bool flag2 = false;
						if (GenVars.DDoorPos[num25] == 0 && num31 - num30 < num28)
						{
							flag2 = true;
						}
						if (GenVars.DDoorPos[num25] == -1 && num32 > num29)
						{
							flag2 = true;
						}
						if (GenVars.DDoorPos[num25] == 1 && (num32 < num29 || num29 == 0))
						{
							flag2 = true;
						}
						if (flag2)
						{
							num29 = num32;
							num28 = num31 - num30;
						}
					}
				}
				if (num28 >= 20)
				{
					continue;
				}
				int num42 = num29;
				int num43 = GenVars.DDoorY[num25];
				int num44 = num43;
				for (; !Main.tile[num42, num43].HasTile; num43++)
				{
					Tile tile3 = Main.tile[num42, num43];
					tile3.HasTile = false;
				}
				while (!Main.tile[num42, num44].HasTile)
				{
					num44--;
				}
				num43--;
				num44++;
				for (int num45 = num44; num45 < num43 - 2; num45++)
				{
					Tile tile3 = Main.tile[num42, num45];
					tile3.HasTile = true;
					tile3.TileType = (byte)tileType;
				}
				WorldGen.PlaceTile(num42, num43, 10, mute: true);
				num42--;
				int num46 = num43 - 3;
				while (!Main.tile[num42, num46].HasTile)
				{
					num46--;
				}
				if (num43 - num46 < num43 - num44 + 5 && Main.tileDungeon[Main.tile[num42, num46].TileType])
				{
					for (int num47 = num43 - 4 - WorldGen.genRand.Next(3); num47 > num46; num47--)
					{
						Tile tile3 = Main.tile[num42, num47];
						tile3.HasTile = true;
						tile3.TileType = (byte)tileType;
					}
				}
				num42 += 2;
				num46 = num43 - 3;
				while (!Main.tile[num42, num46].HasTile)
				{
					num46--;
				}
				if (num43 - num46 < num43 - num44 + 5 && Main.tileDungeon[Main.tile[num42, num46].TileType])
				{
					for (int num48 = num43 - 4 - WorldGen.genRand.Next(3); num48 > num46; num48--)
					{
						Tile tile3 = Main.tile[num42, num48];
						tile3.HasTile = true;
						tile3.TileType = (byte)tileType;
					}
				}
				num43++;
				num42--;
				Tile tile = Main.tile[num42 - 1, num43];
				tile.HasTile = true;
				tile.TileType = (byte)tileType;
				Tile tile2 = Main.tile[num42 + 1, num43];
				tile2.HasTile = true;
				tile2.TileType = (byte)tileType;
			}
			Main.statusText = Lang.gen[58] + " 85%";
			for (int num49 = 0; num49 < numDPlats; num49++)
			{
				int num50 = DPlatX[num49];
				int num51 = DPlatY[num49];
				int num52 = Main.maxTilesX;
				int num53 = 10;
				for (int num54 = num51 - 5; num54 <= num51 + 5; num54++)
				{
					int num55 = num50;
					int num56 = num50;
					bool flag3 = false;
					if (Main.tile[num55, num54].HasTile)
					{
						flag3 = true;
					}
					else
					{
						while (!Main.tile[num55, num54].HasTile)
						{
							num55--;
							if (!Main.tileDungeon[Main.tile[num55, num54].TileType])
							{
								flag3 = true;
							}
						}
						while (!Main.tile[num56, num54].HasTile)
						{
							num56++;
							if (!Main.tileDungeon[Main.tile[num56, num54].TileType])
							{
								flag3 = true;
							}
						}
					}
					if (flag3 || num56 - num55 > num53)
					{
						continue;
					}
					bool flag4 = true;
					int num57 = num50 - num53 / 2 - 2;
					int num58 = num50 + num53 / 2 + 2;
					int num59 = num54 - 5;
					int num60 = num54 + 5;
					for (int num61 = num57; num61 <= num58; num61++)
					{
						for (int num62 = num59; num62 <= num60; num62++)
						{
							if (Main.tile[num61, num62].HasTile && Main.tile[num61, num62].TileType == 19)
							{
								flag4 = false;
								break;
							}
						}
					}
					for (int num63 = num54 + 3; num63 >= num54 - 5; num63--)
					{
						if (Main.tile[num50, num63].HasTile)
						{
							flag4 = false;
							break;
						}
					}
					if (flag4)
					{
						num52 = num54;
						break;
					}
				}
				if (num52 > num51 - 10 && num52 < num51 + 10)
				{
					int num64 = num50;
					int num65 = num52;
					int num66 = num50 + 1;
					while (!Main.tile[num64, num65].HasTile)
					{
						Tile tile = Main.tile[num64, num65];
						tile.HasTile = true;
						tile.TileType = 19;
						num64--;
					}
					for (; !Main.tile[num66, num65].HasTile; num66++)
					{
						Tile tile = Main.tile[num66, num65];
						tile.HasTile = true;
						tile.TileType = 19;
					}
				}
			}
			Main.statusText = Lang.gen[58] + " 90%";
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while (num14 < Main.maxTilesX / 20)
			{
				num12++;
				int num67 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
				int num68 = WorldGen.genRand.Next(GenVars.dMinY, GenVars.dMaxY);
				bool flag5 = true;
				if (Main.tile[num67, num68].WallType == wallType && !Main.tile[num67, num68].HasTile)
				{
					int num69 = 1;
					if (WorldGen.genRand.NextBool(2))
					{
						num69 = -1;
					}
					while (flag5 && !Main.tile[num67, num68].HasTile)
					{
						num67 -= num69;
						if (num67 < 5 || num67 > Main.maxTilesX - 5)
						{
							flag5 = false;
						}
						else if (Main.tile[num67, num68].HasTile && !Main.tileDungeon[Main.tile[num67, num68].TileType])
						{
							flag5 = false;
						}
					}
					if (flag5 && Main.tile[num67, num68].HasTile && Main.tileDungeon[Main.tile[num67, num68].TileType] && Main.tile[num67, num68 - 1].HasTile && Main.tileDungeon[Main.tile[num67, num68 - 1].TileType] && Main.tile[num67, num68 + 1].HasTile && Main.tileDungeon[Main.tile[num67, num68 + 1].TileType])
					{
						num67 += num69;
						for (int num70 = num67 - 3; num70 <= num67 + 3; num70++)
						{
							for (int num71 = num68 - 3; num71 <= num68 + 3; num71++)
							{
								if (Main.tile[num70, num71].HasTile && Main.tile[num70, num71].TileType == 19)
								{
									flag5 = false;
									break;
								}
							}
						}
						if (flag5 && (!Main.tile[num67, num68 - 1].HasTile & !Main.tile[num67, num68 - 2].HasTile & !Main.tile[num67, num68 - 3].HasTile))
						{
							int num72 = num67;
							int num73 = num67;
							for (; num72 > GenVars.dMinX && num72 < GenVars.dMaxX && !Main.tile[num72, num68].HasTile && !Main.tile[num72, num68 - 1].HasTile && !Main.tile[num72, num68 + 1].HasTile; num72 += num69)
							{
							}
							num72 = Math.Abs(num67 - num72);
							bool flag6 = false;
							if (WorldGen.genRand.NextBool(2))
							{
								flag6 = true;
							}
							if (num72 > 5)
							{
								for (int num74 = WorldGen.genRand.Next(1, 4); num74 > 0; num74--)
								{
									Tile tile = Main.tile[num67, num68];
									tile.HasTile = true;
									tile.TileType = 19;
									if (flag6)
									{
										WorldGen.PlaceTile(num67, num68 - 1, 50, mute: true);
										if (WorldGen.genRand.NextBool(50) && Main.tile[num67, num68 - 1].TileType == 50)
										{
											Main.tile[num67, num68 - 1].TileFrameX = 90;
										}
									}
									num67 += num69;
								}
								num12 = 0;
								num14++;
								if (!flag6 && WorldGen.genRand.Next(2) == 0)
								{
									num67 = num73;
									num68--;
									int num75 = 0;
									if (WorldGen.genRand.Next(4) == 0)
									{
										num75 = 1;
									}
									switch (num75)
									{
										case 0:
											num75 = 13;
											break;
										case 1:
											num75 = 49;
											break;
									}
									WorldGen.PlaceTile(num67, num68, num75, mute: true);
									if (Main.tile[num67, num68].TileType == 13)
									{
										if (WorldGen.genRand.Next(2) == 0)
										{
											Main.tile[num67, num68].TileFrameX = 18;
										}
										else
										{
											Main.tile[num67, num68].TileFrameX = 36;
										}
									}
								}
							}
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			Main.statusText = Lang.gen[58] + " 95%";
			int num76 = 0;
			for (int num77 = 0; num77 < GenVars.numDRooms; num77++)
			{
				int num78 = 0;
				while (num78 < 1000)
				{
					int num79 = (int)((double)GenVars.dRoomSize[num77] * 0.4);
					int i2 = GenVars.dRoomX[num77] + WorldGen.genRand.Next(-num79, num79 + 1);
					int num80 = GenVars.dRoomY[num77] + WorldGen.genRand.Next(-num79, num79 + 1);
					int num81 = 0;
					num76++;
					int style = 2;
					switch (num76)
					{
						case 1:
							num81 = 329;
							break;
						case 2:
							num81 = 155;
							break;
						case 3:
							num81 = 156;
							break;
						case 4:
							num81 = 157;
							break;
						case 5:
							num81 = 163;
							break;
						case 6:
							num81 = 113;
							break;
						case 7:
							num81 = 327;
							style = 0;
							break;
						default:
							num81 = 164;
							num76 = 0;
							break;
					}
					if ((double)num80 < Main.worldSurface + 50.0)
					{
						num81 = 327;
						style = 0;
					}
					if (num81 == 0 && WorldGen.genRand.Next(2) == 0)
					{
						num78 = 1000;
						continue;
					}
					if (AddBuriedChest(i2, num80, num81, notNearOtherChests: false, style))
					{
						num78 += 1000;
					}
					num78++;
				}
			}
			GenVars.dMinX -= 25;
			GenVars.dMaxX += 25;
			GenVars.dMinY -= 25;
			GenVars.dMaxY += 25;
			if (GenVars.dMinX < 0)
			{
				GenVars.dMinX = 0;
			}
			if (GenVars.dMaxX > Main.maxTilesX)
			{
				GenVars.dMaxX = Main.maxTilesX;
			}
			if (GenVars.dMinY < 0)
			{
				GenVars.dMinY = 0;
			}
			if (GenVars.dMaxY > Main.maxTilesY)
			{
				GenVars.dMaxY = Main.maxTilesY;
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while (num14 < Main.maxTilesX / 150)
			{
				num12++;
				int num82 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
				int num83 = WorldGen.genRand.Next(GenVars.dMinY, GenVars.dMaxY);
				if (Main.tile[num82, num83].WallType == wallType)
				{
					for (int num84 = num83; num84 > GenVars.dMinY; num84--)
					{
						if (Main.tile[num82, num84 - 1].HasTile && Main.tile[num82, num84 - 1].TileType == tileType)
						{
							bool flag7 = false;
							for (int num85 = num82 - 15; num85 < num82 + 15; num85++)
							{
								for (int num86 = num84 - 15; num86 < num84 + 15; num86++)
								{
									if (num85 > 0 && num85 < Main.maxTilesX && num86 > 0 && num86 < Main.maxTilesY && Main.tile[num85, num86].TileType == 42)
									{
										flag7 = true;
										break;
									}
								}
							}
							if (Main.tile[num82 - 1, num84].HasTile || Main.tile[num82 + 1, num84].HasTile || Main.tile[num82 - 1, num84 + 1].HasTile || Main.tile[num82 + 1, num84 + 1].HasTile || Main.tile[num82, num84 + 2].HasTile)
							{
								flag7 = true;
							}
							if (flag7)
							{
								break;
							}
							Place1x2Top(num82, num84, 42);
							if (Main.tile[num82, num84].TileType != 42)
							{
								break;
							}
							num12 = 0;
							num14++;
							for (int num87 = 0; num87 < 1000; num87++)
							{
								int num88 = num82 + WorldGen.genRand.Next(-12, 13);
								int num89 = num84 + WorldGen.genRand.Next(3, 21);
								if (Main.tile[num88, num89].HasTile || Main.tile[num88, num89 + 1].HasTile || Main.tile[num88 - 1, num89].TileType == 48 || Main.tile[num88 + 1, num89].TileType == 48 || !Collision.CanHit(new Vector2(num88 * 16, num89 * 16), 16, 16, new Vector2(num82 * 16, num84 * 16 + 1), 16, 16))
								{
									continue;
								}
								WorldGen.PlaceTile(num88, num89, 136, mute: true);
								if (!Main.tile[num88, num89].HasTile)
								{
									continue;
								}
								while (num88 != num82 || num89 != num84)
								{
									Tile tile = Main.tile[num88, num89];
									tile.RedWire = true;
									if (num88 > num82)
									{
										num88--;
									}
									if (num88 < num82)
									{
										num88++;
									}
									Tile tile2 = Main.tile[num88, num89];
									tile2.RedWire = true;
									if (num89 > num84)
									{
										num89--;
									}
									if (num89 < num84)
									{
										num89++;
									}
									Tile tile3 = Main.tile[num88, num89];
									tile3.RedWire = true;
								}
								if (Main.rand.Next(3) > 0)
								{
									Main.tile[num82, num84].TileFrameX = 18;
									Main.tile[num82, num84 + 1].TileFrameX = 18;
								}
								break;
							}
							break;
						}
					}
				}
				if (num12 > num13)
				{
					num14++;
					num12 = 0;
				}
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while (num14 < Main.maxTilesX / 500)
			{
				num12++;
				int num90 = WorldGen.genRand.Next(GenVars.dMinX, GenVars.dMaxX);
				int num91 = WorldGen.genRand.Next(GenVars.dMinY, GenVars.dMaxY);
				if (Main.tile[num90, num91].WallType == wallType && WorldGen.placeTrap(num90, num91, 0))
				{
					num12 = num13;
				}
				if (num12 > num13)
				{
					num14++;
					num12 = 0;
				}
			}
		}

		public static void DungeonRoom(int i, int j, int tileType, int wallType)
		{
			double num = WorldGen.genRand.Next(15, 30);
			Vector2 vector = default(Vector2);
			vector.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			vector.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			Vector2 vector2 = default(Vector2);
			vector2.X = i;
			vector2.Y = (float)j - (float)num / 2f;
			int num2 = WorldGen.genRand.Next(10, 20);
			double num3 = vector2.X;
			double num4 = vector2.X;
			double num5 = vector2.Y;
			double num6 = vector2.Y;
			while (num2 > 0)
			{
				num2--;
				int num7 = (int)((double)vector2.X - num * 0.800000011920929 - 5.0);
				int num8 = (int)((double)vector2.X + num * 0.800000011920929 + 5.0);
				int num9 = (int)((double)vector2.Y - num * 0.800000011920929 - 5.0);
				int num10 = (int)((double)vector2.Y + num * 0.800000011920929 + 5.0);
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesX)
				{
					num8 = Main.maxTilesX;
				}
				if (num9 < 0)
				{
					num9 = 0;
				}
				if (num10 > Main.maxTilesY)
				{
					num10 = Main.maxTilesY;
				}
				for (int k = num7; k < num8; k++)
				{
					for (int l = num9; l < num10; l++)
					{
						Main.tile[k, l].LiquidAmount = 0;
						if (Main.tile[k, l].WallType == 0)
						{
							Tile tile = Main.tile[k, l];
							tile.HasTile = true;
							tile.TileType = (byte)tileType;
						}
					}
				}
				for (int m = num7 + 1; m < num8 - 1; m++)
				{
					for (int n = num9 + 1; n < num10 - 1; n++)
					{
						WorldGen.PlaceWall(m, n, wallType, mute: true);
					}
				}
				num7 = (int)((double)vector2.X - num * 0.5);
				num8 = (int)((double)vector2.X + num * 0.5);
				num9 = (int)((double)vector2.Y - num * 0.5);
				num10 = (int)((double)vector2.Y + num * 0.5);
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesX)
				{
					num8 = Main.maxTilesX;
				}
				if (num9 < 0)
				{
					num9 = 0;
				}
				if (num10 > Main.maxTilesY)
				{
					num10 = Main.maxTilesY;
				}
				if ((double)num7 < num3)
				{
					num3 = num7;
				}
				if ((double)num8 > num4)
				{
					num4 = num8;
				}
				if ((double)num9 < num5)
				{
					num5 = num9;
				}
				if ((double)num10 > num6)
				{
					num6 = num10;
				}
				for (int num11 = num7; num11 < num8; num11++)
				{
					for (int num12 = num9; num12 < num10; num12++)
					{
						Tile tile = Main.tile[num11, num12];
						tile.HasTile = false;
						tile.WallType = (byte)wallType;
					}
				}
				vector2 += vector;
				vector.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				vector.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (vector.X > 1f)
				{
					vector.X = 1f;
				}
				if (vector.X < -1f)
				{
					vector.X = -1f;
				}
				if (vector.Y > 1f)
				{
					vector.Y = 1f;
				}
				if (vector.Y < -1f)
				{
					vector.Y = -1f;
				}
			}
			GenVars.dRoomX[GenVars.numDRooms] = (int)vector2.X;
			GenVars.dRoomY[GenVars.numDRooms] = (int)vector2.Y;
			GenVars.dRoomSize[GenVars.numDRooms] = (int)num;
			GenVars.dRoomL[GenVars.numDRooms] = (int)num3;
			GenVars.dRoomR[GenVars.numDRooms] = (int)num4;
			GenVars.dRoomT[GenVars.numDRooms] = (int)num5;
			GenVars.dRoomB[GenVars.numDRooms] = (int)num6;
			GenVars.dRoomTreasure[GenVars.numDRooms] = false;
			GenVars.numDRooms++;
		}

		public static void DungeonHalls(int i, int j, int tileType, int wallType, bool forceX = false)
		{
			Vector2 vector = default(Vector2);
			double num = WorldGen.genRand.Next(4, 6);
			Vector2 vector2 = default(Vector2);
			Vector2 vector3 = default(Vector2);
			int num2 = 1;
			Vector2 vector4 = default(Vector2);
			vector4.X = i;
			vector4.Y = j;
			int num3 = WorldGen.genRand.Next(35, 80);
			if (forceX)
			{
				num3 += 20;
				lastDungeonHall = default(Vector2);
			}
			else if (WorldGen.genRand.Next(5) == 0)
			{
				num *= 2.0;
				num3 /= 2;
			}
			bool flag = false;
			while (!flag)
			{
				num2 = ((WorldGen.genRand.Next(2) != 0) ? 1 : (-1));
				bool flag2 = false;
				if (WorldGen.genRand.Next(2) == 0)
				{
					flag2 = true;
				}
				if (forceX)
				{
					flag2 = true;
				}
				if (flag2)
				{
					vector2.Y = 0f;
					vector2.X = num2;
					vector3.Y = 0f;
					vector3.X = -num2;
					vector.Y = 0f;
					vector.X = num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.Y = -0.2f;
						}
						else
						{
							vector.Y = 0.2f;
						}
					}
				}
				else
				{
					num += 1.0;
					vector.Y = num2;
					vector.X = 0f;
					vector2.X = 0f;
					vector2.Y = num2;
					vector3.X = 0f;
					vector3.Y = -num2;
					if (WorldGen.genRand.Next(2) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.X = 0.3f;
						}
						else
						{
							vector.X = -0.3f;
						}
					}
					else
					{
						num3 /= 2;
					}
				}
				if (lastDungeonHall != vector3)
				{
					flag = true;
				}
			}
			if (!forceX)
			{
				if (vector4.X > (float)(lastMaxTilesX - 200))
				{
					num2 = -1;
					vector2.Y = 0f;
					vector2.X = num2;
					vector.Y = 0f;
					vector.X = num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.Y = -0.2f;
						}
						else
						{
							vector.Y = 0.2f;
						}
					}
				}
				else if (vector4.X < 200f)
				{
					num2 = 1;
					vector2.Y = 0f;
					vector2.X = num2;
					vector.Y = 0f;
					vector.X = num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.Y = -0.2f;
						}
						else
						{
							vector.Y = 0.2f;
						}
					}
				}
				else if (vector4.Y > (float)(lastMaxTilesY - 300))
				{
					num2 = -1;
					num += 1.0;
					vector.Y = num2;
					vector.X = 0f;
					vector2.X = 0f;
					vector2.Y = num2;
					if (WorldGen.genRand.Next(2) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.X = 0.3f;
						}
						else
						{
							vector.X = -0.3f;
						}
					}
				}
				else if ((double)vector4.Y < Main.rockLayer)
				{
					num2 = 1;
					num += 1.0;
					vector.Y = num2;
					vector.X = 0f;
					vector2.X = 0f;
					vector2.Y = num2;
					if (WorldGen.genRand.Next(2) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.X = 0.3f;
						}
						else
						{
							vector.X = -0.3f;
						}
					}
				}
				else if (vector4.X < (float)(Main.maxTilesX / 2) && (double)vector4.X > (double)Main.maxTilesX * 0.25)
				{
					num2 = -1;
					vector2.Y = 0f;
					vector2.X = num2;
					vector.Y = 0f;
					vector.X = num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.Y = -0.2f;
						}
						else
						{
							vector.Y = 0.2f;
						}
					}
				}
				else if (vector4.X > (float)(Main.maxTilesX / 2) && (double)vector4.X < (double)Main.maxTilesX * 0.75)
				{
					num2 = 1;
					vector2.Y = 0f;
					vector2.X = num2;
					vector.Y = 0f;
					vector.X = num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							vector.Y = -0.2f;
						}
						else
						{
							vector.Y = 0.2f;
						}
					}
				}
			}
			if (vector2.Y == 0f)
			{
				GenVars.DDoorX[GenVars.numDDoors] = (int)vector4.X;
				GenVars.DDoorY[GenVars.numDDoors] = (int)vector4.Y;
				GenVars.DDoorPos[GenVars.numDDoors] = 0;
				GenVars.numDDoors++;
			}
			else
			{
				DPlatX[numDPlats] = (int)vector4.X;
				DPlatY[numDPlats] = (int)vector4.Y;
				numDPlats++;
			}
			lastDungeonHall = vector2;
			while (num3 > 0)
			{
				if (vector2.X > 0f && vector4.X > (float)(Main.maxTilesX - 100))
				{
					num3 = 0;
				}
				else if (vector2.X < 0f && vector4.X < 100f)
				{
					num3 = 0;
				}
				else if (vector2.Y > 0f && vector4.Y > (float)(Main.maxTilesY - 100))
				{
					num3 = 0;
				}
				else if (vector2.Y < 0f && (double)vector4.Y < Main.rockLayer + 50.0)
				{
					num3 = 0;
				}
				num3--;
				int num4 = (int)((double)vector4.X - num - 4.0 - (double)WorldGen.genRand.Next(6));
				int num5 = (int)((double)vector4.X + num + 4.0 + (double)WorldGen.genRand.Next(6));
				int num6 = (int)((double)vector4.Y - num - 4.0 - (double)WorldGen.genRand.Next(6));
				int num7 = (int)((double)vector4.Y + num + 4.0 + (double)WorldGen.genRand.Next(6));
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						Main.tile[k, l].LiquidAmount = 0;
						if (Main.tile[k, l].WallType == 0)
						{
							Tile tile = Main.tile[k, l];
							tile.HasTile = true;
							tile.TileType = (byte)tileType;
						}
					}
				}
				for (int m = num4 + 1; m < num5 - 1; m++)
				{
					for (int n = num6 + 1; n < num7 - 1; n++)
					{
						WorldGen.PlaceWall(m, n, wallType, mute: true);
					}
				}
				int num8 = 0;
				if (vector.Y == 0f && WorldGen.genRand.Next((int)num + 1) == 0)
				{
					num8 = WorldGen.genRand.Next(1, 3);
				}
				else if (vector.X == 0f && WorldGen.genRand.Next((int)num - 1) == 0)
				{
					num8 = WorldGen.genRand.Next(1, 3);
				}
				else if (WorldGen.genRand.Next((int)num * 3) == 0)
				{
					num8 = WorldGen.genRand.Next(1, 3);
				}
				num4 = (int)((double)vector4.X - num * 0.5 - (double)num8);
				num5 = (int)((double)vector4.X + num * 0.5 + (double)num8);
				num6 = (int)((double)vector4.Y - num * 0.5 - (double)num8);
				num7 = (int)((double)vector4.Y + num * 0.5 + (double)num8);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int num9 = num4; num9 < num5; num9++)
				{
					for (int num10 = num6; num10 < num7; num10++)
					{
						Tile tile = Main.tile[num9, num10];
						tile.HasTile = false;
						tile.WallType = (byte)wallType;
					}
				}
				vector4 += vector;
			}
			GenVars.dungeonX = (int)vector4.X;
			GenVars.dungeonY = (int)vector4.Y;
			if (vector2.Y == 0f)
			{
				GenVars.DDoorX[GenVars.numDDoors] = (int)vector4.X;
				GenVars.DDoorY[GenVars.numDDoors] = (int)vector4.Y;
				GenVars.DDoorPos[GenVars.numDDoors] = 0;
				GenVars.numDDoors++;
			}
			else
			{
				DPlatX[numDPlats] = (int)vector4.X;
				DPlatY[numDPlats] = (int)vector4.Y;
				numDPlats++;
			}
		}

		public static void DungeonStairs(int i, int j, int tileType, int wallType)
		{
			Vector2 vector = default(Vector2);
			double num = WorldGen.genRand.Next(5, 9);
			int num2 = 1;
			Vector2 vector2 = default(Vector2);
			vector2.X = i;
			vector2.Y = j;
			int num3 = WorldGen.genRand.Next(10, 30);
			num2 = ((i <= GenVars.dEnteranceX) ? 1 : (-1));
			if (i > Main.maxTilesX - 400)
			{
				num2 = -1;
			}
			else if (i < 400)
			{
				num2 = 1;
			}
			vector.Y = -1f;
			vector.X = num2;
			if (WorldGen.genRand.NextBool(3))
			{
				vector.X *= 0.5f;
			}
			else if (WorldGen.genRand.NextBool(3))
			{
				vector.Y *= 2f;
			}
			while (num3 > 0)
			{
				num3--;
				int num4 = (int)((double)vector2.X - num - 4.0 - (double)WorldGen.genRand.Next(6));
				int num5 = (int)((double)vector2.X + num + 4.0 + (double)WorldGen.genRand.Next(6));
				int num6 = (int)((double)vector2.Y - num - 4.0);
				int num7 = (int)((double)vector2.Y + num + 4.0 + (double)WorldGen.genRand.Next(6));
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				int num8 = 1;
				if (vector2.X > (float)(Main.maxTilesX / 2))
				{
					num8 = -1;
				}
				int num9 = (int)(vector2.X + (float)GenVars.dxStrength1 * 0.6f * (float)num8 + (float)GenVars.dxStrength2 * (float)num8);
				int num10 = (int)(GenVars.dyStrength2 * 0.5);
				if ((double)vector2.Y < Main.worldSurface - 5.0 && Main.tile[num9, (int)((double)vector2.Y - num - 6.0 + (double)num10)].WallType == 0 && Main.tile[num9, (int)((double)vector2.Y - num - 7.0 + (double)num10)].WallType == 0 && Main.tile[num9, (int)((double)vector2.Y - num - 8.0 + (double)num10)].WallType == 0)
				{
					GenVars.dSurface = true;
					WorldGen.TileRunner(num9, (int)((double)vector2.Y - num - 6.0 + (double)num10), WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(10, 20), -1, addTile: false, 0f, -1f);
				}
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						Main.tile[k, l].LiquidAmount = 0;
						if (Main.tile[k, l].WallType != wallType)
						{
							Tile tile = Main.tile[k, l];
							tile.WallType = 0;
							tile.HasTile = true;
							tile.TileType = (byte)tileType;
						}
					}
				}
				for (int m = num4 + 1; m < num5 - 1; m++)
				{
					for (int n = num6 + 1; n < num7 - 1; n++)
					{
						WorldGen.PlaceWall(m, n, wallType, mute: true);
					}
				}
				int num11 = 0;
				if (WorldGen.genRand.NextBool((int)num))
				{
					num11 = WorldGen.genRand.Next(1, 3);
				}
				num4 = (int)((double)vector2.X - num * 0.5 - (double)num11);
				num5 = (int)((double)vector2.X + num * 0.5 + (double)num11);
				num6 = (int)((double)vector2.Y - num * 0.5 - (double)num11);
				num7 = (int)((double)vector2.Y + num * 0.5 + (double)num11);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int num12 = num4; num12 < num5; num12++)
				{
					for (int num13 = num6; num13 < num7; num13++)
					{
						Tile tile = Main.tile[num12, num13];
						tile.HasTile = false;
						WorldGen.PlaceWall(num12, num13, wallType, mute: true);
					}
				}
				if (GenVars.dSurface)
				{
					num3 = 0;
				}
				vector2 += vector;
			}
			GenVars.dungeonX = (int)vector2.X;
			GenVars.dungeonY = (int)vector2.Y;
		}

		public static void DungeonEnt(int i, int j, int tileType, int wallType)
		{
			int num = 60;
			for (int k = i - num; k < i + num; k++)
			{
				for (int l = j - num; l < j + num; l++)
				{
					Tile tile = Main.tile[k, l];
					tile.LiquidAmount = 0;
					tile.LiquidType = 0;
				}
			}
			double num2 = GenVars.dxStrength1;
			double num3 = GenVars.dyStrength1;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = (float)j - (float)num3 / 2f;
			GenVars.dMinY = (int)vector.Y;
			int num4 = 1;
			if (i > Main.maxTilesX / 2)
			{
				num4 = -1;
			}
			int num5 = (int)((double)vector.X - num2 * 0.6000000238418579 - (double)WorldGen.genRand.Next(2, 5));
			int num6 = (int)((double)vector.X + num2 * 0.6000000238418579 + (double)WorldGen.genRand.Next(2, 5));
			int num7 = (int)((double)vector.Y - num3 * 0.6000000238418579 - (double)WorldGen.genRand.Next(2, 5));
			int num8 = (int)((double)vector.Y + num3 * 0.6000000238418579 + (double)WorldGen.genRand.Next(8, 16));
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int m = num5; m < num6; m++)
			{
				for (int n = num7; n < num8; n++)
				{
					Main.tile[m, n].LiquidAmount = 0;
					if (Main.tile[m, n].WallType != wallType)
					{
						Main.tile[m, n].WallType = 0;
						if (m > num5 + 1 && m < num6 - 2 && n > num7 + 1 && n < num8 - 2)
						{
							WorldGen.PlaceWall(m, n, wallType, mute: true);
						}
						Tile tile = Main.tile[m, n];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			int num9 = num5;
			int num10 = num5 + 5 + WorldGen.genRand.Next(4);
			int num11 = num7 - 3 - WorldGen.genRand.Next(3);
			int num12 = num7;
			for (int num13 = num9; num13 < num10; num13++)
			{
				for (int num14 = num11; num14 < num12; num14++)
				{
					if (Main.tile[num13, num14].WallType != wallType)
					{
						Tile tile = Main.tile[num13, num14];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			num9 = num6 - 5 - WorldGen.genRand.Next(4);
			num10 = num6;
			num11 = num7 - 3 - WorldGen.genRand.Next(3);
			num12 = num7;
			for (int num15 = num9; num15 < num10; num15++)
			{
				for (int num16 = num11; num16 < num12; num16++)
				{
					if (Main.tile[num15, num16].WallType != wallType)
					{
						Tile tile = Main.tile[num15, num16];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			int num17 = 1 + WorldGen.genRand.Next(2);
			int num18 = 2 + WorldGen.genRand.Next(4);
			int num19 = 0;
			for (int num20 = num5; num20 < num6; num20++)
			{
				for (int num21 = num7 - num17; num21 < num7; num21++)
				{
					if (Main.tile[num20, num21].WallType != wallType)
					{
						Tile tile = Main.tile[num20, num21];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
				num19++;
				if (num19 >= num18)
				{
					num20 += num18;
					num19 = 0;
				}
			}
			for (int num22 = num5; num22 < num6; num22++)
			{
				for (int num23 = num8; num23 < num8 + 100; num23++)
				{
					WorldGen.PlaceWall(num22, num23, 2, mute: true);
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.6000000238418579);
			num6 = (int)((double)vector.X + num2 * 0.6000000238418579);
			num7 = (int)((double)vector.Y - num3 * 0.6000000238418579);
			num8 = (int)((double)vector.Y + num3 * 0.6000000238418579);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num24 = num5; num24 < num6; num24++)
			{
				for (int num25 = num7; num25 < num8; num25++)
				{
					WorldGen.PlaceWall(num24, num25, wallType, mute: true);
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.6 - 1.0);
			num6 = (int)((double)vector.X + num2 * 0.6 + 1.0);
			num7 = (int)((double)vector.Y - num3 * 0.6 - 1.0);
			num8 = (int)((double)vector.Y + num3 * 0.6 + 1.0);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num26 = num5; num26 < num6; num26++)
			{
				for (int num27 = num7; num27 < num8; num27++)
				{
					Main.tile[num26, num27].WallType = (byte)wallType;
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num7 = (int)((double)vector.Y - num3 * 0.5);
			num8 = (int)((double)vector.Y + num3 * 0.5);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num28 = num5; num28 < num6; num28++)
			{
				for (int num29 = num7; num29 < num8; num29++)
				{
					Tile tile = Main.tile[num28, num29];
					tile.HasTile = false;
					tile.WallType = (byte)wallType;
				}
			}
			DPlatX[numDPlats] = (int)vector.X;
			DPlatY[numDPlats] = num8;
			numDPlats++;
			vector.X += (float)num2 * 0.6f * (float)num4;
			vector.Y += (float)num3 * 0.5f;
			num2 = GenVars.dxStrength2;
			num3 = GenVars.dyStrength2;
			vector.X += (float)num2 * 0.55f * (float)num4;
			vector.Y -= (float)num3 * 0.5f;
			num5 = (int)((double)vector.X - num2 * 0.6000000238418579 - (double)WorldGen.genRand.Next(1, 3));
			num6 = (int)((double)vector.X + num2 * 0.6000000238418579 + (double)WorldGen.genRand.Next(1, 3));
			num7 = (int)((double)vector.Y - num3 * 0.6000000238418579 - (double)WorldGen.genRand.Next(1, 3));
			num8 = (int)((double)vector.Y + num3 * 0.6000000238418579 + (double)WorldGen.genRand.Next(6, 16));
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num30 = num5; num30 < num6; num30++)
			{
				for (int num31 = num7; num31 < num8; num31++)
				{
					if (Main.tile[num30, num31].WallType == wallType)
					{
						continue;
					}
					bool flag = true;
					if (num4 < 0)
					{
						if ((double)num30 < (double)vector.X - num2 * 0.5)
						{
							flag = false;
						}
					}
					else if ((double)num30 > (double)vector.X + num2 * 0.5 - 1.0)
					{
						flag = false;
					}
					if (flag)
					{
						Tile tile = Main.tile[num30, num31];
						tile.WallType = 0;
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			for (int num32 = num5; num32 < num6; num32++)
			{
				for (int num33 = num8; num33 < num8 + 100; num33++)
				{
					WorldGen.PlaceWall(num32, num33, 2, mute: true);
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num9 = num5;
			if (num4 < 0)
			{
				num9++;
			}
			num10 = num9 + 5 + WorldGen.genRand.Next(4);
			num11 = num7 - 3 - WorldGen.genRand.Next(3);
			num12 = num7;
			for (int num34 = num9; num34 < num10; num34++)
			{
				for (int num35 = num11; num35 < num12; num35++)
				{
					if (Main.tile[num34, num35].WallType != wallType)
					{
						Tile tile = Main.tile[num34, num35];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			num9 = num6 - 5 - WorldGen.genRand.Next(4);
			num10 = num6;
			num11 = num7 - 3 - WorldGen.genRand.Next(3);
			num12 = num7;
			for (int num36 = num9; num36 < num10; num36++)
			{
				for (int num37 = num11; num37 < num12; num37++)
				{
					if (Main.tile[num36, num37].WallType != wallType)
					{
						Tile tile = Main.tile[num36, num37];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			num17 = 1 + WorldGen.genRand.Next(2);
			num18 = 2 + WorldGen.genRand.Next(4);
			num19 = 0;
			if (num4 < 0)
			{
				num6++;
			}
			for (int num38 = num5 + 1; num38 < num6 - 1; num38++)
			{
				for (int num39 = num7 - num17; num39 < num7; num39++)
				{
					if (Main.tile[num38, num39].WallType != wallType)
					{
						Tile tile = Main.tile[num38, num39];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
				num19++;
				if (num19 >= num18)
				{
					num38 += num18;
					num19 = 0;
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.6);
			num6 = (int)((double)vector.X + num2 * 0.6);
			num7 = (int)((double)vector.Y - num3 * 0.6);
			num8 = (int)((double)vector.Y + num3 * 0.6);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num40 = num5; num40 < num6; num40++)
			{
				for (int num41 = num7; num41 < num8; num41++)
				{
					Main.tile[num40, num41].WallType = 0;
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num7 = (int)((double)vector.Y - num3 * 0.5);
			num8 = (int)((double)vector.Y + num3 * 0.5);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num42 = num5; num42 < num6; num42++)
			{
				for (int num43 = num7; num43 < num8; num43++)
				{
					Tile tile = Main.tile[num42, num43];
					tile.HasTile = false;
					tile.WallType = 0;
				}
			}
			for (int num44 = num5; num44 < num6; num44++)
			{
				if (!Main.tile[num44, num8].HasTile)
				{
					Tile tile = Main.tile[num44, num8];
					tile.HasTile = true;
					tile.TileType = 19;
				}
			}
			Main.dungeonX = (int)vector.X;
			Main.dungeonY = num8;
			int num45 = NPC.NewNPC(new EntitySource_WorldGen(), Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37);
			Main.npc[num45].homeless = false;
			Main.npc[num45].homeTileX = Main.dungeonX;
			Main.npc[num45].homeTileY = Main.dungeonY;
			if (num4 == 1)
			{
				int num46 = 0;
				for (int num47 = num6; num47 < num6 + 25; num47++)
				{
					num46++;
					for (int num48 = num8 + num46; num48 < num8 + 25; num48++)
					{
						Tile tile = Main.tile[num47, num48];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			else
			{
				int num49 = 0;
				for (int num50 = num5; num50 > num5 - 25; num50--)
				{
					num49++;
					for (int num51 = num8 + num49; num51 < num8 + 25; num51++)
					{
						Tile tile = Main.tile[num50, num51];
						tile.HasTile = true;
						tile.TileType = (byte)tileType;
					}
				}
			}
			num17 = 1 + WorldGen.genRand.Next(2);
			num18 = 2 + WorldGen.genRand.Next(4);
			num19 = 0;
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num5 += 2;
			num6 -= 2;
			for (int num52 = num5; num52 < num6; num52++)
			{
				for (int num53 = num7; num53 < num8; num53++)
				{
					WorldGen.PlaceWall(num52, num53, wallType, mute: true);
				}
				num19++;
				if (num19 >= num18)
				{
					num52 += num18 * 2;
					num19 = 0;
				}
			}
			vector.X -= (float)num2 * 0.6f * (float)num4;
			vector.Y += (float)num3 * 0.5f;
			num2 = 15.0;
			num3 = 3.0;
			vector.Y -= (float)num3 * 0.5f;
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num7 = (int)((double)vector.Y - num3 * 0.5);
			num8 = (int)((double)vector.Y + num3 * 0.5);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num54 = num5; num54 < num6; num54++)
			{
				for (int num55 = num7; num55 < num8; num55++)
				{
					Tile tile = Main.tile[num54, num55];
					tile.HasTile = false;
				}
			}
			if (num4 < 0)
			{
				vector.X -= 1f;
			}
			WorldGen.PlaceTile((int)vector.X, (int)vector.Y + 1, 10);
		}

		public static void ChasmRunner(int i, int j, int steps, bool makeOrb = false)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (!makeOrb)
			{
				flag2 = true;
			}
			float num = steps;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)WorldGen.genRand.Next(11) * 0.2f + 0.5f;
			int num2 = 5;
			double num3 = WorldGen.genRand.Next(5) + 7;
			while (num3 > 0.0)
			{
				if (num > 0f)
				{
					num3 += (double)WorldGen.genRand.Next(3);
					num3 -= (double)WorldGen.genRand.Next(3);
					if (num3 < 7.0)
					{
						num3 = 7.0;
					}
					if (num3 > 20.0)
					{
						num3 = 20.0;
					}
					if (num == 1f && num3 < 10.0)
					{
						num3 = 10.0;
					}
				}
				else if ((double)vector.Y > Main.worldSurface + 45.0)
				{
					num3 -= (double)WorldGen.genRand.Next(4);
				}
				if ((double)vector.Y > Main.rockLayer && num > 0f)
				{
					num = 0f;
				}
				num -= 1f;
				if (!flag && (double)vector.Y > Main.worldSurface + 20.0)
				{
					flag = true;
					ChasmRunnerSideways((int)vector.X, (int)vector.Y, -1, WorldGen.genRand.Next(20, 40));
					ChasmRunnerSideways((int)vector.X, (int)vector.Y, 1, WorldGen.genRand.Next(20, 40));
				}
				int num4;
				int num5;
				int num6;
				int num7;
				if (num > (float)num2)
				{
					num4 = (int)((double)vector.X - num3 * 0.5);
					num5 = (int)((double)vector.X + num3 * 0.5);
					num6 = (int)((double)vector.Y - num3 * 0.5);
					num7 = (int)((double)vector.Y + num3 * 0.5);
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (num5 > Main.maxTilesX - 1)
					{
						num5 = Main.maxTilesX - 1;
					}
					if (num6 < 0)
					{
						num6 = 0;
					}
					if (num7 > Main.maxTilesY)
					{
						num7 = Main.maxTilesY;
					}
					for (int k = num4; k < num5; k++)
					{
						for (int l = num6; l < num7; l++)
						{
							if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < num3 * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[k, l].TileType != 31 && Main.tile[k, l].TileType != 22)
							{
								Tile tile = Main.tile[k, l];
								tile.HasTile = false;
							}
						}
					}
				}
				if (num <= 2f && (double)vector.Y < Main.worldSurface + 45.0)
				{
					num = 2f;
				}
				if (num <= 0f)
				{
					if (!flag2)
					{
						flag2 = true;
						AddShadowOrb((int)vector.X, (int)vector.Y);
					}
					else if (!flag3)
					{
						flag3 = false;
						bool flag4 = false;
						int num8 = 0;
						while (!flag4)
						{
							int num9 = WorldGen.genRand.Next((int)vector.X - 25, (int)vector.X + 25);
							int num10 = WorldGen.genRand.Next((int)vector.Y - 50, (int)vector.Y);
							if (num9 < 5)
							{
								num9 = 5;
							}
							if (num9 > Main.maxTilesX - 5)
							{
								num9 = Main.maxTilesX - 5;
							}
							if (num10 < 5)
							{
								num10 = 5;
							}
							if (num10 > Main.maxTilesY - 5)
							{
								num10 = Main.maxTilesY - 5;
							}
							if ((double)num10 > Main.worldSurface)
							{
								WorldGen.Place3x2(num9, num10, 26);
								if (Main.tile[num9, num10].TileType == 26)
								{
									flag4 = true;
									continue;
								}
								num8++;
								if (num8 >= 10000)
								{
									flag4 = true;
								}
							}
							else
							{
								flag4 = true;
							}
						}
					}
				}
				vector += vector2;
				vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
				if ((double)vector2.X > 0.3)
				{
					vector2.X = 0.3f;
				}
				if ((double)vector2.X < -0.3)
				{
					vector2.X = -0.3f;
				}
				num4 = (int)((double)vector.X - num3 * 1.1);
				num5 = (int)((double)vector.X + num3 * 1.1);
				num6 = (int)((double)vector.Y - num3 * 1.1);
				num7 = (int)((double)vector.Y + num3 * 1.1);
				if (num4 < 1)
				{
					num4 = 1;
				}
				if (num5 > Main.maxTilesX - 1)
				{
					num5 = Main.maxTilesX - 1;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int m = num4; m < num5; m++)
				{
					for (int n = num6; n < num7; n++)
					{
						if ((double)(Math.Abs((float)m - vector.X) + Math.Abs((float)n - vector.Y)) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							if (Main.tile[m, n].TileType != 25 && n > j + WorldGen.genRand.Next(3, 20))
							{
								Tile tile = Main.tile[m, n];
								tile.HasTile = true;
							}
							if (steps <= num2)
							{
								Tile tile = Main.tile[m, n];
								tile.HasTile = true;
							}
							if (Main.tile[m, n].TileType != 31)
							{
								Main.tile[m, n].TileType = 25;
							}
							if (Main.tile[m, n].WallType == 2)
							{
								Main.tile[m, n].WallType = 0;
							}
						}
					}
				}
				for (int num11 = num4; num11 < num5; num11++)
				{
					for (int num12 = num6; num12 < num7; num12++)
					{
						if ((double)(Math.Abs((float)num11 - vector.X) + Math.Abs((float)num12 - vector.Y)) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							if (Main.tile[num11, num12].TileType != 31)
							{
								Main.tile[num11, num12].TileType = 25;
							}
							if (steps <= num2)
							{
								Tile tile = Main.tile[num11, num12];
								tile.HasTile = true;
							}
							if (num12 > j + WorldGen.genRand.Next(3, 20))
							{
								WorldGen.PlaceWall(num11, num12, 3, mute: true);
							}
						}
					}
				}
			}
		}

		public static void ChasmRunnerSideways(int i, int j, int direction, int steps)
		{
			float num = steps;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)WorldGen.genRand.Next(10, 21) * 0.1f * (float)direction;
			vector2.Y = (float)WorldGen.genRand.Next(-10, 10) * 0.01f;
			double num2 = WorldGen.genRand.Next(5) + 7;
			while (num2 > 0.0)
			{
				if (num > 0f)
				{
					num2 += (double)WorldGen.genRand.Next(3);
					num2 -= (double)WorldGen.genRand.Next(3);
					if (num2 < 7.0)
					{
						num2 = 7.0;
					}
					if (num2 > 20.0)
					{
						num2 = 20.0;
					}
					if (num == 1f && num2 < 10.0)
					{
						num2 = 10.0;
					}
				}
				else
				{
					num2 -= (double)WorldGen.genRand.Next(4);
				}
				if ((double)vector.Y > Main.rockLayer && num > 0f)
				{
					num = 0f;
				}
				num -= 1f;
				int num3 = (int)((double)vector.X - num2 * 0.5);
				int num4 = (int)((double)vector.X + num2 * 0.5);
				int num5 = (int)((double)vector.Y - num2 * 0.5);
				int num6 = (int)((double)vector.Y + num2 * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < num2 * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[k, l].TileType != 31 && Main.tile[k, l].TileType != 22)
						{
							Tile tile = Main.tile[k, l];
							tile.HasTile = false;
						}
					}
				}
				vector += vector2;
				vector2.Y += (float)WorldGen.genRand.Next(-10, 10) * 0.1f;
				if (vector.Y < (float)(j - 20))
				{
					vector2.Y += (float)WorldGen.genRand.Next(20) * 0.01f;
				}
				if (vector.Y > (float)(j + 20))
				{
					vector2.Y -= (float)WorldGen.genRand.Next(20) * 0.01f;
				}
				if ((double)vector2.Y < -0.5)
				{
					vector2.Y = -0.5f;
				}
				if ((double)vector2.Y > 0.5)
				{
					vector2.Y = 0.5f;
				}
				vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
				switch (direction)
				{
					case -1:
						if ((double)vector2.X > -0.5)
						{
							vector2.X = -0.5f;
						}
						if (vector2.X < -2f)
						{
							vector2.X = -2f;
						}
						break;
					case 1:
						if ((double)vector2.X < 0.5)
						{
							vector2.X = 0.5f;
						}
						if (vector2.X > 2f)
						{
							vector2.X = 2f;
						}
						break;
				}
				num3 = (int)((double)vector.X - num2 * 1.1);
				num4 = (int)((double)vector.X + num2 * 1.1);
				num5 = (int)((double)vector.Y - num2 * 1.1);
				num6 = (int)((double)vector.Y + num2 * 1.1);
				if (num3 < 1)
				{
					num3 = 1;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int m = num3; m < num4; m++)
				{
					for (int n = num5; n < num6; n++)
					{
						if ((double)(Math.Abs((float)m - vector.X) + Math.Abs((float)n - vector.Y)) < num2 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[m, n].WallType != 3)
						{
							if (Main.tile[m, n].TileType != 25 && n > j + WorldGen.genRand.Next(3, 20))
							{
								Tile tile2 = Main.tile[m, n];
								tile2.HasTile = true;
							}
							Tile tile = Main.tile[m, n];
							tile.HasTile = true;
							if (Main.tile[m, n].TileType != 31 && Main.tile[m, n].TileType != 22)
							{
								Main.tile[m, n].TileType = 25;
							}
							if (Main.tile[m, n].WallType == 2)
							{
								Main.tile[m, n].WallType = 0;
							}
						}
					}
				}
				for (int num7 = num3; num7 < num4; num7++)
				{
					for (int num8 = num5; num8 < num6; num8++)
					{
						if ((double)(Math.Abs((float)num7 - vector.X) + Math.Abs((float)num8 - vector.Y)) < num2 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[num7, num8].WallType != 3)
						{
							if (Main.tile[num7, num8].TileType != 31 && Main.tile[num7, num8].TileType != 22)
							{
								Main.tile[num7, num8].TileType = 25;
							}
							Tile tile = Main.tile[num7, num8];
							tile.HasTile = true;
							WorldGen.PlaceWall(num7, num8, 3, mute: true);
						}
					}
				}
			}
			if (WorldGen.genRand.NextBool(3))
			{
				int num9 = (int)vector.X;
				int num10;
				for (num10 = (int)vector.Y; !Main.tile[num9, num10].HasTile; num10++)
				{
				}
				WorldGen.TileRunner(num9, num10, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), 22);
			}
		}

		public static void AddShadowOrb(int x, int y)
		{
			if (x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesY - 10)
			{
				return;
			}
			for (int i = x - 1; i < x + 1; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == 31)
					{
						return;
					}
				}
			}
			Tile tile = Main.tile[x - 1, y - 1];
			tile.HasTile = true;
			tile.TileType = 31;
			tile.TileFrameX = 0;
			tile.TileFrameY = 0;
			Tile tile2 = Main.tile[x, y - 1];
			tile2.HasTile = true;
			tile2.TileType = 31;
			tile2.TileFrameX = 18;
			tile2.TileFrameY = 0;
			Tile tile3 = Main.tile[x - 1, y];
			tile3.HasTile = true;
			tile3.TileType = 31;
			tile3.TileFrameX = 0;
			tile3.TileFrameY = 18;
			Tile tile4 = Main.tile[x, y];
			tile4.HasTile = true;
			tile4.TileType = 31;
			tile4.TileFrameX = 18;
			tile4.TileFrameY = 18;
		}

		public static bool AddBuriedChest(int i, int j, int contain = 0, bool notNearOtherChests = false, int Style = -1)
		{
			/*if (WorldGen.genRand == null) //Is never null (iirc?)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}*/
			for (int k = j; k < Main.maxTilesY; k++)
			{
				if (!Main.tile[i, k].HasTile || !Main.tileSolid[Main.tile[i, k].TileType])
				{
					continue;
				}
				bool flag = false;
				int num = k;
				int num2 = -1;
				int style = 0;
				if ((double)num >= Main.worldSurface + 25.0 || contain > 0)
				{
					style = 1;
				}
				if (Style >= 0)
				{
					style = Style;
				}
				if (num > Main.maxTilesY - 205 && contain == 0)
				{
					if (GenVars.hellChest == 0)
					{
						contain = 274;
						style = 4;
						flag = true;
					}
					else if (GenVars.hellChest == 1)
					{
						contain = 220;
						style = 4;
						flag = true;
					}
					else if (GenVars.hellChest == 2)
					{
						contain = 112;
						style = 4;
						flag = true;
					}
					else if (GenVars.hellChest == 3)
					{
						contain = 218;
						style = 4;
						flag = true;
						GenVars.hellChest = 0;
					}
				}
				num2 = PlaceChest(i - 1, num - 1, 21, notNearOtherChests, style);
				if (num2 >= 0)
				{
					if (flag)
					{
						GenVars.hellChest++;
					}
					int num3 = 0;
					while (num3 == 0)
					{
						if ((double)num < Main.worldSurface + 25.0)
						{
							if (contain > 0)
							{
								Main.chest[num2].item[num3].SetDefaults(contain);
								Main.chest[num2].item[num3].Prefix(-1);
								num3++;
							}
							else
							{
								int num4 = WorldGen.genRand.Next(6);
								if (num4 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(280);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num4 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(281);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num4 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(284);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num4 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(282);
									Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(50, 75);
								}
								if (num4 == 4)
								{
									Main.chest[num2].item[num3].SetDefaults(279);
									Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(25, 50);
								}
								if (num4 == 5)
								{
									Main.chest[num2].item[num3].SetDefaults(285);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								num3++;
							}
							if (WorldGen.genRand.NextBool(3))
							{
								Main.chest[num2].item[num3].SetDefaults(168);
								Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(3, 6);
								num3++;
							}
							if (WorldGen.genRand.NextBool(2))
							{
								int num5 = WorldGen.genRand.Next(2);
								int stack = WorldGen.genRand.Next(8) + 3;
								if (num5 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(20);
								}
								if (num5 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(22);
								}
								Main.chest[num2].item[num3].stack = stack;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num6 = WorldGen.genRand.Next(2);
								int stack2 = WorldGen.genRand.Next(26) + 25;
								if (num6 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(40);
								}
								if (num6 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(42);
								}
								Main.chest[num2].item[num3].stack = stack2;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num7 = WorldGen.genRand.Next(1);
								int stack3 = WorldGen.genRand.Next(3) + 3;
								if (num7 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(28);
								}
								Main.chest[num2].item[num3].stack = stack3;
								num3++;
							}
							if (WorldGen.genRand.Next(3) > 0)
							{
								int num8 = WorldGen.genRand.Next(4);
								int stack4 = WorldGen.genRand.Next(1, 3);
								if (num8 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(292);
								}
								if (num8 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(298);
								}
								if (num8 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(299);
								}
								if (num8 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(290);
								}
								Main.chest[num2].item[num3].stack = stack4;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num9 = WorldGen.genRand.Next(2);
								int stack5 = WorldGen.genRand.Next(11) + 10;
								if (num9 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(8);
								}
								if (num9 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(31);
								}
								Main.chest[num2].item[num3].stack = stack5;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(72);
								Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(10, 30);
								num3++;
							}
							continue;
						}
						if ((double)num < Main.rockLayer)
						{
							if (contain > 0)
							{
								Main.chest[num2].item[num3].SetDefaults(contain);
								Main.chest[num2].item[num3].Prefix(-1);
								num3++;
							}
							else
							{
								int num10 = WorldGen.genRand.Next(7);
								if (num10 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(49);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num10 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(50);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num10 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(52);
								}
								if (num10 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(53);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num10 == 4)
								{
									Main.chest[num2].item[num3].SetDefaults(54);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num10 == 5)
								{
									Main.chest[num2].item[num3].SetDefaults(55);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num10 == 6)
								{
									Main.chest[num2].item[num3].SetDefaults(51);
									Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(26) + 25;
								}
								num3++;
							}
							if (WorldGen.genRand.Next(3) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(166);
								Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(10, 20);
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num11 = WorldGen.genRand.Next(2);
								int stack6 = WorldGen.genRand.Next(10) + 5;
								if (num11 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(22);
								}
								if (num11 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(21);
								}
								Main.chest[num2].item[num3].stack = stack6;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num12 = WorldGen.genRand.Next(2);
								int stack7 = WorldGen.genRand.Next(25) + 25;
								if (num12 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(40);
								}
								if (num12 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(42);
								}
								Main.chest[num2].item[num3].stack = stack7;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num13 = WorldGen.genRand.Next(1);
								int stack8 = WorldGen.genRand.Next(3) + 3;
								if (num13 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(28);
								}
								Main.chest[num2].item[num3].stack = stack8;
								num3++;
							}
							if (WorldGen.genRand.Next(3) > 0)
							{
								int num14 = WorldGen.genRand.Next(7);
								int stack9 = WorldGen.genRand.Next(1, 3);
								if (num14 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(289);
								}
								if (num14 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(298);
								}
								if (num14 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(299);
								}
								if (num14 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(290);
								}
								if (num14 == 4)
								{
									Main.chest[num2].item[num3].SetDefaults(303);
								}
								if (num14 == 5)
								{
									Main.chest[num2].item[num3].SetDefaults(291);
								}
								if (num14 == 6)
								{
									Main.chest[num2].item[num3].SetDefaults(304);
								}
								Main.chest[num2].item[num3].stack = stack9;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int stack10 = WorldGen.genRand.Next(11) + 10;
								Main.chest[num2].item[num3].SetDefaults(8);
								Main.chest[num2].item[num3].stack = stack10;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(72);
								Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(50, 90);
								num3++;
							}
							continue;
						}
						if (num < Main.maxTilesY - 250)
						{
							if (contain > 0)
							{
								Main.chest[num2].item[num3].SetDefaults(contain);
								Main.chest[num2].item[num3].Prefix(-1);
								num3++;
							}
							else
							{
								int num15 = WorldGen.genRand.Next(7);
								if (num15 == 2 && WorldGen.genRand.Next(2) == 0)
								{
									num15 = WorldGen.genRand.Next(7);
								}
								if (num15 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(49);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num15 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(50);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num15 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(52);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num15 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(53);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num15 == 4)
								{
									Main.chest[num2].item[num3].SetDefaults(54);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num15 == 5)
								{
									Main.chest[num2].item[num3].SetDefaults(55);
									Main.chest[num2].item[num3].Prefix(-1);
								}
								if (num15 == 6)
								{
									Main.chest[num2].item[num3].SetDefaults(51);
									Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(26) + 25;
								}
								num3++;
							}
							if (WorldGen.genRand.Next(5) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(43);
								num3++;
							}
							if (WorldGen.genRand.Next(3) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(167);
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num16 = WorldGen.genRand.Next(2);
								int stack11 = WorldGen.genRand.Next(8) + 3;
								if (num16 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(19);
								}
								if (num16 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(21);
								}
								Main.chest[num2].item[num3].stack = stack11;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num17 = WorldGen.genRand.Next(2);
								int stack12 = WorldGen.genRand.Next(26) + 25;
								if (num17 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(41);
								}
								if (num17 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(279);
								}
								Main.chest[num2].item[num3].stack = stack12;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num18 = WorldGen.genRand.Next(1);
								int stack13 = WorldGen.genRand.Next(3) + 3;
								if (num18 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(188);
								}
								Main.chest[num2].item[num3].stack = stack13;
								num3++;
							}
							if (WorldGen.genRand.Next(3) > 0)
							{
								int num19 = WorldGen.genRand.Next(6);
								int stack14 = WorldGen.genRand.Next(1, 3);
								if (num19 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(296);
								}
								if (num19 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(295);
								}
								if (num19 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(299);
								}
								if (num19 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(302);
								}
								if (num19 == 4)
								{
									Main.chest[num2].item[num3].SetDefaults(303);
								}
								if (num19 == 5)
								{
									Main.chest[num2].item[num3].SetDefaults(305);
								}
								Main.chest[num2].item[num3].stack = stack14;
								num3++;
							}
							if (WorldGen.genRand.Next(3) > 1)
							{
								int num20 = WorldGen.genRand.Next(4);
								int stack15 = WorldGen.genRand.Next(1, 3);
								if (num20 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(301);
								}
								if (num20 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(302);
								}
								if (num20 == 2)
								{
									Main.chest[num2].item[num3].SetDefaults(297);
								}
								if (num20 == 3)
								{
									Main.chest[num2].item[num3].SetDefaults(304);
								}
								Main.chest[num2].item[num3].stack = stack15;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								int num21 = WorldGen.genRand.Next(2);
								int stack16 = WorldGen.genRand.Next(15) + 15;
								if (num21 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(8);
								}
								if (num21 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(282);
								}
								Main.chest[num2].item[num3].stack = stack16;
								num3++;
							}
							if (WorldGen.genRand.Next(2) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(73);
								Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(1, 3);
								num3++;
							}
							continue;
						}
						if (contain > 0)
						{
							Main.chest[num2].item[num3].SetDefaults(contain);
							Main.chest[num2].item[num3].Prefix(-1);
							num3++;
						}
						else
						{
							int num22 = WorldGen.genRand.Next(4);
							if (num22 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(49);
								Main.chest[num2].item[num3].Prefix(-1);
							}
							if (num22 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(50);
								Main.chest[num2].item[num3].Prefix(-1);
							}
							if (num22 == 2)
							{
								Main.chest[num2].item[num3].SetDefaults(53);
								Main.chest[num2].item[num3].Prefix(-1);
							}
							if (num22 == 3)
							{
								Main.chest[num2].item[num3].SetDefaults(54);
								Main.chest[num2].item[num3].Prefix(-1);
							}
							num3++;
						}
						if (WorldGen.genRand.Next(3) == 0)
						{
							Main.chest[num2].item[num3].SetDefaults(167);
							num3++;
						}
						if (WorldGen.genRand.Next(2) == 0)
						{
							int num23 = WorldGen.genRand.Next(2);
							int stack17 = WorldGen.genRand.Next(15) + 15;
							if (num23 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(117);
							}
							if (num23 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(19);
							}
							Main.chest[num2].item[num3].stack = stack17;
							num3++;
						}
						if (WorldGen.genRand.Next(2) == 0)
						{
							int num24 = WorldGen.genRand.Next(2);
							int stack18 = WorldGen.genRand.Next(25) + 50;
							if (num24 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(265);
							}
							if (num24 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(278);
							}
							Main.chest[num2].item[num3].stack = stack18;
							num3++;
						}
						if (WorldGen.genRand.Next(2) == 0)
						{
							int num25 = WorldGen.genRand.Next(2);
							int stack19 = WorldGen.genRand.Next(15) + 15;
							if (num25 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(226);
							}
							if (num25 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(227);
							}
							Main.chest[num2].item[num3].stack = stack19;
							num3++;
						}
						if (WorldGen.genRand.Next(4) > 0)
						{
							int num26 = WorldGen.genRand.Next(7);
							int stack20 = WorldGen.genRand.Next(1, 3);
							if (num26 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(296);
							}
							if (num26 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(295);
							}
							if (num26 == 2)
							{
								Main.chest[num2].item[num3].SetDefaults(293);
							}
							if (num26 == 3)
							{
								Main.chest[num2].item[num3].SetDefaults(288);
							}
							if (num26 == 4)
							{
								Main.chest[num2].item[num3].SetDefaults(294);
							}
							if (num26 == 5)
							{
								Main.chest[num2].item[num3].SetDefaults(297);
							}
							if (num26 == 6)
							{
								Main.chest[num2].item[num3].SetDefaults(304);
							}
							Main.chest[num2].item[num3].stack = stack20;
							num3++;
						}
						if (WorldGen.genRand.Next(3) > 0)
						{
							int num27 = WorldGen.genRand.Next(5);
							int stack21 = WorldGen.genRand.Next(1, 3);
							if (num27 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(305);
							}
							if (num27 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(301);
							}
							if (num27 == 2)
							{
								Main.chest[num2].item[num3].SetDefaults(302);
							}
							if (num27 == 3)
							{
								Main.chest[num2].item[num3].SetDefaults(288);
							}
							if (num27 == 4)
							{
								Main.chest[num2].item[num3].SetDefaults(300);
							}
							Main.chest[num2].item[num3].stack = stack21;
							num3++;
						}
						if (WorldGen.genRand.Next(2) == 0)
						{
							int num28 = WorldGen.genRand.Next(2);
							int stack22 = WorldGen.genRand.Next(15) + 15;
							if (num28 == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(8);
							}
							if (num28 == 1)
							{
								Main.chest[num2].item[num3].SetDefaults(282);
							}
							Main.chest[num2].item[num3].stack = stack22;
							num3++;
						}
						if (WorldGen.genRand.Next(2) == 0)
						{
							Main.chest[num2].item[num3].SetDefaults(73);
							Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(2, 5);
							num3++;
						}
					}
					return true;
				}
				return false;
			}
			return false;
		}

		public static int PlaceChest(int x, int y, int type = 21, bool notNearOtherChests = false, int style = 0)
		{
			bool flag = true;
			int num = -1;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					/*if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}*/
					if (Main.tile[i, j].HasTile)
					{
						flag = false;
					}
					if (Main.tile[i, j].LiquidType == LiquidID.Lava)
					{
						flag = false;
					}
				}
				/*if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}*/
				if (!Main.tile[i, y + 1].HasTile || !Main.tileSolid[Main.tile[i, y + 1].TileType])
				{
					flag = false;
				}
			}
			if (flag && notNearOtherChests)
			{
				for (int k = x - 25; k < x + 25; k++)
				{
					for (int l = y - 8; l < y + 8; l++)
					{
						try
						{
							if (Main.tile[k, l].HasTile && Main.tile[k, l].TileType == 21)
							{
								flag = false;
								return -1;
							}
						}
						catch
						{
						}
					}
				}
			}
			if (flag)
			{
				num = Chest.CreateChest(x, y - 1);
				if (num == -1)
				{
					flag = false;
				}
			}
			if (flag)
			{
				Tile tile = Main.tile[x, y - 1];
				tile.HasTile = true;
				tile.TileFrameY = 0;
				tile.TileFrameX = (short)(36 * style);
				tile.TileType = (byte)type;
				Tile tile2 = Main.tile[x + 1, y - 1];
				tile2.HasTile = true;
				tile2.TileFrameY = 0;
				tile2.TileFrameX = (short)(18 + 36 * style);
				tile2.TileType = (byte)type;
				Tile tile3 = Main.tile[x, y];
				tile3.HasTile = true;
				tile3.TileFrameY = 18;
				tile3.TileFrameX = (short)(36 * style);
				tile3.TileType = (byte)type;
				Tile tile4 = Main.tile[x + 1, y];
				tile4.HasTile = true;
				tile4.TileFrameY = 18;
				tile4.TileFrameX = (short)(18 + 36 * style);
				tile4.TileType = (byte)type;
			}
			return num;
		}

		//something broke idk
		public static bool placeTrap_Old(int x2, int y2, int type = -1)
		{
			int num = y2;
			while (!WorldGen.SolidTile(x2, num))
			{
				num++;
				if (num >= Main.maxTilesY - 300)
				{
					return false;
				}
			}
			num--;
			if (Main.tile[x2, num].LiquidAmount > 0 && Main.tile[x2, num].LiquidType == LiquidID.Lava)
			{
				return false;
			}
			if (type == -1 && Main.rand.NextBool(20))
			{
				type = 2;
			}
			else if (type == -1)
			{
				type = Main.rand.Next(2);
			}
			if (Main.tile[x2, num].HasTile || Main.tile[x2 - 1, num].HasTile || Main.tile[x2 + 1, num].HasTile || Main.tile[x2, num - 1].HasTile || Main.tile[x2 - 1, num - 1].HasTile || Main.tile[x2 + 1, num - 1].HasTile || Main.tile[x2, num - 2].HasTile || Main.tile[x2 - 1, num - 2].HasTile || Main.tile[x2 + 1, num - 2].HasTile)
			{
				return false;
			}
			if (Main.tile[x2, num + 1].TileType == 48)
			{
				return false;
			}
			switch (type)
			{
				case 0:
					{
						int num7 = x2;
						int num8 = num;
						num8 -= WorldGen.genRand.Next(3);
						while (!WorldGen.SolidTile(num7, num8))
						{
							num7--;
						}
						int num9 = num7;
						for (num7 = x2; !WorldGen.SolidTile(num7, num8); num7++)
						{
						}
						int num10 = num7;
						int num11 = x2 - num9;
						int num12 = num10 - x2;
						bool flag = false;
						bool flag2 = false;
						if (num11 > 5 && num11 < 50)
						{
							flag = true;
						}
						if (num12 > 5 && num12 < 50)
						{
							flag2 = true;
						}
						if (flag && !WorldGen.SolidTile(num9, num8 + 1))
						{
							flag = false;
						}
						if (flag2 && !WorldGen.SolidTile(num10, num8 + 1))
						{
							flag2 = false;
						}
						if (flag && (Main.tile[num9, num8].TileType == 10 || Main.tile[num9, num8].TileType == 48 || Main.tile[num9, num8 + 1].TileType == 10 || Main.tile[num9, num8 + 1].TileType == 48))
						{
							flag = false;
						}
						if (flag2 && (Main.tile[num10, num8].TileType == 10 || Main.tile[num10, num8].TileType == 48 || Main.tile[num10, num8 + 1].TileType == 10 || Main.tile[num10, num8 + 1].TileType == 48))
						{
							flag2 = false;
						}
						int num13 = 0;
						if (flag && flag2)
						{
							num13 = 1;
							num7 = num9;
							if (WorldGen.genRand.NextBool(2))
							{
								num7 = num10;
								num13 = -1;
							}
						}
						else if (flag2)
						{
							num7 = num10;
							num13 = -1;
						}
						else
						{
							if (!flag)
							{
								return false;
							}
							num7 = num9;
							num13 = 1;
						}
						if (Main.tile[x2, num].WallType > 0)
						{
							WorldGen.PlaceTile(x2, num, 135, mute: true, forced: true, -1, 2);
						}
						else
						{
							WorldGen.PlaceTile(x2, num, 135, mute: true, forced: true, -1, WorldGen.genRand.Next(2, 4));
						}
						WorldGen.KillTile(num7, num8);
						WorldGen.PlaceTile(num7, num8, 137, mute: true, forced: true, -1, num13);
						int num14 = x2;
						int num15 = num;
						while (num14 != num7 || num15 != num8)
						{
							Tile tile = Main.tile[num14, num15];
							tile.RedWire = true;
							if (num14 > num7)
							{
								num14--;
							}
							if (num14 < num7)
							{
								num14++;
							}
							Tile tile2 = Main.tile[num14, num15];
							tile2.RedWire = true;
							if (num15 > num8)
							{
								num15--;
							}
							if (num15 < num8)
							{
								num15++;
							}
							Tile tile3 = Main.tile[num14, num15];
							tile3.RedWire = true;
						}
						return true;
					}
				case 1:
					{
						int num16 = x2;
						int num17 = num - 8;
						num16 += WorldGen.genRand.Next(-1, 2);
						bool flag3 = true;
						while (flag3)
						{
							bool flag4 = true;
							int num18 = 0;
							for (int l = num16 - 2; l <= num16 + 3; l++)
							{
								for (int m = num17; m <= num17 + 3; m++)
								{
									if (!WorldGen.SolidTile(l, m))
									{
										flag4 = false;
									}
									if (Main.tile[l, m].HasTile && (Main.tile[l, m].TileType == 0 || Main.tile[l, m].TileType == 1 || Main.tile[l, m].TileType == 59))
									{
										num18++;
									}
								}
							}
							num17--;
							if ((double)num17 < Main.worldSurface)
							{
								return false;
							}
							if (flag4 && num18 > 2)
							{
								flag3 = false;
							}
						}
						if (num - num17 <= 5 || num - num17 >= 40)
						{
							return false;
						}
						for (int n = num16; n <= num16 + 1; n++)
						{
							for (int num19 = num17; num19 <= num; num19++)
							{
								if (WorldGen.SolidTile(n, num19))
								{
									WorldGen.KillTile(n, num19);
								}
							}
						}
						for (int num20 = num16 - 2; num20 <= num16 + 3; num20++)
						{
							for (int num21 = num17 - 2; num21 <= num17 + 3; num21++)
							{
								if (WorldGen.SolidTile(num20, num21))
								{
									Main.tile[num20, num21].TileType = 1;
								}
							}
						}
						WorldGen.PlaceTile(x2, num, 135, mute: true, forced: true, -1, WorldGen.genRand.Next(2, 4));
						WorldGen.PlaceTile(num16, num17 + 2, 130, mute: true);
						WorldGen.PlaceTile(num16 + 1, num17 + 2, 130, mute: true);
						WorldGen.PlaceTile(num16 + 1, num17 + 1, 138, mute: true);
						num17 += 2;
						Tile tile = Main.tile[num16, num17];
						tile.RedWire = true;
						Tile tile2 = Main.tile[num16 + 1, num17];
						tile2.RedWire = true;
						num17++;
						WorldGen.PlaceTile(num16, num17, 130, mute: true);
						WorldGen.PlaceTile(num16 + 1, num17, 130, mute: true);
						Tile tile3 = Main.tile[num16, num17];
						tile3.RedWire = true;
						Tile tile4 = Main.tile[num16 + 1, num17];
						tile4.RedWire = true;
						WorldGen.PlaceTile(num16, num17 + 1, 130, mute: true);
						WorldGen.PlaceTile(num16 + 1, num17 + 1, 130, mute: true);
						Tile tile5 = Main.tile[num16, num17 + 1];
						tile5.RedWire = true;
						Tile tile6 = Main.tile[num16 + 1, num17 + 1];
						tile6.RedWire = true;
						int num22 = x2;
						int num23 = num;
						while (num22 != num16 || num23 != num17)
						{
							Tile tile7 = Main.tile[num22, num23];
							tile7.RedWire = true;
							if (num22 > num16)
							{
								num22--;
							}
							if (num22 < num16)
							{
								num22++;
							}
							Tile tile8 = Main.tile[num22, num23];
							tile8.RedWire = true;
							if (num23 > num17)
							{
								num23--;
							}
							if (num23 < num17)
							{
								num23++;
							}
							Tile tile9 = Main.tile[num22, num23];
							tile9.RedWire = true;
						}
						return true;
					}
				case 2:
					{
						int num2 = Main.rand.Next(4, 7);
						int num3 = x2;
						num3 += Main.rand.Next(-1, 2);
						int num4 = num;
						for (int i = 0; i < num2; i++)
						{
							num4++;
							if (!WorldGen.SolidTile(num3, num4))
							{
								return false;
							}
						}
						for (int j = num3 - 2; j <= num3 + 2; j++)
						{
							for (int k = num4 - 2; k <= num4 + 2; k++)
							{
								if (!WorldGen.SolidTile(j, k))
								{
									return false;
								}
							}
						}
						WorldGen.KillTile(num3, num4);
						Tile tile = Main.tile[num3, num4];
						tile.HasTile = true;
						tile.TileType = 141;
						tile.TileFrameX = 0;
						tile.TileFrameY = (short)(18 * Main.rand.Next(2));
						WorldGen.PlaceTile(x2, num, 135, mute: true, forced: true, -1, WorldGen.genRand.Next(2, 4));
						int num5 = x2;
						int num6 = num;
						while (num5 != num3 || num6 != num4)
						{
							Tile tile2 = Main.tile[num5, num6];
							tile2.RedWire = true;
							if (num5 > num3)
							{
								num5--;
							}
							if (num5 < num3)
							{
								num5++;
							}
							Tile tile3 = Main.tile[num5, num6];
							tile3.RedWire = true;
							if (num6 > num4)
							{
								num6--;
							}
							if (num6 < num4)
							{
								num6++;
							}
							Tile tile4 = Main.tile[num5, num6];
							tile4.RedWire = true;
						}
						break;
					}
			}
			return false;
		}

		public static void Place1x2Top(int x, int y, int type)
		{
			short frameX = 0;
			if (Main.tile[x, y - 1].HasTile && Main.tileSolid[Main.tile[x, y - 1].TileType] && !Main.tileSolidTop[Main.tile[x, y - 1].TileType] && !Main.tile[x, y + 1].HasTile)
			{
				Tile tile = Main.tile[x, y];
				tile.HasTile = true;
				tile.TileFrameY = 0;
				tile.TileFrameX = frameX;
				tile.TileType = (byte)type;
				Tile tile2 = Main.tile[x, y + 1];
				tile2.HasTile = true;
				tile2.TileFrameY = 18;
				tile2.TileFrameX = frameX;
				tile2.TileType = (byte)type;
			}
		}

		public static void Cavinator(int i, int j, int steps)
		{
			double num = WorldGen.genRand.Next(7, 15);
			double num2 = num;
			int num3 = 1;
			if (WorldGen.genRand.NextBool(2))
			{
				num3 = -1;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			int num4 = WorldGen.genRand.Next(20, 40);
			Vector2 vector2 = default(Vector2);
			vector2.Y = (float)WorldGen.genRand.Next(10, 20) * 0.01f;
			vector2.X = num3;
			while (num4 > 0)
			{
				num4--;
				int num5 = (int)((double)vector.X - num * 0.5);
				int num6 = (int)((double)vector.X + num * 0.5);
				int num7 = (int)((double)vector.Y - num * 0.5);
				int num8 = (int)((double)vector.Y + num * 0.5);
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesX)
				{
					num6 = Main.maxTilesX;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesY)
				{
					num8 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						float num9 = Math.Abs((float)k - vector.X);
						float num10 = Math.Abs((float)l - vector.Y);
						if (Math.Sqrt(num9 * num9 + num10 * num10) < num2 * 0.4)
						{
							Tile tile = Main.tile[k, l];
							tile.HasTile = false;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				vector2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > (float)num3 + 0.5f)
				{
					vector2.X = (float)num3 + 0.5f;
				}
				if (vector2.X < (float)num3 - 0.5f)
				{
					vector2.X = (float)num3 - 0.5f;
				}
				if (vector2.Y > 2f)
				{
					vector2.Y = 2f;
				}
				if (vector2.Y < 0f)
				{
					vector2.Y = 0f;
				}
			}
			if (steps > 0 && (double)(int)vector.Y < Main.rockLayer + 50.0)
			{
				Cavinator((int)vector.X, (int)vector.Y, steps - 1);
			}
		}

		public static void CaveOpenater(int i, int j)
		{
			double num = WorldGen.genRand.Next(7, 12);
			double num2 = num;
			int num3 = 1;
			if (WorldGen.genRand.NextBool(2))
			{
				num3 = -1;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			int num4 = 100;
			Vector2 vector2 = default(Vector2);
			vector2.Y = 0f;
			vector2.X = num3;
			while (num4 > 0)
			{
				if (Main.tile[(int)vector.X, (int)vector.Y].WallType == 0)
				{
					num4 = 0;
				}
				num4--;
				int num5 = (int)((double)vector.X - num * 0.5);
				int num6 = (int)((double)vector.X + num * 0.5);
				int num7 = (int)((double)vector.Y - num * 0.5);
				int num8 = (int)((double)vector.Y + num * 0.5);
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesX)
				{
					num6 = Main.maxTilesX;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesY)
				{
					num8 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						float num9 = Math.Abs((float)k - vector.X);
						float num10 = Math.Abs((float)l - vector.Y);
						if (Math.Sqrt(num9 * num9 + num10 * num10) < num2 * 0.4)
						{
							Tile tile = Main.tile[k, l];
							tile.HasTile = false;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				vector2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > (float)num3 + 0.5f)
				{
					vector2.X = (float)num3 + 0.5f;
				}
				if (vector2.X < (float)num3 - 0.5f)
				{
					vector2.X = (float)num3 - 0.5f;
				}
				if (vector2.Y > 0f)
				{
					vector2.Y = 0f;
				}
				if ((double)vector2.Y < -0.5)
				{
					vector2.Y = -0.5f;
				}
			}
		}

		public static void MineHouse(int i, int j)
		{
			if (i < 50 || i > Main.maxTilesX - 50 || j < 50 || j > Main.maxTilesY - 50)
			{
				return;
			}
			int num = WorldGen.genRand.Next(6, 12);
			int num2 = WorldGen.genRand.Next(3, 6);
			int num3 = WorldGen.genRand.Next(15, 30);
			int num4 = WorldGen.genRand.Next(15, 30);
			if (WorldGen.SolidTile(i, j) || Main.tile[i, j].WallType > 0)
			{
				return;
			}
			int num5 = j - num;
			int num6 = j + num2;
			for (int k = 0; k < 2; k++)
			{
				bool flag = true;
				int num7 = i;
				int num8 = j;
				int num9 = -1;
				int num10 = num3;
				if (k == 1)
				{
					num9 = 1;
					num10 = num4;
					num7++;
				}
				while (flag)
				{
					if (num8 - num < num5)
					{
						num5 = num8 - num;
					}
					if (num8 + num2 > num6)
					{
						num6 = num8 + num2;
					}
					for (int l = 0; l < 2; l++)
					{
						int num11 = num8;
						bool flag2 = true;
						int num12 = num;
						int num13 = -1;
						if (l == 1)
						{
							num11++;
							num12 = num2;
							num13 = 1;
						}
						while (flag2)
						{
							if (num7 != i && Main.tile[num7 - num9, num11].WallType != 27 && (WorldGen.SolidTile(num7 - num9, num11) || !Main.tile[num7 - num9, num11].HasTile))
							{
								Tile tile = Main.tile[num7 - num9, num11];
								tile.HasTile = true;
								tile.TileType = 30;
							}
							if (WorldGen.SolidTile(num7 - 1, num11))
							{
								Main.tile[num7 - 1, num11].TileType = 30;
							}
							if (WorldGen.SolidTile(num7 + 1, num11))
							{
								Main.tile[num7 + 1, num11].TileType = 30;
							}
							if (WorldGen.SolidTile(num7, num11))
							{
								int num14 = 0;
								if (WorldGen.SolidTile(num7 - 1, num11))
								{
									num14++;
								}
								if (WorldGen.SolidTile(num7 + 1, num11))
								{
									num14++;
								}
								if (WorldGen.SolidTile(num7, num11 - 1))
								{
									num14++;
								}
								if (WorldGen.SolidTile(num7, num11 + 1))
								{
									num14++;
								}
								if (num14 < 2)
								{
									Tile tile = Main.tile[num7, num11];
									tile.HasTile = false;
								}
								else
								{
									flag2 = false;
									Main.tile[num7, num11].TileType = 30;
								}
							}
							else
							{
								Tile tile = Main.tile[num7, num11];
								tile.WallType = 27;
								tile.LiquidAmount = 0;
								tile.LiquidType = 0;
							}
							num11 += num13;
							num12--;
							if (num12 <= 0)
							{
								if (!Main.tile[num7, num11].HasTile)
								{
									Tile tile = Main.tile[num7, num11];
									tile.HasTile = true;
									tile.TileType = 30;
								}
								flag2 = false;
							}
						}
					}
					num10--;
					num7 += num9;
					if (WorldGen.SolidTile(num7, num8))
					{
						int num15 = 0;
						int num16 = 0;
						int num17 = num8;
						bool flag3 = true;
						while (flag3)
						{
							num17--;
							num15++;
							if (WorldGen.SolidTile(num7 - num9, num17))
							{
								num15 = 999;
								flag3 = false;
							}
							else if (!WorldGen.SolidTile(num7, num17))
							{
								flag3 = false;
							}
						}
						num17 = num8;
						flag3 = true;
						while (flag3)
						{
							num17++;
							num16++;
							if (WorldGen.SolidTile(num7 - num9, num17))
							{
								num16 = 999;
								flag3 = false;
							}
							else if (!WorldGen.SolidTile(num7, num17))
							{
								flag3 = false;
							}
						}
						if (num16 <= num15)
						{
							if (num16 > num2)
							{
								num10 = 0;
							}
							else
							{
								num8 += num16 + 1;
							}
						}
						else if (num15 > num)
						{
							num10 = 0;
						}
						else
						{
							num8 -= num15 + 1;
						}
					}
					if (num10 <= 0)
					{
						flag = false;
					}
				}
			}
			int num18 = i - num3 - 1;
			int num19 = i + num4 + 2;
			int num20 = num5 - 1;
			int num21 = num6 + 2;
			for (int m = num18; m < num19; m++)
			{
				for (int n = num20; n < num21; n++)
				{
					if (Main.tile[m, n].WallType == 27 && !Main.tile[m, n].HasTile)
					{
						if (Main.tile[m - 1, n].WallType != 27 && m < i && !WorldGen.SolidTile(m - 1, n))
						{
							WorldGen.PlaceTile(m, n, 30, mute: true);
							Main.tile[m, n].WallType = 0;
						}
						if (Main.tile[m + 1, n].WallType != 27 && m > i && !WorldGen.SolidTile(m + 1, n))
						{
							WorldGen.PlaceTile(m, n, 30, mute: true);
							Main.tile[m, n].WallType = 0;
						}
						for (int num22 = m - 1; num22 <= m + 1; num22++)
						{
							for (int num23 = n - 1; num23 <= n + 1; num23++)
							{
								if (WorldGen.SolidTile(num22, num23))
								{
									Main.tile[num22, num23].TileType = 30;
								}
							}
						}
					}
					if (Main.tile[m, n].TileType == 30 && Main.tile[m - 1, n].WallType == 27 && Main.tile[m + 1, n].WallType == 27 && (Main.tile[m, n - 1].WallType == 27 || Main.tile[m, n - 1].HasTile) && (Main.tile[m, n + 1].WallType == 27 || Main.tile[m, n + 1].HasTile))
					{
						Tile tile = Main.tile[m, n];
						tile.HasTile = false;
						tile.WallType = 27;
					}
				}
			}
			for (int num24 = num18; num24 < num19; num24++)
			{
				for (int num25 = num20; num25 < num21; num25++)
				{
					if (Main.tile[num24, num25].WallType == 30)
					{
						if (Main.tile[num24 - 1, num25].WallType == 27 && Main.tile[num24 + 1, num25].WallType == 27 && !Main.tile[num24 - 1, num25].HasTile && !Main.tile[num24 + 1, num25].HasTile)
						{
							Tile tile = Main.tile[num24, num25];
							tile.HasTile = false;
							tile.WallType = 27;
						}
						if (Main.tile[num24, num25 - 1].TileType != 21 && Main.tile[num24 - 1, num25].WallType == 27 && Main.tile[num24 + 1, num25].TileType == 30 && Main.tile[num24 + 2, num25].WallType == 27 && !Main.tile[num24 - 1, num25].HasTile && !Main.tile[num24 + 2, num25].HasTile)
						{
							Tile tile = Main.tile[num24, num25];
							tile.HasTile = false;
							tile.WallType = 27;
							Tile tile2 = Main.tile[num24 + 1, num25];
							tile2.HasTile = false;
							tile2.WallType = 27;
						}
						if (Main.tile[num24, num25 - 1].WallType == 27 && Main.tile[num24, num25 + 1].WallType == 27 && !Main.tile[num24, num25 - 1].HasTile && !Main.tile[num24, num25 + 1].HasTile)
						{
							Tile tile = Main.tile[num24, num25];
							tile.HasTile = false;
							tile.WallType = 27;
						}
					}
				}
			}
			for (int num26 = num18; num26 < num19; num26++)
			{
				for (int num27 = num21; num27 > num20; num27--)
				{
					bool flag4 = false;
					if (Main.tile[num26, num27].HasTile && Main.tile[num26, num27].TileType == 30)
					{
						int num28 = -1;
						for (int num29 = 0; num29 < 2; num29++)
						{
							if (!WorldGen.SolidTile(num26 + num28, num27) && Main.tile[num26 + num28, num27].WallType == 0)
							{
								int num30 = 0;
								int num31 = num27;
								int num32 = num27;
								while (Main.tile[num26, num31].HasTile && Main.tile[num26, num31].TileType == 30 && !WorldGen.SolidTile(num26 + num28, num31) && Main.tile[num26 + num28, num31].WallType == 0)
								{
									num31--;
									num30++;
								}
								num31++;
								int num33 = num31 + 1;
								if (num30 > 4)
								{
									if (WorldGen.genRand.Next(2) == 0)
									{
										num31 = num32 - 1;
										bool flag5 = true;
										for (int num34 = num26 - 2; num34 <= num26 + 2; num34++)
										{
											for (int num35 = num31 - 2; num35 <= num31; num35++)
											{
												if (num34 != num26 && Main.tile[num34, num35].HasTile)
												{
													flag5 = false;
												}
											}
										}
										if (flag5)
										{
											Tile tile = Main.tile[num26, num31];
											tile.HasTile = false;
											Tile tile2 = Main.tile[num26, num31 - 1];
											tile2.HasTile = false;
											Tile tile3 = Main.tile[num26, num31 - 2];
											tile3.HasTile = false;
											WorldGen.PlaceTile(num26, num31, 10, mute: true);
											flag4 = true;
										}
									}
									if (!flag4)
									{
										for (int num36 = num33; num36 < num32; num36++)
										{
											Main.tile[num26, num36].TileType = 124;
										}
									}
								}
							}
							num28 = 1;
						}
					}
					if (flag4)
					{
						break;
					}
				}
			}
			int num37;
			for (num37 = num18; num37 < num19; num37++)
			{
				bool flag6 = true;
				for (int num38 = num20; num38 < num21; num38++)
				{
					for (int num39 = num37 - 2; num39 <= num37 + 2; num39++)
					{
						if (Main.tile[num39, num38].HasTile && (!WorldGen.SolidTile(num39, num38) || Main.tile[num39, num38].TileType == 10))
						{
							flag6 = false;
						}
					}
				}
				if (flag6)
				{
					for (int num40 = num20; num40 < num21; num40++)
					{
						if (Main.tile[num37, num40].WallType == 27 && !Main.tile[num37, num40].HasTile)
						{
							WorldGen.PlaceTile(num37, num40, 124, mute: true);
						}
					}
				}
				num37 += WorldGen.genRand.Next(3);
			}
			for (int num41 = 0; num41 < 4; num41++)
			{
				int num42 = WorldGen.genRand.Next(num18 + 2, num19 - 1);
				int num43 = WorldGen.genRand.Next(num20 + 2, num21 - 1);
				while (Main.tile[num42, num43].WallType != 27)
				{
					num42 = WorldGen.genRand.Next(num18 + 2, num19 - 1);
					num43 = WorldGen.genRand.Next(num20 + 2, num21 - 1);
				}
				while (Main.tile[num42, num43].HasTile)
				{
					num43--;
				}
				for (; !Main.tile[num42, num43].HasTile; num43++)
				{
				}
				num43--;
				if (Main.tile[num42, num43].WallType != 27)
				{
					continue;
				}
				if (WorldGen.genRand.NextBool(3))
				{
					int num44 = WorldGen.genRand.Next(9);
					if (num44 == 0)
					{
						num44 = 14;
					}
					if (num44 == 1)
					{
						num44 = 16;
					}
					if (num44 == 2)
					{
						num44 = 18;
					}
					if (num44 == 3)
					{
						num44 = 86;
					}
					if (num44 == 4)
					{
						num44 = 87;
					}
					if (num44 == 5)
					{
						num44 = 94;
					}
					if (num44 == 6)
					{
						num44 = 101;
					}
					if (num44 == 7)
					{
						num44 = 104;
					}
					if (num44 == 8)
					{
						num44 = 106;
					}
					WorldGen.PlaceTile(num42, num43, num44, mute: true);
				}
				else
				{
					int style = WorldGen.genRand.Next(2, 43);
					PlaceTile_Old(num42, num43, 105, mute: true, forced: true, -1, style);
				}
			}
		}

		public static void IslandHouse(int i, int j)
		{
			byte type = (byte)WorldGen.genRand.Next(45, 48);
			byte wall = (byte)WorldGen.genRand.Next(10, 13);
			Vector2 vector = new Vector2(i, j);
			int num = 1;
			if (WorldGen.genRand.NextBool(2))
			{
				num = -1;
			}
			int num2 = WorldGen.genRand.Next(7, 12);
			int num3 = WorldGen.genRand.Next(5, 7);
			vector.X = i + (num2 + 2) * num;
			for (int k = j - 15; k < j + 30; k++)
			{
				if (Main.tile[(int)vector.X, k].HasTile)
				{
					vector.Y = k - 1;
					break;
				}
			}
			vector.X = i;
			int num4 = (int)(vector.X - (float)num2 - 2f);
			int num5 = (int)(vector.X + (float)num2 + 2f);
			int num6 = (int)(vector.Y - (float)num3 - 2f);
			int num7 = (int)(vector.Y + 2f + (float)WorldGen.genRand.Next(3, 5));
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int l = num4; l <= num5; l++)
			{
				for (int m = num6; m < num7; m++)
				{
					Tile tile = Main.tile[l, m];
					tile.HasTile = true;
					tile.TileType = type;
					tile.WallType = 0;
				}
			}
			num4 = (int)(vector.X - (float)num2);
			num5 = (int)(vector.X + (float)num2);
			num6 = (int)(vector.Y - (float)num3);
			num7 = (int)(vector.Y + 1f);
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int n = num4; n <= num5; n++)
			{
				for (int num8 = num6; num8 < num7; num8++)
				{
					if (Main.tile[n, num8].WallType == 0)
					{
						Tile tile = Main.tile[n, num8];
						tile.HasTile = false;
						tile.WallType = wall;
					}
				}
			}
			int num9 = i + (num2 + 1) * num;
			int num10 = (int)vector.Y;
			for (int num11 = num9 - 2; num11 <= num9 + 2; num11++)
			{
				Tile tile = Main.tile[num11, num10];
				tile.HasTile = false;
				Tile tile2 = Main.tile[num11, num10 - 1];
				tile2.HasTile = false;
				Tile tile3 = Main.tile[num11, num10 - 2];
				tile3.HasTile = false;
			}
			WorldGen.PlaceTile(num9, num10, 10, mute: true);
			int contain = 0;
			int num12 = houseCount;
			if (num12 > 2)
			{
				num12 = WorldGen.genRand.Next(3);
			}
			switch (num12)
			{
				case 0:
					contain = 159;
					break;
				case 1:
					contain = 65;
					break;
				case 2:
					contain = 158;
					break;
			}
			AddBuriedChest(i, num10 - 3, contain, notNearOtherChests: false, 2);
			houseCount++;
		}

		public static bool PlacePot(int x, int y, int type = 28)
		{
			bool flag = true;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j].HasTile)
					{
						flag = false;
					}
				}
				if (!Main.tile[i, y + 1].HasTile || !Main.tileSolid[Main.tile[i, y + 1].TileType])
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = -1; l < 1; l++)
					{
						int num = k * 18 + WorldGen.genRand.Next(3) * 36;
						int num2 = (l + 1) * 18;
						Tile tile = Main.tile[x + k, y + l];
						tile.HasTile = true;
						tile.TileFrameX = (short)num;
						tile.TileFrameY = (short)num2;
						tile.TileType = (byte)type;
					}
				}
				return true;
			}
			return false;
		}

		public static bool GrowEpicTree(int i, int y)
		{
			int j;
			for (j = y; Main.tile[i, j].TileType == 20; j++)
			{
			}
			if (Main.tile[i, j].HasTile && Main.tile[i, j].TileType == 2 && Main.tile[i, j - 1].WallType == 0 && Main.tile[i, j - 1].LiquidAmount == 0 && ((Main.tile[i - 1, j].HasTile && (Main.tile[i - 1, j].TileType == 2 || Main.tile[i - 1, j].TileType == 23 || Main.tile[i - 1, j].TileType == 60 || Main.tile[i - 1, j].TileType == 109)) || (Main.tile[i + 1, j].HasTile && (Main.tile[i + 1, j].TileType == 2 || Main.tile[i + 1, j].TileType == 23 || Main.tile[i + 1, j].TileType == 60 || Main.tile[i + 1, j].TileType == 109))))
			{
				int num = 1;
				if (WorldGen.EmptyTileCheck(i - num, i + num, j - 55, j - 1, 20))
				{
					bool flag = false;
					bool flag2 = false;
					int num2 = WorldGen.genRand.Next(20, 30);
					int num3;
					for (int k = j - num2; k < j; k++)
					{
						Tile tile = Main.tile[i, k];
						tile.TileFrameNumber = (byte)WorldGen.genRand.Next(3);
						tile.HasTile = true;
						tile.TileType = 5;
						num3 = WorldGen.genRand.Next(3);
						int num4 = WorldGen.genRand.Next(10);
						if (k == j - 1 || k == j - num2)
						{
							num4 = 0;
						}
						while (((num4 == 5 || num4 == 7) && flag) || ((num4 == 6 || num4 == 7) && flag2))
						{
							num4 = WorldGen.genRand.Next(10);
						}
						flag = false;
						flag2 = false;
						if (num4 == 5 || num4 == 7)
						{
							flag = true;
						}
						if (num4 == 6 || num4 == 7)
						{
							flag2 = true;
						}
						switch (num4)
						{
							case 1:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 0;
									Main.tile[i, k].TileFrameY = 66;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 0;
									Main.tile[i, k].TileFrameY = 88;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 0;
									Main.tile[i, k].TileFrameY = 110;
								}
								break;
							case 2:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 22;
									Main.tile[i, k].TileFrameY = 0;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 22;
									Main.tile[i, k].TileFrameY = 22;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 22;
									Main.tile[i, k].TileFrameY = 44;
								}
								break;
							case 3:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 44;
									Main.tile[i, k].TileFrameY = 66;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 44;
									Main.tile[i, k].TileFrameY = 88;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 44;
									Main.tile[i, k].TileFrameY = 110;
								}
								break;
							case 4:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 22;
									Main.tile[i, k].TileFrameY = 66;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 22;
									Main.tile[i, k].TileFrameY = 88;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 22;
									Main.tile[i, k].TileFrameY = 110;
								}
								break;
							case 5:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 88;
									Main.tile[i, k].TileFrameY = 0;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 88;
									Main.tile[i, k].TileFrameY = 22;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 88;
									Main.tile[i, k].TileFrameY = 44;
								}
								break;
							case 6:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 66;
									Main.tile[i, k].TileFrameY = 66;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 66;
									Main.tile[i, k].TileFrameY = 88;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 66;
									Main.tile[i, k].TileFrameY = 110;
								}
								break;
							case 7:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 110;
									Main.tile[i, k].TileFrameY = 66;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 110;
									Main.tile[i, k].TileFrameY = 88;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 110;
									Main.tile[i, k].TileFrameY = 110;
								}
								break;
							default:
								if (num3 == 0)
								{
									Main.tile[i, k].TileFrameX = 0;
									Main.tile[i, k].TileFrameY = 0;
								}
								if (num3 == 1)
								{
									Main.tile[i, k].TileFrameX = 0;
									Main.tile[i, k].TileFrameY = 22;
								}
								if (num3 == 2)
								{
									Main.tile[i, k].TileFrameX = 0;
									Main.tile[i, k].TileFrameY = 44;
								}
								break;
						}
						if (num4 == 5 || num4 == 7)
						{
							Tile tile3 = Main.tile[i - 1, k];
							tile3.HasTile = true;
							tile3.TileType = 5;
							num3 = WorldGen.genRand.Next(3);
							if (WorldGen.genRand.Next(3) < 2)
							{
								if (num3 == 0)
								{
									Main.tile[i - 1, k].TileFrameX = 44;
									Main.tile[i - 1, k].TileFrameY = 198;
								}
								if (num3 == 1)
								{
									Main.tile[i - 1, k].TileFrameX = 44;
									Main.tile[i - 1, k].TileFrameY = 220;
								}
								if (num3 == 2)
								{
									Main.tile[i - 1, k].TileFrameX = 44;
									Main.tile[i - 1, k].TileFrameY = 242;
								}
							}
							else
							{
								if (num3 == 0)
								{
									Main.tile[i - 1, k].TileFrameX = 66;
									Main.tile[i - 1, k].TileFrameY = 0;
								}
								if (num3 == 1)
								{
									Main.tile[i - 1, k].TileFrameX = 66;
									Main.tile[i - 1, k].TileFrameY = 22;
								}
								if (num3 == 2)
								{
									Main.tile[i - 1, k].TileFrameX = 66;
									Main.tile[i - 1, k].TileFrameY = 44;
								}
							}
						}
						if (num4 != 6 && num4 != 7)
						{
							continue;
						}
						Tile tile2 = Main.tile[i + 1, k];
						tile2.HasTile = true;
						tile2.TileType = 5;
						num3 = WorldGen.genRand.Next(3);
						if (WorldGen.genRand.Next(3) < 2)
						{
							if (num3 == 0)
							{
								Main.tile[i + 1, k].TileFrameX = 66;
								Main.tile[i + 1, k].TileFrameY = 198;
							}
							if (num3 == 1)
							{
								Main.tile[i + 1, k].TileFrameX = 66;
								Main.tile[i + 1, k].TileFrameY = 220;
							}
							if (num3 == 2)
							{
								Main.tile[i + 1, k].TileFrameX = 66;
								Main.tile[i + 1, k].TileFrameY = 242;
							}
						}
						else
						{
							if (num3 == 0)
							{
								Main.tile[i + 1, k].TileFrameX = 88;
								Main.tile[i + 1, k].TileFrameY = 66;
							}
							if (num3 == 1)
							{
								Main.tile[i + 1, k].TileFrameX = 88;
								Main.tile[i + 1, k].TileFrameY = 88;
							}
							if (num3 == 2)
							{
								Main.tile[i + 1, k].TileFrameX = 88;
								Main.tile[i + 1, k].TileFrameY = 110;
							}
						}
					}
					int num5 = WorldGen.genRand.Next(3);
					bool flag3 = false;
					bool flag4 = false;
					if (Main.tile[i - 1, j].HasTile && (Main.tile[i - 1, j].TileType == 2 || Main.tile[i - 1, j].TileType == 23 || Main.tile[i - 1, j].TileType == 60 || Main.tile[i - 1, j].TileType == 109))
					{
						flag3 = true;
					}
					if (Main.tile[i + 1, j].HasTile && (Main.tile[i + 1, j].TileType == 2 || Main.tile[i + 1, j].TileType == 23 || Main.tile[i + 1, j].TileType == 60 || Main.tile[i + 1, j].TileType == 109))
					{
						flag4 = true;
					}
					if (!flag3)
					{
						if (num5 == 0)
						{
							num5 = 2;
						}
						if (num5 == 1)
						{
							num5 = 3;
						}
					}
					if (!flag4)
					{
						if (num5 == 0)
						{
							num5 = 1;
						}
						if (num5 == 2)
						{
							num5 = 3;
						}
					}
					if (flag3 && !flag4)
					{
						num5 = 1;
					}
					if (flag4 && !flag3)
					{
						num5 = 2;
					}
					if (num5 == 0 || num5 == 1)
					{
						Tile tile = Main.tile[i + 1, j - 1];
						tile.HasTile = true;
						tile.TileType = 5;
						num3 = WorldGen.genRand.Next(3);
						if (num3 == 0)
						{
							Main.tile[i + 1, j - 1].TileFrameX = 22;
							Main.tile[i + 1, j - 1].TileFrameY = 132;
						}
						if (num3 == 1)
						{
							Main.tile[i + 1, j - 1].TileFrameX = 22;
							Main.tile[i + 1, j - 1].TileFrameY = 154;
						}
						if (num3 == 2)
						{
							Main.tile[i + 1, j - 1].TileFrameX = 22;
							Main.tile[i + 1, j - 1].TileFrameY = 176;
						}
					}
					if (num5 == 0 || num5 == 2)
					{
						Tile tile = Main.tile[i - 1, j - 1];
						tile.HasTile = true;
						tile.TileType = 5;
						num3 = WorldGen.genRand.Next(3);
						if (num3 == 0)
						{
							Main.tile[i - 1, j - 1].TileFrameX = 44;
							Main.tile[i - 1, j - 1].TileFrameY = 132;
						}
						if (num3 == 1)
						{
							Main.tile[i - 1, j - 1].TileFrameX = 44;
							Main.tile[i - 1, j - 1].TileFrameY = 154;
						}
						if (num3 == 2)
						{
							Main.tile[i - 1, j - 1].TileFrameX = 44;
							Main.tile[i - 1, j - 1].TileFrameY = 176;
						}
					}
					num3 = WorldGen.genRand.Next(3);
					switch (num5)
					{
						case 0:
							if (num3 == 0)
							{
								Main.tile[i, j - 1].TileFrameX = 88;
								Main.tile[i, j - 1].TileFrameY = 132;
							}
							if (num3 == 1)
							{
								Main.tile[i, j - 1].TileFrameX = 88;
								Main.tile[i, j - 1].TileFrameY = 154;
							}
							if (num3 == 2)
							{
								Main.tile[i, j - 1].TileFrameX = 88;
								Main.tile[i, j - 1].TileFrameY = 176;
							}
							break;
						case 1:
							if (num3 == 0)
							{
								Main.tile[i, j - 1].TileFrameX = 0;
								Main.tile[i, j - 1].TileFrameY = 132;
							}
							if (num3 == 1)
							{
								Main.tile[i, j - 1].TileFrameX = 0;
								Main.tile[i, j - 1].TileFrameY = 154;
							}
							if (num3 == 2)
							{
								Main.tile[i, j - 1].TileFrameX = 0;
								Main.tile[i, j - 1].TileFrameY = 176;
							}
							break;
						case 2:
							if (num3 == 0)
							{
								Main.tile[i, j - 1].TileFrameX = 66;
								Main.tile[i, j - 1].TileFrameY = 132;
							}
							if (num3 == 1)
							{
								Main.tile[i, j - 1].TileFrameX = 66;
								Main.tile[i, j - 1].TileFrameY = 154;
							}
							if (num3 == 2)
							{
								Main.tile[i, j - 1].TileFrameX = 66;
								Main.tile[i, j - 1].TileFrameY = 176;
							}
							break;
					}
					if (WorldGen.genRand.Next(3) < 2)
					{
						num3 = WorldGen.genRand.Next(3);
						if (num3 == 0)
						{
							Main.tile[i, j - num2].TileFrameX = 22;
							Main.tile[i, j - num2].TileFrameY = 198;
						}
						if (num3 == 1)
						{
							Main.tile[i, j - num2].TileFrameX = 22;
							Main.tile[i, j - num2].TileFrameY = 220;
						}
						if (num3 == 2)
						{
							Main.tile[i, j - num2].TileFrameX = 22;
							Main.tile[i, j - num2].TileFrameY = 242;
						}
					}
					else
					{
						num3 = WorldGen.genRand.Next(3);
						if (num3 == 0)
						{
							Main.tile[i, j - num2].TileFrameX = 0;
							Main.tile[i, j - num2].TileFrameY = 198;
						}
						if (num3 == 1)
						{
							Main.tile[i, j - num2].TileFrameX = 0;
							Main.tile[i, j - num2].TileFrameY = 220;
						}
						if (num3 == 2)
						{
							Main.tile[i, j - num2].TileFrameX = 0;
							Main.tile[i, j - num2].TileFrameY = 242;
						}
					}
					WorldGen.RangeFrame(i - 2, j - num2 - 1, i + 2, j + 1);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, i, (int)((double)j - (double)num2 * 0.5), num2 + 1);
					}
					return true;
				}
			}
			return false;
		}

		public static void GrowTree(int i, int y)
		{
			int j;
			for (j = y; Main.tile[i, j].TileType == 20; j++)
			{
			}
			if (((Main.tile[i - 1, j - 1].LiquidAmount != 0 || Main.tile[i - 1, j - 1].LiquidAmount != 0 || Main.tile[i + 1, j - 1].LiquidAmount != 0) && Main.tile[i, j].TileType != 60) || !Main.tile[i, j].HasTile || (Main.tile[i, j].TileType != 2 && Main.tile[i, j].TileType != 23 && Main.tile[i, j].TileType != 60 && Main.tile[i, j].TileType != 109 && Main.tile[i, j].TileType != 147) || Main.tile[i, j - 1].WallType != 0 || ((!Main.tile[i - 1, j].HasTile || (Main.tile[i - 1, j].TileType != 2 && Main.tile[i - 1, j].TileType != 23 && Main.tile[i - 1, j].TileType != 60 && Main.tile[i - 1, j].TileType != 109 && Main.tile[i - 1, j].TileType != 147)) && (!Main.tile[i + 1, j].HasTile || (Main.tile[i + 1, j].TileType != 2 && Main.tile[i + 1, j].TileType != 23 && Main.tile[i + 1, j].TileType != 60 && Main.tile[i + 1, j].TileType != 109 && Main.tile[i + 1, j].TileType != 147))))
			{
				return;
			}
			int num = 1;
			int num2 = 16;
			if (Main.tile[i, j].TileType == 60)
			{
				num2 += 5;
			}
			if (!WorldGen.EmptyTileCheck(i - num, i + num, j - num2, j - 1, 20))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num3 = WorldGen.genRand.Next(5, num2 + 1);
			int num4;
			for (int k = j - num3; k < j; k++)
			{
				Tile tile = Main.tile[i, k];
				tile.TileFrameNumber = (byte)WorldGen.genRand.Next(3);
				tile.HasTile = true;
				tile.TileType = 5;
				num4 = WorldGen.genRand.Next(3);
				int num5 = WorldGen.genRand.Next(10);
				if (k == j - 1 || k == j - num3)
				{
					num5 = 0;
				}
				while (((num5 == 5 || num5 == 7) && flag) || ((num5 == 6 || num5 == 7) && flag2))
				{
					num5 = WorldGen.genRand.Next(10);
				}
				flag = false;
				flag2 = false;
				if (num5 == 5 || num5 == 7)
				{
					flag = true;
				}
				if (num5 == 6 || num5 == 7)
				{
					flag2 = true;
				}
				switch (num5)
				{
					case 1:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 0;
							Main.tile[i, k].TileFrameY = 66;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 0;
							Main.tile[i, k].TileFrameY = 88;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 0;
							Main.tile[i, k].TileFrameY = 110;
						}
						break;
					case 2:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 22;
							Main.tile[i, k].TileFrameY = 0;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 22;
							Main.tile[i, k].TileFrameY = 22;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 22;
							Main.tile[i, k].TileFrameY = 44;
						}
						break;
					case 3:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 44;
							Main.tile[i, k].TileFrameY = 66;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 44;
							Main.tile[i, k].TileFrameY = 88;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 44;
							Main.tile[i, k].TileFrameY = 110;
						}
						break;
					case 4:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 22;
							Main.tile[i, k].TileFrameY = 66;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 22;
							Main.tile[i, k].TileFrameY = 88;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 22;
							Main.tile[i, k].TileFrameY = 110;
						}
						break;
					case 5:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 88;
							Main.tile[i, k].TileFrameY = 0;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 88;
							Main.tile[i, k].TileFrameY = 22;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 88;
							Main.tile[i, k].TileFrameY = 44;
						}
						break;
					case 6:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 66;
							Main.tile[i, k].TileFrameY = 66;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 66;
							Main.tile[i, k].TileFrameY = 88;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 66;
							Main.tile[i, k].TileFrameY = 110;
						}
						break;
					case 7:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 110;
							Main.tile[i, k].TileFrameY = 66;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 110;
							Main.tile[i, k].TileFrameY = 88;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 110;
							Main.tile[i, k].TileFrameY = 110;
						}
						break;
					default:
						if (num4 == 0)
						{
							Main.tile[i, k].TileFrameX = 0;
							Main.tile[i, k].TileFrameY = 0;
						}
						if (num4 == 1)
						{
							Main.tile[i, k].TileFrameX = 0;
							Main.tile[i, k].TileFrameY = 22;
						}
						if (num4 == 2)
						{
							Main.tile[i, k].TileFrameX = 0;
							Main.tile[i, k].TileFrameY = 44;
						}
						break;
				}
				if (num5 == 5 || num5 == 7)
				{
					Tile tile2 = Main.tile[i - 1, k];
					tile2.HasTile = true;
					tile2.TileType = 5;
					num4 = WorldGen.genRand.Next(3);
					if (WorldGen.genRand.Next(3) < 2)
					{
						if (num4 == 0)
						{
							Main.tile[i - 1, k].TileFrameX = 44;
							Main.tile[i - 1, k].TileFrameY = 198;
						}
						if (num4 == 1)
						{
							Main.tile[i - 1, k].TileFrameX = 44;
							Main.tile[i - 1, k].TileFrameY = 220;
						}
						if (num4 == 2)
						{
							Main.tile[i - 1, k].TileFrameX = 44;
							Main.tile[i - 1, k].TileFrameY = 242;
						}
					}
					else
					{
						if (num4 == 0)
						{
							Main.tile[i - 1, k].TileFrameX = 66;
							Main.tile[i - 1, k].TileFrameY = 0;
						}
						if (num4 == 1)
						{
							Main.tile[i - 1, k].TileFrameX = 66;
							Main.tile[i - 1, k].TileFrameY = 22;
						}
						if (num4 == 2)
						{
							Main.tile[i - 1, k].TileFrameX = 66;
							Main.tile[i - 1, k].TileFrameY = 44;
						}
					}
				}
				if (num5 != 6 && num5 != 7)
				{
					continue;
				}
				Tile tile3 = Main.tile[i + 1, k];
				tile3.HasTile = true;
				tile3.TileType = 5;
				num4 = WorldGen.genRand.Next(3);
				if (WorldGen.genRand.Next(3) < 2)
				{
					if (num4 == 0)
					{
						Main.tile[i + 1, k].TileFrameX = 66;
						Main.tile[i + 1, k].TileFrameY = 198;
					}
					if (num4 == 1)
					{
						Main.tile[i + 1, k].TileFrameX = 66;
						Main.tile[i + 1, k].TileFrameY = 220;
					}
					if (num4 == 2)
					{
						Main.tile[i + 1, k].TileFrameX = 66;
						Main.tile[i + 1, k].TileFrameY = 242;
					}
				}
				else
				{
					if (num4 == 0)
					{
						Main.tile[i + 1, k].TileFrameX = 88;
						Main.tile[i + 1, k].TileFrameY = 66;
					}
					if (num4 == 1)
					{
						Main.tile[i + 1, k].TileFrameX = 88;
						Main.tile[i + 1, k].TileFrameY = 88;
					}
					if (num4 == 2)
					{
						Main.tile[i + 1, k].TileFrameX = 88;
						Main.tile[i + 1, k].TileFrameY = 110;
					}
				}
			}
			int num6 = WorldGen.genRand.Next(3);
			bool flag3 = false;
			bool flag4 = false;
			if (Main.tile[i - 1, j].HasTile && (Main.tile[i - 1, j].TileType == 2 || Main.tile[i - 1, j].TileType == 23 || Main.tile[i - 1, j].TileType == 60 || Main.tile[i - 1, j].TileType == 109 || Main.tile[i - 1, j].TileType == 147))
			{
				flag3 = true;
			}
			if (Main.tile[i + 1, j].HasTile && (Main.tile[i + 1, j].TileType == 2 || Main.tile[i + 1, j].TileType == 23 || Main.tile[i + 1, j].TileType == 60 || Main.tile[i + 1, j].TileType == 109 || Main.tile[i + 1, j].TileType == 147))
			{
				flag4 = true;
			}
			if (!flag3)
			{
				if (num6 == 0)
				{
					num6 = 2;
				}
				if (num6 == 1)
				{
					num6 = 3;
				}
			}
			if (!flag4)
			{
				if (num6 == 0)
				{
					num6 = 1;
				}
				if (num6 == 2)
				{
					num6 = 3;
				}
			}
			if (flag3 && !flag4)
			{
				num6 = 1;
			}
			if (flag4 && !flag3)
			{
				num6 = 2;
			}
			if (num6 == 0 || num6 == 1)
			{
				Tile tile = Main.tile[i + 1, j - 1];
				tile.HasTile = true;
				tile.TileType = 5;
				num4 = WorldGen.genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[i + 1, j - 1].TileFrameX = 22;
					Main.tile[i + 1, j - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[i + 1, j - 1].TileFrameX = 22;
					Main.tile[i + 1, j - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[i + 1, j - 1].TileFrameX = 22;
					Main.tile[i + 1, j - 1].TileFrameY = 176;
				}
			}
			if (num6 == 0 || num6 == 2)
			{
				Tile tile = Main.tile[i - 1, j - 1];
				tile.HasTile = true;
				tile.TileType = 5;
				num4 = WorldGen.genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[i - 1, j - 1].TileFrameX = 44;
					Main.tile[i - 1, j - 1].TileFrameY = 132;
				}
				if (num4 == 1)
				{
					Main.tile[i - 1, j - 1].TileFrameX = 44;
					Main.tile[i - 1, j - 1].TileFrameY = 154;
				}
				if (num4 == 2)
				{
					Main.tile[i - 1, j - 1].TileFrameX = 44;
					Main.tile[i - 1, j - 1].TileFrameY = 176;
				}
			}
			num4 = WorldGen.genRand.Next(3);
			switch (num6)
			{
				case 0:
					if (num4 == 0)
					{
						Main.tile[i, j - 1].TileFrameX = 88;
						Main.tile[i, j - 1].TileFrameY = 132;
					}
					if (num4 == 1)
					{
						Main.tile[i, j - 1].TileFrameX = 88;
						Main.tile[i, j - 1].TileFrameY = 154;
					}
					if (num4 == 2)
					{
						Main.tile[i, j - 1].TileFrameX = 88;
						Main.tile[i, j - 1].TileFrameY = 176;
					}
					break;
				case 1:
					if (num4 == 0)
					{
						Main.tile[i, j - 1].TileFrameX = 0;
						Main.tile[i, j - 1].TileFrameY = 132;
					}
					if (num4 == 1)
					{
						Main.tile[i, j - 1].TileFrameX = 0;
						Main.tile[i, j - 1].TileFrameY = 154;
					}
					if (num4 == 2)
					{
						Main.tile[i, j - 1].TileFrameX = 0;
						Main.tile[i, j - 1].TileFrameY = 176;
					}
					break;
				case 2:
					if (num4 == 0)
					{
						Main.tile[i, j - 1].TileFrameX = 66;
						Main.tile[i, j - 1].TileFrameY = 132;
					}
					if (num4 == 1)
					{
						Main.tile[i, j - 1].TileFrameX = 66;
						Main.tile[i, j - 1].TileFrameY = 154;
					}
					if (num4 == 2)
					{
						Main.tile[i, j - 1].TileFrameX = 66;
						Main.tile[i, j - 1].TileFrameY = 176;
					}
					break;
			}
			if (WorldGen.genRand.Next(4) < 3)
			{
				num4 = WorldGen.genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[i, j - num3].TileFrameX = 22;
					Main.tile[i, j - num3].TileFrameY = 198;
				}
				if (num4 == 1)
				{
					Main.tile[i, j - num3].TileFrameX = 22;
					Main.tile[i, j - num3].TileFrameY = 220;
				}
				if (num4 == 2)
				{
					Main.tile[i, j - num3].TileFrameX = 22;
					Main.tile[i, j - num3].TileFrameY = 242;
				}
			}
			else
			{
				num4 = WorldGen.genRand.Next(3);
				if (num4 == 0)
				{
					Main.tile[i, j - num3].TileFrameX = 0;
					Main.tile[i, j - num3].TileFrameY = 198;
				}
				if (num4 == 1)
				{
					Main.tile[i, j - num3].TileFrameX = 0;
					Main.tile[i, j - num3].TileFrameY = 220;
				}
				if (num4 == 2)
				{
					Main.tile[i, j - num3].TileFrameX = 0;
					Main.tile[i, j - num3].TileFrameY = 242;
				}
			}
			WorldGen.RangeFrame(i - 2, j - num3 - 1, i + 2, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, (int)((double)j - (double)num3 * 0.5), num3 + 1);
			}
		}

		public static void AddTrees()
		{
			for (int i = 1; i < Main.maxTilesX - 1; i++)
			{
				for (int j = 20; (double)j < Main.worldSurface; j++)
				{
					GrowTree(i, j);
				}
				if (WorldGen.genRand.Next(3) == 0)
				{
					i++;
				}
				if (WorldGen.genRand.Next(4) == 0)
				{
					i++;
				}
			}
		}

		public static void PlantAlch()
		{
			int num = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
			int num2 = 0;
			for (num2 = ((WorldGen.genRand.Next(40) == 0) ? WorldGen.genRand.Next((int)(Main.rockLayer + (double)Main.maxTilesY) / 2, Main.maxTilesY - 20) : ((WorldGen.genRand.Next(10) != 0) ? WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 20) : WorldGen.genRand.Next(0, Main.maxTilesY - 20))); num2 < Main.maxTilesY - 20 && !Main.tile[num, num2].HasTile; num2++)
			{
			}
			if (Main.tile[num, num2].HasTile && !Main.tile[num, num2 - 1].HasTile && Main.tile[num, num2 - 1].LiquidAmount == 0)
			{
				if (Main.tile[num, num2].TileType == 2 || Main.tile[num, num2].TileType == 109)
				{
					PlaceAlch(num, num2 - 1, 0);
				}
				if (Main.tile[num, num2].TileType == 60)
				{
					PlaceAlch(num, num2 - 1, 1);
				}
				if (Main.tile[num, num2].TileType == 0 || Main.tile[num, num2].TileType == 59)
				{
					PlaceAlch(num, num2 - 1, 2);
				}
				if (Main.tile[num, num2].TileType == 23 || Main.tile[num, num2].TileType == 25)
				{
					PlaceAlch(num, num2 - 1, 3);
				}
				if (Main.tile[num, num2].TileType == 53 || Main.tile[num, num2].TileType == 116)
				{
					PlaceAlch(num, num2 - 1, 4);
				}
				if (Main.tile[num, num2].TileType == 57)
				{
					PlaceAlch(num, num2 - 1, 5);
				}
				if (Main.tile[num, num2 - 1].HasTile && Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, num, num2 - 1, 1);
				}
			}
		}

		public static bool PlaceAlch(int x, int y, int style)
		{
			if (!Main.tile[x, y].HasTile && Main.tile[x, y + 1].HasTile)
			{
				bool flag = false;
				switch (style)
				{
					case 0:
						if (Main.tile[x, y + 1].TileType != 2 && Main.tile[x, y + 1].TileType != 78 && Main.tile[x, y + 1].TileType != 109)
						{
							flag = true;
						}
						if (Main.tile[x, y].LiquidAmount > 0)
						{
							flag = true;
						}
						break;
					case 1:
						if (Main.tile[x, y + 1].TileType != 60 && Main.tile[x, y + 1].TileType != 78)
						{
							flag = true;
						}
						if (Main.tile[x, y].LiquidAmount > 0)
						{
							flag = true;
						}
						break;
					case 2:
						if (Main.tile[x, y + 1].TileType != 0 && Main.tile[x, y + 1].TileType != 59 && Main.tile[x, y + 1].TileType != 78)
						{
							flag = true;
						}
						if (Main.tile[x, y].LiquidAmount > 0)
						{
							flag = true;
						}
						break;
					case 3:
						if (Main.tile[x, y + 1].TileType != 23 && Main.tile[x, y + 1].TileType != 25 && Main.tile[x, y + 1].TileType != 78)
						{
							flag = true;
						}
						if (Main.tile[x, y].TileType > 0)
						{
							flag = true;
						}
						break;
					case 4:
						if (Main.tile[x, y + 1].TileType != 53 && Main.tile[x, y + 1].TileType != 78 && Main.tile[x, y + 1].TileType != 116)
						{
							flag = true;
						}
						if (Main.tile[x, y].LiquidAmount > 0 && Main.tile[x, y].LiquidType == LiquidID.Lava)
						{
							flag = true;
						}
						break;
					case 5:
						if (Main.tile[x, y + 1].TileType != 57 && Main.tile[x, y + 1].TileType != 78)
						{
							flag = true;
						}
						if (Main.tile[x, y].LiquidAmount > 0 && Main.tile[x, y].LiquidType != LiquidID.Lava)
						{
							flag = true;
						}
						break;
				}
				if (!flag)
				{
					Tile tile = Main.tile[x, y];
					tile.HasTile = true;
					tile.TileType = 82;
					tile.TileFrameX = (short)(18 * style);
					tile.TileFrameY = 0;
					return true;
				}
			}
			return false;
		}

		public static void AddPlants()
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 1; j < Main.maxTilesY; j++)
				{
					if (Main.tile[i, j].TileType == 2 && Main.tile[i, j].HasTile)
					{
						if (!Main.tile[i, j - 1].HasTile)
						{
							WorldGen.PlaceTile(i, j - 1, 3, mute: true);
						}
					}
					else if (Main.tile[i, j].TileType == 23 && Main.tile[i, j].HasTile && !Main.tile[i, j - 1].HasTile)
					{
						WorldGen.PlaceTile(i, j - 1, 24, mute: true);
					}
				}
			}
		}

		public static void GrowShroom(int i, int y)
		{
			if (Main.tile[i - 1, y - 1].LiquidType == LiquidID.Lava || Main.tile[i - 1, y - 1].LiquidType == LiquidID.Lava || Main.tile[i + 1, y - 1].LiquidType == LiquidID.Lava || !Main.tile[i, y].HasTile || Main.tile[i, y].TileType != 70 || Main.tile[i, y - 1].WallType != 0 || !Main.tile[i - 1, y].HasTile || Main.tile[i - 1, y].TileType != 70 || !Main.tile[i + 1, y].HasTile || Main.tile[i + 1, y].TileType != 70 || !WorldGen.EmptyTileCheck(i - 2, i + 2, y - 13, y - 1, 71))
			{
				return;
			}
			int num = WorldGen.genRand.Next(4, 11);
			for (int j = y - num; j < y; j++)
			{
				Tile tile = Main.tile[i, j];
				tile.TileFrameNumber = (byte)WorldGen.genRand.Next(3);
				tile.HasTile = true;
				tile.TileType = 72;
				int num2 = WorldGen.genRand.Next(3);
				if (num2 == 0)
				{
					Main.tile[i, j].TileFrameX = 0;
					Main.tile[i, j].TileFrameY = 0;
				}
				if (num2 == 1)
				{
					Main.tile[i, j].TileFrameX = 0;
					Main.tile[i, j].TileFrameY = 18;
				}
				if (num2 == 2)
				{
					Main.tile[i, j].TileFrameX = 0;
					Main.tile[i, j].TileFrameY = 36;
				}
			}
			int num3 = WorldGen.genRand.Next(3);
			if (num3 == 0)
			{
				Main.tile[i, y - num].TileFrameX = 36;
				Main.tile[i, y - num].TileFrameY = 0;
			}
			if (num3 == 1)
			{
				Main.tile[i, y - num].TileFrameX = 36;
				Main.tile[i, y - num].TileFrameY = 18;
			}
			if (num3 == 2)
			{
				Main.tile[i, y - num].TileFrameX = 36;
				Main.tile[i, y - num].TileFrameY = 36;
			}
			WorldGen.RangeFrame(i - 2, y - num - 1, i + 2, y + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, (int)((double)y - (double)num * 0.5), num + 1);
			}
		}

		public static bool PlaceTile_Old(int i, int j, int type, bool mute = false, bool forced = false, int plr = -1, int style = 0)
		{
			if (type >= 150)
			{
				return false;
			}
			bool result = false;
			if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
			{
				if (forced || Collision.EmptyTile(i, j) || !Main.tileSolid[type] || (type == 23 && Main.tile[i, j].TileType == 0 && Main.tile[i, j].HasTile) || (type == 2 && Main.tile[i, j].TileType == 0 && Main.tile[i, j].HasTile) || (type == 109 && Main.tile[i, j].TileType == 0 && Main.tile[i, j].HasTile) || (type == 60 && Main.tile[i, j].TileType == 59 && Main.tile[i, j].HasTile) || (type == 70 && Main.tile[i, j].TileType == 59 && Main.tile[i, j].HasTile))
				{
					Main.tile[i, j].TileFrameX = 0;
					Main.tile[i, j].TileFrameY = 0;
					switch (type)
					{
						case 105:
							Place2xX(i, j, type, style);
							WorldGen.SquareTileFrame(i, j);
							break;
					}
				}
			}
			return result;
		}

		public static void Place2xX(int x, int y, int type, int style = 0)
		{
			int num = style * 36;
			int num2 = 3;
			if (type == 104)
			{
				num2 = 5;
			}
			bool flag = true;
			for (int i = y - num2 + 1; i < y + 1; i++)
			{
				if (Main.tile[x, i].HasTile)
				{
					flag = false;
				}
				if (Main.tile[x + 1, i].HasTile)
				{
					flag = false;
				}
			}
			if (flag && Main.tile[x, y + 1].HasTile && Main.tileSolid[Main.tile[x, y + 1].TileType] && Main.tile[x + 1, y + 1].HasTile && Main.tileSolid[Main.tile[x + 1, y + 1].TileType])
			{
				for (int j = 0; j < num2; j++)
				{
					Tile tile = Main.tile[x, y - num2 + 1 + j];
					tile.HasTile = true;
					tile.TileFrameY = (short)(j * 18);
					tile.TileFrameX = (short)num;
					tile.TileType = (byte)type;
					Tile tile2 = Main.tile[x + 1, y - num2 + 1 + j];
					tile2.HasTile = true;
					tile2.TileFrameY = (short)(j * 18);
					tile2.TileFrameX = (short)(num + 18);
					tile2.TileType = (byte)type;
				}
			}
		}

		//Reset all backgrounds
		public static void SetBackgroundNormal()
		{
			WorldGen.corruptBG = 0;
			WorldGen.crimsonBG = 0;
			WorldGen.desertBG = 0;
			WorldGen.hallowBG = 0;
			WorldGen.jungleBG = 0;
			WorldGen.mushroomBG = 0;
			WorldGen.oceanBG = 0;
			WorldGen.snowBG = 0;
			WorldGen.treeBG1 = 0;
			WorldGen.treeBG2 = 0;
			WorldGen.treeBG3 = 0;
			WorldGen.treeBG4 = 0;
			WorldGen.underworldBG = 0;
		}
	}
}
