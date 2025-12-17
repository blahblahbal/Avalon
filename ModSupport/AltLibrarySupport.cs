using AltLibrary;
using AltLibrary.Common.AltBiomes;
using AltLibrary.Common.AltOres;
using AltLibrary.Common.Conditions;
using AltLibrary.Common.Hooks;
using AltLibrary.Common.Systems;
using AltLibrary.Core.Generation;
using Avalon.Common;
using Avalon.Items.Accessories.PreHardmode;
using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Herbs;
using Avalon.Items.Material.Ores;
using Avalon.Items.Other;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Melee.Hardmode.VirulentScythe;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Weapons.Melee.PreHardmode.Snotsabre;
using Avalon.NPCs.Critters;
using Avalon.NPCs.Hardmode;
using Avalon.Tiles.Contagion.BacciliteBrick;
using Avalon.Tiles.Contagion.Chunkstone;
using Avalon.Tiles.Contagion.ContagionChest;
using Avalon.Tiles.Contagion.ContagionGrasses;
using Avalon.Tiles.Contagion.HardenedSnotsand;
using Avalon.Tiles.Contagion.IckyAltar;
using Avalon.Tiles.Contagion.Snotsand;
using Avalon.Tiles.Contagion.Snotsandstone;
using Avalon.Tiles.Contagion.YellowIce;
using Avalon.Walls.Contagion.ContagionBoilWall;
using Avalon.Walls.Contagion.ContagionCystWall;
using Avalon.Walls.Contagion.ContagionGrassWall;
using Avalon.Walls.Contagion.ContagionLumpWall;
using Avalon.Walls.Contagion.ContagionMouldWall;
using Avalon.Walls.Contagion.HardenedSnotsandWall;
using Avalon.Walls.Contagion.SnotsandstoneWall;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Savanna = Avalon.WorldGeneration.Passes.Savanna;

namespace Avalon.ModSupport;

public class AltLibrarySupport : ModSystem
{
	static bool? enabled = null;
	public static bool Enabled => enabled ??= ModLoader.HasMod(nameof(AltLibrary));
	public override void Unload()
	{
		enabled = null;
	}
	public override void PostSetupContent()
	{
		[JITWhenModsEnabled(nameof(AltLibrary))]
		static void Do()
		{
			ModContent.GetInstance<CrimsonAltBiome>().ArrowType = ModContent.ItemType<Items.Ammo.BloodyArrow>();
		}
		if (!Enabled) return;
		Do();
	}
	public override void PostUpdateTime()
	{
		UpdateBiomeFields();
	}
	public static void UpdateBiomeFields()
	{
		[JITWhenModsEnabled(nameof(AltLibrary))]
		static void Do()
		{
			AltBiome evil = WorldBiomeManager.GetWorldEvil(true, true);
			ref WorldEvil worldEvil = ref ModContent.GetInstance<AvalonWorld>().WorldEvil;
			if (evil is ContagionAltBiome) worldEvil = WorldEvil.Contagion;
			else if (evil is CrimsonAltBiome) worldEvil = WorldEvil.Crimson;
			else if (evil is CorruptionAltBiome) worldEvil = WorldEvil.Corruption;
			else worldEvil = WorldEvil.External;

			AltBiome jungle = WorldBiomeManager.GetWorldJungle(true);
			ref WorldJungle worldJungle = ref ModContent.GetInstance<AvalonWorld>().WorldJungle;
			if (jungle is SavannaAltBiome) worldJungle = WorldJungle.Savanna;
			else if (jungle is JungleAltBiome) worldJungle = WorldJungle.Jungle;
			else worldJungle = WorldJungle.External;

			// if y'all add a Rhodium slot for other mods to use, put code to import that from AltLib too
		}
		if (!Enabled) return;
		Do();
	}
	[JITWhenModsEnabled(nameof(AltLibrary))]
	public static void ImportSaveData()
	{
		AvalonWorld avalonWorld = ModContent.GetInstance<AvalonWorld>();
		switch (avalonWorld.WorldEvil)
		{
			case WorldEvil.Corruption:
				WorldBiomeManager.WorldEvilBiome = ModContent.GetInstance<CorruptionAltBiome>();
				break;
			case WorldEvil.Crimson:
				WorldBiomeManager.WorldEvilBiome = ModContent.GetInstance<CrimsonAltBiome>();
				break;
			case WorldEvil.Contagion:
				WorldBiomeManager.WorldEvilBiome = ModContent.GetInstance<ContagionAltBiome>();
				break;
		}
		switch (avalonWorld.WorldJungle)
		{
			case WorldJungle.Jungle:
				WorldBiomeManager.WorldJungle = ModContent.GetInstance<JungleAltBiome>();
				break;
			case WorldJungle.Savanna:
				WorldBiomeManager.WorldJungle = ModContent.GetInstance<SavannaAltBiome>();
				break;
		}
		// if y'all add a Rhodium slot for other mods to use, put code to import that to AltLib too
	}
	public static bool PreWorldGen(AvalonWorld world)
	{
		[JITWhenModsEnabled(nameof(AltLibrary))]
		static void Do(AvalonWorld world)
		{
			if (WorldBiomeManager.GetWorldEvil(true) == ModContent.GetInstance<ContagionAltBiome>())
			{
				AvalonWorld.contagionBG = WorldGen.genRand.Next(AvalonWorld.contagionBGCount);
			}
		}
		if (!Enabled) return false;
		Do(world);
		return true;
	}
	public static void ReplaceShopConditions(ref Condition corruption, ref Condition crimson, ref Condition contagion)
	{
		[JITWhenModsEnabled(nameof(AltLibrary))]
		static void Do(ref Condition corruption, ref Condition crimson, ref Condition contagion)
		{
			corruption = ShopConditions.GetWorldEvilCondition<CorruptionAltBiome>();
			crimson = ShopConditions.GetWorldEvilCondition<CrimsonAltBiome>();
			contagion = ShopConditions.GetWorldEvilCondition<ContagionAltBiome>();
		}
		if (!Enabled) return;
		Do(ref corruption, ref crimson, ref contagion);
	}
	public static bool EvilBiomeArrow(ref int itemType)
	{
		if (!Enabled) return false;
		Do(ref itemType);
		[JITWhenModsEnabled(nameof(AltLibrary))]
		static void Do(ref int itemType)
		{
			itemType = WorldBiomeManager.GetWorldEvil(true, true).ArrowType ?? ItemID.UnholyArrow;
		}
		return true;
	}
	public static void TryAddStalactite(int stalactite, params int[] anchors)
	{
		if (Enabled) AddStalactite(stalactite, anchors);
	}
	[JITWhenModsEnabled(nameof(AltLibrary))]
	public static void AddStalactite(int stalactite, params int[] anchors) => AltStalactites.AddStalactite(stalactite, anchors);
	public static void TryAddVine(int stalactite, params int[] anchors)
	{
		if (Enabled) AddVine(stalactite, anchors);
	}
	[JITWhenModsEnabled(nameof(AltLibrary))]
	public static void AddVine(int stalactite, params int[] anchors) => AltVines.AddVine(stalactite, anchors);
}
[ExtendsFromMod(nameof(AltLibrary))]
public class ContagionAltBiome : AltBiome
{
	public override string WorldIcon => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/IconOverlayContagion";
	public override string OuterTexture => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/LoadingOuterContagion";
	public override string IconSmall => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/IconContagion";
	public override Color OuterColor => new(175, 148, 199);
	public override IShoppingBiome Biome => ModContent.GetInstance<Biomes.Contagion>();
	public override Color NameColor => Color.Green;
	public override void SetStaticDefaults()
	{
		BiomeType = BiomeType.Evil;

		AddTileConversion(ModContent.TileType<Ickgrass>(), TileID.Grass);
		AddTileConversion(ModContent.TileType<ContagionJungleGrass>(), TileID.JungleGrass);
		AddTileConversion(ModContent.TileType<Chunkstone>(), TileID.Stone);
		AddTileConversion(ModContent.TileType<Snotsand>(), TileID.Sand);
		AddTileConversion(ModContent.TileType<Snotsandstone>(), TileID.Sandstone);
		AddTileConversion(ModContent.TileType<HardenedSnotsand>(), TileID.HardenedSand);
		AddTileConversion(ModContent.TileType<YellowIce>(), TileID.IceBlock);

		GERunnerConversion.Add(TileID.Silt, ModContent.TileType<Snotsand>());

		/* missing flesh/lesion counterpart
		BiomeFlesh = ;
		BiomeFleshWall = ;

		FleshDoorTile = ;
		FleshChairTile = ;
		FleshTableTile = ;
		FleshChestTile = ;
		FleshDoorTileStyle = 7;
		FleshChairTileStyle = 7;
		FleshTableTileStyle = 7;
		FleshChestTileStyle = 7;
		*/

		FountainTile = ModContent.TileType<Tiles.Furniture.WaterFountains>();
		FountainTileStyle = 0;

		SeedType = ModContent.ItemType<ContagionSeeds>();
		BiomeOre = ModContent.TileType<Tiles.Ores.BacciliteOre>();
		BiomeOreItem = ModContent.ItemType<BacciliteOre>();
		BiomeOreBrick = ModContent.TileType<BacciliteBrickTile>();
		ArrowType = ModContent.ItemType<Items.Ammo.IckyArrow>();
		AltarTile = ModContent.TileType<IckyAltar>();

		BiomeChestItem = ModContent.ItemType<VirulentScythe>();
		BiomeChestTile = ModContent.TileType<ContagionChestTile>();
		BiomeChestTileStyle = 1;
		BiomeKeyItem = ModContent.ItemType<ContagionKey>();

		MimicType = ModContent.NPCType<ContagionMimic>();

		BloodBunny = ModContent.NPCType<ContaminatedBunny>();
		BloodPenguin = ModContent.NPCType<ContaminatedPenguin>();
		BloodGoldfish = ModContent.NPCType<ContaminatedGoldfish>();

		AddWallConversions<ContagionLumpWallUnsafe>(
			WallID.RocksUnsafe3
		);
		AddWallConversions<ContagionMouldWallUnsafe>(
			WallID.Cave3Unsafe,
			WallID.RocksUnsafe2
		);
		AddWallConversions<ContagionCystWallUnsafe>(
			WallID.Cave4Unsafe,
			WallID.Cave5Unsafe,
			WallID.RocksUnsafe1
		);
		AddWallConversions<ContagionBoilWallUnsafe>(
			WallID.Cave8Unsafe,
			WallID.RocksUnsafe4
		);
		AddWallConversions<SnotsandstoneWallUnsafe>(
			WallID.Sandstone,
			WallID.CorruptSandstone,
			WallID.CrimsonSandstone,
			WallID.HallowSandstone
		);
		AddWallConversions<HardenedSnotsandWallUnsafe>(
			WallID.HardenedSand,
			WallID.CorruptHardenedSand,
			WallID.CrimsonHardenedSand,
			WallID.HallowHardenedSand
		);
		AddWallConversions<ContagionGrassWallUnsafe>(
			WallID.GrassUnsafe,
			WallID.Grass,
			WallID.FlowerUnsafe,
			WallID.Flower
		);

		EvilBiomeGenerationPass = new ContagionGenerationPass();
	}
	public override AltMaterialContext MaterialContext
	{
		get
		{
			AltMaterialContext context = new();
			context.SetEvilHerb(ModContent.ItemType<Barfbush>());
			context.SetEvilBar(ModContent.ItemType<BacciliteBar>());
			context.SetEvilOre(ModContent.ItemType<BacciliteOre>());
			context.SetVileInnard(ModContent.ItemType<YuckyBit>());
			context.SetVileComponent(ModContent.ItemType<Pathogen>());
			context.SetEvilBossDrop(ModContent.ItemType<Booger>());
			context.SetEvilSword(ModContent.ItemType<Snotsabre>());
			return context;
		}
	}
	[ExtendsFromMod(nameof(AltLibrary))]
	public class ContagionGenerationPass : EvilBiomeGenerationPass
	{
		public override string ProgressMessage => Language.GetTextValue("Mods.Avalon.World.Generation.Contagion.Message");
		public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound)
		{
			WorldBiomeGeneration.ChangeRange.ResetRange();

			WorldGeneration.Passes.Contagion.ContagionRunner(evilBiomePosition, (int)GenVars.worldSurfaceLow - 10 + (Main.maxTilesY / 8));
			for (int i = evilBiomePositionWestBound; i < evilBiomePositionEastBound; i++)
			{
				int j = (int)GenVars.worldSurfaceLow;
				while (j < Main.worldSurface - 1.0)
				{
					if (Main.tile[i, j].HasTile)
					{
						int num220 = j + WorldGen.genRand.Next(10, 14);
						for (int num221 = j; num221 < num220; num221++)
						{
							if (Main.tile[i, num221].TileType == TileID.JungleGrass && i >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i < evilBiomePositionEastBound - WorldGen.genRand.Next(5))
							{
								Main.tile[i, num221].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
							}
						}
						break;
					}
					j++;
				}
			}
			double num222 = Main.worldSurface + 40.0;
			for (int i = evilBiomePositionWestBound; i < evilBiomePositionEastBound; i++)
			{
				num222 += WorldGen.genRand.Next(-2, 3);
				if (num222 < Main.worldSurface + 30.0)
				{
					num222 = Main.worldSurface + 30.0;
				}
				if (num222 > Main.worldSurface + 50.0)
				{
					num222 = Main.worldSurface + 50.0;
				}
				int num57 = i;
				bool flag13 = false;
				int num224 = (int)GenVars.worldSurfaceLow;
				while (num224 < num222)
				{
					if (Main.tile[num57, num224].HasTile)
					{
						if (Main.tile[num57, num224].TileType == TileID.Sand && num57 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && num57 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
						{
							Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsand>();
						}
						if (Main.tile[num57, num224].TileType == TileID.Dirt && num224 < Main.worldSurface - 1.0 && !flag13)
						{
							WorldGen.grassSpread = 0;
							WorldGen.SpreadGrass(num57, num224, 0, ModContent.TileType<Ickgrass>(), true, default);
						}
						flag13 = true;
						if (Main.tile[num57, num224].TileType == TileID.Stone && num57 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && num57 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5))
						{
							Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Chunkstone>();
						}
						if (Main.tile[num57, num224].TileType == TileID.Grass)
						{
							Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Ickgrass>();
						}
						if (Main.tile[num57, num224].TileType == TileID.IceBlock)
						{
							Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<YellowIce>();
						}
						if (Main.tile[num57, num224].TileType == TileID.HardenedSand)
						{
							Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
						}
						if (Main.tile[num57, num224].TileType == TileID.Sandstone)
						{
							Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsandstone>();
						}
					}
					num224++;
				}
			}
			int num225 = WorldGen.genRand.Next(10, 15);
			for (int num226 = 0; num226 < num225; num226++)
			{
				int num227 = 0;
				bool flag14 = false;
				int num228 = 0;
				while (!flag14)
				{
					num227++;
					int num229 = WorldGen.genRand.Next(evilBiomePositionWestBound - num228, evilBiomePositionEastBound + num228);
					int num230 = WorldGen.genRand.Next((int)(Main.worldSurface - num228 / 2), (int)(Main.worldSurface + 100.0 + num228));
					if (num227 > 100)
					{
						num228++;
						num227 = 0;
					}
					if (!Main.tile[num229, num230].HasTile)
					{
						while (!Main.tile[num229, num230].HasTile)
						{
							num230++;
						}
						num230--;
					}
					else
					{
						while (Main.tile[num229, num230].HasTile && num230 > Main.worldSurface)
						{
							num230--;
						}
					}
					if (num228 > 10 || (Main.tile[num229, num230 + 1].HasTile && Main.tile[num229, num230 + 1].TileType == TileID.Crimstone))
					{
						WorldGen.Place3x2(num229, num230, (ushort)ModContent.TileType<IckyAltar>());
						if (Main.tile[num229, num230].TileType == (ushort)ModContent.TileType<IckyAltar>())
						{
							flag14 = true;
						}
					}
					if (num228 > 100)
					{
						flag14 = true;
					}
				}
			}

			WorldBiomeGeneration.ChangeRange.AddChangeToRange(evilBiomePositionWestBound, (int)GenVars.worldSurfaceLow);
			WorldBiomeGeneration.ChangeRange.AddChangeToRange(evilBiomePositionEastBound, (int)Main.worldSurface + 50);

			WorldBiomeGeneration.EvilBiomeGenRanges.Add(WorldBiomeGeneration.ChangeRange.GetRange());
		}

		public override void PostGenerateEvil() { }
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class SavannaAltBiome : AltBiome
{
	public override string IconSmall => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldIcons/IconTropics";
	public override Color OuterColor => new(175, 148, 199);
	public override IShoppingBiome Biome => ModContent.GetInstance<Biomes.Savanna>();
	public override Color NameColor => new(191, 162, 78);
	public override bool Selectable => ModContent.GetInstance<AvalonClientConfig>().BetaTropicsGen;
	public override void SetStaticDefaults()
	{
		BiomeType = BiomeType.Jungle;
		BiomeGrass = ModContent.TileType<Tiles.Savanna.SavannaGrass>();
	}
	public override AltMaterialContext MaterialContext => new()
	{
		TropicalHerb = ModContent.ItemType<TwilightPlume>(),
		TropicalBar = ModContent.ItemType<XanthophyteBar>(),
		//TropicalComponent = ModContent.ItemType<>(),
		//TropicalSword = ModContent.ItemType<>()
	};
	public override void ModifyGenPass(List<GenPass> passes, GenPass originalPass)
	{
		switch (originalPass.Name)
		{
			case "Wet Jungle":
				originalPass.Disable();
				passes.Add(new PassLegacy("Wet Savanna", new WorldGenLegacyMethod(Savanna.JunglesWetTask)));
				break;
			case "Ice":
				passes.Add(new PassLegacy("Tuhrtl Brick Unsolid", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration config)
				{
					Main.tileSolid[ModContent.TileType<Tiles.Savanna.TuhrtlBrick>()] = false;
					Main.tileSolid[ModContent.TileType<Tiles.Savanna.BrambleSpikes>()] = false;
				})));
				break;
			case "Mud Caves To Grass":
				originalPass.Disable();
				passes.Add(new PassLegacy("Loam", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration configuration)
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
				passes.Add(new PassLegacy("Loam Caves To Grass", new WorldGenLegacyMethod(Savanna.JunglesGrassTask)));
				break;
			case "Jungle Temple":
				originalPass.Disable();
				passes.Add(new PassLegacy("Tuhrtl Outpost", new WorldGenLegacyMethod(Savanna.TuhrtlOutpostTask)));
				passes.Add(new PassLegacy("Outpost Traps", new WorldGenLegacyMethod(Savanna.TuhrtlOutpostReplaceTraps)));
				break;
			case "Hives":
				originalPass.Disable();
				passes.Add(new PassLegacy("Wasp Nests", new WorldGenLegacyMethod(Savanna.WaspNests)));
				break;
			case "Jungle Chests":
				originalPass.Disable();
				passes.Add(new PassLegacy("Savanna Sanctums", new WorldGenLegacyMethod(Savanna.SavannaSanctumTask)));
				break;
			case "Muds Walls In Jungle":
				originalPass.Disable();
				passes.Add(new PassLegacy("Loam Walls in Savanna", new WorldGenLegacyMethod(delegate (GenerationProgress progress, GameConfiguration passConfig)
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
				})));
				break;
			case "Temple":
				originalPass.Disable();
				passes.Add(new PassLegacy("Re-solidify Lihzahrd Brick", new WorldGenLegacyMethod(Savanna.LihzahrdBrickReSolidTask)));
				break;
			case "Glowing Mushrooms and Jungle Plants":
				originalPass.Disable();
				passes.Add(new PassLegacy("Glowing Mushrooms and Savanna Plants", new WorldGenLegacyMethod(Savanna.GlowingMushroomsandJunglePlantsTask)));
				break;
			case "Jungle Plants":
				originalPass.Disable();
				passes.Add(new PassLegacy("Savanna Plants", new WorldGenLegacyMethod(Savanna.JungleBushesTask)));
				break;
		}
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class BronzeAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<CopperOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.BronzeOre>();
		bar = ModContent.ItemType<BronzeBar>();
		Watch = ModContent.ItemType<BronzeWatch>();
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class NickelAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<IronOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.NickelOre>();
		bar = ModContent.ItemType<NickelBar>();
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class ZincAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<SilverOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.ZincOre>();
		bar = ModContent.ItemType<ZincBar>();
		Watch = ModContent.ItemType<ZincWatch>();
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class BismuthAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<GoldOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.BismuthOre>();
		bar = ModContent.ItemType<BismuthBar>();
		Watch = ModContent.ItemType<BismuthWatch>();
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class DuritaniumAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<CobaltOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.DurataniumOre>();
		bar = ModContent.ItemType<DurataniumBar>();
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class NaquadahAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<MythrilOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.NaquadahOre>();
		bar = ModContent.ItemType<NaquadahBar>();
	}
}
[ExtendsFromMod(nameof(AltLibrary))]
public class TroxiniumAltOre : AltOre
{
	public override OreSlot OreSlot => ModContent.GetInstance<AdamantiteOreSlot>();
	public override void SetStaticDefaults()
	{
		ore = ModContent.TileType<Tiles.Ores.TroxiniumOre>();
		bar = ModContent.ItemType<TroxiniumBar>();
	}
}
// it would be possible to add support for other mods to add their own Rhodium variants by extending OreSlot