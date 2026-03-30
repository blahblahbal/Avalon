using Avalon.Common.Interfaces;
using Avalon.Common.Templates;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class RhodiumGreatswordSlash : EnergySlashTemplate, ISyncedOnHitEffect
{
	public override LocalizedText DisplayName => ModContent.GetInstance<RhodiumGreatsword>().DisplayName;
	public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.TheHorsemansBlade}";
	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.penetrate = 3;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		float percent = Main.player[Projectile.owner].GetModPlayer<RhodiumGreatswordPlayer>().Power / (float)RhodiumGreatswordPlayer.maxPower;
		DrawSlash(new Color(0.5f, 0.05f, 0.3f, 1f) * (0.2f + percent * 0.8f), new Color(0.9f, 0.2f, 0.3f, 0.6f) * (0.2f + percent * 0.8f), new Color(1f, 0.2f, 0.2f, 0.8f) * (0.2f + percent * 0.8f), new Color(0.7f, 0.4f, 0.2f, 0f) * (0.4f + percent * 0.6f), 0, 1f, 0f, 0.1f, -0.3f, true, true);
		//DrawSlash(new Color(0.5f, 0.05f, 0.3f, 1f), new Color(0.9f, 0.2f, 0.3f, 0.6f), new Color(1f, 0.2f, 0.2f, 0.8f), new Color(0.7f, 0.4f, 0.2f, 0f), 0, 1f, 0f, 0.1f, -0.3f, true, true);
		return false;
	}
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];

		int dType = ModContent.DustType<SimpleColorableGlowyDust>();

		float percent = Main.player[Projectile.owner].GetModPlayer<RhodiumGreatswordPlayer>().Power / (float)RhodiumGreatswordPlayer.maxPower;

		ClassExtensions.GetPointOnSwungItemPath(60f, 110f, 0.8f + 0.2f * Main.rand.NextFloat(), player.HeldItem.scale, out var location2, out var outwardDirection2, player);
		Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
		Dust d = Dust.NewDustPerfect(location2, dType, vector2 * 2f, 100);
		d.noGravity = true;
		d.velocity *= 2f;
		d.scale *= 0.7f;
		d.color = Color.Lerp(new Color(1f, 0.5f, 0.3f, 0f), new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f),Main.rand.NextFloat()) * (0.4f + percent * 0.6f);
		d.fadeIn = Main.rand.NextFloat(1.3f);

		for (int j = 0; j < 2; j++)
		{
			ClassExtensions.GetPointOnSwungItemPath(60f, 70f, 0.2f + 0.8f * Main.rand.NextFloat(), player.HeldItem.scale, out var location22, out var outwardDirection22, player);
			Vector2 vector22 = outwardDirection22.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);
			Dust d2 = Dust.NewDustPerfect(location22, dType, vector22 * 2f, 100);
			d2.noGravity = true;
			d2.velocity *= 2f;
			d2.scale *= 0.9f;
			d2.color = new Color(0.8f, 0.2f, Main.rand.NextFloat(0.2f, 0.4f), 0f) * (0.2f + percent * 0.6f);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Main.player[Projectile.owner].GetModPlayer<RhodiumGreatswordPlayer>().Power);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Main.player[Projectile.owner].GetModPlayer<RhodiumGreatswordPlayer>().Power = reader.ReadByte();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		RhodiumGreatswordPlayer rgp = Main.player[Projectile.owner].GetModPlayer<RhodiumGreatswordPlayer>();
		rgp.Power++;
		if(RhodiumGreatswordPlayer.maxPower < rgp.Power)
		{
			rgp.Power = RhodiumGreatswordPlayer.maxPower;
		}
		Projectile.netUpdate = true;
	}
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		Vector2 point = Main.rand.NextVector2FromRectangle(target.Hitbox);
		for (int i = 0; i < 3; i++)
		{
			SparkleParticle p = new();
			p.ColorTint = new Color(1f, 0.3f, Main.rand.NextFloat(0.3f, 0.6f), 0f);
			p.HighlightColor = new Color(1f, 0.7f, 0.4f, 0f);
			p.FadeInEnd = Main.rand.NextFloat(4, 7);
			p.FadeOutStart = p.FadeInEnd + 5;
			p.FadeOutEnd = Main.rand.NextFloat(17, 21);
			p.Scale = new Vector2(4, 2);
			p.Rotation = Main.rand.NextFloat(-0.1f, 0.1f) + (i * MathHelper.TwoPi / 3f);
			p.DrawHorizontalAxis = false;
			ParticleSystem.NewParticle(p, point);
		}
	}
}
