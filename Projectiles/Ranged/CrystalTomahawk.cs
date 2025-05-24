using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged
{
	public class CrystalTomahawk : ModProjectile
	{
		private static Asset<Texture2D>? trailTexture;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			trailTexture = ModContent.Request<Texture2D>(Texture + "_Trail");
		}
		public override void SetDefaults()
		{
			Projectile.Size = new Vector2(36);
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 60;
			Projectile.DamageType = DamageClass.Ranged;
		}
		public override void AI()
		{
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation += Projectile.velocity.Length() * 0.02f * Projectile.direction;

			Color[] Colors = { Color.LightSkyBlue, Color.Magenta, Color.White, Color.Magenta };
			Color Color1 = ClassExtensions.CycleThroughColors(Colors, 60) * 0.5f;

			Lighting.AddLight(Projectile.Center, new Vector3(Color1.R / 255f, Color1.G / 255f, Color1.B / 255f));

			//int[] Dusts = { DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
			int[] Dusts = { DustID.BlueCrystalShard, DustID.PinkCrystalShard, DustID.PurpleCrystalShard };
			if (Main.rand.NextBool(3))
			{
				Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), Dusts[Main.rand.Next(3)], Projectile.velocity, 64, default, 1f);
				dust2.fadeIn = 0.6f + Main.rand.NextFloat() * 0.5f;
				dust2.noGravity = true;
				dust2.noLightEmittence = true;
			}

			Projectile.ai[0]++;
			if (Projectile.ai[0] > 40)
			{
				Projectile.velocity.X *= 0.96f;
				if (Projectile.velocity.Y < 24)
				{
					Projectile.velocity.Y += 0.3f;
				}
			}
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = Projectile.width - 16;
			height = Projectile.height - 16;
			return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
		}

		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			int[] Dusts = { DustID.BlueCrystalShard, DustID.PinkCrystalShard, DustID.PurpleCrystalShard };
			int[] DustsGlow = { DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
			for (int i = 0; i < 10; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), DustsGlow[Main.rand.Next(3)], Main.rand.NextVector2Circular(12, 12), 64, default, 1f);
				dust2.fadeIn = Main.rand.NextFloat(0, 1);
				dust2.noGravity = true;
				//dust2.velocity += -Projectile.oldVelocity;
			}
			for (int i = 0; i < 10; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), Dusts[Main.rand.Next(3)], Main.rand.NextVector2Circular(12, 12), 64, default, 1f);
				dust2.fadeIn = Main.rand.NextFloat(0, 1);
				dust2.noGravity = !Main.rand.NextBool(5);
			}
			if (Main.myPlayer == Projectile.owner)
			{
				for (int i = 0; i < 4; i++)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2CircularEdge(14, 14) * Main.rand.NextFloat(0.3f, 1f), ProjectileID.CrystalShard, Projectile.damage / 4, 0, Projectile.owner);
				}
				//Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AeonExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
			}
			if (Projectile.ai[1] == 0)
				ParticleSystem.AddParticle(new CrystalSparkle(), Projectile.Center + Vector2.Normalize(Projectile.velocity) * 20f, Vector2.Zero, default);
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.ai[1] = 1;
			int[] DustsGlow = { DustID.IceTorch, DustID.HallowedTorch, DustID.WhiteTorch };
			int[] Dusts = { DustID.BlueCrystalShard, DustID.PinkCrystalShard, DustID.PurpleCrystalShard };
			for (int i = 0; i < 10; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), DustsGlow[Main.rand.Next(3)], Main.rand.NextVector2Circular(12, 12), 64, default, 1f);
				dust2.fadeIn = Main.rand.NextFloat(0, 1);
				dust2.noGravity = true;
				dust2.velocity += -Projectile.oldVelocity * 0.5f;
				//dust2.velocity += -Projectile.oldVelocity;
			}
			for (int i = 0; i < 10; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), Dusts[Main.rand.Next(3)], Main.rand.NextVector2Circular(12, 12), 64, default, 1f);
				dust2.fadeIn = Main.rand.NextFloat(0, 1);
				dust2.noGravity = !Main.rand.NextBool(5);
				dust2.velocity += -Projectile.oldVelocity * 0.5f;
			}
			//for (int i = 0; i < 10; i++)
			//{
			//    Dust dust2 = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(18, 18), DustsGlow[Main.rand.Next(3)], Main.rand.NextVector2Circular(9, 9), 64, default, 1f);
			//    dust2.fadeIn = Main.rand.NextFloat(0, 1);
			//    dust2.noGravity = true;
			//    dust2.velocity += -Projectile.oldVelocity * 0.5f;
			//}
			//SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

			//if (Projectile.velocity.X != oldVelocity.X)
			//{
			//    Projectile.velocity.X = -oldVelocity.X * 0.7f;
			//}
			//if (Projectile.velocity.Y != oldVelocity.Y)
			//{
			//    Projectile.velocity.Y = -oldVelocity.Y * 0.7f;
			//}

			//Projectile.penetrate--;
			return true;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			int frameHeight = TextureAssets.Projectile[Type].Value.Height / Main.projFrames[Projectile.type];
			Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, TextureAssets.Projectile[Type].Value.Width, frameHeight);
			int length = ProjectileID.Sets.TrailCacheLength[Projectile.type];
			SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Color[] Colors = { Color.LightSkyBlue, Color.Magenta, Color.White, Color.Magenta };
			Color Color1 = ClassExtensions.CycleThroughColors(Colors, 60) * 0.5f;
			Color1.A = 64;

			for (int i = 1; i < length; i++)
			{
				float multiply = (float)(length - i) / length;
				Main.EntitySpriteDraw(trailTexture.Value, Projectile.oldPos[i] - Main.screenPosition + (Projectile.Size / 2f), frame, Color1 * multiply, Projectile.oldRot[i], new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale, spriteEffects, 0);
			}

			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.position - Main.screenPosition + (Projectile.Size / 2f), frame, Color.Lerp(lightColor, Color.White, 0.5f) * Projectile.Opacity, Projectile.rotation, new Vector2(TextureAssets.Projectile[Type].Value.Width, frameHeight) / 2, Projectile.scale, spriteEffects, 0);

			return false;
		}
	}
}
