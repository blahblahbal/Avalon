using Avalon.Hooks;
using Avalon.Logic;
using Avalon.ModSupport;
using Avalon.NPCs.Bosses.PreHardmode;
using Avalon.NPCs.PreHardmode;
using Avalon.Reflection;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Herbs;
using Avalon.Tiles.Ores;
using Avalon.Tiles.Savanna;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using static Avalon.Hooks.JungleSelectionMenu;

namespace Avalon.Common;

public class AvalonWorld : ModSystem
{
	public enum CopperVariant
	{
		Copper,
		Tin,
		Bronze,
		Random,
	}

	public enum IronVariant
	{
		Iron,
		Lead,
		Nickel,
		Random,
	}

	public enum SilverVariant
	{
		Silver,
		Tungsten,
		Zinc,
		Random,
	}

	public enum GoldVariant
	{
		Gold,
		Platinum,
		Bismuth,
		Random,
	}

	public enum RhodiumVariant
	{
		Rhodium = 0,
		Osmium = 1,
		Iridium = 2,
	}

	private static bool crackedBrick;

	public WorldEvil WorldEvil;

	public WorldJungle WorldJungle;

	public static bool cavesWorld;

	public static bool retroWorld;

	public static int DungeonLocationX;
	public static int JungleLocationX;

	public static int SandstormTimeLeft = 0;
	public static sbyte SandstormDirection = 1;

	public static int WallOfSteel { get; set; } = -1;
	public static int WallOfSteelB { get; set; }
	public static int WallOfSteelF { get; set; }
	public static int WallOfSteelT { get; set; }

	public static CopperVariant CopperOre = CopperVariant.Random;
	public static IronVariant IronOre = IronVariant.Random;
	public static SilverVariant SilverOre = SilverVariant.Random;
	public static GoldVariant GoldOre = GoldVariant.Random;
	public static RhodiumVariant? RhodiumOre { get; set; }

	public int SecondaryContagionBG { get; set; } = 0;
	public WorldJungleSelection SelectedWorldJungle { get; set; } = WorldJungleSelection.Random;

	public static int totalSick; //Amount of Tiles 
	public static int totalSick2; //Amount of tiles calculated again
	public static byte tSick; //Percent of world is infected
	public static int[] NewTileCounts = new int[TileLoader.TileCount]; //to account for all new modded tiles and not just the vanilla tile amount

	public static bool StopStalacAndGrassFromBreakingUponHardmodeGeneration = false;

	public static bool SpawnDesertBeak = false;
	public static int VultureKillCount = 0;

	public override void OnWorldUnload() //Here we reset the numbers for the calculations to make sure they dont carry over to other worlds
	{
		//Im not too fimiliar how contagion world tags work but imo it would be best if it was reset in an OnWorldUnload and OnWorldLoad somewhere
		retroWorld = false;
	}
	public override void ClearWorld()
	{
		cavesWorld = false;
		retroWorld = false;
		tSick = 0;
		totalSick = 0;
		totalSick2 = 0;
	}
	public override void PostAddRecipes()
	{
		ItemTrader.ChlorophyteExtractinator = Extractinator.CreateAvalonChlorophyteExtractinator();
	}
	public override void SaveWorldData(TagCompound tag)
	{
		tag["Avalon:RhodiumOre"] = (byte?)RhodiumOre;
		tag["Avalon:WorldEvil"] = (byte)WorldEvil;
		tag["Avalon:WorldJungle"] = (byte)WorldJungle;
		tag["Avalon:ContagionBG"] = SecondaryContagionBG;
		AvalonConfig config = ModContent.GetInstance<AvalonConfig>();
		Dictionary<string, AvalonConfig.WorldDataValues> tempDict = config.GetWorldData();
		AvalonConfig.WorldDataValues worldData;

		if (WorldEvil == WorldEvil.Contagion)
		{
			worldData.contagion = true;
		}
		else
		{
			worldData.contagion = false;
		}

		if (WorldJungle == WorldJungle.Savanna)
		{
			worldData.tropics = true;
		}
		else
		{
			worldData.tropics = false;
		}

		string path = Path.ChangeExtension(Main.worldPathName, ".twld");
		tempDict[path] = worldData;
		config.SetWorldData(tempDict);

		AvalonConfig.Save(config);

		tag["Avalon:JungleX"] = JungleLocationX;

		tag["Avalon:CavesSecretSeed"] = cavesWorld;
		tag["Avalon:RetroSecretSeed"] = retroWorld;

		SaveUnsafeTileData(tag);
	}
	public override void SaveWorldHeader(TagCompound tag)
	{
		tag["Avalon:WorldEvil"] = (byte)WorldEvil;
		tag["Avalon:WorldJungle"] = (byte)WorldJungle;
		tag["Avalon:RetroSecretSeed"] = retroWorld;
		tag["Avalon:CavesSecretSeed"] = cavesWorld;
	}

	public override void LoadWorldData(TagCompound tag)
	{
		if (tag.ContainsKey("Avalon:CavesSecretSeed"))
		{
			cavesWorld = tag.GetBool("Avalon:CavesSecretSeed");
		}

		if (tag.ContainsKey("Avalon:RetroSecretSeed"))
		{
			retroWorld = tag.GetBool("Avalon:RetroSecretSeed");
		}

		if (tag.ContainsKey("Avalon:ContagionBG"))
		{
			SecondaryContagionBG = tag.GetAsInt("Avalon:ContagionBG");
		}

		if (tag.ContainsKey("Avalon:JungleX"))
		{
			JungleLocationX = tag.GetAsInt("Avalon:JungleX");
		}

		if (tag.ContainsKey("Avalon:RhodiumOre"))
		{
			RhodiumOre = (RhodiumVariant)tag.Get<byte>("Avalon:RhodiumOre");
		}

		if (tag.ContainsKey("RhodiumOre"))
		{
			RhodiumOre = (RhodiumVariant)tag.Get<byte>("RhodiumOre");
		}

		WorldEvil = (WorldEvil)(tag.Get<byte?>("Avalon:WorldEvil") ?? WorldGen.WorldGenParam_Evil);

		WorldJungle = (WorldJungle)(tag.Get<byte>("Avalon:WorldJungle"));

		Console.WriteLine(WorldEvil);

		for (var i = 0; i < Main.tile.Width; ++i)
			for (var j = 0; j < Main.tile.Height; ++j)
				if (ContagionCountCollection.Contains(Main.tile[i, j].TileType)) //Better calculations, this is only loaded when the world is loaded
					totalSick2++;                                                //to make sure that the world doesn't lag when calculating normally

		LoadUnsafeTileData(tag);

		//Love terraria hardcoding how % are caculated, re-caculated here because the current caculations earlier in the file saving are so early they cant caculate modded tiles (as modded tiles haddent loaded at that point)
		for (int x = 0; x < Main.maxTilesX; x++)
		{
			for (int j = 0; j < Main.maxTilesY; j++)
			{
				int type = Main.tile[x, j].TileType;
				if (type != -1)
				{
					if (j <= Main.worldSurface)
					{
						if (j <= Main.worldSurface)
						{
							WorldGen.tileCounts[type] += 1 * 5;
						}
						else
						{
							int num4 = (int)(Main.worldSurface - (double)j + 1.0);
							int num5 = 1 - num4;
							WorldGen.tileCounts[type] += num4 * 5 + num5;
						}
					}
					else
					{
						WorldGen.tileCounts[type] += 1;
					}
				}
			}
		}
		WorldGen.AddUpAlignmentCounts(true);

		#region Ophioid boss message stuff
		// the string is unset in the OnWorldUnload method in CreepingColony.cs
		if (ExxoAvalonOrigins.OphioidMod != null && WorldEvil == WorldEvil.Contagion)
		{
			ExxoAvalonOrigins.OphioidMod.Call("SetCustomWorldEvilForDeathMessage", Language.GetText("Mods.Avalon.Biomes.Contagion.DisplayName"));
		}
		#endregion
	}
	public static bool failedUnsafeLoad = false;
	private unsafe void SaveUnsafeTileData(TagCompound tag) //Copied from Starlight River's AuroraWaterSystem.cs because I havent worked with pointers much
	{
		if (failedUnsafeLoad)
		{
			Mod.Logger.Info("Did not save Avalon:TileData as it previously failed to load.");
			failedUnsafeLoad = false;
			return;
		}

		AvalonTileData[] myData = Main.tile.GetData<AvalonTileData>();
		byte[] data = new byte[myData.Length];

		fixed (AvalonTileData* ptr = myData)
		{
			byte* bytePtr = (byte*)ptr;
			var span = new Span<byte>(bytePtr, myData.Length);
			var target = new Span<byte>(data);
			span.CopyTo(target);
		}
		tag.Add("Avalon:TileData", data);
	}
	private unsafe void LoadUnsafeTileData(TagCompound tag) //Copied from Starlight River's AuroraWaterSystem.cs because I havent worked with pointers much
	{
		AvalonTileData[] targetData = Main.tile.GetData<AvalonTileData>();
		byte[] data = tag.GetByteArray("Avalon:TileData");

		if (targetData.Length != data.Length)
		{
			Mod.Logger.Error($"Failed to load Avalon:TileData raw data, saved data was of incorrect size. Loaded data was {data.Length}, expected {targetData.Length}. Avalon:TileData will not be loaded.");
			failedUnsafeLoad = true;
			return;
		}

		fixed (AvalonTileData* ptr = targetData)
		{
			byte* bytePtr = (byte*)ptr;
			var span = new Span<byte>(bytePtr, targetData.Length);
			var target = new Span<byte>(data);
			target.CopyTo(span);
		}
	}

	public override void PreUpdateWorld()
	{
		Main.tileSolidTop[ModContent.TileType<FallenStarTile>()] = Main.dayTime;
	}
	/// <inheritdoc />
	public override void PreWorldGen()
	{
		if (AltLibrarySupport.PreWorldGen(this)) return;
		WorldEvil = ModContent.GetInstance<ContagionSelectionMenu>().SelectedWorldEvil switch
		{
			ContagionSelectionMenu.WorldEvilSelection.Random => Main.rand.Next(Enum.GetValues<WorldEvil>()),
			ContagionSelectionMenu.WorldEvilSelection.Corruption => WorldEvil.Corruption,
			ContagionSelectionMenu.WorldEvilSelection.Crimson => WorldEvil.Crimson,
			ContagionSelectionMenu.WorldEvilSelection.Contagion => WorldEvil.Contagion,
			_ => throw new ArgumentOutOfRangeException(),
		};

		//uncomment for savanna!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

		if (ModContent.GetInstance<AvalonClientConfig>().BetaTropicsGen)
		{
			WorldJungle = SelectedWorldJungle switch
			{
				WorldJungleSelection.Random => Main.rand.Next(Enum.GetValues<WorldJungle>()),
				WorldJungleSelection.Jungle => WorldJungle.Jungle,
				WorldJungleSelection.Tropics => WorldJungle.Savanna,
				_ => throw new ArgumentOutOfRangeException(),
			};
		}
		else
		{
			WorldJungle = WorldJungle.Jungle;
		}


		//WorldJungle = WorldJungle.Jungle; // WorldJungle.Tropics; // (WorldJungle)WorldGen.genRand.Next(2);
		MethodInfo? setValue = typeof(LocalizedText).GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance);

		if (setValue != null)
		{
			if (WorldJungle == WorldJungle.Savanna)
			{
				setValue.Invoke(Lang.gen[11], [Language.GetTextValue("Mods.Avalon.World.Generation.Tropics.Generating")]);
			}
			else
			{
				setValue.Invoke(Lang.gen[11], [ExxoAvalonOrigins.JungleGenMessage]);
			}
		}

		WorldGen.WorldGenParam_Evil = (int)WorldEvil;
		WorldGen.crimson = WorldEvil == WorldEvil.Crimson;

		if (WorldEvil == WorldEvil.Contagion)
		{
			SecondaryContagionBG = WorldGen.genRand.NextBool(2) ? 1 : 0;
		}
	}
	public override void PostWorldGen()
	{
		MethodInfo? setValue = typeof(LocalizedText).GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance);
		if (setValue != null)
		{
			setValue.Invoke(Lang.gen[11], new object[] { ExxoAvalonOrigins.JungleGenMessage });
		}
	}
	public override void PostUpdateWorld()
	{
		SandstormTimeLeft--;
		if (SandstormTimeLeft > 60 * 35)
		{
			Main.windSpeedCurrent = 1f * SandstormDirection;
		}
		else if (SandstormTimeLeft < 0)
		{
			SandstormTimeLeft = 0;
		}

		#region desert beak natural spawning
		if (Main.dayTime && Main.time == 0 && !ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak && NPC.downedBoss3 && NPC.downedBoss2)
		{
			bool flag = false;
			foreach (var player in Main.ActivePlayers)
			{
				if (player.active && player.ConsumedLifeCrystals >= 10 && (int)player.statDefense >= 16)
				{
					flag = true;
					break;
				}
			}

			if (flag && Main.rand.NextBool(6))
			{
				SpawnDesertBeak = true;
				if (Main.netMode == NetmodeID.SinglePlayer)
				{
					Main.NewText(Language.GetTextValue("Mods.Avalon.DesertBeakMessage"), 50, 255, 130);
				}
				else if (Main.netMode == NetmodeID.Server)
				{
					ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.DesertBeakMessage"), new Color(50, 255, 130));
				}
			}
		}
		if (SpawnDesertBeak && Main.time > 18000 && Main.dayTime)
		{
			if (VultureKillCount > 5)
			{
				foreach (var player in Main.ActivePlayers)
				{
					if (player.ZoneDesert)
					{
						SpawnDesertBeak = false;
						NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DesertBeak>());
						VultureKillCount = 0;
						break;
					}
				}
			}
		}
		#endregion

		//ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral((ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion).ToString()), Color.White);

		// these 2 (num12 and 13) are used for herb spawning; if I revert a change we'll need them
		int num12 = 151;
		int num13 = (int)Utils.Lerp(num12, num12 * 2.8, Utils.Clamp(Main.maxTilesX / 4200.0 - 1.0, 0.0, 1.0));
		float num2 = 3E-05f * (float)WorldGen.GetWorldUpdateRate();
		// float num3 = 1.5E-05f * (float)Main.worldRate;
		for (int num4 = 0; num4 < Main.maxTilesX * Main.maxTilesY * num2; num4++)
		{
			int xCoord = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
			int yCoord = WorldGen.genRand.Next(10, /*(int)Main.worldSurface - 1*/ Main.maxTilesY - 20);
			int num7 = xCoord - 1;
			int num8 = xCoord + 2;
			int num9 = yCoord - 1;
			int num10 = yCoord + 2;
			int num11 = yCoord + 1;
			if (num7 < 10)
			{
				num7 = 10;
			}

			if (num8 > Main.maxTilesX - 10)
			{
				num8 = Main.maxTilesX - 10;
			}

			if (num9 < 10)
			{
				num9 = 10;
			}

			if (num10 > Main.maxTilesY - 10)
			{
				num10 = Main.maxTilesY - 10;
			}

			#region hardmode/superhardmode stuff
			if (Main.tile[xCoord, yCoord].HasUnactuatedTile)
			{
				//ContagionHardmodeSpread(num5, num6);
				if (Main.hardMode)
				{
					SpreadXanthophyte(xCoord, yCoord);
				}
				if (Main.hardMode)
				{
					SpreadShroomite(xCoord, yCoord);
				}
				//if (SuperHardmode && ModContent.GetInstance<BiomeTileCounts>().WorldDarkMatterTiles < BiomeTileCounts.DarkMatterTilesHardLimit)
				//{
				//    DarkMatterSpread(num5, num6);
				//}
			}
			#endregion hardmode/superhardmode stuff

			#region bone fish spawning
			foreach (var player in Main.ActivePlayers)
			{
				if (player.active && !player.dead && player.lavaWet)
				{
					float dist = Vector2.Distance(player.position, new Vector2(xCoord, yCoord) * 16);
					if (player.ZoneUnderworldHeight && dist > Main.screenWidth / 2 && dist < Main.screenWidth && NPCHelper.GetSpawnRate() >= 60)
					{
						if (!Main.tile[xCoord, yCoord].HasTile && Main.tile[xCoord, yCoord].LiquidType == LiquidID.Lava && Main.tile[xCoord, yCoord].LiquidAmount > 70 &&
							Main.rand.NextBool(NPCHelper.GetSpawnRate() / 60))
						{
							NPC.NewNPC(player.GetSource_Misc(""), xCoord * 16, yCoord * 16, ModContent.NPCType<BoneFish>());
						}
					}
				}
			}
			#endregion

			#region contagion thorny bushes
			if (TileID.Sets.SpreadOverground[Main.tile[xCoord, yCoord].TileType])
			{
				int type = Main.tile[xCoord, yCoord].TileType;
				if ((type == ModContent.TileType<ContagionThornyBushes>()) && WorldGen.genRand.NextBool(3))
				{
					WorldGen.GrowSpike(xCoord, yCoord, (ushort)ModContent.TileType<ContagionThornyBushes>(), (ushort)ModContent.TileType<Ickgrass>());
				}
				else if (!Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].LiquidAmount == 0 &&
					!Main.tile[xCoord, yCoord].IsHalfBlock && Main.tile[xCoord, yCoord].Slope == SlopeType.Solid &&
					WorldGen.genRand.NextBool(13) && (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<ContagionJungleGrass>()))
				{
					WorldGen.PlaceTile(xCoord, num9, ModContent.TileType<ContagionThornyBushes>(), mute: true);
				}
			}
			#endregion

			#region bloodberry and holybird spawning
			if (Main.tile[xCoord, num9].TileType == TileID.ImmatureHerbs && Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].TileFrameX == 54 &&
				(Main.tile[xCoord, yCoord].TileType == TileID.CrimsonGrass || Main.tile[xCoord, yCoord].TileType == TileID.Crimstone))
			{
				Tile t = Main.tile[xCoord, num9];
				t.TileType = (ushort)ModContent.TileType<Bloodberry>();
				t.TileFrameX = 0;
			}

			if (Main.tile[xCoord, num9].TileType == TileID.ImmatureHerbs && Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].TileFrameX == 0 &&
				Main.tile[xCoord, yCoord].TileType == TileID.HallowedGrass)
			{
				Tile t = Main.tile[xCoord, num9];
				t.TileType = (ushort)ModContent.TileType<Holybird>();
				t.TileFrameX = 0;
			}
			#endregion

			#region planter box grass growth
			if (Main.tile[xCoord, yCoord].TileType == ModContent.TileType<BarfbushPlanterBox>())
			{
				if (!Main.tile[xCoord, num9].HasTile && WorldGen.genRand.NextBool(2))
				{
					Tile tile = Main.tile[xCoord, num9];
					tile.HasTile = true;
					tile.TileType = TileID.Plants;
					int style = WorldGen.genRand.NextFromList<int>(6, 7, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 24, 27, 30, 33, 36, 39, 42);
					switch (style)
					{
						case 21:
						case 24:
						case 27:
						case 30:
						case 33:
						case 36:
						case 39:
						case 42:
							style += WorldGen.genRand.Next(3);
							break;
					}
					tile.TileFrameX = (short)(style * 18);
					//WorldGen.PlaceTile(num5, num9, 3, mute: true);
					if (Main.netMode == NetmodeID.Server && Main.tile[xCoord, num9].HasTile)
					{
						NetMessage.SendTileSquare(-1, xCoord, num9);
					}
				}
			}
			#endregion

			#region killing things if the block above/below isn't the necessary type
			// kill contagion vines if block above isn't contagion grass
			if (!(Main.tile[xCoord, num9].TileType == ModContent.TileType<Ickgrass>() || Main.tile[xCoord, num9].TileType == ModContent.TileType<ContagionJungleGrass>() || Main.tile[xCoord, num9].TileType == ModContent.TileType<ContagionVines>()) &&
				Main.tile[xCoord, yCoord].TileType == ModContent.TileType<ContagionVines>())
			{
				WorldGen.KillTile(xCoord, yCoord);
			}
			// kill contagion short grass if block below isn't contagion grass
			if (!(Main.tile[xCoord, num11].TileType == ModContent.TileType<Ickgrass>() || Main.tile[xCoord, num11].TileType == ModContent.TileType<ContagionJungleGrass>()) && Main.tile[xCoord, yCoord].TileType == ModContent.TileType<ContagionShortGrass>())
			{
				WorldGen.KillTile(xCoord, yCoord);
			}
			// kill barfbush if block below isn't contagion grass or chunkstone
			if (!(Main.tile[xCoord, num11].TileType == ModContent.TileType<Ickgrass>() || Main.tile[xCoord, num11].TileType == ModContent.TileType<ContagionJungleGrass>() || Main.tile[xCoord, num11].TileType == ModContent.TileType<Chunkstone>() || Data.Sets.TileSets.SuitableForPlantingHerbs[Main.tile[xCoord, num11].TileType]) &&
				Main.tile[xCoord, yCoord].TileType == ModContent.TileType<Barfbush>())
			{
				WorldGen.KillTile(xCoord, yCoord);
			}
			#endregion

			#region lazite grass
			if (Main.tile[xCoord, yCoord].TileType == ModContent.TileType<LaziteGrass>())
			{
				int num14 = Main.tile[xCoord, yCoord].TileType;

				// where lazite tallgrass would grow
				if (!Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].LiquidAmount == 0 &&
					!Main.tile[xCoord, yCoord].IsHalfBlock && Main.tile[xCoord, yCoord].Slope == SlopeType.Solid &&
					WorldGen.genRand.NextBool(15) && num14 == ModContent.TileType<LaziteGrass>())
				{
					WorldGen.PlaceTile(xCoord, num9, ModContent.TileType<LaziteShortGrass>(), true);
					Main.tile[xCoord, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 10) * 18);
					if (Main.tile[xCoord, num9].HasTile)
					{
						Tile t = Main.tile[xCoord, num9];
						t.TileColor = Main.tile[xCoord, yCoord].TileColor;
					}

					if (Main.netMode == NetmodeID.Server && Main.tile[xCoord, num9].HasTile)
					{
						NetMessage.SendTileSquare(-1, xCoord, num9, 1);
					}
				}

				bool flag2 = false;
				for (int m = num7; m < num8; m++)
				{
					for (int n = num9; n < num10; n++)
					{
						if ((xCoord != m || yCoord != n) && Main.tile[m, n].HasTile)
						{
							if (Main.tile[m, n].TileType == ModContent.TileType<BlastedStone>())
							{
								TileColorCache color = Main.tile[xCoord, yCoord].BlockColorAndCoating();
								WorldGen.SpreadGrass(m, n, ModContent.TileType<BlastedStone>(),
									ModContent.TileType<LaziteGrass>(), false, color);
							}

							if (Main.tile[m, n].TileType == num14)
							{
								WorldGen.SquareTileFrame(m, n);
								flag2 = true;
							}
						}
					}
				}

				if (Main.netMode == NetmodeID.Server && flag2)
				{
					NetMessage.SendTileSquare(-1, xCoord, yCoord, 3);
				}
			}
			#endregion

			#region large herb growth
			if (Main.tile[xCoord, yCoord].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
				Main.tile[xCoord, yCoord].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
				Main.tile[xCoord, yCoord].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
			{
				GrowLargeHerb(xCoord, yCoord);
			}
			#endregion

			#region impgrass
			if (Main.tile[xCoord, yCoord].TileType == ModContent.TileType<Ectograss>())
			{
				int num14 = Main.tile[xCoord, yCoord].TileType;
				bool flag2 = false;
				for (int m = num7; m < num8; m++)
				{
					for (int n = num9; n < num10; n++)
					{
						if ((xCoord != m || yCoord != n) && Main.tile[m, n].HasTile)
						{
							if (Main.tile[m, n].TileType == TileID.Ash)
							{
								TileColorCache color = Main.tile[xCoord, yCoord].BlockColorAndCoating();
								WorldGen.SpreadGrass(m, n, TileID.Ash, ModContent.TileType<Ectograss>(), false,
									color);
							}

							if (Main.tile[m, n].TileType == num14)
							{
								WorldGen.SquareTileFrame(m, n);
								flag2 = true;
							}
						}
					}
				}

				if (Main.netMode == NetmodeID.Server && flag2)
				{
					NetMessage.SendTileSquare(-1, xCoord, yCoord, 3);
				}
			}
			#endregion

			#region contagion shortgrass/barfbush spawning
			if (Main.tile[xCoord, yCoord].TileType == ModContent.TileType<Ickgrass>() || Main.tile[xCoord, yCoord].TileType == ModContent.TileType<ContagionJungleGrass>())
			{
				int num14 = Main.tile[xCoord, yCoord].TileType;
				if (!Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].LiquidAmount == 0 &&
					!Main.tile[xCoord, yCoord].IsHalfBlock && Main.tile[xCoord, yCoord].Slope == SlopeType.Solid &&
					WorldGen.genRand.NextBool(5) && (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>()))
				{
					WorldGen.PlaceTile(xCoord, num9, ModContent.TileType<ContagionShortGrass>(), true);
					Main.tile[xCoord, num9].TileFrameX = (short)(WorldGen.genRand.Next(0, 11) * 18);
					if (Main.tile[xCoord, num9].HasTile)
					{
						Tile t = Main.tile[xCoord, num9];
						t.TileColor = Main.tile[xCoord, yCoord].TileColor;
					}

					if (Main.netMode == NetmodeID.Server && Main.tile[xCoord, num9].HasTile)
					{
						NetMessage.SendTileSquare(-1, xCoord, num9, 1);
					}
				}

				if (!Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].LiquidAmount == 0 && !Main.tile[xCoord, yCoord].IsHalfBlock && Main.tile[xCoord, yCoord].Slope == SlopeType.Solid && WorldGen.genRand.NextBool(yCoord > Main.worldSurface ? 500 : 200) && (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>()))
				{
					WorldGen.PlaceTile(xCoord, num9, ModContent.TileType<Barfbush>(), true, false, -1, 0);
					if (Main.tile[xCoord, num9].HasTile)
					{
						Tile t = Main.tile[xCoord, num9];
						t.TileColor = Main.tile[xCoord, yCoord].TileColor;
					}
					if (Main.netMode == NetmodeID.Server && Main.tile[xCoord, num9].HasTile)
					{
						NetMessage.SendTileSquare(-1, xCoord, num9, 1);
					}
				}
				bool flag2 = false;
				for (int m = num7; m < num8; m++)
				{
					for (int n = num9; n < num10; n++)
					{
						if ((xCoord != m || yCoord != n) && Main.tile[m, n].HasTile)
						{
							if (Main.tile[m, n].TileType == 0 || ((num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>()) && Main.tile[m, n].TileType == TileID.Grass))
							{
								TileColorCache color = Main.tile[xCoord, yCoord].BlockColorAndCoating();
								WorldGen.SpreadGrass(m, n, 0, num14, false, color);
								if (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>())
								{
									WorldGen.SpreadGrass(m, n, TileID.Grass, ModContent.TileType<Ickgrass>(), false, color);
								}
								if (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>())
								{
									WorldGen.SpreadGrass(m, n, TileID.JungleGrass, ModContent.TileType<ContagionJungleGrass>(), false, color);
								}
								if (num14 == ModContent.TileType<Ickgrass>() || num14 == ModContent.TileType<ContagionJungleGrass>())
								{
									WorldGen.SpreadGrass(m, n, TileID.HallowedGrass, ModContent.TileType<Ickgrass>(), false, color);
								}
								if (Main.tile[m, n].TileType == ModContent.TileType<Ickgrass>() || Main.tile[m, n].TileType == ModContent.TileType<ContagionJungleGrass>())
								{
									WorldGen.SquareTileFrame(m, n, true);
									flag2 = true;
								}
							}
							if (Main.tile[m, n].TileType == 0 || (num14 == 109 && Main.tile[m, n].TileType == 2) || (num14 == 109 && Main.tile[m, n].TileType == 23) || (num14 == 109 && Main.tile[m, n].TileType == 199))
							{
								if (num14 == TileID.HallowedGrass)
								{
									TileColorCache color = Main.tile[xCoord, yCoord].BlockColorAndCoating();
									WorldGen.SpreadGrass(m, n, ModContent.TileType<Ickgrass>(), num14, false, color);
								}
							}
						}
					}
				}
				if (Main.netMode == NetmodeID.Server && flag2)
				{
					NetMessage.SendTileSquare(-1, xCoord, yCoord, 3);
				}
			}
			#endregion

			#region ectovines growing
			if ((Main.tile[xCoord, yCoord].TileType == ModContent.TileType<Ectograss>() ||
				 Main.tile[xCoord, yCoord].TileType == ModContent.TileType<Ectovines>()) &&
				WorldGen.genRand.NextBool(15) && !Main.tile[xCoord, yCoord + 1].HasTile && // change back to NextBool(15)
				Main.tile[xCoord, yCoord + 1].LiquidType != LiquidID.Lava)
			{
				bool flag10 = false;
				for (int num47 = yCoord; num47 > yCoord - 10; num47--)
				{
					if (Main.tile[xCoord, num47].BottomSlope)
					{
						flag10 = false;
						break;
					}

					if (Main.tile[xCoord, num47].HasTile &&
						Main.tile[xCoord, num47].TileType == ModContent.TileType<Ectograss>() &&
						!Main.tile[xCoord, num47].BottomSlope)
					{
						flag10 = true;
						break;
					}
				}

				if (flag10)
				{
					int num48 = xCoord;
					int num49 = yCoord + 1;
					Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<Ectovines>();

					Tile t = Main.tile[num48, num49];
					t.HasTile = true;
					WorldGen.SquareTileFrame(num48, num49);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, num48, num49, 3);
					}
				}
			}
			#endregion

			#region contagion vines growing
			if ((Main.tile[xCoord, yCoord].TileType == ModContent.TileType<Ickgrass>() ||
				 Main.tile[xCoord, yCoord].TileType == ModContent.TileType<ContagionJungleGrass>() ||
				 Main.tile[xCoord, yCoord].TileType == ModContent.TileType<ContagionVines>()) &&
				WorldGen.genRand.NextBool(15) && !Main.tile[xCoord, yCoord + 1].HasTile &&
				Main.tile[xCoord, yCoord + 1].LiquidType != LiquidID.Lava)
			{
				bool flag10 = false;
				for (int num47 = yCoord; num47 > yCoord - 10; num47--)
				{
					if (Main.tile[xCoord, num47].BottomSlope)
					{
						flag10 = false;
						break;
					}

					if (Main.tile[xCoord, num47].HasTile &&
						(Main.tile[xCoord, num47].TileType == ModContent.TileType<Ickgrass>() ||
						 Main.tile[xCoord, num47].TileType == ModContent.TileType<ContagionJungleGrass>()) &&
						!Main.tile[xCoord, num47].BottomSlope)
					{
						flag10 = true;
						break;
					}
				}

				if (flag10)
				{
					int num48 = xCoord;
					int num49 = yCoord + 1;
					Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<ContagionVines>();

					Tile t = Main.tile[num48, num49];
					t.HasTile = true;
					WorldGen.SquareTileFrame(num48, num49);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, num48, num49, 3);
					}
				}
			}
			#endregion

			#region tropical shortgrass to long grass
			if (Main.tile[xCoord, yCoord].TileType == ModContent.TileType<SavannaShortGrass>() && WorldGen.genRand.NextBool(3) && Main.tile[xCoord, yCoord].TileFrameX < 144)
			{
				if (WorldGen.genRand.NextBool(4))
					Main.tile[xCoord, yCoord].TileFrameX = (short)(162 + WorldGen.genRand.Next(8) * 18);

				Main.tile[xCoord, yCoord].TileType = (ushort)ModContent.TileType<SavannaLongGrass>();
				if (Main.netMode == NetmodeID.Server)
					NetMessage.SendTileSquare(-1, xCoord, yCoord);
			}
			#endregion

			#region tropical grass
			if (Main.tile[xCoord, yCoord].TileType == ModContent.TileType<SavannaGrass>())
			{
				int num14 = Main.tile[xCoord, yCoord].TileType;
				// twilight plume
				if (!Main.tile[xCoord, num9].HasTile && Main.tile[xCoord, num9].LiquidAmount == 0 &&
					!Main.tile[xCoord, yCoord].IsHalfBlock && Main.tile[xCoord, yCoord].Slope == SlopeType.Solid &&
					WorldGen.genRand.NextBool(yCoord > Main.worldSurface ? 75 : 250) && num14 == ModContent.TileType<SavannaGrass>())
				{
					WorldGen.PlaceTile(xCoord, num9, ModContent.TileType<TwilightPlume>(), true, false, -1, 0);
					if (Main.tile[xCoord, num9].HasTile)
					{
						Tile t = Main.tile[xCoord, num9];
						t.TileColor = Main.tile[xCoord, yCoord].TileColor;
					}
					if (Main.netMode == NetmodeID.Server && Main.tile[xCoord, num9].HasTile)
					{
						NetMessage.SendTileSquare(-1, xCoord, num9, 1);
					}
				}
				// regular grass
				if (!Main.tile[xCoord, num9].HasTile &&
					!Main.tile[xCoord, yCoord].IsHalfBlock && Main.tile[xCoord, yCoord].Slope == SlopeType.Solid &&
					WorldGen.genRand.NextBool(5) && num14 == ModContent.TileType<SavannaGrass>())
				{
					Tile t2 = Main.tile[xCoord, num9];
					t2.TileType = (ushort)ModContent.TileType<SavannaShortGrass>();

					if (WorldGen.genRand.NextBool(60) && num9 > Main.rockLayer) // shroom cap
					{
						t2.TileFrameX = 8 * 18;
					}
					else if (WorldGen.genRand.NextBool(230) && num9 > Main.rockLayer) // nature's gift
					{
						t2.TileFrameX = 9 * 18;
					}
					else if (WorldGen.genRand.NextBool(15))
					{
						t2.TileFrameX = (short)(WorldGen.genRand.Next(10, 23) * 18);
					}
					else
					{
						t2.TileFrameX = (short)(WorldGen.genRand.Next(8) * 18);
					}

					if (Main.tile[xCoord, num9].HasTile)
					{
						t2.TileColor = Main.tile[xCoord, yCoord].TileColor;
					}

					if (Main.netMode == NetmodeID.Server && Main.tile[xCoord, num9].HasTile)
					{
						NetMessage.SendTileSquare(-1, xCoord, num9, 1);
					}
				}
				bool flag2 = false;
				for (int m = num7; m < num8; m++)
				{
					for (int n = num9; n < num10; n++)
					{
						if ((xCoord != m || yCoord != n) && Main.tile[m, n].HasTile)
						{
							if (Main.tile[m, n].TileType == ModContent.TileType<Loam>())
							{
								TileColorCache color = Main.tile[xCoord, yCoord].BlockColorAndCoating();
								WorldGen.SpreadGrass(m, n, ModContent.TileType<Loam>(), ModContent.TileType<SavannaGrass>(), false, color);
							}

							if (Main.tile[m, n].TileType == num14)
							{
								//WorldGen.SquareTileFrame(m, n);
								flag2 = true;
							}
						}
					}
				}

				if (Main.netMode == NetmodeID.Server && flag2)
				{
					NetMessage.SendTileSquare(-1, xCoord, yCoord, 3);
				}
			}
			#endregion tropical grass

			#region tropics vines growing
			if ((Main.tile[xCoord, yCoord].TileType == ModContent.TileType<SavannaGrass>() ||
				Main.tile[xCoord, yCoord].TileType == ModContent.TileType<SavannaVines>()) &&
				WorldGen.genRand.NextBool(15) && !Main.tile[xCoord, yCoord + 1].HasTile &&
				Main.tile[xCoord, yCoord + 1].LiquidType != LiquidID.Lava)
			{
				bool flag10 = false;
				for (int num47 = yCoord; num47 > yCoord - 10; num47--)
				{
					if (Main.tile[xCoord, num47].BottomSlope)
					{
						flag10 = false;
						break;
					}

					if (Main.tile[xCoord, num47].HasTile &&
						Main.tile[xCoord, num47].TileType == ModContent.TileType<SavannaGrass>() &&
						!Main.tile[xCoord, num47].BottomSlope)
					{
						flag10 = true;
						break;
					}
				}

				if (flag10)
				{
					int num48 = xCoord;
					int num49 = yCoord + 1;
					Main.tile[num48, num49].TileType = (ushort)ModContent.TileType<SavannaVines>();

					Tile t = Main.tile[num48, num49];
					t.HasTile = true;
					WorldGen.SquareTileFrame(num48, num49);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, num48, num49, 3);
					}
				}
			}
			#endregion tropical vines
		}

		#region more fallen stars
		if (!Main.dayTime && Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.AdvancedBuffs.AdvStarbright>()) || Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.Starbright>()))
		{
			float num66 = Main.maxTilesX / 4200;
			if (Main.rand.Next(4000) < 10f * num66)
			{
				int num67 = 12;
				int num68 = Main.rand.Next(Main.maxTilesX - 50) + 100;
				num68 *= 16;
				int num69 = Main.rand.Next((int)(Main.maxTilesY * 0.05));
				num69 *= 16;
				var vector = new Vector2(num68, num69);
				float num70 = Main.rand.Next(-100, 101);
				float num71 = Main.rand.Next(200) + 100;
				float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
				num72 = num67 / num72;
				num70 *= num72;
				num71 *= num72;
				Projectile.NewProjectile(Entity.GetSource_None(), vector.X, vector.Y, num70, num71, ProjectileID.FallingStar, 1000, 10f, Main.myPlayer);
			}
		}
		if (!Main.dayTime && Main.player[Main.myPlayer].HasBuff(ModContent.BuffType<Buffs.AdvancedBuffs.AdvStarbright>()))
		{
			float num66 = Main.maxTilesX / 4200;
			if (Main.rand.Next(4000) < 10f * num66)
			{
				int num67 = 12;
				int num68 = Main.rand.Next(Main.maxTilesX - 50) + 100;
				num68 *= 16;
				int num69 = Main.rand.Next((int)(Main.maxTilesY * 0.05));
				num69 *= 16;
				var vector = new Vector2(num68, num69);
				float num70 = Main.rand.Next(-100, 101);
				float num71 = Main.rand.Next(200) + 100;
				float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
				num72 = num67 / num72;
				num70 *= num72;
				num71 *= num72;
				Projectile.NewProjectile(Entity.GetSource_None(), vector.X, vector.Y, num70, num71, ProjectileID.FallingStar, 1000, 10f, Main.myPlayer);
			}
		}
		if (!Main.dayTime && Main.bloodMoon)
		{
			float num66 = Main.maxTilesX / 4200;
			if (Main.rand.Next(9000) < 10f * num66)
			{
				int num67 = 12;
				int num68 = Main.rand.Next(Main.maxTilesX - 50) + 100;
				num68 *= 16;
				int num69 = Main.rand.Next((int)(Main.maxTilesY * 0.05));
				num69 *= 16;
				var vector = new Vector2(num68, num69);
				float num70 = Main.rand.Next(-100, 101);
				float num71 = Main.rand.Next(200) + 100;
				float num72 = (float)Math.Sqrt(num70 * num70 + num71 * num71);
				num72 = num67 / num72;
				num70 *= num72;
				num71 *= num72;
				Projectile.NewProjectile(Entity.GetSource_None(), vector.X, vector.Y, num70, num71, ProjectileID.FallingStar, 1000, 10f, Main.myPlayer);
			}
		}
		#endregion

		#region npc collision
		for (int thing = 0; thing < Main.npc.Length; thing++)
		{
			NPC npc1 = Main.npc[thing];
			if ((npc1.type == NPCID.Corruptor /*|| npc1.type == ModContent.NPCType<NPCs.GuardianCorruptor>()*/) && npc1.active)
			{
				for (int thing2 = 0; thing2 < Main.npc.Length; thing2++)
				{
					NPC npc2 = Main.npc[thing2];
					if ((npc2.type == ModContent.NPCType<NPCs.Hardmode.Hallowor>() /*|| npc2.type == ModContent.NPCType<NPCs.AegisHallowor>()*/) && npc2.active)
					{
						int radius;
						string text;
						if (npc1.type == NPCID.Corruptor && npc2.type == ModContent.NPCType<NPCs.Hardmode.Hallowor>())
						{
							radius = 2;
							text = "Dark and light have been obliterated...";
							MakeOblivionOre(npc1, npc2, text, radius);
						}
						//else if (npc1.type == ModContent.NPCType<NPCs.GuardianCorruptor>() && npc2.type == ModContent.NPCType<NPCs.AegisHallowor>())
						//{
						//    radius = 3;
						//    text = "Dark and light have been eliminated...";
						//    MakeOblivionOre(npc1, npc2, text, radius);
						//}
					}
				}
			}
		}

		// reflected npc projectiles
		for (int thing = 0; thing < Main.npc.Length; thing++)
		{
			NPC npc1 = Main.npc[thing];
			if (npc1.aiStyle == 9 && npc1.GetGlobalNPC<AvalonGlobalNPCInstance>().CanDamageMobs)
			{
				for (int thing2 = 0; thing2 < Main.npc.Length; thing2++)
				{
					NPC npc2 = Main.npc[thing2];
					if (npc2.whoAmI != npc1.whoAmI)
						DamageMobWithNPCProjectile(npc1, npc2);
				}
			}
		}
		#endregion

		//#region platform leaf
		//int numNPCs = 0;
		//for (int n = 0; n < Main.npc.Length; n++)
		//{
		//    NPC n1 = Main.npc[n];
		//    Point tile = n1.position.ToTileCoordinates() + new Point(0, n1.height / 16 + 1);

		//    int xpos;
		//    int ypos;
		//    for (xpos = Main.tile[tile.X, tile.Y].TileFrameX / 18; xpos > 2; xpos -= 3) { }
		//    for (ypos = Main.tile[tile.X, tile.Y].TileFrameY / 18; ypos > 3; ypos -= 4) { }
		//    xpos = tile.X - xpos;
		//    ypos = tile.Y - ypos;
		//    if (Main.tile[tile.X, tile.Y].TileType == ModContent.TileType<PlatformLeaf>() && Main.tile[tile.X, tile.Y].TileFrameY < 18 && !n1.noGravity && !n1.noTileCollide && n1.active)
		//    {
		//        numNPCs++;
		//        if (numNPCs > 1 || n1.velocity.Y > 4.5f)
		//        {
		//            for (int i = xpos; i < xpos + 3; i++)
		//            {
		//                for (int j = ypos; j < ypos + 4; j++)
		//                {
		//                    Main.tile[i, j].TileFrameY += 74;

		//                }
		//            }
		//            numNPCs = 0;
		//            SoundStyle s = new SoundStyle("Terraria/Sounds/Grass") { Pitch = -0.2f };
		//            SoundEngine.PlaySound(s, new Vector2((xpos + 1) * 16, ypos * 16));
		//            WorldGen.TreeGrowFX(xpos + 1, ypos + 3, 2, ModContent.GoreType<PlatformLeafLeaf>(), true);
		//            break;
		//        }
		//    }
		//}
		//#endregion
	}

	public static void GenerateSulphur()
	{
		Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

		for (int x = 10; x < Main.maxTilesX - 10; x++)
		{
			for (int y = (int)Main.rockLayer + 10; y < Main.maxTilesY - 200; y++)
			{
				if (Main.tile[x, y].TileType == TileID.SmallPiles)
				{
					// skip the top row of the tilesheet
					if (Main.tile[x, y].TileFrameY != 0)
					{
						// skip the frames it shouldn't replace
						if (Main.tile[x, y].TileFrameX >= 576 && Main.tile[x, y].TileFrameX <= 882 || Main.tile[x, y].TileFrameX >= 1116 && Main.tile[x, y].TileFrameX <= 1458 ||
							Main.tile[x, y].TileFrameX >= 216 && Main.tile[x, y].TileFrameX <= 306 && Main.tile[x, y].TileFrameY == 36)
						{
						}
						else if (WorldGen.genRand.NextBool(2))
						{
							WorldGen.KillTile(x, y);
							WorldGen.PlaceTile(x, y, ModContent.TileType<SmallSulphurBlob>(), true, true, style: WorldGen.genRand.Next(6));
						}
					}
				}
				if (Main.tile[x, y].TileType == TileID.LargePiles)
				{
					// skip the frames it shouldn't replace
					if (Main.tile[x, y].TileFrameX >= 324 && Main.tile[x, y].TileFrameX <= 360 || Main.tile[x, y].TileFrameX >= 702 && Main.tile[x, y].TileFrameX <= 1386)
					{
					}
					else if (WorldGen.genRand.NextBool(3))
					{
						WorldGen.KillTile(x, y);
						WorldGen.PlaceTile(x, y, ModContent.TileType<SulphurBlob>(), true, style: WorldGen.genRand.Next(6));
					}
				}
			}
		}

		//for (int a = 0; a < (int)(Main.maxTilesX * Main.maxTilesY * 0.00012); a++)
		//{
		//    int x = Main.rand.Next(100, Main.maxTilesX - 100);
		//    int y = Main.rand.Next((int)Main.rockLayer, Main.maxTilesY - 150);
		//    WorldGen.OreRunner(x, y, Main.rand.Next(3, 6), Main.rand.Next(3, 5),
		//        (ushort)ModContent.TileType<Tiles.Ores.SulphurOre>());
		//}

		if (Main.netMode == NetmodeID.SinglePlayer)
		{
			Main.NewText("The underground smells like rotten eggs...", 210, 183, 4);
		}
		else if (Main.netMode == NetmodeID.Server)
		{
			ChatHelper.BroadcastChatMessage(
				NetworkText.FromLiteral("The underground smells like rotten eggs..."),
				new Color(210, 183, 4));
		}
	}
	public static void GrowLargeHerb(int x, int y)
	{
		if (Main.tile[x, y].HasTile)
		{
			if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>() &&
				WorldGen.genRand.NextBool(8)) // phase 1 to 2
			{
				bool grow = false;
				if (Main.tile[x, y].TileFrameX == 108) // shiverthorn check
				{
					if (Main.rand.NextBool(2))
					{
						grow = true;
					}
				}
				else
				{
					grow = true;
				}

				if (grow)
				{
					Main.tile[x, y].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
					if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
					{
						Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
					}

					if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
					{
						Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
					}

					if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
					{
						Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
					}

					if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage1>())
					{
						Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage2>();
					}

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, y, 2);
						NetMessage.SendTileSquare(-1, x, y + 1, 2);
						NetMessage.SendTileSquare(-1, x, y + 2, 2);
						NetMessage.SendTileSquare(-1, x, y - 1, 2);
						NetMessage.SendTileSquare(-1, x, y - 2, 2);
					}

					WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
				}
			}
			else if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>() &&
					 WorldGen.genRand.NextBool(7)) // phase 2 to 3
			{
				bool grow = false;
				if (Main.tile[x, y].TileFrameX == 108) // shiverthorn check
				{
					if (Main.rand.NextBool(2))
					{
						grow = true;
					}
				}
				else
				{
					grow = true;
				}

				if (grow)
				{
					Main.tile[x, y].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
					if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
					{
						Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
					}

					if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
					{
						Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
					}

					if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
					{
						Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
					}

					if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage2>())
					{
						Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage3>();
					}

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, y, 2);
						NetMessage.SendTileSquare(-1, x, y + 1, 2);
						NetMessage.SendTileSquare(-1, x, y + 2, 2);
						NetMessage.SendTileSquare(-1, x, y - 1, 2);
						NetMessage.SendTileSquare(-1, x, y - 2, 2);
					}

					WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
				}
			}
			else if (Main.tile[x, y].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>() &&
					 WorldGen.genRand.NextBool(5)) // phase 3 to 4
			{
				Main.tile[x, y].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
				if (Main.tile[x, y + 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
				{
					Main.tile[x, y + 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
				}

				if (Main.tile[x, y + 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
				{
					Main.tile[x, y + 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
				}

				if (Main.tile[x, y - 1].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
				{
					Main.tile[x, y - 1].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
				}

				if (Main.tile[x, y - 2].TileType == (ushort)ModContent.TileType<LargeHerbsStage3>())
				{
					Main.tile[x, y - 2].TileType = (ushort)ModContent.TileType<LargeHerbsStage4>();
				}

				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendTileSquare(-1, x, y, 2);
					NetMessage.SendTileSquare(-1, x, y + 1, 2);
					NetMessage.SendTileSquare(-1, x, y + 2, 2);
					NetMessage.SendTileSquare(-1, x, y - 1, 2);
					NetMessage.SendTileSquare(-1, x, y - 2, 2);
				}

				WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
			}


			if (ExxoAvalonOrigins.ThoriumContentEnabled)
			{
				if (Main.tile[x, y].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage1").Type &&
					WorldGen.genRand.NextBool(8)) // phase 1 to 2
				{
					Main.tile[x, y].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type;
					if (Main.tile[x, y + 1].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage1").Type)
					{
						Main.tile[x, y + 1].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type;
					}

					if (Main.tile[x, y + 2].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage1").Type)
					{
						Main.tile[x, y + 2].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type;
					}

					if (Main.tile[x, y - 1].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage1").Type)
					{
						Main.tile[x, y - 1].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type;
					}

					if (Main.tile[x, y - 2].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage1").Type)
					{
						Main.tile[x, y - 2].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type;
					}

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, y, 2);
						NetMessage.SendTileSquare(-1, x, y + 1, 2);
						NetMessage.SendTileSquare(-1, x, y + 2, 2);
						NetMessage.SendTileSquare(-1, x, y - 1, 2);
						NetMessage.SendTileSquare(-1, x, y - 2, 2);
					}

					WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
				}
				else if (Main.tile[x, y].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type &&
						 WorldGen.genRand.NextBool(7)) // phase 2 to 3
				{
					Main.tile[x, y].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type;
					if (Main.tile[x, y + 1].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type)
					{
						Main.tile[x, y + 1].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type;
					}

					if (Main.tile[x, y + 2].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type)
					{
						Main.tile[x, y + 2].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type;
					}

					if (Main.tile[x, y - 1].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type)
					{
						Main.tile[x, y - 1].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type;
					}

					if (Main.tile[x, y - 2].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage2").Type)
					{
						Main.tile[x, y - 2].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type;
					}

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, y, 2);
						NetMessage.SendTileSquare(-1, x, y + 1, 2);
						NetMessage.SendTileSquare(-1, x, y + 2, 2);
						NetMessage.SendTileSquare(-1, x, y - 1, 2);
						NetMessage.SendTileSquare(-1, x, y - 2, 2);
					}

					WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
				}
				else if (Main.tile[x, y].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type &&
						 WorldGen.genRand.NextBool(5)) // phase 3 to 4
				{
					Main.tile[x, y].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage4").Type;
					if (Main.tile[x, y + 1].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type)
					{
						Main.tile[x, y + 1].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage4").Type;
					}

					if (Main.tile[x, y + 2].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type)
					{
						Main.tile[x, y + 2].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage4").Type;
					}

					if (Main.tile[x, y - 1].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type)
					{
						Main.tile[x, y - 1].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage4").Type;
					}

					if (Main.tile[x, y - 2].TileType == ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage3").Type)
					{
						Main.tile[x, y - 2].TileType = ExxoAvalonOrigins.Mod.Find<ModTile>("LargeMarineKelpStage4").Type;
					}

					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, y, 2);
						NetMessage.SendTileSquare(-1, x, y + 1, 2);
						NetMessage.SendTileSquare(-1, x, y + 2, 2);
						NetMessage.SendTileSquare(-1, x, y - 1, 2);
						NetMessage.SendTileSquare(-1, x, y - 2, 2);
					}

					WorldGeneration.Utils.SquareTileFrameArea(x, y, 4, true);
				}
			}
		}
	}

	//public static void CheckLargeHerb(int x, int y, int type)
	//{
	//    if (WorldGen.destroyObject)
	//    {
	//        return;
	//    }

	//    Tile t = Main.tile[x, y];
	//    int style = t.TileFrameX / 18;
	//    bool destroy = false;
	//    int fixedY = y;
	//    int yFrame = Main.tile[x, y].TileFrameY;
	//    fixedY -= yFrame / 18;
	//    if (!WorldGen.SolidTile2(x, fixedY + 3) || !Main.tile[x, fixedY].HasTile ||
	//        !Main.tile[x, fixedY + 1].HasTile || !Main.tile[x, fixedY + 2].HasTile)
	//    {
	//        destroy = true;
	//    }

	//    if (destroy)
	//    {
	//        WorldGen.destroyObject = true;
	//        for (int u = fixedY; u < fixedY + 3; u++)
	//        {
	//            WorldGen.KillTile(x, u);
	//        }

	//        // 469 through 471 are the immature tiles of the large herb; 472 is the mature version
	//        if (type == (ushort)ModContent.TileType<LargeHerbsStage1>() ||
	//            type == (ushort)ModContent.TileType<LargeHerbsStage2>() ||
	//            type == (ushort)ModContent.TileType<LargeHerbsStage3>())
	//        {
	//            int item = 0;
	//            switch (style)
	//            {
	//                case 0:
	//                    item = ModContent.ItemType<LargeDaybloomSeed>();
	//                    break;
	//                case 1:
	//                    item = ModContent.ItemType<LargeMoonglowSeed>();
	//                    break;
	//                case 2:
	//                    item = ModContent.ItemType<LargeBlinkrootSeed>();
	//                    break;
	//                case 3:
	//                    item = ModContent.ItemType<LargeDeathweedSeed>();
	//                    break;
	//                case 4:
	//                    item = ModContent.ItemType<LargeWaterleafSeed>();
	//                    break;
	//                case 5:
	//                    item = ModContent.ItemType<LargeFireblossomSeed>();
	//                    break;
	//                case 6:
	//                    item = ModContent.ItemType<LargeShiverthornSeed>();
	//                    break;
	//                case 7:
	//                    item = ModContent.ItemType<LargeBloodberrySeed>();
	//                    break;
	//                case 8:
	//                    item = ModContent.ItemType<LargeSweetstemSeed>();
	//                    break;
	//                case 9:
	//                    item = ModContent.ItemType<LargeBarfbushSeed>();
	//                    break;
	//                case 10:
	//                    item = ModContent.ItemType<LargeHolybirdSeed>();
	//                    break;
	//                case 11:
	//                    item = ModContent.ItemType<LargeTwilightPlumeSeed>();
	//                    break;
	//            } // 3710 through 3719 are the seeds

	//            if (item > 0)
	//            {
	//                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, fixedY * 16, 16, 48, item);
	//            }
	//        }
	//        else if (type == ModContent.TileType<LargeHerbsStage4>())
	//        {
	//            int item = 0;
	//            switch (style)
	//            {
	//                case 0:
	//                    item = ModContent.ItemType<LargeDaybloom>();
	//                    break;
	//                case 1:
	//                    item = ModContent.ItemType<LargeMoonglow>();
	//                    break;
	//                case 2:
	//                    item = ModContent.ItemType<LargeBlinkroot>();
	//                    break;
	//                case 3:
	//                    item = ModContent.ItemType<LargeDeathweed>();
	//                    break;
	//                case 4:
	//                    item = ModContent.ItemType<LargeWaterleaf>();
	//                    break;
	//                case 5:
	//                    item = ModContent.ItemType<LargeFireblossom>();
	//                    break;
	//                case 6:
	//                    item = ModContent.ItemType<LargeShiverthorn>();
	//                    break;
	//                case 7:
	//                    item = ModContent.ItemType<LargeBloodberry>();
	//                    break;
	//                case 8:
	//                    item = ModContent.ItemType<LargeSweetstem>();
	//                    break;
	//                case 9:
	//                    item = ModContent.ItemType<LargeBarfbush>();
	//                    break;
	//                case 10:
	//                    item = ModContent.ItemType<LargeHolybird>();
	//                    break;
	//                case 11:
	//                    item = ModContent.ItemType<LargeTwilightPlume>();
	//                    break;
	//            }

	//            if (item > 0)
	//            {
	//                Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, fixedY * 16, 16, 48, item);
	//            }

	//            // 3700 through 3709 are the large herbs
	//        }

	//        WorldGen.destroyObject = false;
	//    }
	//}
	public static void ShatterCrackedBricks(int i, int j, Tile tileCache, bool fail)
	{
		if (tileCache.TileType != ModContent.TileType<CrackedOrangeBrick>() && tileCache.TileType != ModContent.TileType<CrackedPurpleBrick>() || Main.netMode == NetmodeID.MultiplayerClient || crackedBrick || j < Main.maxTilesY - 200)
		{
			return;
		}
		crackedBrick = true;
		for (int k = i - 4; k <= i + 4; k++)
		{
			for (int l = j - 4; l <= j + 4; l++)
			{
				int maxValue = 15;
				if (!WorldGen.SolidTile(k, l + 1))
				{
					maxValue = 7;
				}
				else if (k == i && l == j - 1 && !fail)
				{
					maxValue = 7;
				}
				if ((k != i || l != j) && Main.tile[k, l].HasTile && (Main.tile[k, l].TileType == ModContent.TileType<CrackedOrangeBrick>() || Main.tile[k, l].TileType == ModContent.TileType<CrackedPurpleBrick>()) && WorldGen.genRand.NextBool(maxValue))
				{
					WorldGen.KillTile(k, l, fail: false, effectOnly: false, noItem: true);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, k, l);
					}
				}
			}
		}
		crackedBrick = false;
	}
	public static void GenerateHallowedOre()
	{
		Main.rand ??= new UnifiedRandom((int)DateTime.Now.Ticks);

		double num5 = Main.rockLayer;
		int xloc = -100 + Main.maxTilesX - 100;
		int yloc = -(int)num5 + Main.maxTilesY - 200;
		int sum = xloc * yloc;
		int amount = (sum / 18000) * 5;
		for (int zz = 0; zz < amount; zz++)
		{
			int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
			double num6 = Main.rockLayer;
			int j2 = WorldGen.genRand.Next((int)num6, Main.maxTilesY - 200);
			WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(6, 10), (ushort)ModContent.TileType<Tiles.Ores.HallowedOre>());
		}
		if (Main.netMode == NetmodeID.SinglePlayer)
		{
			Main.NewText(Language.GetTextValue("Mods.Avalon.Altars.Hallowed"), 220, 170, 0);
		}
		else if (Main.netMode == NetmodeID.Server)
		{
			ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Avalon.Altars.Hallowed"), new Color(220, 170, 0));
		}
	}
	public static void ConvertFromThings(int x, int y, int convert, bool tileframe = true)
	{
		Tile tile = Main.tile[x, y];
		int type = tile.TileType;
		int wall = tile.WallType;
		if (!WorldGen.InWorld(x, y, 1))
		{
			return;
		}
		// convert from contagion to purity
		if (convert == 0)
		{
			if (Main.tile[x, y] != null)
			{
				if (wall == ModContent.WallType<Walls.ContagionGrassWall>())
				{
					Main.tile[x, y].WallType = WallID.GrassUnsafe;
				}
				else if (wall == ModContent.WallType<Walls.ChunkstoneWall>() || wall == ModContent.WallType<Walls.ChunkstoneWallSafe>())
				{
					Main.tile[x, y].WallType = WallID.Stone;
				}
				else if (wall == ModContent.WallType<Walls.ContagionLumpWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionLumpWall>())
				{
					Main.tile[x, y].WallType = WallID.RocksUnsafe1;
				}
				else if (wall == ModContent.WallType<Walls.ContagionMouldWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionMouldWall>())
				{
					Main.tile[x, y].WallType = WallID.RocksUnsafe2;
				}
				else if (wall == ModContent.WallType<Walls.ContagionCystWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionCystWall>())
				{
					Main.tile[x, y].WallType = WallID.RocksUnsafe3;
				}
				else if (wall == ModContent.WallType<Walls.ContagionBoilWallUnsafe>() || wall == ModContent.WallType<Walls.ContagionBoilWall>())
				{
					Main.tile[x, y].WallType = WallID.RocksUnsafe4;
				}
				else if (wall == ModContent.WallType<Walls.HardenedSnotsandWallUnsafe>() || wall == ModContent.WallType<Walls.HardenedSnotsandWall>())
				{
					Main.tile[x, y].WallType = WallID.HardenedSand;
				}
				else if (wall == ModContent.WallType<Walls.SnotsandstoneWallUnsafe>() || wall == ModContent.WallType<Walls.SnotsandstoneWall>())
				{
					Main.tile[x, y].WallType = WallID.Sandstone;
				}
			}
			if (Main.tile[x, y] != null)
			{
				Tile tileUp = Main.tile[x, y - 1];
				int typeUp = tileUp.TileType;
				Tile tileDown = Main.tile[x, y + 1];
				int typeDown = tileDown.TileType;
				if (type == ModContent.TileType<Ickgrass>())
				{
					tile.TileType = TileID.Grass;
					if (typeDown == ModContent.TileType<ContagionVines>())
					{
						for (int downVar = 1; typeDown == ModContent.TileType<ContagionVines>() && !Main.tileSolid[typeDown]; downVar++)
						{
							Tile tileDownVar = Main.tile[x, y + downVar];
							int typeDownVar = tileDownVar.TileType;
							if (typeDownVar == ModContent.TileType<ContagionVines>())
							{
								tileDownVar.TileType = TileID.Vines;
							}
							else
							{
								break;
							}
						}
					}
				}
				if (type == ModContent.TileType<ContagionJungleGrass>())
				{
					tile.TileType = TileID.JungleGrass;
					if (typeDown == ModContent.TileType<ContagionVines>())
					{
						for (int downVar = 1; typeDown == ModContent.TileType<ContagionVines>() && !Main.tileSolid[typeDown]; downVar++)
						{
							Tile tileDownVar = Main.tile[x, y + downVar];
							int typeDownVar = tileDownVar.TileType;
							if (typeDownVar == ModContent.TileType<ContagionVines>())
							{
								tileDownVar.TileType = TileID.JungleVines;
							}
							else
							{
								break;
							}
						}
					}
				}
				else if (type == ModContent.TileType<YellowIce>())
				{
					tile.TileType = TileID.IceBlock;
				}
				else if (type == ModContent.TileType<Snotsand>())
				{
					tile.TileType = TileID.Sand;
				}
				else if (type == ModContent.TileType<Chunkstone>())
				{
					tile.TileType = TileID.Stone;
				}
				else if (type == ModContent.TileType<Snotsandstone>())
				{
					tile.TileType = TileID.Sandstone;
				}
				else if (type == ModContent.TileType<HardenedSnotsand>())
				{
					tile.TileType = TileID.HardenedSand;
				}
				else if (type == ModContent.TileType<ContagionShortGrass>())
				{
					tile.TileType = TileID.Plants;
				}
				//if (TileID.Sets.Conversion.Grass[type] || type == 0)
				//{
				//    WorldGen.SquareTileFrame(x, y);
				//}
			}
		}
		if (convert == 1)
		{
			if (Main.tile[x, y] != null)
			{
				if (wall == ModContent.WallType<Walls.ContagionGrassWall>() || wall == WallID.CrimsonGrassUnsafe || wall == WallID.CorruptGrassUnsafe || wall == WallID.HallowedGrassUnsafe)
				{
					Main.tile[x, y].WallType = WallID.JungleUnsafe;
				}
				else if (wall == WallID.DirtUnsafe)
				{
					Main.tile[x, y].WallType = WallID.MudUnsafe;
				}
			}
			if (Main.tile[x, y] != null)
			{
				if (type == ModContent.TileType<Ickgrass>() || type == ModContent.TileType<ContagionJungleGrass>() || type == TileID.CrimsonGrass || type == TileID.CrimsonJungleGrass || type == TileID.CorruptGrass || type == TileID.CorruptJungleGrass || type == TileID.Grass || type == TileID.HallowedGrass)
				{
					tile.TileType = TileID.JungleGrass;
				}
				else if (type == TileID.Dirt)
				{
					tile.TileType = TileID.Mud;
				}
				else if (type == ModContent.TileType<Snotsand>() || type == TileID.Sand || type == TileID.Crimsand || type == TileID.Ebonsand || type == TileID.Pearlsand)
				{
					tile.TileType = TileID.Sand;
				}
				else if (type == ModContent.TileType<Chunkstone>() || type == TileID.Pearlstone || type == TileID.Crimstone || type == TileID.Ebonstone)
				{
					tile.TileType = TileID.Stone;
				}
				else if (type == ModContent.TileType<Snotsandstone>() || type == TileID.HallowSandstone || type == TileID.CrimsonSandstone || type == TileID.CorruptSandstone)
				{
					tile.TileType = TileID.Sandstone;
				}
				else if (type == ModContent.TileType<HardenedSnotsand>() || type == TileID.HallowHardenedSand || type == TileID.CrimsonHardenedSand || type == TileID.CorruptHardenedSand)
				{
					tile.TileType = TileID.HardenedSand;
				}
				//else if (type == ModContent.TileType<YellowIce>() || type == TileID.HallowedIce || type == TileID.FleshIce || type == TileID.CorruptIce || type == TileID.IceBlock)
				//{
				//    tile.type = (ushort)ModContent.TileType<GreenIce>();
				//}
				//if (TileID.Sets.Conversion.Grass[type] || type == 0)
				//{
				//    WorldGen.SquareTileFrame(x, y);
				//}
			}
		}
		// contagion from things
		if (convert == 2)
		{
			if (Main.tile[x, y] != null)
			{
				if (WallID.Sets.Conversion.Grass[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionGrassWall>();
				}
				else if (WallID.Sets.Conversion.Stone[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ChunkstoneWall>();
				}
				else if (WallID.Sets.Conversion.NewWall1[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionLumpWallUnsafe>();
				}
				else if (WallID.Sets.Conversion.NewWall2[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionMouldWallUnsafe>();
				}
				else if (WallID.Sets.Conversion.NewWall3[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionCystWallUnsafe>();
				}
				else if (WallID.Sets.Conversion.NewWall4[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.ContagionBoilWallUnsafe>();
				}
				else if (WallID.Sets.Conversion.HardenedSand[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.HardenedSnotsandWallUnsafe>();
				}
				else if (WallID.Sets.Conversion.Sandstone[wall])
				{
					Main.tile[x, y].WallType = (ushort)ModContent.WallType<Walls.SnotsandstoneWallUnsafe>();
				}
			}
			if (Main.tile[x, y] != null)
			{
				Tile tileUp = Main.tile[x, y - 1];
				int typeUp = tileUp.TileType;
				Tile tileDown = Main.tile[x, y + 1];
				int typeDown = tileDown.TileType;
				if (TileID.Sets.Conversion.Grass[type] && type != ModContent.TileType<Ickgrass>())
				{
					tile.TileType = (ushort)ModContent.TileType<Ickgrass>();
					if (typeUp == TileID.Plants || typeUp == TileID.Plants2 || typeUp == TileID.CorruptPlants || typeUp == TileID.CrimsonPlants || typeUp == TileID.HallowedPlants || typeUp == TileID.HallowedPlants2 || typeUp == TileID.JunglePlants || typeUp == TileID.JunglePlants2)
					{
						tileUp.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
					}
					if (TileID.Sets.IsVine[typeDown] && typeDown != ModContent.TileType<ContagionVines>())
					{
						for (int downVar = 1; TileID.Sets.IsVine[typeDown] && !Main.tileSolid[typeDown]; downVar++)
						{
							Tile tileDownVar = Main.tile[x, y + downVar];
							int typeDownVar = tileDownVar.TileType;
							if (TileID.Sets.IsVine[typeDownVar] && typeDownVar != ModContent.TileType<ContagionVines>())
							{
								tileDownVar.TileType = (ushort)ModContent.TileType<ContagionVines>();
							}
							else
							{
								break;
							}
						}
					}
				}
				if (TileID.Sets.Conversion.JungleGrass[type] && type != ModContent.TileType<ContagionJungleGrass>())
				{
					tile.TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
					if (typeUp == TileID.Plants || typeUp == TileID.Plants2 || typeUp == TileID.CorruptPlants || typeUp == TileID.CrimsonPlants || typeUp == TileID.HallowedPlants || typeUp == TileID.HallowedPlants2 || typeUp == TileID.JunglePlants || typeUp == TileID.JunglePlants2)
					{
						tileUp.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
					}
					if (TileID.Sets.IsVine[typeDown] && typeDown != ModContent.TileType<ContagionVines>())
					{
						for (int downVar = 1; TileID.Sets.IsVine[typeDown] && !Main.tileSolid[typeDown]; downVar++)
						{
							Tile tileDownVar = Main.tile[x, y + downVar];
							int typeDownVar = tileDownVar.TileType;
							if (TileID.Sets.IsVine[typeDownVar] && typeDownVar != ModContent.TileType<ContagionVines>())
							{
								tileDownVar.TileType = (ushort)ModContent.TileType<ContagionVines>();
							}
							else
							{
								break;
							}
						}
					}
				}
				else if (TileID.Sets.Conversion.Ice[type] && type != ModContent.TileType<YellowIce>())
				{
					tile.TileType = (ushort)ModContent.TileType<YellowIce>();
				}
				else if (TileID.Sets.Conversion.Sand[type] && type != ModContent.TileType<Snotsand>())
				{
					tile.TileType = (ushort)ModContent.TileType<Snotsand>();
				}
				else if (TileID.Sets.Conversion.Stone[type] && type != ModContent.TileType<Chunkstone>())
				{
					tile.TileType = (ushort)ModContent.TileType<Chunkstone>();
				}
				else if (TileID.Sets.Conversion.Sandstone[type] && type != ModContent.TileType<Snotsandstone>())
				{
					tile.TileType = (ushort)ModContent.TileType<Snotsandstone>();
				}
				else if (TileID.Sets.Conversion.HardenedSand[type] && type != ModContent.TileType<HardenedSnotsand>())
				{
					tile.TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
				}
				else if (type == TileID.Plants || type == TileID.Plants2 || type == TileID.CorruptPlants || type == TileID.CrimsonPlants || type == TileID.HallowedPlants || type == TileID.HallowedPlants2 || type == TileID.JunglePlants || type == TileID.JunglePlants2)
				{
					if ((tile.TileFrameX > 144 && tile.TileFrameX < 162 || tile.TileFrameX % 414 > 144 && tile.TileFrameX % 414 < 162) && type != TileID.CrimsonPlants)
					{
						tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
						tile.TileFrameX %= 414 - 18;
					}
					else if (tile.TileFrameX % 414 == 144 && type == TileID.CrimsonPlants)
					{
						tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
						tile.TileFrameX %= 414 - 18;
					}
					else if (tile.TileFrameX % 414 == 270 && type == TileID.CrimsonPlants)
					{
						tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
						tile.TileFrameX = 144;
					}
					else
					{
						tile.TileType = (ushort)ModContent.TileType<ContagionShortGrass>();
						tile.TileFrameX %= 414;
					}
					if (TileID.Sets.Conversion.Grass[typeDown] && typeDown != ModContent.TileType<Ickgrass>())
					{
						tileDown.TileType = (ushort)ModContent.TileType<Ickgrass>();
					}
					if (TileID.Sets.Conversion.JungleGrass[typeDown] && typeDown != ModContent.TileType<Ickgrass>())
					{
						tileDown.TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
					}
				}
				//if (TileID.Sets.Conversion.Grass[type] || type == 0)
				//{
				//    WorldGen.SquareTileFrame(x, y);
				//}
			}
		}
		//if (tileframe)
		//{
		//    if (Main.netMode == NetmodeID.SinglePlayer)
		//    {
		//        WorldGen.SquareTileFrame(x, y);
		//    }
		//    else if (Main.netMode == NetmodeID.Server)
		//    {
		//        NetMessage.SendTileSquare(-1, x, y, 1);
		//    }
		//}
	}

	public static void CheckContagionCatTail(int x, int j)
	{
		if (Main.tile[x, j] == null)
		{
			return;
		}
		int num = j;
		bool flag = false;
		int num2 = num;
		while ((!Main.tile[x, num2].HasTile || !Main.tileSolid[Main.tile[x, num2].TileType] || Main.tileSolidTop[Main.tile[x, num2].TileType]) && num2 < Main.maxTilesY - 50)
		{
			if (Main.tile[x, num2].HasTile && Main.tile[x, num2].TileType != ModContent.TileType<ContagionCattails>())
			{
				flag = true;
			}
			if (!Main.tile[x, num2].HasTile)
			{
				break;
			}
			num2++;
			if (Main.tile[x, num2] == null)
			{
				return;
			}
		}
		num = num2 - 1;
		if (Main.tile[x, num] == null)
		{
			return;
		}
		while (Main.tile[x, num] != null && Main.tile[x, num].LiquidAmount > 0 && num > 50)
		{
			if ((Main.tile[x, num].HasTile && Main.tile[x, num].TileType != ModContent.TileType<ContagionCattails>()) || Main.tile[x, num].LiquidType != 0)
			{
				flag = true;
			}
			num--;
			if (Main.tile[x, num] == null)
			{
				return;
			}
		}
		num++;
		if (Main.tile[x, num] == null)
		{
			return;
		}
		int num3 = num;
		int num4 = 8;//catTailDistance;
		if (num2 - num3 > num4)
		{
			flag = true;
		}
		int type = Main.tile[x, num2].TileType;
		int num5 = -1;
		if (type == ModContent.TileType<Ickgrass>())
		{
			num5 = ModContent.TileType<ContagionCattails>();
		}
		switch (type)
		{
			case 2:
			case 477:
				num5 = 0;
				break;
			case 53:
				num5 = 18;
				break;
			case 199:
			case 234:
			case 662:
				num5 = 54;
				break;
			case 23:
			case 112:
			case 661:
				num5 = 72;
				break;
			case 70:
				num5 = 90;
				break;
		}
		if (!Main.tile[x, num2].HasTile)
		{
			flag = true;
		}
		if (num5 < 0)
		{
			flag = true;
		}
		num = num2 - 1;
		if (Main.tile[x, num] != null && !Main.tile[x, num].HasTile)
		{
			for (int num6 = num; num6 >= num3; num6--)
			{
				if (Main.tile[x, num6] == null)
				{
					return;
				}
				if (Main.tile[x, num6].HasTile && Main.tile[x, num6].TileType == ModContent.TileType<ContagionCattails>())
				{
					num = num6;
					break;
				}
			}
		}
		while (Main.tile[x, num] != null && Main.tile[x, num].HasTile && Main.tile[x, num].TileType == ModContent.TileType<ContagionCattails>())
		{
			num--;
		}
		num++;
		if (Main.tile[x, num2 - 1] != null && Main.tile[x, num2 - 1].LiquidAmount < 127 && WorldGen.genRand.NextBool(4))
		{
			flag = true;
		}
		if (Main.tile[x, num] != null && Main.tile[x, num].TileFrameX >= 180 && Main.tile[x, num].LiquidAmount > 127 && WorldGen.genRand.NextBool(4))
		{
			flag = true;
		}
		if (Main.tile[x, num] != null && Main.tile[x, num2 - 1] != null && Main.tile[x, num].TileFrameX > 18)
		{
			if (Main.tile[x, num2 - 1].TileFrameX < 36 || Main.tile[x, num2 - 1].TileFrameX > 72)
			{
				flag = true;
			}
			else if (Main.tile[x, num].TileFrameX < 90)
			{
				flag = true;
			}
			else if (Main.tile[x, num].TileFrameX >= 108 && Main.tile[x, num].TileFrameX <= 162)
			{
				Main.tile[x, num].TileFrameX = 90;
			}
		}
		if (num2 > num + 4 && Main.tile[x, num + 4] != null && Main.tile[x, num + 3] != null && Main.tile[x, num + 4].LiquidAmount == 0 && Main.tile[x, num + 3].TileType == ModContent.TileType<ContagionCattails>())
		{
			flag = true;
		}
		if (flag)
		{
			int num7 = num3;
			if (num < num3)
			{
				num7 = num;
			}
			num7 -= 4;
			for (int i = num7; i <= num2; i++)
			{
				if (Main.tile[x, i] != null && Main.tile[x, i].HasTile && (Main.tile[x, i].TileType == 519 || Main.tile[x, i].TileType == ModContent.TileType<ContagionCattails>()))
				{
					WorldGen.KillTile(x, i);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, i);
					}
					WorldGen.SquareTileFrame(x, i);
				}
			}
		}
		else
		{
			for (int k = num; k < num2; k++)
			{
				if (Main.tile[x, k] != null && Main.tile[x, k].HasTile && (Main.tile[x, k].TileType == 519 || Main.tile[x, k].TileType == ModContent.TileType<ContagionCattails>()))
				{
					if (num5 == ModContent.TileType<ContagionCattails>())
					{
						Main.tile[x, k].TileType = (ushort)ModContent.TileType<ContagionCattails>();
						Main.tile[x, k].TileFrameY = 0;
					}
					else
					{
						Main.tile[x, k].TileType = 519;
						Main.tile[x, k].TileFrameY = (short)num5;
					}
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendTileSquare(-1, x, num);
					}
				}
			}
		}
	}

	public static void GrowContagionCatTail(int x, int j)
	{
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			return;
		}
		int num = j;
		Tile tile;
		while (true)
		{
			tile = Main.tile[x, num];
			if (tile.LiquidAmount <= 0 || num <= 50)
			{
				break;
			}
			num--;
		}
		num++;
		int i = num;
		while (true)
		{
			tile = Main.tile[x, i];
			if (tile.HasTile)
			{
				bool[] tileSolid = Main.tileSolid;
				tile = Main.tile[x, i];
				if (tileSolid[tile.TileType])
				{
					bool[] tileSolidTop = Main.tileSolidTop;
					tile = Main.tile[x, i];
					if (!tileSolidTop[tile.TileType])
					{
						break;
					}
				}
			}
			if (i >= Main.maxTilesY - 50)
			{
				break;
			}
			i++;
		}
		num = i - 1;
		while (true)
		{
			tile = Main.tile[x, num];
			if (!tile.HasTile)
			{
				break;
			}
			tile = Main.tile[x, num];
			if (tile.TileType != ModContent.TileType<ContagionCattails>())
			{
				break;
			}
			num--;
		}
		num++;
		tile = Main.tile[x, num];
		if (tile.TileFrameX == 90)
		{
			tile = Main.tile[x, num - 1];
			if (tile.HasTile)
			{
				bool[] tileCut = Main.tileCut;
				tile = Main.tile[x, num - 1];
				if (tileCut[tile.TileType])
				{
					WorldGen.KillTile(x, num - 1);
					if (Main.netMode == NetmodeID.Server)
					{
						NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, num - 1);
					}
				}
			}
		}
		tile = Main.tile[x, num - 1];
		if (tile.HasTile)
		{
			return;
		}
		tile = Main.tile[x, num];
		if (tile.TileFrameX == 0)
		{
			tile = Main.tile[x, num];
			tile.TileFrameX = 18;
			WorldGen.SquareTileFrame(x, num);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, x, num);
			}
		}
		else
		{
			tile = Main.tile[x, num];
			if (tile.TileFrameX == 18)
			{
				tile = Main.tile[x, num];
				tile.TileFrameX = (short)(18 * WorldGen.genRand.Next(2, 5));
				tile = Main.tile[x, num - 1];
				tile.HasTile = true;
				tile = Main.tile[x, num - 1];
				tile.TileType = (ushort)ModContent.TileType<ContagionCattails>();
				tile = Main.tile[x, num - 1];
				tile.TileFrameX = 90;
				tile = Main.tile[x, num - 1];
				ref short frameY = ref tile.TileFrameY;
				tile = Main.tile[x, num];
				frameY = tile.TileFrameY;
				tile = Main.tile[x, num - 1];
				tile.IsHalfBlock = false;
				tile = Main.tile[x, num - 1];
				tile.Slope = 0;
				tile = Main.tile[x, num - 1];
				tile.CopyPaintAndCoating(Main.tile[x, num]);
				WorldGen.SquareTileFrame(x, num);
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendTileSquare(-1, x, num);
				}
			}
			else
			{
				tile = Main.tile[x, num];
				if (tile.TileFrameX == 90)
				{
					tile = Main.tile[x, num - 1];
					if (tile.LiquidAmount == 0)
					{
						tile = Main.tile[x, num - 2];
						if (!tile.HasTile)
						{
							tile = Main.tile[x, num];
							if (tile.LiquidAmount <= 0)
							{
								tile = Main.tile[x, num + 1];
								if (tile.LiquidAmount <= 0)
								{
									tile = Main.tile[x, num + 2];
									if (tile.LiquidAmount <= 0)
									{
										goto IL_0482;
									}
								}
							}
							if (WorldGen.genRand.NextBool(3))
							{
								tile = Main.tile[x, num];
								tile.TileFrameX = 108;
								tile = Main.tile[x, num - 1];
								tile.HasTile = true;
								tile = Main.tile[x, num - 1];
								tile.TileType = (ushort)ModContent.TileType<ContagionCattails>();
								tile = Main.tile[x, num - 1];
								tile.TileFrameX = 90;
								tile = Main.tile[x, num - 1];
								ref short frameY2 = ref tile.TileFrameY;
								tile = Main.tile[x, num];
								frameY2 = tile.TileFrameY;
								tile = Main.tile[x, num - 1];
								tile.IsHalfBlock = false;
								tile = Main.tile[x, num - 1];
								tile.Slope = 0;
								tile = Main.tile[x, num - 1];
								tile.CopyPaintAndCoating(Main.tile[x, num]);
								WorldGen.SquareTileFrame(x, num);
								goto IL_0670;
							}
						}
						goto IL_0482;
					}
					tile = Main.tile[x, num];
					tile.TileFrameX = 108;
					tile = Main.tile[x, num - 1];
					tile.HasTile = true;
					tile = Main.tile[x, num - 1];
					tile.TileType = (ushort)ModContent.TileType<ContagionCattails>();
					tile = Main.tile[x, num - 1];
					tile.TileFrameX = 90;
					tile = Main.tile[x, num - 1];
					ref short frameY3 = ref tile.TileFrameY;
					tile = Main.tile[x, num];
					frameY3 = tile.TileFrameY;
					tile = Main.tile[x, num - 1];
					tile.IsHalfBlock = false;
					tile = Main.tile[x, num - 1];
					tile.Slope = 0;
					tile = Main.tile[x, num - 1];
					tile.CopyPaintAndCoating(Main.tile[x, num]);
					WorldGen.SquareTileFrame(x, num);
				}
			}
		}
		goto IL_0670;
	IL_0670:
		WorldGen.SquareTileFrame(x, num - 1, resetFrame: false);
		if (Main.netMode == NetmodeID.Server)
		{
			NetMessage.SendTileSquare(-1, x, num - 1, 1, 2);
		}
		return;
	IL_0482:
		int num2 = WorldGen.genRand.Next(3);
		tile = Main.tile[x, num];
		tile.TileFrameX = (short)(126 + num2 * 18);
		tile = Main.tile[x, num - 1];
		tile.HasTile = true;
		tile = Main.tile[x, num - 1];
		tile.TileType = (ushort)ModContent.TileType<ContagionCattails>();
		tile = Main.tile[x, num - 1];
		tile.TileFrameX = (short)(180 + num2 * 18);
		tile = Main.tile[x, num - 1];
		ref short frameY4 = ref tile.TileFrameY;
		tile = Main.tile[x, num];
		frameY4 = tile.TileFrameY;
		tile = Main.tile[x, num - 1];
		tile.IsHalfBlock = false;
		tile = Main.tile[x, num - 1];
		tile.Slope = 0;
		tile = Main.tile[x, num - 1];
		tile.CopyPaintAndCoating(Main.tile[x, num]);
		WorldGen.SquareTileFrame(x, num);
		goto IL_0670;
	}
	public static bool AddManaCrystal(int i, int j)
	{
		int k = j;
		while (k < Main.maxTilesY)
		{
			if (Main.tile[i, k].HasTile && Main.tileSolid[Main.tile[i, k].TileType])
			{
				int num = k - 1;
				if (Main.tile[i, num - 1].LiquidType == LiquidID.Lava || Main.tile[i - 1, num - 1].LiquidType == LiquidID.Lava)
				{
					return false;
				}
				if (!WorldGen.EmptyTileCheck(i - 1, i, num - 1, num, -1))
				{
					return false;
				}
				if (!Main.tileSolid[Main.tile[i, num].TileType] || !Main.tileSolid[Main.tile[i - 1, num].TileType])
				{
					return false;
				}
				if (!Main.wallDungeon[Main.tile[i, num].WallType] || !Main.tileSolid[Main.tile[i, num].TileType])
				{
					return false;
				}
				if (num < Main.rockLayer)
				{
					return false;
				}
				Main.tile[i - 1, num - 1].Active(true);
				Main.tile[i - 1, num - 1].TileType = TileID.ManaCrystal;
				Main.tile[i - 1, num - 1].TileFrameX = 0;
				Main.tile[i - 1, num - 1].TileFrameY = 0;
				Main.tile[i, num - 1].Active(true);
				Main.tile[i, num - 1].TileType = TileID.ManaCrystal;
				Main.tile[i, num - 1].TileFrameX = 18;
				Main.tile[i, num - 1].TileFrameY = 0;
				Main.tile[i - 1, num].Active(true);
				Main.tile[i - 1, num].TileType = TileID.ManaCrystal;
				Main.tile[i - 1, num].TileFrameX = 0;
				Main.tile[i - 1, num].TileFrameY = 18;
				Main.tile[i, num].Active(true);
				Main.tile[i, num].TileType = TileID.ManaCrystal;
				Main.tile[i, num].TileFrameX = 18;
				Main.tile[i, num].TileFrameY = 18;
				return true;
			}
			else
			{
				k++;
			}
		}
		return false;
	}

	public void DamageMobWithNPCProjectile(NPC sourceNPC, NPC npcToHit)
	{
		//if (sourceNPC.getRect().Intersects(npcToHit.getRect()))
		if (Terraria.Collision.CheckAABBvAABBCollision(sourceNPC.Center, new Vector2(sourceNPC.width, sourceNPC.height), npcToHit.Center, new Vector2(npcToHit.width, npcToHit.height)))
		{
			sourceNPC.GetGlobalNPC<AvalonGlobalNPCInstance>().DamageMobsTimer++;
			if (sourceNPC.GetGlobalNPC<AvalonGlobalNPCInstance>().DamageMobsTimer % 5 == 0)
			{
				NPC.HitInfo dmg = new NPC.HitInfo();
				dmg.Damage = sourceNPC.damage;
				dmg.Knockback = 1f;
				dmg.HitDirection = 1;
				npcToHit.StrikeNPC(dmg);
				sourceNPC.GetGlobalNPC<AvalonGlobalNPCInstance>().DamageMobsTimer = 0;
			}
		}
	}

	public void MakeOblivionOre(NPC npc1, NPC npc2, string text, int radius)
	{
		if (Terraria.Collision.CheckAABBvAABBCollision(npc1.Center, new Vector2(npc1.width, npc1.height), npc2.Center, new Vector2(npc2.width, npc2.height)))
		{
			//WorldGeneration.Utils.MakeOblivionOreCircle((int)(npc1.position.X / 16f), (int)(npc1.position.Y / 16f), radius, ModContent.TileType<Tiles.Ores.CaesiumOre>()); // oblivion ore
			npc1.life = 0;
			npc1.NPCLoot();
			npc1.active = false;
			SoundEngine.PlaySound(npc1.DeathSound, npc1.position);
			npc2.life = 0;
			npc2.NPCLoot();
			npc2.active = false;
			SoundEngine.PlaySound(npc2.DeathSound, npc2.position);
			var color = new Color(135, 78, 0);
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(text, color);
			}
			else if (Main.netMode == NetmodeID.Server)
			{
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color);
			}
		}
	}
	public void SpreadXanthophyte(int x, int y)
	{
		if (Main.tile[x, y].IsActuated)
		{
			return;
		}

		int type = Main.tile[x, y].TileType;

		if (y > (Main.worldSurface + Main.rockLayer) / 2.0)
		{
			if ((type == ModContent.TileType<SavannaGrass>()/* || type == ModContent.TileType<Tiles.BrownIce>()*/) && WorldGen.genRand.NextBool(325))
			{
				int num6 = x + WorldGen.genRand.Next(-10, 11);
				int num7 = y + WorldGen.genRand.Next(-10, 11);
				if (Main.tile[num6, num7].HasTile && (Main.tile[num6, num7].TileType == ModContent.TileType<Loam>()/* || Main.tile[num6, num7].type == ModContent.TileType<Tiles.BrownIce>()*/) && (!Main.tile[num6, num7 - 1].HasTile || (Main.tile[num6, num7 - 1].TileType != 5 && Main.tile[num6, num7 - 1].TileType != 236 && Main.tile[num6, num7 - 1].TileType != 238)) && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<XanthophyteOre>()))
				{
					Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<XanthophyteOre>();
					WorldGen.SquareTileFrame(num6, num7, true);
				}
			}
			if (type == (ushort)ModContent.TileType<XanthophyteOre>() && !WorldGen.genRand.NextBool(3))
			{
				int num8 = x;
				int num9 = y;
				int num10 = WorldGen.genRand.Next(4);
				if (num10 == 0)
				{
					num8++;
				}
				if (num10 == 1)
				{
					num8--;
				}
				if (num10 == 2)
				{
					num9++;
				}
				if (num10 == 3)
				{
					num9--;
				}
				if (Main.tile[num8, num9].HasTile && (Main.tile[num8, num9].TileType == ModContent.TileType<Loam>() || Main.tile[num8, num9].TileType == ModContent.TileType<SavannaGrass>()) /*|| Main.tile[num8, num9].type == ModContent.TileType<Tiles.BrownIce>())*/ && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<XanthophyteOre>()))
				{
					Main.tile[num8, num9].TileType = (ushort)ModContent.TileType<XanthophyteOre>();
					WorldGen.SquareTileFrame(num8, num9, true);
				}
			}
		}
	}
	public void SpreadShroomite(int x, int y)
	{
		if (Main.tile[x, y].IsActuated)
		{
			return;
		}

		int type = Main.tile[x, y].TileType;

		if (y > (Main.worldSurface + Main.rockLayer) / 2.0)
		{
			if ((type == TileID.MushroomGrass) && WorldGen.genRand.NextBool(150))
			{
				int num6 = x + WorldGen.genRand.Next(-10, 11);
				int num7 = y + WorldGen.genRand.Next(-10, 11);
				if (Main.tile[num6, num7].HasTile && (Main.tile[num6, num7].TileType == TileID.Mud) && (!Main.tile[num6, num7 - 1].HasTile || (Main.tile[num6, num7 - 1].TileType != 5 && Main.tile[num6, num7 - 1].TileType != 236 && Main.tile[num6, num7 - 1].TileType != 238)) && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<ShroomiteOre>()))
				{
					Main.tile[num6, num7].TileType = (ushort)ModContent.TileType<ShroomiteOre>();
					WorldGen.SquareTileFrame(num6, num7, true);
				}
			}
			if (type == (ushort)ModContent.TileType<ShroomiteOre>() && !WorldGen.genRand.NextBool(3))
			{
				int num8 = x;
				int num9 = y;
				int num10 = WorldGen.genRand.Next(4);
				if (num10 == 0)
				{
					num8++;
				}
				if (num10 == 1)
				{
					num8--;
				}
				if (num10 == 2)
				{
					num9++;
				}
				if (num10 == 3)
				{
					num9--;
				}
				if (Main.tile[num8, num9].HasTile && (Main.tile[num8, num9].TileType == TileID.Mud || Main.tile[num8, num9].TileType == TileID.MushroomGrass) && GrowingOreSpread.GrowingOre(x, y, ModContent.TileType<ShroomiteOre>()))
				{
					Main.tile[num8, num9].TileType = (ushort)ModContent.TileType<ShroomiteOre>();
					WorldGen.SquareTileFrame(num8, num9, true);
				}
			}
		}
	}

	#region Contagion Collection
	public static List<int> ContagionCountCollection = new List<int> {
				ModContent.TileType<Chunkstone>(),
				ModContent.TileType<Ickgrass>(),
				ModContent.TileType<ContagionJungleGrass>(),
				ModContent.TileType<Snotsand>(),
				ModContent.TileType<YellowIce>(),
				ModContent.TileType<Snotsandstone>(),
				ModContent.TileType<HardenedSnotsand>()
			};
	#endregion
}
