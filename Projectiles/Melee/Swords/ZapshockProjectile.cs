using Avalon.Core;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class ZapshockProjectile : ModProjectile
{
	public override string Texture => ModContent.GetInstance<Zapshock>().Texture;
	private static VertexStrip _vertexStrip = new VertexStrip();
	private List<Vector2> _points = [];
	private List<float> _rotations = [];
	public override bool ShouldUpdatePosition()
	{
		return false;
	}
	public override void SetDefaults()
	{
		Projectile.friendly = true;
		Projectile.width = Projectile.height = 8;
		Projectile.tileCollide = false;
		Projectile.timeLeft = 20;
		Projectile.idStaticNPCHitCooldown = 11;
		Projectile.usesIDStaticNPCImmunity = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Magic;
	}
	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (Projectile.timeLeft < 10)
			return false;
		for (int i = 1; i < _points.Count; i++)
		{
			float collsionPoint = 0;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), _points[i], _points[i - 1], 16, ref collsionPoint))
				return true;
		}
		return base.Colliding(projHitbox, targetHitbox);
	}
	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.HitDirectionOverride = Math.Sign(target.Center.X - Main.player[Projectile.owner].Center.X);
	}
	public override void AI()
	{
		if (Projectile.ai[1] == 0 && Projectile.ai[2] == 0)
		{
			SoundEngine.PlaySound(SoundID.Item72 with { MaxInstances = 10, pitchVariance = 0.3f, pitch = 0.1f, volume = 0.7f }, Projectile.position);
			for (int i = 0; i < 2; i++)
			{
				var p = VanillaParticles.RequestPrettySparkleParticle();
				p.ColorTint = Color.Lerp(Color.Magenta, Color.Blue, Main.rand.NextFloat(0.5f));
				p.TimeToLive = Main.rand.Next(10, 15);
				p.Scale = new Vector2(3, 0.75f);
				p.Rotation = Main.rand.NextFloatDirection();
				p.LocalPosition = Projectile.velocity;
				Main.ParticleSystem_World_OverPlayers.Add(p);

				var p2 = VanillaParticles.RequestPrettySparkleParticle();
				p2.ColorTint = Color.Lerp(Color.Magenta, Color.Blue, Main.rand.NextFloat(0.5f));
				p2.TimeToLive = Main.rand.Next(20, 25);
				p2.Scale = new Vector2(3, 0.75f);
				p2.Rotation = Main.rand.NextFloatDirection();
				p2.LocalPosition = Projectile.Center;
				Main.ParticleSystem_World_BehindPlayers.Add(p2);
			}

			for (int i = 0; i < 30; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.velocity, DustID.RainbowMk2);
				d.color = Color.Lerp(Color.Magenta, Color.White, Main.rand.NextFloat(0.3f, 0.6f)) with { A = 0 };
				d.noGravity = true;
				d.scale = Main.rand.NextFloat(0.3f, 1f);
				d.velocity += Main.rand.NextVector2Circular(5, 5);

				Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.RainbowMk2);
				d2.color = Color.Lerp(Color.Magenta, Color.White, Main.rand.NextFloat(0.3f, 0.6f)) with { A = 0 };
				d2.noGravity = true;
				d2.scale = Main.rand.NextFloat(0.3f, 1f);
				d2.velocity += Main.rand.NextVector2Circular(5, 5);
			}
			if (Main.myPlayer == Projectile.owner)
			{
				for (int i = 0; i < 3; i++)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.velocity, Projectile.velocity + Main.rand.NextVector2CircularEdge(1, 1) * Main.rand.NextFloat(64, 128), Type, Projectile.damage, Projectile.knockBack, Projectile.owner, Utils.RandomNextSeed((ulong)(Main.timeForVisualEffects * i)), 1);
				}
			}
		}
		Projectile.ai[2]++;
		if (Projectile.ai[2] == 7)
		{
			Projectile.ai[2] = 5;
			Projectile.ai[0] = Utils.RandomNextSeed((ulong)(Projectile.ai[0] * 0.3f));
		}
		var seed = (ulong)Projectile.ai[0];
		_points.Clear();
		_rotations.Clear();
		float iterations = Math.Abs(Projectile.Center.X - Projectile.velocity.X) / 30 + 5;
		for (int i = 0; i < iterations; i++)
		{
			float percent = i / iterations;
			float sin = MathF.Sin(percent * MathHelper.Pi);
			Vector2 mainPoint = Vector2.Lerp(Projectile.Center, Projectile.velocity, percent) - new Vector2(0, sin * (100 - Projectile.ai[1] * 70));

			if (i != 0)
			{
				float warpAmount = Utils.Remap(Projectile.timeLeft, 10, 20, MathHelper.PiOver2, 0.1f);
				mainPoint = mainPoint.RotatedBy((Utils.RandomFloat(ref seed) - 0.5f) * warpAmount * sin, _points[i - 1]);

			}
			//mainPoint += new Vector2(Utils.RandomFloat(ref seed) - 0.5f, Utils.RandomFloat(ref seed) - 0.5f) * sin * 30;
			_points.Add(mainPoint);
			if (i != 0)
			{
				_rotations.Add(mainPoint.DirectionTo(_points[i - 1]).ToRotation());
				//if (Projectile.ai[2] == 1)
				//	for (int i2 = 0; i2 < 3; i2++)
				//	{
				//		if (Main.rand.NextBool(3))
				//		{
				//			Dust d = Dust.NewDustPerfect(Vector2.Lerp(_points[i], _points[i - 1], Main.rand.NextFloat()), DustID.RainbowMk2);
				//			d.color = Color.Lerp(Color.Magenta, Color.White, Main.rand.NextFloat(0.3f, 0.6f)) with { A = 0 };
				//			d.noGravity = true;
				//			d.velocity *= 0.1f;
				//			d.scale = Main.rand.NextFloat(0.3f, 0.8f);
				//		}
				//	}
			}
		}
		_rotations.Add(_rotations[_rotations.Count - 1]);

		if (Projectile.ai[1] == 0)
			for (int i = 0; i < 4; i++) // this is done multiple times to jankily fix the opacity at the end
			{
				_points.Add(Projectile.velocity);
				_rotations.Add(_rotations[_rotations.Count - 1]);
			}
		else
		{
			_points.Add(Projectile.velocity);
			_rotations.Add(_rotations[_rotations.Count - 1]);
		}
		//for (int i = 0; i < Points.Count; i++)
		//{
		//	Dust d = Dust.NewDustPerfect(Points[i], DustID.ShadowbeamStaff);
		//	d.noGravity = true;
		//}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		//var tex = AssetReferences.Assets.Textures.GlowCircle.Asset;
		//Rectangle middle = new Rectangle(0, 11, 24, 1);
		//Rectangle cap = new Rectangle(0, 0, 24, 11);

		//Vector2 capOrigin = new Vector2(cap.Width / 2, cap.Height);
		//Vector2 middleOrigin = new Vector2(middle.Width / 2, 0);
		//Color color = new Color(1f, 0.3f, 1f, 0f);
		//float width = 0.3f;
		//for (int i = 0; i < _points.Count - 1; i++)
		//{
		//	Vector2 direction = _points[i].DirectionTo(_points[i + 1]);
		//	float rotation = direction.ToRotation() - MathHelper.PiOver2;
		//	float distance = _points[i].Distance(_points[i + 1]);
		//	Vector2 drawPos = _points[i] - Main.screenPosition;
		//	//Utils.DrawLine(Main.spriteBatch, Points[i], Points[i + 1], Color.Purple, Color.Purple, 3);
		//	Main.EntitySpriteDraw(tex.Value, _points[i + 1] - Main.screenPosition, cap, color, rotation + MathHelper.Pi, capOrigin, width, SpriteEffects.None);
		//	Main.EntitySpriteDraw(tex.Value, drawPos, cap, color, rotation, capOrigin, width, SpriteEffects.None);
		//	Main.EntitySpriteDraw(tex.Value, drawPos, middle, color, rotation, middleOrigin, new Vector2(width, distance), SpriteEffects.None);
		//}

		MiscShaderData miscShaderData = GameShaders.Misc["Zapshock"];
		miscShaderData.UseOpacity(Projectile.Opacity * 4 * MathF.Pow(Projectile.timeLeft / 20f, 5));
		miscShaderData.Apply();
		_vertexStrip.PrepareStripWithProceduralPadding(_points.ToArray(), _rotations.ToArray(), StripColors, StripWidth, -Main.screenPosition);
		_vertexStrip.DrawTrail();
		Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		return false;
	}
	public override void Load()
	{
		MiscShaderData shader = new MiscShaderData(Main.Assets.Request<Effect>("PixelShader"), "MagicMissile").UseProjectionMatrix(doUse: true);
		shader.UseImage2(TextureAssets.MagicPixel);
		shader.UseImage1(AssetReferences.Projectiles.Melee.Swords.ZapshockProjectileShape.Asset);
		shader.UseImage0(AssetReferences.Projectiles.Melee.Swords.ZapshockProjectileGradient.Asset);
		GameShaders.Misc.Add("Zapshock", shader);
	}
	private static Color StripColors(float progressOnStrip)
	{
		return Color.White with { A = 0 };
	}
	private float StripWidth(float progressOnStrip)
	{
		return ((40f - progressOnStrip) * MathF.Pow(Projectile.timeLeft / 20f, 2 + progressOnStrip * 4)) * MathHelper.Clamp(progressOnStrip * 10,0.5f,1) * (Projectile.ai[1] == 0? 1.3f : 0.7f);
	}
}
