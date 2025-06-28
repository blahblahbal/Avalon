using Avalon.Projectiles.Hostile.Mechasting;
using Avalon.Projectiles.Hostile.WallOfSteel;
using Avalon.Projectiles.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets;

public static class ProjectileSets
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
        ProjectileID.HallowBossRainbowStreak,
		ProjectileID.EyeFire,
        ModContent.ProjectileType<ElectricBolt>(),
        ModContent.ProjectileType<StingerLaser>(),
        ModContent.ProjectileType<Mechastinger>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.PoisonGasTrap>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.PoisonGasTrapStarter>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.FireballTrap>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.FireballTrapStarter>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrap>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrapStarter>(),
		ModContent.ProjectileType<WoSLaserSmall>(),
		ModContent.ProjectileType<WoSCursedFireball>(),
		ModContent.ProjectileType<WoSBeegLaser>(),
		ModContent.ProjectileType<WoSRocket>());
		//ModContent.ProjectileType<Ghostflame>(),
		//ModContent.ProjectileType<WallofSteelLaser>(),
		//,
		//,
		//,
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


	public static readonly bool[] MinionProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.HornetStinger,
        ProjectileID.ImpFireball,
        ProjectileID.MiniRetinaLaser,
        ProjectileID.PygmySpear,
        ProjectileID.UFOLaser,
        ProjectileID.MiniSharkron,
        ProjectileID.StardustCellMinionShot);

    public static readonly bool[] TrapProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.PoisonDart,
        ProjectileID.PoisonDartTrap,
        ProjectileID.Boulder,
        ProjectileID.GeyserTrap,
        ProjectileID.FlamethrowerTrap,
        ProjectileID.SpearTrap,
        ProjectileID.SpikyBallTrap,
        ProjectileID.VenomDartTrap,
        ProjectileID.GasTrap,
        ProjectileID.Explosives,
        ProjectileID.Landmine,
        ProjectileID.FlamesTrap,
        ProjectileID.MiniBoulder,
        ProjectileID.BouncyBoulder,
        ProjectileID.LifeCrystalBoulder,
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.PoisonGasTrap>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.PoisonGasTrapStarter>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.FireballTrap>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.FireballTrapStarter>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrap>(),
        ModContent.ProjectileType<Projectiles.Hostile.TuhrtlOutpost.CannonballTrapStarter>()
    );

    public static readonly bool[] TrueMeleeProjectiles = ProjectileID.Sets.Factory.CreateBoolSet(
        ModContent.ProjectileType<WoodenClubProj>(),
        ModContent.ProjectileType<MarrowMasherProj>(),
        ModContent.ProjectileType<UrchinMaceProj>(),
        ModContent.ProjectileType<HallowedRapierProj>(),
		ModContent.ProjectileType<HallowedRapierVis>(),
		ModContent.ProjectileType<CraniumCrusherProj>(),
        ModContent.ProjectileType<HellboundHalberdProj>());

    public static readonly bool[] EarthRelatedItems = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.BoulderStaffOfEarth,
        ProjectileID.HeatRay,
        ProjectileID.GolemFist,
        ProjectileID.Stynger,
        ProjectileID.PossessedHatchet,
        ModContent.ProjectileType<PossessedFlamesaw>(),
        ModContent.ProjectileType<PossessedFlamesawChop>()
    );
}
