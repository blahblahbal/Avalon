using Avalon.Common;
using Avalon.Tiles.Contagion;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tile = Avalon.Data.Sets.Tile;

namespace Avalon.Hooks;

internal class ContagionStalac : ModHook
{
    protected override void Apply()
    {
        On_WorldGen.GetDesiredStalagtiteStyle += On_WorldGen_GetDesiredStalactiteStyle;
        IL_WorldGen.PaintTheSand += IL_AddStalacCheck;
        IL_WorldGen.PlaceTile += IL_AddStalacCheck;
        IL_WorldGen.PlaceTight += IL_AddStalacCheck;
        IL_WorldGen.TileFrame += IL_AddStalacCheck;
        IL_WorldGen.BlockBelowMakesSandFall += IL_AddStalacCheck;
        IL_WorldGen.UpdateWorld_OvergroundTile += IL_AddStalacCheck;
        IL_WorldGen.UpdateWorld_UndergroundTile += IL_AddStalacCheck;
        IL_WorldGen.ReplaceTile_EliminateNaturalExtras += IL_AddStalacCheck;
        On_WorldGen.IsFitToPlaceFlowerIn += On_WorldGen_IsFitToPlaceFlowerIn;
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
