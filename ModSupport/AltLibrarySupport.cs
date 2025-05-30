using AltLibrary;
using AltLibrary.Common.AltBiomes;
using AltLibrary.Common.Systems;
using AltLibrary.Core.Generation;
using AltLibrary.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Avalon.Tiles.Contagion;
using Avalon.WorldGeneration.Passes;
using Avalon.Items.Other;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Material.Ores;
using Avalon.NPCs.Hardmode;
using Avalon.NPCs.Critters;
using Avalon.Items.Placeable.Seed;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Material.Herbs;
using Avalon.Walls;
using Avalon.WorldGeneration.Enums;
using Avalon.Common;
using AltLibrary.Common.Conditions;
using Avalon.Biomes;

// Y'all should really have an editorconfig file, but I'm going to try to minimize what I change in other files at all, so you should be able to make everything consistent with one Ctrl+K, Ctrl+D
namespace Avalon.ModSupport {
	public class AltLibrarySupport : ModSystem {
		static bool? enabled = null;
		public static bool Enabled => enabled ??= ModLoader.HasMod(nameof(AltLibrary));
		public override void Unload() {
			enabled = null;
		}
		public override void PostSetupContent() {
			ModContent.GetInstance<CrimsonAltBiome>().ArrowType = ModContent.ItemType<Items.Ammo.BloodyArrow>();
		}
		public override void PostUpdateTime() {
			if (!Enabled) return;
			Do();
			[JITWhenModsEnabled(nameof(AltLibrary))]
			static void Do() {
				AltBiome evil = WorldBiomeManager.GetWorldEvil(true, true);
				ref WorldEvil worldEvil = ref ModContent.GetInstance<AvalonWorld>().WorldEvil;
				if (evil is ContagionAltBiome) worldEvil = WorldEvil.Contagion;
				else if (evil is CrimsonAltBiome) worldEvil = WorldEvil.Crimson;
				else worldEvil = WorldEvil.Corruption;
			}
		}
		public static bool PreWorldGen(AvalonWorld world) {
			if (!Enabled) return false;
			Do(world);
			[JITWhenModsEnabled(nameof(AltLibrary))]
			static void Do(AvalonWorld world) {
				if (WorldBiomeManager.GetWorldEvil(true) == ModContent.GetInstance<ContagionAltBiome>()) {
					world.SecondaryContagionBG = WorldGen.genRand.NextBool(2) ? 1 : 0;
				}
			}
			return true;
		}

		public static void ReplaceShopConditions(ref Condition corruption, ref Condition crimson, ref Condition contagion) {
			if (!Enabled) return;
			Do(ref corruption, ref crimson, ref contagion);
			[JITWhenModsEnabled(nameof(AltLibrary))]
			static void Do(ref Condition corruption, ref Condition crimson, ref Condition contagion) {
				corruption = ShopConditions.GetWorldEvilCondition<CorruptionAltBiome>();
				crimson = ShopConditions.GetWorldEvilCondition<CrimsonAltBiome>();
				contagion = ShopConditions.GetWorldEvilCondition<ContagionAltBiome>();
			}
		}

		public static bool EvilBiomeArrow(ref int itemType) {
			if (!Enabled) return false;
			Do(ref itemType);
			[JITWhenModsEnabled(nameof(AltLibrary))]
			static void Do(ref int itemType) {
				itemType = WorldBiomeManager.GetWorldEvil(true, true).ArrowType ?? ItemID.UnholyArrow;
			}
			return true;
		}
	}
	[ExtendsFromMod(nameof(AltLibrary))]
	public class ContagionAltBiome : AltBiome {
		public override string IconLarge => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/IconOverlayContagion";
		public override string OuterTexture => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/LoadingOuterContagion";
		public override string IconSmall => $"{nameof(Avalon)}/{ExxoAvalonOrigins.TextureAssetsPath}/UI/WorldCreation/IconContagion";
		public override Color OuterColor => new(175, 148, 199);
		public override IShoppingBiome Biome => ModContent.GetInstance<Biomes.Contagion>();
		public override Color NameColor => Color.Green;
		public override void SetStaticDefaults() {
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
			BiomeOreBrick = ModContent.TileType<Tiles.BacciliteBrick>();
			ArrowType = ModContent.ItemType<Items.Ammo.IckyArrow>();
			AltarTile = ModContent.TileType<IckyAltar>();

			BiomeChestItem = ModContent.ItemType<VirulentScythe>();
			BiomeChestTile = ModContent.TileType<ContagionChest>();
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
			AddWallConversions<ContagionGrassWall>(
				WallID.GrassUnsafe,
				WallID.Grass,
				WallID.FlowerUnsafe,
				WallID.Flower
			);

			EvilBiomeGenerationPass = new ContagionGenerationPass();
		}
		public override AltMaterialContext MaterialContext {
			get {
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
		public class ContagionGenerationPass : EvilBiomeGenerationPass {
			public override string ProgressMessage => Language.GetTextValue("Mods.Avalon.World.Generation.Contagion.Message");
			public override void GenerateEvil(int evilBiomePosition, int evilBiomePositionWestBound, int evilBiomePositionEastBound) {
				WorldBiomeGeneration.ChangeRange.ResetRange();

				WorldGeneration.Passes.Contagion.ContagionRunner(evilBiomePosition, (int)GenVars.worldSurfaceLow - 10 + (Main.maxTilesY / 8));
				for (int i = evilBiomePositionWestBound; i < evilBiomePositionEastBound; i++) {
					int j = (int)GenVars.worldSurfaceLow;
					while (j < Main.worldSurface - 1.0) {
						if (Main.tile[i, j].HasTile) {
							int num220 = j + WorldGen.genRand.Next(10, 14);
							for (int num221 = j; num221 < num220; num221++) {
								if (Main.tile[i, num221].TileType == TileID.JungleGrass && i >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && i < evilBiomePositionEastBound - WorldGen.genRand.Next(5)) {
									Main.tile[i, num221].TileType = (ushort)ModContent.TileType<ContagionJungleGrass>();
								}
							}
							break;
						}
						j++;
					}
				}
				double num222 = Main.worldSurface + 40.0;
				for (int i = evilBiomePositionWestBound; i < evilBiomePositionEastBound; i++) {
					num222 += WorldGen.genRand.Next(-2, 3);
					if (num222 < Main.worldSurface + 30.0) {
						num222 = Main.worldSurface + 30.0;
					}
					if (num222 > Main.worldSurface + 50.0) {
						num222 = Main.worldSurface + 50.0;
					}
					int num57 = i;
					bool flag13 = false;
					int num224 = (int)GenVars.worldSurfaceLow;
					while (num224 < num222) {
						if (Main.tile[num57, num224].HasTile) {
							if (Main.tile[num57, num224].TileType == TileID.Sand && num57 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && num57 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5)) {
								Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsand>();
							}
							if (Main.tile[num57, num224].TileType == TileID.Dirt && num224 < Main.worldSurface - 1.0 && !flag13) {
								WorldGen.grassSpread = 0;
								WorldGen.SpreadGrass(num57, num224, 0, ModContent.TileType<Ickgrass>(), true, default);
							}
							flag13 = true;
							if (Main.tile[num57, num224].TileType == TileID.Stone && num57 >= evilBiomePositionWestBound + WorldGen.genRand.Next(5) && num57 <= evilBiomePositionEastBound - WorldGen.genRand.Next(5)) {
								Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Chunkstone>();
							}
							if (Main.tile[num57, num224].TileType == TileID.Grass) {
								Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Ickgrass>();
							}
							if (Main.tile[num57, num224].TileType == TileID.IceBlock) {
								Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<YellowIce>();
							}
							if (Main.tile[num57, num224].TileType == TileID.HardenedSand) {
								Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<HardenedSnotsand>();
							}
							if (Main.tile[num57, num224].TileType == TileID.Sandstone) {
								Main.tile[num57, num224].TileType = (ushort)ModContent.TileType<Snotsandstone>();
							}
						}
						num224++;
					}
				}
				int num225 = WorldGen.genRand.Next(10, 15);
				for (int num226 = 0; num226 < num225; num226++) {
					int num227 = 0;
					bool flag14 = false;
					int num228 = 0;
					while (!flag14) {
						num227++;
						int num229 = WorldGen.genRand.Next(evilBiomePositionWestBound - num228, evilBiomePositionEastBound + num228);
						int num230 = WorldGen.genRand.Next((int)(Main.worldSurface - num228 / 2), (int)(Main.worldSurface + 100.0 + num228));
						if (num227 > 100) {
							num228++;
							num227 = 0;
						}
						if (!Main.tile[num229, num230].HasTile) {
							while (!Main.tile[num229, num230].HasTile) {
								num230++;
							}
							num230--;
						} else {
							while (Main.tile[num229, num230].HasTile && num230 > Main.worldSurface) {
								num230--;
							}
						}
						if (num228 > 10 || (Main.tile[num229, num230 + 1].HasTile && Main.tile[num229, num230 + 1].TileType == TileID.Crimstone)) {
							WorldGen.Place3x2(num229, num230, (ushort)ModContent.TileType<IckyAltar>());
							if (Main.tile[num229, num230].TileType == (ushort)ModContent.TileType<IckyAltar>()) {
								flag14 = true;
							}
						}
						if (num228 > 100) {
							flag14 = true;
						}
					}
				}

				int minY = WorldBiomeGeneration.ChangeRange.GetRange().Top;
				WorldBiomeGeneration.ChangeRange.AddChangeToRange(evilBiomePositionWestBound, (int)GenVars.worldSurfaceLow);
				WorldBiomeGeneration.ChangeRange.AddChangeToRange(evilBiomePositionEastBound, (int)Main.worldSurface + 50);

				WorldBiomeGeneration.EvilBiomeGenRanges.Add(WorldBiomeGeneration.ChangeRange.GetRange());
			}

			public override void PostGenerateEvil() { }
		}
	}
}
