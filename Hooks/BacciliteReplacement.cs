using Avalon.Common;
using Terraria;
using Terraria.ModLoader;
using Avalon.Tiles.Ores;
using Terraria.ID;
using MonoMod.Cil;

namespace Avalon.Hooks
{
    internal class BacciliteReplacement : ModHook
    {
        protected override void Apply()
        {
            On_WorldGen.TileRunner += On_WorldGen_TileRunner;
            IL_WorldGen.ShimmerMakeBiome += AddShimmerAlternativeChecks;
            IL_WorldGen.badOceanCaveTiles += IL_WorldGen_badOceanCaveTiles;
            IL_WorldGen.GERunner += IL_WorldGen_GERunner;
            On_WorldGen.PlacePot += On_WorldGen_PlacePot;
        }

        private bool On_WorldGen_PlacePot(On_WorldGen.orig_PlacePot orig, int x, int y, ushort type, int style)
        {
            if (Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.Contagion.Chunkstone>() ||
                Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.Contagion.Snotsand>() ||
                Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.Contagion.Ickgrass>())
            {
                style = 0;
                type = (ushort)ModContent.TileType<Tiles.Contagion.ContagionPot>();
            }
            if (Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.Savanna.SavannaGrass>() ||
                Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.Savanna.Loam>())
            {
                style = 0;
                type = (ushort)ModContent.TileType<Tiles.Savanna.SavannaPot>();
            }
            if (Main.tile[x, y + 1].TileType == ModContent.TileType<Tiles.Savanna.TuhrtlBrick>())
            {
                style = 0;
                type = (ushort)ModContent.TileType<Tiles.Savanna.TuhrtlPot>();
            }
            return orig.Invoke(x, y, type, style);
        }

        private void IL_WorldGen_GERunner(ILContext il)
        {
            Utilities.AddAlternativeIdChecks(il, TileID.Crimstone, id => Data.Sets.TileSets.Chunkstone[id]);
        }

        private void IL_WorldGen_badOceanCaveTiles(ILContext il)
        {
            Utilities.AddAlternativeIdChecks(il, TileID.Ebonstone, id => Data.Sets.TileSets.Chunkstone[id]);
            Utilities.AddAlternativeIdChecks(il, TileID.DemonAltar, id => Data.Sets.TileSets.Altar[id]);
            Utilities.AddAlternativeIdChecks(il, TileID.ShadowOrbs, id => Data.Sets.TileSets.Orb[id]);
            Utilities.AddAlternativeIdChecks(il, WallID.EbonstoneUnsafe, id => Data.Sets.WallSets.Chunkstone[id]);
        }

        public static void AddShimmerAlternativeChecks(ILContext il) =>
            Utilities.AddAlternativeIdChecks(il, TileID.Ebonstone, id => Data.Sets.TileSets.Chunkstone[id]);

        private void On_WorldGen_TileRunner(On_WorldGen.orig_TileRunner orig, int i, int j, double strength, int steps, int type, bool addTile, double speedX, double speedY, bool noYChange, bool overRide, int ignoreTileType)
        {
            if (ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldGeneration.Enums.WorldEvil.Contagion && (type == TileID.Demonite || type == TileID.Crimtane))
            {
                type = ModContent.TileType<BacciliteOre>();
            }
            orig(i, j, strength, steps, type, addTile, speedX, speedY, noYChange, overRide, ignoreTileType);
        }
    }
}
