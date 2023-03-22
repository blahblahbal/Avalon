using Terraria.ID;

namespace Avalon.Data.Sets;

public static class Projectile
{
    public static readonly bool[] DontReflect = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.Stinger,
        ProjectileID.RainCloudMoving,
        ProjectileID.RainCloudRaining,
        ProjectileID.BloodCloudMoving,
        ProjectileID.BloodCloudRaining,
        ProjectileID.FrostHydra,
        ProjectileID.InfernoFriendlyBolt,
        ProjectileID.InfernoFriendlyBlast,
        ProjectileID.PhantasmalDeathray,
        ProjectileID.SaucerDeathray,
        ProjectileID.PhantasmalBolt,
        ProjectileID.PhantasmalEye,
        ProjectileID.FlyingPiggyBank,
        ProjectileID.Glowstick,
        ProjectileID.BouncyGlowstick,
        ProjectileID.SpelunkerGlowstick,
        ProjectileID.StickyGlowstick,
        ProjectileID.WaterGun,
        ProjectileID.SlimeGun,
        ProjectileID.SandnadoHostile,
        ProjectileID.HallowBossLastingRainbow,
        ProjectileID.HallowBossRainbowStreak);
        //ModContent.ProjectileType<Ghostflame>(),
        //ModContent.ProjectileType<WallofSteelLaser>(),
        //ModContent.ProjectileType<ElectricBolt>(),
        //ModContent.ProjectileType<HomingRocket>(),
        //ModContent.ProjectileType<StingerLaser>(),
        //ModContent.ProjectileType<DarkCinder>(),
        //ModContent.ProjectileType<DarkFlame>(),
        //ModContent.ProjectileType<DarkGeyser>(),
        //ModContent.ProjectileType<DarkMatterFireball>(),
        //ModContent.ProjectileType<DarkMatterFlamethrower>(),
        //ModContent.ProjectileType<CaesiumFireball>(),
        //ModContent.ProjectileType<CaesiumCrystal>(),
        //ModContent.ProjectileType<CaesiumGas>(),
        //ModContent.ProjectileType<SpikyBall>(),
        //ModContent.ProjectileType<Spike>(),
        //ModContent.ProjectileType<CrystalShard>(),
        //ModContent.ProjectileType<WallofSteelLaserEnd>(),
        //ModContent.ProjectileType<WallofSteelLaserStart>(),
        //ModContent.ProjectileType<CrystalBit>(),
        //ModContent.ProjectileType<CrystalBeam>(),
        //ModContent.ProjectileType<WoSLaserSmall>(),
        //ModContent.ProjectileType<WoSCursedFireball>());

    public static readonly bool[] MinionProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.HornetStinger,
        ProjectileID.ImpFireball,
        ProjectileID.MiniRetinaLaser,
        ProjectileID.PygmySpear,
        ProjectileID.UFOLaser,
        ProjectileID.MiniSharkron,
        ProjectileID.StardustCellMinionShot);
}
