using Avalon.Common;
using Avalon.ModSupport;
using Avalon.Tiles.Contagion.Chunkstone;
using Avalon.Tiles.Contagion.ContagionGrasses;
using Avalon.Tiles.Contagion.ContagionStalagmites;
using Avalon.Tiles.Contagion.HardenedSnotsand;
using Avalon.Tiles.Contagion.SmallPlants;
using Avalon.Tiles.Contagion.Snotsandstone;
using Avalon.WorldGeneration.Helpers;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TileSets = Avalon.Data.Sets.TileSets;

namespace Avalon.Hooks;

internal class ContagionConversionHook : ModHook
{
	protected override void Apply()
	{
		if (!AltLibrarySupport.Enabled)
		{
			On_WorldGen.GetDesiredStalagtiteStyle += On_WorldGen_GetDesiredStalactiteStyle;

			IL_WorldGen.PaintTheSand += IL_AddStalacCheck;
			IL_WorldGen.PlaceTile += IL_AddStalacCheck;
			IL_WorldGen.PlaceTight += IL_AddStalacCheck;
			IL_WorldGen.BlockBelowMakesSandFall += IL_AddStalacCheck;

			IL_WorldGen.TileFrame += IL_AddStalacCheck;

			IL_WorldGen.UpdateWorld_OvergroundTile += IL_AddStalacCheck;

			IL_WorldGen.UpdateWorld_UndergroundTile += IL_AddStalacCheck;

			IL_WorldGen.ReplaceTile_EliminateNaturalExtras += IL_AddStalacCheck;
		}

		IL_WorldGen.PlantCheck += IL_WorldGen_PlantCheck;
		IL_WorldGen.TileFrame += IL_WorldGen_TileFrame;

		On_WorldGen.UpdateWorld_OvergroundTile += On_WorldGen_UpdateWorld_OvergroundTile;

		On_WorldGen.UpdateWorld_UndergroundTile += On_WorldGen_UpdateWorld_UndergroundTile;

		On_WorldGen.IsFitToPlaceFlowerIn += On_WorldGen_IsFitToPlaceFlowerIn;
	}

	private static void IL_WorldGen_PlantCheck(ILContext il)
	{
		var cursor = new ILCursor(il);
		const int belowTileTypeLocation = 0;
		const int tileTypeLocation = 1;
		const int xArg = 0;
		const int yArg = 1;

		// Extend check for whether or not a plant check is required
		ILLabel exitBranchLabel = null!;
		cursor.GotoNext(MoveType.Before, i => i.MatchLdcI4(662), i => i.MatchBneUn(out exitBranchLabel!));

		cursor.GotoNext(MoveType.Before, i => i.MatchLdcI4(637), i => i.MatchBneUn(out _));
		cursor.Emit(OpCodes.Ldloc, belowTileTypeLocation);
		cursor.EmitDelegate((int tileType, int belowTileType) => tileType != ModContent.TileType<ContagionShortGrass>() ||
																 belowTileType == ModContent.TileType<ContagionJungleGrass>() ||
																 belowTileType == ModContent.TileType<Ickgrass>());
		// If false then break out of && chain
		cursor.Emit(OpCodes.Brfalse, exitBranchLabel);
		// If true then continue, push tile type back on stack
		cursor.Emit(OpCodes.Ldloc, tileTypeLocation);

		// Check isMushroom
		cursor.GotoNext(MoveType.Before, i => i.MatchStloc(2));
		cursor.Emit(OpCodes.Ldloc, tileTypeLocation);
		cursor.Emit(OpCodes.Ldarg, xArg);
		cursor.Emit(OpCodes.Ldarg, yArg);
		cursor.EmitDelegate((bool origValue, int tileType, int x, int y) =>
		{
			if (tileType == ModContent.TileType<ContagionShortGrass>() && Main.tile[x, y].TileFrameX == ContagionShortGrass.MushroomFrameX)
				return true;

			return origValue;
		});

		// Assign tile type if is contagion
		var continueDefaultCheckLabel = cursor.DefineLabel();
		cursor.GotoNext(MoveType.Before, i => i.MatchLdcI4(109), i => i.MatchBgt(out _));
		cursor.EmitDelegate((int belowTileType) => belowTileType == ModContent.TileType<Ickgrass>() || belowTileType == ModContent.TileType<ContagionJungleGrass>());
		cursor.Emit(OpCodes.Brfalse, continueDefaultCheckLabel);
		cursor.EmitDelegate(ModContent.TileType<ContagionShortGrass>);
		cursor.Emit(OpCodes.Stloc, tileTypeLocation);
		cursor.MarkLabel(continueDefaultCheckLabel);
		cursor.Emit(OpCodes.Ldloc, 0);

		// Assign mushroom if is mushroom
		cursor.GotoNext(MoveType.After, i => i.MatchLdcI4(144), i => i.MatchStindI2());
		cursor.Emit(OpCodes.Ldloc, tileTypeLocation);
		cursor.Emit(OpCodes.Ldarg, xArg);
		cursor.Emit(OpCodes.Ldarg, yArg);
		cursor.EmitDelegate((int tileType, int x, int y) =>
		{
			if (tileType == ModContent.TileType<ContagionShortGrass>())
			{
				Main.tile[x, y].TileFrameX = ContagionShortGrass.MushroomFrameX;
			}
		});
	}

	private static void IL_WorldGen_TileFrame(ILContext il)
	{
		var cursor = new ILCursor(il);

		// Add vine condition for conversion
		cursor.GotoNext(MoveType.Before, i => i.MatchStloc(121));
		cursor.Emit(OpCodes.Ldloc, 84); // up
		cursor.EmitDelegate((ushort origValue, int up) =>
		{
			if (up == ModContent.TileType<ContagionVines>() || ContagionVines.CanGrowFromTile(up))
			{
				return (ushort)ModContent.TileType<ContagionVines>();
			}
			return origValue;
		});

		// Add vine condition for kill
		cursor.GotoNext(MoveType.Before, i => i.MatchStloc(122));
		cursor.Emit(OpCodes.Ldloc, 3); // num
		cursor.Emit(OpCodes.Ldloc, 84); // up
		cursor.EmitDelegate((bool origValue, int num, int up) =>
		{
			if (num == ModContent.TileType<ContagionVines>() && up != ModContent.TileType<ContagionJungleGrass>() && up != ModContent.TileType<Ickgrass>())
			{
				return true;
			}
			return origValue;
		});
	}

	private static void On_WorldGen_UpdateWorld_OvergroundTile(On_WorldGen.orig_UpdateWorld_OvergroundTile orig, int x, int y, bool checkNPCSpawns, int wallDist)
	{
		orig(x, y, checkNPCSpawns, wallDist);
		VinesHelper.VinesRandomUpdate<ContagionVines>(x, y, 20, 60, ContagionVines.CanGrowFromTile);
	}

	private static void On_WorldGen_UpdateWorld_UndergroundTile(On_WorldGen.orig_UpdateWorld_UndergroundTile orig, int x, int y, bool checkNPCSpawns, int wallDist)
	{
		orig(x, y, checkNPCSpawns, wallDist);
		VinesHelper.VinesRandomUpdate<ContagionVines>(x, y, 7, 70, ContagionVines.CanGrowFromTile);
	}

	private static bool On_WorldGen_IsFitToPlaceFlowerIn(On_WorldGen.orig_IsFitToPlaceFlowerIn orig, int x, int y, int typeAttemptedToPlace)
	{
		var origResult = orig(x, y, typeAttemptedToPlace);
		var tile = Main.tile[x, y + 1];
		var canPlaceContagionShortGrass = y >= 1 && y <= Main.maxTilesY - 1 &&
										  tile is { HasTile: true, Slope: 0, IsHalfBlock: false } &&
										  typeAttemptedToPlace == ModContent.TileType<ContagionShortGrass>() &&
										  (tile.TileType == ModContent.TileType<Ickgrass>() || tile.TileType == ModContent.TileType<ContagionJungleGrass>());
		return canPlaceContagionShortGrass || origResult;
	}

	private static void IL_AddStalacCheck(ILContext il)
	{
		Utilities.AddAlternativeIdChecks(il, TileID.Stalactite, id => TileSets.Stalac.Contains(id));
	}

	private static void On_WorldGen_GetDesiredStalactiteStyle(On_WorldGen.orig_GetDesiredStalagtiteStyle orig, int x, int j, out bool fail, out int desiredStyle, out int height, out int y)
	{
		orig(x, j, out fail, out desiredStyle, out height, out y);
		switch (fail)
		{
			case true when desiredStyle == ModContent.TileType<Chunkstone>() || desiredStyle == ModContent.TileType<HardenedSnotsand>() || desiredStyle == ModContent.TileType<Snotsandstone>():
				fail = false;
				desiredStyle = 7;
				for (var i = y; i < y + height; i++)
				{
					Main.tile[x, i].TileType = (ushort)ModContent.TileType<ContagionStalactgmites>();
				}
				break;

			case false when Main.tile[x, j].TileType == ModContent.TileType<ContagionStalactgmites>():
				for (var i = y; i < y + height; i++)
				{
					Main.tile[x, i].TileType = TileID.Stalactite;
				}
				break;
		}
	}
}
