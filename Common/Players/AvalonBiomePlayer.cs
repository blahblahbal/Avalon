using Avalon.Biomes;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Common.Players;

public class AvalonBiomePlayer : ModPlayer
{
    public bool ZoneContagion => Player.InModBiome(ModContent.GetInstance<Contagion>());
    public bool ZoneCaesium => Player.InModBiome(ModContent.GetInstance<CaesiumBlastplains>());
    //public bool ZoneCrystal => Player.InModBiome(ModContent.GetInstance<CrystalMines>());
    public bool ZoneDarkMatter => Player.InModBiome(ModContent.GetInstance<DarkMatter>());
    public bool ZoneHellcastle => Player.InModBiome(ModContent.GetInstance<Hellcastle>());
    public bool ZoneNearHellcastle => Player.InModBiome(ModContent.GetInstance<NearHellcastle>());
    public bool ZoneSkyFortress => Player.InModBiome(ModContent.GetInstance<SkyFortress>());
    //public bool ZoneTropics => Player.InModBiome(ModContent.GetInstance<Tropics>());
    //public bool ZoneTuhrtlOutpost => Player.InModBiome(ModContent.GetInstance<TuhrtlOutpost>());
    public bool ZoneUndergroundContagion => Player.InModBiome(ModContent.GetInstance<UndergroundContagion>());
    //public bool ZoneUndergroundTropics => Player.InModBiome(ModContent.GetInstance<UndergroundTropics>());
    public bool ZoneAltDungeon => Player.InModBiome(ModContent.GetInstance<DungeonAltColors>());
    public bool ZoneContagionDesert => Player.InModBiome(ModContent.GetInstance<ContagionDesert>());
    public bool ZoneTime { get; private set; }
    public bool ZoneBlight { get; private set; }
    public bool ZoneFright => Player.InModBiome(ModContent.GetInstance<FrightCandleBiome>());
    public bool ZoneMight => Player.InModBiome(ModContent.GetInstance<MightCandleBiome>());
    public bool ZoneNight => Player.InModBiome(ModContent.GetInstance<NightCandleBiome>());
    public bool ZoneTorture { get; private set; }
    public bool ZoneIceSoul => Player.InModBiome(ModContent.GetInstance<IceCandleBiome>());
    public bool ZoneFlight => Player.InModBiome(ModContent.GetInstance<FlightCandleBiome>());
    public bool ZoneHumidity { get; private set; }
    public bool ZoneDelight => Player.InModBiome(ModContent.GetInstance<DelightCandleBiome>());
    public bool ZoneSight => Player.InModBiome(ModContent.GetInstance<SightCandleBiome>());
    public override void PostUpdate()
    {
        if (ZoneAltDungeon)
        {
            Player.ZoneDungeon = true;
        }
        //if (ZoneHellcastle || ZoneNearHellcastle)
        //{
        //    Player.ZoneGraveyard = true;
        //}
        //if (ZoneContagionDesert)
        //{
        //    Player.ZoneDesert = true;
        //    Terraria.Graphics.Effects.Filters.Scene["Sandstorm"].Activate(Player.position);
        //}
        //Main.NewText(Player.ZoneDesert);
    }
    public override void PostUpdateBuffs()
    {
        if (ZoneFlight)
        {
            Player.slowFall = true;
        }
        if (ZoneFright)
        {
            Player.statDefense += 5;
        }
        if (ZoneIceSoul)
        {
            Player.slippy = Player.slippy2 = true;
        }
        if (ZoneMight)
        {
            Player.GetDamage(DamageClass.Generic) += 0.1f;
        }
        if (ZoneNight)
        {
            Player.wereWolf = true;
        }
        if (ZoneSight)
        {
            Player.detectCreature = Player.dangerSense = Player.nightVision = true;
        }
        if (ZoneDelight)
        {
            Player.lifeRegen += 3;
        }
    }

    public void UpdateZones(BiomeTileCounts biomeTileCounts)
    {
        Point tileCoordinates = Player.Center.ToTileCoordinates();
        ushort wallType = Main.tile[tileCoordinates.X, tileCoordinates.Y].WallType;

        //ZoneContagion = biomeTileCounts.ContagionTiles > 350;
        //ZoneUndergroundContagion = biomeTileCounts.ContagionTiles > 350 && Player.ZoneRockLayerHeight;
        //ZoneCaesium = biomeTileCounts.CaesiumTiles > 200 && Player.ZoneUnderworldHeight;
        //ZoneCrystal = biomeTileCounts.CrystalTiles > 150;
        //ZoneDarkMatter = biomeTileCounts.DarkTiles > 450;
        //ZoneNearHellcastle = biomeTileCounts.HellCastleTiles > 350 && Player.ZoneUnderworldHeight;
        //ZoneHellcastle = biomeTileCounts.HellCastleTiles > 350 &&
        //                 wallType == ModContent.WallType<ImperviousBrickWallUnsafe>() && Player.ZoneUnderworldHeight;
        //ZoneSkyFortress = biomeTileCounts.SkyFortressTiles > 75;
        //ZoneTropics = biomeTileCounts.TropicsTiles > 200;
        //ZoneTuhrtlOutpost = ZoneTropics && wallType == ModContent.WallType<TuhrtlBrickWallUnsafe>() &&
        //                    Player.ZoneRockLayerHeight;
        ZoneBlight = biomeTileCounts.BlightTiles > 0;
        ZoneTorture = biomeTileCounts.TortureTiles > 0;
        ZoneHumidity = biomeTileCounts.HumidityTiles > 0;
        //ZoneAltDungeon = biomeTileCounts.DungeonAltTiles > 250 && Main.wallDungeon[wallType] && tileCoordinates.Y > Main.worldSurface;
    }
}
