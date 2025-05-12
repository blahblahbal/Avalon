using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee;

public class Sporalash : FlailTemplate
{
	public override int LaunchTimeLimit => 16;
	public override float LaunchSpeed => 18f;
	public override float MaxLaunchLength => 600f;
	public override float RetractAcceleration => 5f;
	public override float MaxRetractSpeed => 16f;
	public override float ForcedRetractAcceleration => 6f;
	public override float MaxForcedRetractSpeed => 16f;
	public override int DefaultHitCooldown => 10;
	public override int SpinHitCooldown => 20;
	public override int MovingHitCooldown => 10;
	public override int DustType => DustID.JunglePlants;

	public override void SetStaticDefaults()
	{
		// These lines facilitate the trail drawing
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
		Projectile.width = 24; // The width of your projectile
		Projectile.height = 24; // The height of your projectile
		Projectile.friendly = true; // Deals damage to enemies
		Projectile.penetrate = -1; // Infinite pierce
		Projectile.DamageType = DamageClass.Melee; // Deals melee damage
		Projectile.usesLocalNPCImmunity = true; // Used for hit cooldown changes in the ai hook
		Projectile.localNPCHitCooldown = 10; // This facilitates custom hit cooldown logic
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));

		// Vanilla flails all use aiStyle 15, but the code isn't customizable so an adaption of that aiStyle is used in the AI method
	}
}
