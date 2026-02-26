using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Longbows;

public class IridiumLongbowHeld : LongbowTemplate
{
	public override void SetDefaults()
	{
		base.SetDefaults();
		DrawOffsetX = -16;
		DrawOriginOffsetY = -25;
	}
	public override bool ArrowEffect(Projectile projectile, float Power, byte variant = 0)
	{
		int arrows = (int)Math.Floor(Power * 3);
		if (arrows > 0)
		{
			for (int i = 0; i < 3; i++)
			{
				SparkleParticle p = new();
				p.ColorTint = new Color(Main.rand.NextFloat(0.6f, 0.8f), 1f, 0.6f, 0f);
				p.FadeInEnd = Main.rand.NextFloat(2, 5);
				p.FadeOutStart = p.FadeInEnd;
				p.FadeOutEnd = Main.rand.NextFloat(13, 18);
				p.Scale = new Vector2(4, 2);
				p.Rotation = (i / 3f * MathHelper.TwoPi) + Main.rand.NextFloat(-0.1f, 0.1f) + MathHelper.PiOver2;
				p.DrawHorizontalAxis = false;
				ParticleSystem.NewParticle(p, Projectile.Center);
			}
			SoundEngine.PlaySound(SoundID.Item94 with { Volume = 0.75f, Pitch = 0.2f}, projectile.position);
			projectile.GetGlobalProjectile<IridiumLongbowGlobalProj>().Arrows = arrows;
		}
		return false;
	}
	public override void PostDraw(Color lightColor)
	{
		if (Main.player[Projectile.owner].channel)
		{
			Color arrowColor = Color.Lerp(Color.Chartreuse, Color.Green, Main.masterColor) with { A = 0 };
			DrawArrow(arrowColor * Power, Vector2.Zero, true);
		}
	}
}
public class IridiumLongbowGlobalProj : GlobalProjectile
{
	public override bool InstancePerEntity => true;
	public int Arrows = 0;

	public override void AI(Projectile projectile)
	{
		if (Arrows == 0)
			return;
		Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ModContent.DustType<SimpleColorableGlowyDust>());
		d.velocity += projectile.velocity * 0.3f;
		d.color = new Color(Main.rand.NextFloat(0.6f,0.8f), 1f, 0.6f, 0f);
		d.noGravity = true;
		d.scale = 1f;
	}
	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Arrows == 0)
			return;
		List<NPC> npcs = new List<NPC>();
		foreach(NPC n in Main.ActiveNPCs)
		{
			if (n == target)
				continue;
			if(n.CanBeChasedBy() && n.Center.Distance(projectile.Center) < 500)
			{
				npcs.Add(n);
			}
		}
		npcs.Sort((x,y) => x.Center.Distance(projectile.Center).CompareTo(y.Center.Distance(projectile.Center)));
		int type = ModContent.ProjectileType<IridiumLongbowEnergyArrow>();
		for (int i = 0; i < Math.Min(npcs.Count,Arrows); i++)
		{
			Projectile.NewProjectile(projectile.GetSource_FromThis(),projectile.Center, projectile.Center.DirectionTo(npcs[i].Center) * 8, type, projectile.damage / 3, projectile.knockBack / 2, projectile.owner, -100, ai2: target.whoAmI);
		}
	}
}
