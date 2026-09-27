using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Common.Interfaces;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Spears;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Spears;

public class DarklightLanceProjectile : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<DarklightLance>().DisplayName;
	protected override float HoldoutRangeMax => 270;
	protected override float HoldoutRangeMin => 40;

	public override void AI()
	{
		base.AI();
		Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<GlowySoulDust>());
		d.noGravity = true;
		d.velocity += Projectile.velocity * 3;
		d.customData = GlowySoulDust.SoulType.Fright;
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		var gnpc = target.GetGlobalNPC<DarklightLanceDebuffNPC>();
		int lightIndex = -1;
		int nightIndex = -1;

		int dustType = ModContent.DustType<GlowySoulDust>();
		int explosionAI = -1;

		float multiplier = target.GetGlobalNPC<AvalonGlobalNPCInstance>().Pathogen ? 1.5f : 1;

		if (gnpc.Light)
		{
			explosionAI = 0;
			if (!gnpc.Night)
				SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			lightIndex = target.FindBuffIndex(ModContent.BuffType<DarklightLanceLight>());
			modifiers.FlatBonusDamage += DarklightLanceDebuffNPC.LifeRegenLoss * target.buffTime[lightIndex] / 120f * multiplier;
		}
		if (gnpc.Night)
		{
			explosionAI = 1;
			nightIndex = target.FindBuffIndex(ModContent.BuffType<DarklightLanceNight>());
			modifiers.FlatBonusDamage += DarklightLanceDebuffNPC.LifeRegenLoss * target.buffTime[nightIndex] / 120f * multiplier;
			if (gnpc.Light)
			{
				explosionAI = 2;
				modifiers.FlatBonusDamage += DarklightLanceDebuffNPC.BonusLifeRegenLoss * Math.Min(target.buffTime[lightIndex], target.buffTime[nightIndex]) / 120f * multiplier;
			}
		}
		if(explosionAI > -1)
		{
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DarklightLanceExplosion>(), 0, 0, Projectile.owner, explosionAI);
		}
		if(nightIndex != -1)
			target.RequestBuffRemoval(target.buffType[nightIndex]);
		if (lightIndex != -1)
			target.RequestBuffRemoval(target.buffType[lightIndex]);
		//Main.NewText(modifiers.FlatBonusDamage.Value);
	}
}
public class DarklightLanceExplosion : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<DarklightLance>().DisplayName;
	public override string Texture => ModContent.GetInstance<DarklightLance>().Texture;
	public override bool? CanDamage()
	{
		return false;
	}
	public override void SetDefaults()
	{
		Projectile.aiStyle = -1;
		Projectile.width = Projectile.height = 0;
		Projectile.hide = true;
	}
	public override void AI()
	{
		SoundEngine.PlaySound(Sounds.Item.FireballImpact.Asset with {PitchVariance = 0.3f, pitch = 0.3f ,MaxInstances = 10}, Projectile.position);
		int dustType = ModContent.DustType<GlowySoulDust>();
		if (Projectile.ai[0] == 0 || Projectile.ai[0] == 2)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
				d.noGravity = true;
				d.velocity *= 3f;
				d.velocity += Projectile.oldVelocity * 2;
				d.scale *= 1.5f;

				d.customData = GlowySoulDust.SoulType.Light;
			}
		}
		if (Projectile.ai[0] == 1 || Projectile.ai[0] == 2)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
				d.noGravity = true;
				d.velocity *= 3f;
				d.velocity += Projectile.oldVelocity * 2;
				d.scale *= 1.5f;

				d.customData = GlowySoulDust.SoulType.Night;
			}
		}
		if (Projectile.ai[0] == 2)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, dustType);
				d.noGravity = true;
				d.velocity *= 5f;
				d.velocity += Projectile.oldVelocity * 2;
				d.scale *= 2;

				d.customData = GlowySoulDust.SoulType.Fright;
			}
		}
		Projectile.Kill();
	}
}