using Avalon.Buffs.Debuffs;
using Avalon.Core;
using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Misc;

public class StasisShot : ModProjectile
{
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.friendly = true;
		Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.Opacity = 0;
	}
	private SlotId _blizzardSound;
	public override void AI()
	{
		if (Projectile.ai[2] > 0)
		{
			Projectile.ai[2]++;
			int newWidth = (int)MathHelper.SmoothStep(0, 64 + Projectile.ai[0] * 128, MathHelper.Min(Projectile.ai[2] / 60f + 0.1f, 1));//Utils.Remap(Projectile.ai[2], 0, 60, 0, 64 + Projectile.ai[0] * 128);
			Projectile.Resize(newWidth, newWidth);
			if (Projectile.ai[2] < 30)
			{
				Projectile.Opacity = Projectile.ai[2] / 60;
			}
			else if (Projectile.ai[2] > 200 + (Projectile.ai[0] * 100))
			{
				Projectile.Opacity -= 1 / 60f;
				if (Projectile.ai[2] > 210 + (Projectile.ai[0] * 100))
					Projectile.damage = 0;
				if (Projectile.ai[2] > 260 + (Projectile.ai[0] * 100))
				{
					Projectile.Kill();
				}
			}
			SoundEngine.TryGetActiveSound(_blizzardSound, out var sound);
			if(sound == null)
			{
				_blizzardSound = SoundEngine.PlaySound(SoundID.BlizzardInsideBuildingLoop with { MaxInstances = 10, Type = SoundType.Sound }, Projectile.Center);
			}
			else
			{
				sound.Volume = Projectile.Opacity * 6 * Projectile.ai[0];
				sound.Pitch = 1 + Projectile.Opacity;
			}
			if (Main.rand.NextBool())
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.width / 2), ModContent.DustType<SimpleColorableGlowyDust>());
				d.color = Color.Lerp(Color.White, Color.Lerp(Color.Blue, Color.Cyan, Main.rand.NextFloat()), Main.rand.NextFloat(0.6f)) with { A = 64 } * Projectile.Opacity;
				d.noGravity = true;
				d.velocity = d.position.DirectionTo(Projectile.Center).RotatedBy(-MathHelper.PiOver2) * Main.rand.NextFloat(3);
				d.noLight = true;
			}
			return;
		}
		float radius = 14 * MathHelper.Lerp(Projectile.ai[0], 1, 0.5f);
		var d2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(radius,radius), ModContent.DustType<SimpleColorableGlowyDust>());
		d2.velocity = Projectile.velocity * 0.1f + Main.rand.NextVector2Square(-1, 1) * Projectile.ai[0];
		d2.noGravity = true;
		d2.color = Color.Lerp(Color.White, Color.Lerp(Color.Blue, Color.Cyan, Main.rand.NextFloat()), Main.rand.NextFloat(0.6f)) with { A = 64 };
		d2.noLight = true;
		Projectile.rotation += Projectile.direction * 0.5f;
		//if (Projectile.ai[1] == 0f)
		//{
		//	Projectile.oldPosition = Projectile.position;
		//	Projectile.ai[1] = 1f;
		//}
		//for (float i = 0; i < 1; i += 0.2f)
		//{
		//	var d = Dust.NewDustPerfect(Vector2.Lerp(Projectile.Center + Projectile.velocity, Projectile.oldPosition + (Projectile.Size / 2), i), ModContent.DustType<SimpleColorableGlowyDust>());
		//	d.velocity = Projectile.velocity * 0.1f + Main.rand.NextVector2Square(-1, 1);
		//	d.noGravity = true;
		//	d.color = Color.Lerp(Color.White, Color.Lerp(Color.Blue, Color.Cyan, Main.rand.NextFloat()), Main.rand.NextFloat(0.6f)) with { A = 64 };
		//	d.scale = 0.5f + Projectile.ai[0];
		//	d.noLight = true;
		//}
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		Vector2 closePoint = targetHitbox.ClosestPointInRect(Projectile.Center);
		if (Projectile.ai[2] > 0 && closePoint.Distance(Projectile.Center) < Projectile.width / 2)
			return true;
		return base.Colliding(projHitbox, targetHitbox);
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Item69 with { pitch = -Projectile.ai[0] * 0.5f }, Projectile.position);
		Projectile.velocity = Vector2.Zero;
		Projectile.tileCollide = false;
		Projectile.ai[2] = 1;
		Projectile.knockBack = 0;
		Projectile.damage /= 8;

		for (int i = 0; i < 35; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.width / 2), ModContent.DustType<SimpleColorableGlowyDust>());
			d.color = Color.Lerp(Color.White, Color.Lerp(Color.Blue, Color.Cyan, Main.rand.NextFloat()), Main.rand.NextFloat(0.6f)) with { A = 64 };
			d.noGravity = true;
			d.velocity = Main.rand.NextVector2Circular(3, 3);
			d.noLight = true;
		}
		for(int i = 0; i < 35; i++)
		{
			Dust d = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2, Projectile.width / 2), DustID.Snow);
			d.noGravity = true;
			d.velocity = Main.rand.NextVector2Circular(6, 6) * (1f + Projectile.ai[0]);
			d.fadeIn += Projectile.ai[0] * 2;
			d.noLight = true;
			d.scale += Main.rand.NextFloat(Projectile.ai[0]);
		}
		return false;
	}
	public override bool PreDraw(ref Color lightColor)
	{
		if (Projectile.ai[2] == 0)
		{
			var t = AssetReferences.Projectiles.Ranged.Misc.StasisSnowball.Asset;
			DrawData ball = new(t.Value, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, t.Size() / 2, MathHelper.Lerp(Projectile.ai[0],1,0.5f), SpriteEffects.None);
			for(int i = 0; i< 6; i++)
			{
				Main.EntitySpriteDraw(ball with { position = ball.position - Projectile.velocity * i, color = Color.Lerp(ball.color, lightColor.MultiplyRGB(Color.DodgerBlue), i / 6f) with { A = 0 } * (1f - i / 6f) * 0.4f, scale = ball.scale * (1f - i * 0.1f)});
			}
			Main.EntitySpriteDraw(ball);

			return false;
		}

		DrawData d = new(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 128, 128), Color.White with { A = 64 } * Projectile.Opacity, (float)(Main.timeForVisualEffects * 0.1f), new Vector2(64, 64), Projectile.width / 128f, SpriteEffects.None);
		Main.EntitySpriteDraw(d with { sourceRect = new Rectangle(0, 130, 128, 128), color = lightColor.MultiplyRGBA(new Color(0, 32, 128, 0)) * Projectile.Opacity * 0.3f });
		Main.EntitySpriteDraw(d with { sourceRect = new Rectangle(0, 130, 128, 128), color = lightColor.MultiplyRGBA(new Color(128, 128, 128, 64)) * Projectile.Opacity * 0.6f, scale = d.scale + Vector2.One * 0.3f * Projectile.Opacity });
		for (int i = 1; i < 5; i++)
		{
			Main.EntitySpriteDraw(d with { sourceRect = new Rectangle(0, 260, 128, 128), color = lightColor.MultiplyRGBA(Color.Lerp(new Color(1f, 1f, 1f, 0), new Color(0, 32, 128, 0), (i - 1) / 5f)) * ((float)Math.Sin(Main.timeForVisualEffects * 0.1f + i * 255) + 1f) * Projectile.Opacity, rotation = d.rotation * 0.25f * i, effect = (SpriteEffects)i });
		}
		Main.EntitySpriteDraw(d);
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		base.OnKill(timeLeft);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CryoStasis>(), 60 * 3);
	}
}
