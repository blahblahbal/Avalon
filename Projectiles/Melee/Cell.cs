using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Common.Templates;
using Avalon.Dusts;

namespace Avalon.Projectiles.Melee;

public class Cell : FlailTemplate
{
	public override int LaunchTimeLimit => 16;
	public override float LaunchSpeed => 14f;
	public override float MaxLaunchLength => 700f;
	public override float RetractAcceleration => 3f;
	public override float MaxRetractSpeed => 13f;
	public override float ForcedRetractAcceleration => 6f;
	public override float MaxForcedRetractSpeed => 16f;
	public override int DefaultHitCooldown => 10;
	public override int SpinHitCooldown => 20;
	public override int MovingHitCooldown => 10;
	public override int DustType => ModContent.DustType<ContagionWeapons>();
	public override string ChainTexturePath => "Avalon/Projectiles/Melee/Cell_Chain";

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		base.SetStaticDefaults();
	}

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
        Projectile.width = 20; // The width of your projectile
        Projectile.height = 20; // The height of your projectile
        Projectile.friendly = true; // Deals damage to enemies
        Projectile.penetrate = -1; // Infinite pierce
        Projectile.DamageType = DamageClass.Melee; // Deals melee damage
        Projectile.usesLocalNPCImmunity = true; // Used for hit cooldown changes in the ai hook
        Projectile.localNPCHitCooldown = 10; // This facilitates custom hit cooldown logic
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2));
    }
}
