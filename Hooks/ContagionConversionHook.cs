using Avalon.Common;
using Avalon.Tiles.Contagion;
using Avalon.WorldGeneration.Helpers;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tile = Avalon.Data.Sets.Tile;

namespace Avalon.Hooks;

internal class ContagionConversionHook : ModHook
{
    protected override void Apply()
    {
        On_WorldGen.GetDesiredStalagtiteStyle += On_WorldGen_GetDesiredStalactiteStyle;
        
        IL_WorldGen.PaintTheSand += IL_AddStalacCheck;
        IL_WorldGen.PlaceTile += IL_AddStalacCheck;
        IL_WorldGen.PlaceTight += IL_AddStalacCheck;
        IL_WorldGen.BlockBelowMakesSandFall += IL_AddStalacCheck;
        
        IL_WorldGen.TileFrame += IL_AddStalacCheck;
        IL_WorldGen.TileFrame += IL_WorldGen_TileFrame;
        
        IL_WorldGen.UpdateWorld_OvergroundTile += IL_AddStalacCheck;
        On_WorldGen.UpdateWorld_OvergroundTile += On_WorldGen_UpdateWorld_OvergroundTile;
        
        IL_WorldGen.UpdateWorld_UndergroundTile += IL_AddStalacCheck;
        On_WorldGen.UpdateWorld_UndergroundTile += On_WorldGen_UpdateWorld_UndergroundTile;
        
        IL_WorldGen.ReplaceTile_EliminateNaturalExtras += IL_AddStalacCheck;
        On_WorldGen.IsFitToPlaceFlowerIn += On_WorldGen_IsFitToPlaceFlowerIn;
    }

    private static void IL_WorldGen_TileFrame(ILContext il)
    {
        var cursor = new ILCursor(il);

        cursor.GotoNext(MoveType.Before, i => i.MatchStloc(121));
        cursor.Emit(OpCodes.Ldloc, 84); // up
        cursor.EmitDelegate((ushort origValue, int up) =>
        {
            if (up == ModContent.TileType<ContagionVines>() || up == ModContent.TileType<ContagionJungleGrass>() || up == ModContent.TileType<Ickgrass>())
            {
                return (ushort)ModContent.TileType<ContagionVines>();
            }
            return origValue;
        });

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

    private void On_WorldGen_UpdateWorld_OvergroundTile(On_WorldGen.orig_UpdateWorld_OvergroundTile orig, int x, int y, bool checkNPCSpawns, int wallDist)
    {
        orig(x, y, checkNPCSpawns, wallDist);
        VineHelper.VineRandomUpdate(x, y, 20, 60);
    }

    private void On_WorldGen_UpdateWorld_UndergroundTile(On_WorldGen.orig_UpdateWorld_UndergroundTile orig, int x, int y, bool checkNPCSpawns, int wallDist)
    {
        orig(x, y, checkNPCSpawns, wallDist);
        VineHelper.VineRandomUpdate(x, y, 7, 70);
    }

    private static bool On_WorldGen_IsFitToPlaceFlowerIn(On_WorldGen.orig_IsFitToPlaceFlowerIn orig, int x, int y, int typeAttemptedToPlace)
    {
        var origResult = orig(x, y, typeAttemptedToPlace);
        var tile = Main.tile[x, y + 1];
        var canPlaceContagionShortGrass = y >= 1 && y <= Main.maxTilesY - 1 &&
                                          tile is{HasTile: true, Slope: 0, IsHalfBlock: false} &&
                                          typeAttemptedToPlace == ModContent.TileType<ContagionShortGrass>() &&
                                          (tile.TileType == ModContent.TileType<Ickgrass>() || tile.TileType == ModContent.TileType<ContagionJungleGrass>());
        return canPlaceContagionShortGrass || origResult;
    }

    private static void IL_AddStalacCheck(ILContext il)
    {
        Utilities.AddAlternativeIdChecks(il, TileID.Stalactite, id => Tile.Stalac.Contains(id));
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
