using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Spears;

public class ObsidianGlaiveProj : SpearTemplate2
{
	public override LocalizedText DisplayName => ModContent.GetInstance<ObsidianGlaive>().DisplayName;
	protected override float HoldoutRangeMax => 170;
	protected override float HoldoutRangeMin => 40;
	public override void SetDefaults()
	{
		base.SetDefaults();
	}
	public override bool PreDraw(ref Color lightColor)
	{
		return base.PreDraw(ref lightColor);
	}

	public override void PostAI()
	{
		Player player = Main.player[Projectile.owner];
		Projectile.position.Y += 5 * player.gravDir;
		Projectile.position.X += player.direction * 3;
		float duration = player.itemAnimationMax;

		Projectile.velocity = Projectile.velocity.RotatedBy(player.direction * (1 / duration) * Projectile.ai[2]);

		Dust d2 = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, 0, 0);
		d2.customData = 0;
		d2.noGravity = true;
		d2.scale += 0.7f;
		d2.velocity += Projectile.velocity * 4;
		d2.velocity.Y *= 0.8f;
		if (Main.rand.NextBool(4))
		{
			Dust d = Dust.NewDustDirect(Projectile.position - Projectile.velocity * 3, Projectile.width, Projectile.height, DustID.Torch, 0, 0);
			d.customData = 0;
			d.fadeIn = 0.7f;
			d.velocity += Projectile.velocity * 4;
			d.velocity.Y *= 0.8f;
		}
		if (Main.rand.NextBool(5))
		{
			Dust d = Dust.NewDustDirect(Projectile.position - Projectile.velocity * 3, Projectile.width, Projectile.height, DustID.Obsidian, 0, 0, 128);
			d.customData = 0;
			d.noGravity = true;
			d.fadeIn = 1f;
			d.velocity += Projectile.velocity * 6;
			d.velocity.Y *= 0.5f;
		}
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(BuffID.OnFire3, 160);
		}
	}
}