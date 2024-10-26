using Avalon.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.Phantasm
{
	public class PhantomDeathray : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 32;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 600;
			Projectile.scale = 0.5f;
		}
		public override void AI()
		{
			NPC owner = Main.npc[(int)Projectile.ai[0]];
			Projectile.Center = owner.Center;
			if (!owner.active)
			{
				Projectile.Kill();
			}

			Projectile.rotation = MathHelper.SmoothStep(0, MathHelper.TwoPi * 2 * owner.direction, Projectile.timeLeft / (float)ContentSamples.ProjectilesByType[ModContent.ProjectileType<PhantomDeathray>()].timeLeft) + (Projectile.ai[2] * MathHelper.PiOver2);
			if (Projectile.timeLeft > ContentSamples.ProjectilesByType[ModContent.ProjectileType<PhantomDeathray>()].timeLeft - 10)
				Projectile.scale += 0.1f;

			else if (Projectile.timeLeft < 40)
			{
				Projectile.scale *= 0.9f;
			}

			if (Projectile.ai[2] == 0 && Projectile.timeLeft % 10 == 0)
			{
				PunchCameraModifier modifier = new PunchCameraModifier(Projectile.Center, Main.rand.NextVector2Circular(1, 1), 8f, 10f, 40, 3000f);
				Main.instance.CameraModifiers.Add(modifier);
			}

			float[] samples = new float[3];
			Projectile.velocity = (Projectile.rotation + MathHelper.PiOver2).ToRotationVector2();
			Collision.LaserScan(Projectile.Center, Projectile.velocity, Projectile.width * Projectile.scale, 2400f, samples);
			Dust d = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * Projectile.ai[1], ModContent.DustType<PhantoplasmDust>(), null, 128);
			d.noGravity = true;
			d.scale = Main.rand.NextFloat(1, 3);

			Projectile.ai[1] = 0f;
			int num4;
			for (int num832 = 0; num832 < samples.Length; num832 = num4 + 1)
			{
				Projectile.ai[1] += samples[num832];
				num4 = num832;
			}
			Projectile.ai[1] /= 3;

			Dust d2 = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * Main.rand.NextFloat(Projectile.ai[1]), ModContent.DustType<PhantoplasmDust>(),null,128);
			d2.noGravity = true;
			d2.scale = Main.rand.NextFloat(1, 2);
		}
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if(Projectile.scale < 0.5f)
			{
				return false;
			}

			float collsionPoint = 0;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.ai[1],Projectile.scale * 26, ref collsionPoint);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Asset<Texture2D> tex = TextureAssets.Projectile[Type];
			Rectangle start = new Rectangle(0,0,26,22);
			Rectangle mid = new Rectangle(0, 24, 26, 30);
			Rectangle end = new Rectangle(0, 56, 26, 22);
			Color drawColor = new Color(1f, 1f, 1f, 0f);

			for (int x = 1; x <= 3; x++)
			{
				Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(0, 14).RotatedBy(Projectile.rotation), start, drawColor, Projectile.rotation, new Vector2(start.Width / 2, start.Height), (Projectile.scale * x), SpriteEffects.None);
				int max = (int)Math.Floor((Projectile.ai[1]) / mid.Height);
				for (int i = 0; i < max; i++)
				{
					Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(0, (mid.Height * i)).RotatedBy(Projectile.rotation), mid, drawColor, Projectile.rotation, new Vector2(start.Width / 2, 0), new Vector2((Projectile.scale * x) + (((float)Math.Sin(Main.timeForVisualEffects * 0.2f + (i * 0.5f))) * 0.3f * Projectile.scale), 1f), SpriteEffects.None);
				}
				Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(0, mid.Height * max).RotatedBy(Projectile.rotation), new Rectangle(mid.X, mid.Y, mid.Width, (int)(mid.Height * ((Projectile.ai[1] / 30f) - max))), drawColor, Projectile.rotation, new Vector2(start.Width / 2, 0), new Vector2((Projectile.scale * x) + (((float)Math.Sin(Main.timeForVisualEffects * 0.2f + (max * 0.5f))) * 0.3f * Projectile.scale), 1), SpriteEffects.None);

				Main.EntitySpriteDraw(tex.Value, Projectile.Center - Main.screenPosition + new Vector2(0, Projectile.ai[1] - 1).RotatedBy(Projectile.rotation), end, drawColor, Projectile.rotation, new Vector2(start.Width / 2, 0), new Vector2((Projectile.scale * x) + (((float)Math.Sin(Main.timeForVisualEffects * 0.2f + (max * 0.5f))) * 0.3f * Projectile.scale), 1), SpriteEffects.None);

				drawColor *= 0.5f;
			}
			if (Projectile.ai[2] != 0)
				return false;

			Asset<Texture2D> glow = ModContent.Request<Texture2D>("Avalon/Assets/Textures/SparklyBig");
			for (int i = 0; i < 3; i++)
			{
				Main.EntitySpriteDraw(glow.Value, Projectile.Center - Main.screenPosition, null, new Color(1f, 0.6f, 0.8f, 0f), (float)(Main.timeForVisualEffects * 0.1f) + (MathHelper.TwoPi / 3 * i), glow.Size() / 2, Projectile.scale * (2f + Main.masterColor), SpriteEffects.None);
			}
			return false;
		}
	}
}
