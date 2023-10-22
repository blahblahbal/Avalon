using System;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Avalon.Systems;

public class BiomeTileCounts : ModSystem
{
    public const int DarkMatterTilesHardLimit = 250000;
    
    public int WorldDarkMatterTiles;
    public int ContagionTiles { get; private set; }
    public int TropicsTiles { get; private set; }
    public int HellCastleTiles { get; private set; }
    public int DarkTiles { get; private set; }
    public int CaesiumTiles { get; private set; }
    public int SkyFortressTiles { get; private set; }
    public int CrystalTiles { get; private set; }
    public int BlightTiles { get; private set; }
    public int FrightTiles { get; private set; }
    public int MightTiles { get; private set; }
    public int NightTiles { get; private set; }
    public int TortureTiles { get; private set; }
    public int IceSoulTiles { get; private set; }
    public int FlightTiles { get; private set; }
    public int HumidityTiles { get; private set; }
    public int DelightTiles { get; private set; }
    public int SightTiles { get; private set; }
    public int DungeonAltTiles { get; private set; }
    public int DarkMonolithTiles { get; private set; }
    public int ContagionDesertTiles { get; private set; }

    public int AshenOvergrowthTiles { get; private set; }

    public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
        HellCastleTiles = tileCounts[ModContent.TileType<ImperviousBrick>()];

        ContagionTiles = tileCounts[ModContent.TileType<Chunkstone>()] +
                         tileCounts[ModContent.TileType<HardenedSnotsand>()] +
                         tileCounts[ModContent.TileType<Snotsandstone>()] +
                         tileCounts[ModContent.TileType<Ickgrass>()] +
                         tileCounts[ModContent.TileType<ContagionJungleGrass>()] +
                         tileCounts[ModContent.TileType<Snotsand>()] +
                         tileCounts[ModContent.TileType<YellowIce>()];
        DarkMonolithTiles = tileCounts[ModContent.TileType<Tiles.DarkMatter.DarkMatterMonolith>()];
        AshenOvergrowthTiles = tileCounts[TileID.AshGrass] + 
                               tileCounts[TileID.AshPlants];

        Main.SceneMetrics.JungleTileCount += tileCounts[ModContent.TileType<GreenIce>()];
        Main.SceneMetrics.SnowTileCount += tileCounts[ModContent.TileType<GreenIce>()];
        Main.SceneMetrics.SandTileCount += tileCounts[ModContent.TileType<Snotsand>()] + tileCounts[ModContent.TileType<HardenedSnotsand>()] + tileCounts[ModContent.TileType<Snotsandstone>()];
        //ContagionDesertTiles += tileCounts[ModContent.TileType<Snotsand>()];
        

        /*TropicsTiles = tileCounts[ModContent.TileType<TropicalStone>()] +
                       tileCounts[ModContent.TileType<TuhrtlBrick>()] +
                       tileCounts[ModContent.TileType<Loam>()] +
                       tileCounts[ModContent.TileType<TropicalGrass>()];
        
        DarkTiles = tileCounts[ModContent.TileType<DarkMatter>()] +
                    tileCounts[ModContent.TileType<DarkMatterSand>()] +
                    tileCounts[ModContent.TileType<BlackIce>()] +
                    tileCounts[ModContent.TileType<DarkMatterSoil>()] +
                    tileCounts[ModContent.TileType<HardenedDarkSand>()] +
                    tileCounts[ModContent.TileType<Darksandstone>()] +
                    tileCounts[ModContent.TileType<DarkMatterGrass>()];*/
        DungeonAltTiles += tileCounts[ModContent.TileType<OrangeBrick>()] +
            tileCounts[ModContent.TileType<PurpleBrick>()] +
            tileCounts[ModContent.TileType<YellowBrick>()] +
            tileCounts[ModContent.TileType<CrackedYellowBrick>()] +
            tileCounts[ModContent.TileType<CrackedOrangeBrick>()] +
            tileCounts[ModContent.TileType<CrackedPurpleBrick>()];
        CaesiumTiles = tileCounts[ModContent.TileType<BlastedStone>()];
        SkyFortressTiles = tileCounts[ModContent.TileType<SkyBrick>()];
        /*CrystalTiles = tileCounts[ModContent.TileType<CrystalStone>()];
        BlightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.BlightCandle>()];
        FrightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.FrightCandle>()];
        MightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.MightCandle>()];
        NightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.NightCandle>()];
        TortureTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.TortureCandle>()];
        IceSoulTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.IceCandle>()];
        FlightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.FlightCandle>()];
        HumidityTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.HumidityCandle>()];
        DelightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.DelightCandle>()];
        SightTiles = tileCounts[ModContent.TileType<Tiles.SoulCandles.SightCandle>()];
        DarkMonolithTiles = tileCounts[ModContent.TileType<DarkMatterMonolith>()];*/
        //Main.LocalPlayer.GetModPlayer<ExxoBiomePlayer>().UpdateZones(this);
    }

    public override void SaveWorldData(TagCompound tag)
    {
        tag["WorldDarkMatterTiles"] = WorldDarkMatterTiles;
    }

    public override void LoadWorldData(TagCompound tag)
    {
        if (tag.ContainsKey("WorldDarkMatterTiles"))
        {
            WorldDarkMatterTiles = tag.Get<int>("WorldDarkMatterTiles");
        }
    }
}
