using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets;

[ReinitializeDuringResizeArrays]
public static class ProjectileSets
{
	public static bool[] CanBeReflected = ProjectileID.Sets.Factory.CreateNamedSet("CanBeReflected").Description("Vanilla's logic for determining if something can be reflected should also count these projectiles").RegisterBoolSet();
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
		ProjectileID.EyeFire);
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
        ProjectileID.LifeCrystalBoulder
    );

	public static readonly bool[] TrueMeleeProjectiles = ProjectileID.Sets.Factory.CreateBoolSet();

    public static readonly bool[] EarthRelatedItems = ProjectileID.Sets.Factory.CreateBoolSet(
        ProjectileID.BoulderStaffOfEarth,
        ProjectileID.HeatRay,
        ProjectileID.GolemFist,
        ProjectileID.Stynger,
        ProjectileID.PossessedHatchet
    );
}
