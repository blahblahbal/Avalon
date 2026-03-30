using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class OsmiumGreatswordThrown : ModProjectile
{
	public override string Texture => ModContent.GetInstance<OsmiumGreatsword>().Texture;
	public override LocalizedText DisplayName => ModContent.GetInstance<OsmiumGreatsword>().DisplayName;

	public override void SetDefaults()
	{
		Projectile.hide = true;
		Projectile.aiStyle = -1;
		Projectile.width = Projectile.height = 64;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.usesLocalNPCImmunity = true;
		Projectile.localNPCHitCooldown = 30;
	}
	public const int TimeForMaxDamage = 45;
	public const float Gravity = 0.2f;
	public override void AI()
	{
		Player player = Main.player[Projectile.owner];
		if (Projectile.ai[2] == 0)
		{
			Projectile.spriteDirection = player.direction;
			Projectile.scale = Projectile.ai[0];
			int width = (int)(Projectile.scale * 64);
			Projectile.Resize(width, width);
			Projectile.rotation = player.itemRotation;
		}
		if (Projectile.ai[1] == 0)
		{
			Projectile.ai[2]++;
			if (Projectile.ai[2] == TimeForMaxDamage)
			{
				if (Main.myPlayer == Projectile.owner)
					SoundEngine.PlaySound(SoundID.MaxMana);

				for (int i = 0; i < 4; i++)
				{
					SparkleParticle s = new();
					s.Velocity = new Vector2(2, 0).RotatedBy(i * MathHelper.PiOver2);
					s.Rotation = MathHelper.PiOver2 * i;
					s.Scale = new Vector2(4f, 0.5f);
					s.DrawVerticalAxis = false;
					s.FadeInEnd = 4;
					s.FadeOutStart = 4;
					s.FadeOutEnd = 8;
					s.AdditiveAmount = 1f;
					s.ColorTint = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
					ParticleSystem.NewParticle(s, Projectile.Center);
				}
			}

			Projectile.rotation += Projectile.spriteDirection * 0.2f;
			Projectile.velocity.Y += Gravity;
			if (player.controlUseItem)
			{
				Projectile.ai[1] = 1;
				Projectile.netUpdate = true;
			}
		}
		else if (Projectile.ai[1] is > 0 and <= 2)
		{
			int type = ModContent.DustType<SimpleColorableGlowyDustFlat>();
			if (Projectile.ai[1] == 1)
			{
				SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.position);
				if(PerfectTiming)
					SoundEngine.PlaySound(SoundID.DD2_KoboldIgnite, Projectile.position);
				for (int i = 0; i < 15; i++)
				{
					Dust d = Dust.NewDustPerfect(Projectile.Center, type);
					d.velocity = Main.rand.NextVector2CircularEdge(1, 0.5f) * Main.rand.NextFloat(5, 10);
					d.color = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
					d.noGravity = true;
					d.scale = Main.rand.NextFloat(1f, 2f);
					d.noLight = true;

					Dust d2 = Dust.NewDustPerfect(d.position, type);
					d2.frame = d.frame;
					d2.rotation = d.rotation;
					d2.velocity = d.velocity;
					d2.color = new Color(1f, 1f, 1f, 0f);
					d2.noGravity = true;
					d2.scale = d.scale * Main.rand.NextFloat(0.5f, 0.9f);
					d2.noLight = true;
				}
				Projectile.ai[1] = 2;
			}

			Dust d3 = Dust.NewDustPerfect(Projectile.Center, type);
			d3.velocity = Projectile.velocity.RotatedByRandom(0.7f) * Main.rand.NextFloat();
			d3.color = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
			d3.noGravity = true;
			d3.scale = Main.rand.NextFloat(0.7f, 1.3f);
			d3.noLight = true;
			Dust d4 = Dust.NewDustPerfect(d3.position, type);
			d4.frame = d3.frame;
			d4.rotation = d3.rotation;
			d4.velocity = d3.velocity;
			d4.color = new Color(1f, 1f, 1f, 0f);
			d4.noGravity = true;
			d4.scale = d3.scale * Main.rand.NextFloat(0.5f, 0.9f);
			d4.noLight = true;

			Projectile.extraUpdates = PerfectTiming ? 15 : 5;
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 9;
			Projectile.rotation = Projectile.spriteDirection == 1? (-MathHelper.PiOver4 - MathHelper.Pi) : (MathHelper.PiOver4 + MathHelper.Pi);

			if (PerfectTiming)
			{
				SparkleParticle s = new();
				s.Rotation = MathHelper.PiOver2;
				s.Scale = new Vector2(4f, 0.5f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = 5;
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = 15;
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f) * 0.45f;
				s.HighlightColor *= 0.45f;
				ParticleSystem.NewParticle(s, Projectile.Center);
			}
		}
		else
		{
			Projectile.damage = 0;
			Projectile.alpha += PerfectTiming? 1 : 3;
			if(Projectile.alpha > 255)
			{
				Projectile.Kill();
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.ai[1] == 0)
		{
			if (Collision.SolidCollision(Projectile.Center, 1, 32))
			{
				return true;
			}
			Projectile.ai[1] = 1;
		}
		else if (Projectile.ai[1] == 2)
		{
			//Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			Point center = (Projectile.Center + Projectile.velocity).ToTileCoordinates();
			for (int x = center.X - 3; x < center.X + 3; x++)
			{
				for (int y = center.Y - 2; y < center.Y + 2; y++)
				{
					if (Main.tile[x, y].HasTile && (Main.tileSolid[Main.tile[x, y].TileType] || Main.tileSolidTop[Main.tile[x, y].TileType]))
					{
						for (int i = 0; i < 5; i++)
						{
							Dust d = Main.dust[WorldGen.KillTile_MakeTileDust(x, y, Main.tile[x, y])];
							d.velocity.Y -= Main.rand.NextFloat(4);
							d.noGravity = Main.rand.NextBool();
							d.velocity.X *= 3;
						}
					}
				}
			}
			for (int i = 0; i < 5; i++)
			{
				Gore g = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), Projectile.Center, Main.rand.NextVector2Circular(1, 1), Main.rand.Next(GoreID.Smoke1, GoreID.Smoke3 + 1), Main.rand.NextFloat(0.25f, 0.75f));
				g.rotation = Main.rand.NextFloat(MathHelper.TwoPi);

				SparkleParticle p = new();
				p.ColorTint = new Color(0.3f, Main.rand.NextFloat(0.4f, 0.75f), 1f, 0.75f);
				p.FadeInEnd = Main.rand.NextFloat(2, 5);
				p.FadeOutStart = p.FadeInEnd;
				p.FadeOutEnd = Main.rand.NextFloat(15, 17);
				p.Scale = new Vector2(2, 1);
				p.Velocity = Main.rand.NextVector2CircularEdge(1, 0.7f) * Main.rand.NextFloat(7, 9);
				p.Velocity.Y -= 4;
				p.Rotation = p.Velocity.ToRotation() + MathHelper.PiOver2;
				p.DrawHorizontalAxis = false;
				ParticleSystem.NewParticle(p, Projectile.Center);
			}
			Projectile.ai[1] = 3;
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Projectile.position);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage += Utils.Remap(Projectile.ai[2], 0, TimeForMaxDamage, -0.25f, 2);
		if (PerfectTiming)
		{
			modifiers.SetCrit();
			modifiers.CritDamage += (Main.player[Projectile.owner].GetCritChance(Projectile.DamageType) + Projectile.CritChance) / 100f;
		}
		if (Projectile.ai[1] > 0)
		{
			modifiers.SourceDamage += 4;
		}
	}
	private bool PerfectTiming => (Projectile.ai[2] >= TimeForMaxDamage && Projectile.ai[2] < TimeForMaxDamage + 8);
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 8;
		height = Projectile.ai[1] == 0 ? width : 24;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}
	public override bool PreDraw(ref Color lightColor)
	{
		SpriteEffects e = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
		Vector2 origin = Vector2.Lerp(new Vector2(Projectile.spriteDirection == 1 ? 7 : (64 - 7), 57), new Vector2(32), MathHelper.Clamp(Projectile.ai[2] * 0.05f,0,1));
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value,Projectile.Center - Main.screenPosition,null, lightColor * Projectile.Opacity, Projectile.rotation, origin,Projectile.scale, e);
		return false;
	}
}
