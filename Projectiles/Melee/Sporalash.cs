using Avalon.Common;
using Avalon.Common.Templates;
using Terraria;
using Terraria.ID;

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
	public override int ChainVariants => 16;

	public override void SetStaticDefaults()
	{
		// These lines facilitate the trail drawing
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 24;
		Projectile.height = 24;
	}

	public override bool EmitDust(int dustType, int antecedent, int consequent, float fadeIn, bool noGravity, float scale, byte alpha)
	{
		if (Projectile.velocity.Length() > 3 || CurrentAIState == AIState.Spinning) // The base method does not specify conditions for spawning the dust, so you are able to specify anything here
		{
			if (CurrentAIState == AIState.Spinning)
			{
				fadeIn = Main.rand.NextFloat(0.7f, 1.2f);
			}
			else
			{
				fadeIn = Main.rand.NextFloat(0.9f, 1.3f);
			}
			antecedent = 2;
			consequent = 3;
			dustType = DustID.JunglePlants;
			return base.EmitDust(dustType, antecedent, consequent, fadeIn, noGravity, scale, alpha);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(3));
		}
		base.ModifyHitNPC(target, ref modifiers);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.Poisoned, TimeUtils.SecondsToTicks(3));
		}
		base.ModifyHitPlayer(target, ref modifiers);
	}
}
