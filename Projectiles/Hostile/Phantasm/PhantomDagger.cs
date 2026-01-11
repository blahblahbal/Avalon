using Avalon;
using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Avalon.Dusts;
using Avalon.Items.Weapons.Magic;
using Avalon.NPCs.Bosses.Hardmode.Phantasm;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.Phantasm;

public class PhantomDagger : SoulDagger
{
	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Data.Sets.ProjectileSets.DontReflect[Type] = true;
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		NPCs.Bosses.Hardmode.Phantasm.Phantasm.ApplyShadowCurse(target);
	}
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		if (Projectile.tileCollide)
			behindNPCsAndTiles.Add(index);
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, new Vector3(0f, 1f, 1f) * 0.2f);

		if (Main.npc[(int)Projectile.ai[0]].type != ModContent.NPCType<NPCs.Bosses.Hardmode.Phantasm.Phantasm>())
		{
			Projectile.Kill();
			return;
		}
		NPC phantasm = Main.npc[(int)Projectile.ai[0]];
		Player target = Main.player[phantasm.target];
		if (!phantasm.active)
			Projectile.Kill();

		float rotationMultipler = (MathHelper.TwoPi / 90);

		if(Projectile.alpha > 0)
		{
			Projectile.alpha -= 16;
		}

		Projectile.ai[1]++;
		Projectile.ai[2]++;

		if(Projectile.timeLeft <= 60)
		{
			Dust d= Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<PhantoplasmDust>());
			d.velocity = new Vector2(0, Main.rand.NextFloat(10)).RotatedBy(Projectile.rotation + Main.rand.NextFloat(-0.3f, 0.3f));
			d.noGravity = true;
			d.alpha = 128;
			d.scale = 1;
			if (Main.rand.NextBool(3))
			{
				PrettySparkleParticle s = VanillaParticlePools.PoolPrettySparkle.RequestParticle();
				s.LocalPosition = Projectile.Center;
				s.Velocity = new Vector2(0, Main.rand.NextFloat(7.5f)).RotatedBy(Projectile.rotation + Main.rand.NextFloat(-0.3f, 0.3f));
				s.Rotation = s.Velocity.ToRotation();
				s.Scale = new Vector2(5f, 0.7f);
				s.DrawVerticalAxis = false;
				s.FadeInEnd = Main.rand.Next(3, 10);
				s.FadeOutStart = s.FadeInEnd;
				s.FadeOutEnd = Main.rand.Next(20, 40);
				s.AdditiveAmount = 1f;
				s.ColorTint = new Color(1f, 0f, 0.2f);
				Main.ParticleSystem_World_OverPlayers.Add(s);
			}

			if (Projectile.timeLeft == 1)
			{
				if(Main.myPlayer == Projectile.owner)
				{
					int type = ModContent.ProjectileType<LostSoul>();
					if (phantasm.ModNPC is NPCs.Bosses.Hardmode.Phantasm.Phantasm tasm && tasm.phase > 8)
						type = ModContent.ProjectileType<Phantom>();
					for (int i = 0; i < 3; i++)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0,1).RotatedBy(MathHelper.TwoPi / 3 * i), type, 20, 1, -1, target.whoAmI);
					}
				}
				for (int i = 0; i < 15; i++)
				{
					Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
					d2.noGravity = true;
					d2.velocity *= 3;
				}
				SoundEngine.PlaySound(SoundID.NPCDeath39, Projectile.position);
			}
		}

		if (Projectile.ai[1] > 100)
		{
			if (Projectile.ai[1] < 160)
			{
				Projectile.rotation = Utils.AngleLerp(Projectile.Center.DirectionTo(target.Center).ToRotation() + MathHelper.PiOver2, Projectile.rotation, 0.7f);
				Projectile.Center = phantasm.Center + new Vector2(0, -150).RotatedBy(Projectile.ai[2] * rotationMultipler);
			}
			else if (Projectile.ai[1] == 161)
			{
				Projectile.velocity = Projectile.Center.DirectionTo(target.Center) * 30;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
				SoundStyle sound = SoundID.Zombie53;
				sound.Volume = 1.3f;
				SoundEngine.PlaySound(sound, Projectile.position);
				Projectile.hide = true;
			}
			else if (!Projectile.tileCollide && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.tileCollide = true;
			}
		}
		else
		{
			Projectile.Center = phantasm.Center + new Vector2(0, -150).RotatedBy(Projectile.ai[2] * rotationMultipler);
			Projectile.rotation = Projectile.ai[2] * rotationMultipler;
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.velocity != Vector2.Zero)
		{
			//Projectile.position += Projectile.velocity * 2;
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Projectile.position);
			for (int i = 0; i < 15; i++)
			{
				Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
				d2.noGravity = true;
				d2.velocity *= 3;
			}
		}
		Projectile.velocity = Vector2.Zero;

		if (Projectile.timeLeft > 200)
		{
			Projectile.timeLeft = 200;
		}

		return false;
	}
}
