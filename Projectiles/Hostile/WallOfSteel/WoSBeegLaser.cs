	using System;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using ReLogic.Content;
using Terraria;
	using Terraria.DataStructures;
	using Terraria.Graphics.Shaders;
	using Terraria.ID;
	using Terraria.ModLoader;

	namespace Avalon.Projectiles.Hostile.WallOfSteel;

	public class WoSBeegLaser : ModProjectile
	{
		//private Color laserColor;
		//private readonly Color[] colorArray = new Color[3];
		//private int colorShift;
		private static Asset<Texture2D> BeamMiddleTexture;
		private static Asset<Texture2D> BeamStartTexture;
		private static Asset<Texture2D> BeamEndTexture;
		private static Asset<Texture2D> Gradient;

	public override void Load()
	{
		BeamMiddleTexture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WoSBeamMiddle");
		BeamStartTexture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WoSBeamStart");
		BeamEndTexture = ModContent.Request<Texture2D>("Avalon/Assets/Textures/WoSBeamEnd");
		Gradient = ModContent.Request<Texture2D>("Avalon/Assets/Shaders/Gradient");
	}

	public override void SetDefaults()
	{
		Projectile.width = 8;
		Projectile.height = 8;
		Projectile.aiStyle = -1;
		Projectile.alpha = 0;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.tileCollide = false;
		Projectile.penetrate = -1;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.timeLeft = 3600;
		Projectile.tileCollide = false;
	}
	public override void PostDraw(Color lightColor)
	{
		Projectile p = Projectile;

		float num204 = Projectile.localAI[1];

		//colorArray[0] = new Color(255, 0, 0, 255); // TODO: make the laser shift colors better
		//colorArray[1] = new Color(255, 0, 0, 255);
		//colorArray[2] = new Color(255, 0, 0, 255);

		//colorShift++;
		//if (colorShift > 60)
		//{
		//	colorShift = 0;
		//}

		//if (colorShift <= 15)
		//{
		//	laserColor = colorArray[0];
		//}
		//else if (colorShift > 15 && colorShift <= 30 || colorShift > 45 && colorShift <= 61)
		//{
		//	laserColor = colorArray[1];
		//}
		//else if (colorShift > 30 && colorShift <= 45)
		//{
		//	laserColor = colorArray[2];
		//}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

		float frequency = 4f;
		float varianceReduction = 3f;
		float saturation = MathF.Sin((float)Main.timeForVisualEffects / frequency) / varianceReduction + (1 - 1f / varianceReduction);

		GameShaders.Misc["Avalon:WOSLaser"].UseOpacity(1f);
		GameShaders.Misc["Avalon:WOSLaser"].UseColor(Color.Red);
		GameShaders.Misc["Avalon:WOSLaser"].UseSaturation(saturation);
		GameShaders.Misc["Avalon:WOSLaser"].UseImage1(Gradient);
		GameShaders.Misc["Avalon:WOSLaser"].Apply();

		// regular fade in/out
		//float fadeIn = (Projectile.timeLeft > (3600 - 44) ? Projectile.timeLeft - (3600 - 44) : 0) / 44f;
		//float fadeInEased = 1 - MathF.Sqrt(1 - MathF.Pow(fadeIn, 5));
		//float fadeOut = (Projectile.timeLeft < 44 + 3421 ? (44 - (Projectile.timeLeft - 3421)) : 0) / 44f;
		//float fadeOutEased = MathF.Sin(fadeOut * MathHelper.Pi / 2);
		//float fadeInOutEased = fadeInEased + fadeOutEased;

		// regular fade in, flickering fade out
		float fadeIn = (Projectile.timeLeft > (3600 - 44) ? Projectile.timeLeft - (3600 - 44) : 0) / 44f;
		float fadeInEased = 1 - MathF.Sqrt(1 - MathF.Pow(fadeIn, 5));
		float fadeOut = (Projectile.timeLeft < 88 + 3421 ? (88 - (Projectile.timeLeft - 3421)) : 0) / 88f;
		float fadeOutEased = (88f - fadeOut * 88f > 33f) ? MathF.Sin(fadeOut * MathHelper.Pi / 2) : 0;
		float fadeOut2 = fadeOutEased == 0 ? (Projectile.timeLeft < 88 + 3421 ? (88 - (Projectile.timeLeft - 3421)) : 0) / 88f : 0;
		float fadeOutElastic = fadeOut2 == 0 ? 0 : fadeOut2 == 1 ? 1 : MathF.Pow(2f, -4f * fadeOut2) * MathF.Sin((fadeOut2 * 10f - 0.75f) * MathHelper.TwoPi) + 1f;
		float fadeInOutEased = fadeInEased + fadeOutEased + fadeOutElastic;

		Main.EntitySpriteDraw(BeamEndTexture.Value, p.Center - Main.screenPosition, null, Color.White, Projectile.rotation, BeamEndTexture.Size() / 2f, new Vector2(Projectile.scale - fadeInOutEased, Projectile.scale), SpriteEffects.None, 0);
		num204 -= (BeamEndTexture.Value.Height / 2 + BeamStartTexture.Value.Height) * Projectile.scale;
		Vector2 center2 = p.Center;
		center2 += Projectile.velocity * Projectile.scale * BeamStartTexture.Value.Height / 2f;
		if (num204 > 0f)
		{
			float num205 = 0f;
			var rectangle7 = new Rectangle(0, 16 * (Projectile.timeLeft / 3 % 5), BeamStartTexture.Value.Width, 16);
			while (num205 + 1f < num204)
			{
				if (num204 - num205 < rectangle7.Height)
				{
					rectangle7.Height = (int)(num204 - num205);
				}
				Main.EntitySpriteDraw(BeamMiddleTexture.Value, center2 - Main.screenPosition, rectangle7, Color.White, Projectile.rotation, new Vector2(rectangle7.Width / 2, 0f), new Vector2(Projectile.scale - fadeInOutEased, Projectile.scale), SpriteEffects.None, 0);
				Lighting.AddLight(center2, new Vector3(255f / 255f, 128f / 128f, 128f / 128f) * (1f - fadeInOutEased));
				num205 += rectangle7.Height * Projectile.scale;
				center2 += Projectile.velocity * rectangle7.Height * Projectile.scale;
				rectangle7.Y += 16;
				if (rectangle7.Y + rectangle7.Height > BeamMiddleTexture.Value.Height)
				{
					rectangle7.Y = 0;
				}
			}
		}
		Main.EntitySpriteDraw(BeamStartTexture.Value, center2 - Main.screenPosition, null, Color.White, Projectile.rotation, BeamEndTexture.Frame().Top(), new Vector2(Projectile.scale - fadeInOutEased, Projectile.scale), SpriteEffects.None, 0);
		
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
	}

	public bool Colliding2(Rectangle myRect, Rectangle targetRect)
	{
		float collisionPoint6 = 0f;
		if (Collision.CheckAABBvLineCollision(targetRect.TopLeft(), targetRect.Size(), Projectile.Center, Projectile.Center + Projectile.velocity * Projectile.localAI[1], 36f * Projectile.scale, ref collisionPoint6))
		{
			return true;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (Colliding2(projHitbox, targetHitbox))
		{
			Main.player[Main.myPlayer].Hurt(PlayerDeathReason.ByProjectile(Main.myPlayer, Projectile.whoAmI), Projectile.damage, Projectile.direction);
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		var playerRect = new Rectangle((int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height);
		if (Projectile.Colliding(hitbox, playerRect))
		{
			Main.player[Main.myPlayer].Hurt(PlayerDeathReason.ByProjectile(Main.myPlayer, Projectile.whoAmI), Projectile.damage, Projectile.direction);
		}
	}

	public override void AI()
	{
		Vector2 samplingPoint = Projectile.Center;
		Projectile.position = Main.npc[(int)Projectile.ai[1]].Center + new Vector2((Main.npc[(int)Projectile.ai[1]].direction == 1 ? 50 : -65), -12) + new Vector2(Projectile.width, Projectile.height) / 2f;
		Projectile.localAI[0]++;
		if (Projectile.localAI[0] >= 180f)
		{
			Projectile.Kill();
			return;
		}

		float num828 = Projectile.velocity.ToRotation();
		Player targetPlayer = Main.npc[(int)Projectile.ai[1]].PlayerTarget();
		num828 = Main.npc[(int)Projectile.ai[1]].DirectionTo(targetPlayer.Center).ToRotation();
		Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(num828, MathHelper.ToRadians(0.3f)).ToRotationVector2();
		Projectile.rotation = Projectile.velocity.ToRotation().AngleTowards(num828, MathHelper.ToRadians(0.3f)) - 1.57079637f;
		float[] array5 = new float[3];
		Collision.LaserScan(samplingPoint, Projectile.velocity, Projectile.width * Projectile.scale, 2400f, array5);
		float num831 = 0f;
		int num4;
		for (int num832 = 0; num832 < array5.Length; num832 = num4 + 1)
		{
			num831 += array5[num832];
			num4 = num832;
		}
		num831 /= 3;
		Projectile.localAI[1] = MathHelper.Lerp(Projectile.localAI[1], num831, 0.5f);
		Vector2 vector58 = Projectile.Center + Projectile.velocity * (Projectile.localAI[1] - 14f);
		for (int num833 = 0; num833 < 2; num833 = num4 + 1)
		{
			float num834 = Projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? (-1f) : 1f) * 1.57079637f;
			float num835 = (float)Main.rand.NextDouble() * 2f + 2f;
			var vector59 = new Vector2((float)Math.Cos(num834) * num835, (float)Math.Sin(num834) * num835);
			int num836 = Dust.NewDust(vector58, 0, 0, DustID.Firework_Red, vector59.X, vector59.Y, Scale: 0.7f);
			Main.dust[num836].noGravity = true;
			Main.dust[num836].scale = 1f;
			num4 = num833;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 value42 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
			int num837 = Dust.NewDust(vector58 + value42 - Vector2.One * 4f, 8, 8, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
			Dust dust = Main.dust[num837];
			dust.velocity *= 0.5f;
			Main.dust[num837].velocity.Y = 0f - Math.Abs(Main.dust[num837].velocity.Y);
		}
	}
}
