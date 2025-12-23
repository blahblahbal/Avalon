using Avalon.Dusts;
using Avalon.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Avalon.NPCs.Bosses;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common;
using Avalon.Buffs.Debuffs;
using ReLogic.Content;
using Avalon.NPCs.Bosses.Hardmode;
using System.Collections.Generic;
using Avalon;

namespace Avalon.NPCs.Bosses.Hardmode.Phantasm.Projectiles;

public class SoulDagger : ModProjectile
{
	public int alpha = 255;
	public override void SetDefaults()
	{
		Projectile.width = 9;
		Projectile.height = 9;
		Projectile.aiStyle = -1;
		Projectile.tileCollide = false;
		Projectile.alpha = 256;
		Projectile.friendly = false;
		Projectile.hostile = true;
		Projectile.scale = 1.3f;
		Projectile.timeLeft = 99999999;
		//Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(ModContent.BuffType<ShadowCurse>(), 60 * 5);
		}
	}
	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		if (Projectile.tileCollide)
			behindNPCsAndTiles.Add(index);
	}
	public override void AI()
	{
		Lighting.AddLight(Projectile.Center, new Vector3(0f, 1f, 1f) * 0.2f);

		if (Main.npc[(int)Projectile.ai[0]].type != ModContent.NPCType<Phantasm>())
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
			Dust d= Dust.NewDustPerfect(Projectile.Center, DustID.DungeonSpirit);
			d.velocity = new Vector2(0, Main.rand.NextFloat(10)).RotatedBy(Projectile.rotation + Main.rand.NextFloat(-0.3f, 0.3f));
			d.noGravity = true;
			d.alpha = 128;
			d.scale = 2;
			if(Projectile.timeLeft == 1)
			{
				if(Main.myPlayer == Projectile.owner)
				{
					for (int i = 0; i < 3; i++)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0,1).RotatedBy(MathHelper.TwoPi / 3 * i), ModContent.ProjectileType<LostSoul>(), 20, 1, -1, target.whoAmI);
					}
				}
				for (int i = 0; i < 15; i++)
				{
					Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit);
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
	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 1;
		height = 1;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}

	public int randTex = Main.rand.Next(3);
	public override bool PreDraw(ref Color lightColor)
	{
		Asset<Texture2D> texture = TextureAssets.Projectile[Type];
		Rectangle frame = new Rectangle(0, texture.Height() / 3 * randTex, texture.Width(),texture.Height() / 3);
		Vector2 drawPos = Projectile.Center - Main.screenPosition;
		Color color = new Color(1f,1f,1f,0.75f) * Projectile.Opacity;
		for (int i = 1; i < 4; i++)
		{
			Main.EntitySpriteDraw(texture.Value, drawPos + new Vector2(Projectile.velocity.X * (-i * 1), Projectile.velocity.Y * (-i * 1)), frame, (color * (1 - (i * 0.25f))) * 0.75f, Projectile.rotation, texture.Size() / new Vector2(2,3) - new Vector2(0, 20f), Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(texture.Value, drawPos, frame, color, Projectile.rotation, texture.Value.Size() / new Vector2(2,3) - new Vector2(0, 20f), Projectile.scale, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos, frame, color * 0.3f, Projectile.rotation, texture.Value.Size() / new Vector2(2, 3) - new Vector2(0, 14f), Projectile.scale * 1.3f, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(texture.Value, drawPos, frame, color * 0.15f, Projectile.rotation, texture.Value.Size() / new Vector2(2, 3) - new Vector2(0, 10f), Projectile.scale * 1.6f, SpriteEffects.None, 0);
		return false;
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if(Projectile.velocity != Vector2.Zero)
		{
			//Projectile.position += Projectile.velocity * 2;
			SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Projectile.position);
			for (int i = 0; i < 15; i++)
			{
				Dust d2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit);
				d2.noGravity = true;
				d2.velocity *= 3;
			}
		}
		Projectile.velocity = Vector2.Zero;

		if(Projectile.timeLeft > 200)
		{
			Projectile.timeLeft = 200;
		}

		return false;
	}
}
