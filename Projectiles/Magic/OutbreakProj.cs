using Avalon.Buffs.Debuffs;
using Avalon.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Avalon.Projectiles.Magic;

public class OutbreakProj : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Projectile.type] = 4;
	}
	public override void SetDefaults()
	{
		Projectile.width = 10;
		Projectile.height = 10;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.timeLeft = TimeUtils.SecondsToTicks(6);
		Projectile.tileCollide = false;
		Rectangle dims = this.GetDims();
		DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
		DrawOriginOffsetY = -(int)((dims.Width / 2) - (Projectile.Size.Y / 2)) - 12;
	}
	private int LatchedNPC
	{
		get => (int)Projectile.ai[0];
		set => Projectile.ai[0] = value;
	}
	private int PosSeed
	{
		get => (int)Projectile.ai[1];
		set => Projectile.ai[1] = value;
	}
	public override void AI()
	{
		//float R = 0.45f + Projectile.ai[1] * 0.43712f % 0.3f;
		//float G = 0.8f + Projectile.ai[1] * 0.62329f % 0.2f;
		//float B = 0.35f + Projectile.ai[1] * 0.52784f % 0.6f;
		//Lighting.AddLight(Projectile.Center, new Vector3(R, G, B) * 0.35f * Projectile.Opacity);

		//if (Projectile.ai[2] >= 0)
		//{
		//	if (Projectile.timeLeft < 1000)
		//	{
		//		Projectile.velocity = Projectile.SafeDirectionTo(Main.npc[(int)Projectile.ai[2]].Center) * Projectile.velocity.Length();
		//	}
		//}
		//else
		//{
		//	float mod = Main.rand.NextFloat(-MathF.PI * 0.04f, MathF.PI * 0.04f);
		//	Projectile.ai[0] += mod;
		//	Projectile.rotation = Projectile.ai[0];
		//	Projectile.velocity = Projectile.velocity.RotatedBy(mod);
		//	Projectile.velocity *= 0.965f + (Projectile.ai[1] % 0.025f);
		//}

		////AvalonGlobalProjectile.AvoidOtherGas(Projectile);

		////Projectile.scale *= 1.01f - (Projectile.ai[1] % 0.01f);
		////Projectile.alpha += 5;
		//if (Projectile.alpha >= 255) Projectile.Kill();


		NPC target = Main.npc[LatchedNPC];

		if (!target.active)
		{
			Projectile.Kill();
		}
		//Projectile.Center = target.Hitbox.ClosestPointInRect((Vector2.Normalize(Projectile.velocity) * MathHelper.Max(target.width, target.height)) + target.Center);
		UnifiedRandom seededRand = new(PosSeed);
		//Projectile.Center = posSeed.NextVector2FromRectangle(target.Hitbox)/* + posSeed.NextVector2CircularEdge(Projectile.velocity.Length(), Projectile.velocity.Length())*/;
		Projectile.Center = target.Center + seededRand.NextVector2Circular(target.width / 2f, target.height / 2f);

		int combinedWidthHeight = target.width + target.height - 1;
		float randVal = seededRand.Next(combinedWidthHeight);
		Vector2 rectOuterPos;
		if (randVal >= target.width - 1)
		{
			rectOuterPos = new((target.width - 1) * seededRand.Next(2), randVal - target.width + 1);
		}
		else
		{
			rectOuterPos = new(randVal, (target.height - 1) * seededRand.Next(2));
		}
		rectOuterPos += target.position;
		Projectile.Center = rectOuterPos;

		//Vector2 vel = Projectile.velocity;
		//AvalonGlobalProjectile.AvoidOtherGas(Projectile);
		//Projectile.Center += Projectile.velocity;
		//Projectile.velocity = vel;

		//Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		Projectile.rotation = Projectile.AngleTo(target.Center) - MathHelper.PiOver2 + Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4) * (Projectile.scale > 0.76f ? 0.1f : Utils.Remap((1f - (Projectile.scale * 4f % 1f)), 0, 1f, 0.01f, 0.4f));
		Animate();
	}
	public void Animate()
	{
		Projectile type = ContentSamples.ProjectilesByType[Type];
		float scale = Utils.Remap(type.timeLeft - Projectile.timeLeft, 0, type.timeLeft, 0f, 1f);
		float sizeLerp = Easings.PowIn(scale, 2f);
		Projectile.scale = Utils.Remap(sizeLerp, 0, 1f, 0, 0.75f);
		Projectile.Opacity = sizeLerp;
		float ballonScale = ((int)(scale * 4)) / 4f + Easings.PowOut(scale * 4f % 1f, 6f) / 4f;
		Projectile.scale = ballonScale;
		Projectile.Opacity = Utils.Remap(ballonScale, 0f, 1f, 0.25f, 1f);

		//Lighting.AddLight(Projectile.Center, new Vector3(11f / 255f, 19f / 255f, 7f / 255f) * ballonScale);

		//Projectile.frameCounter++;
		//if (Projectile.frameCounter >= 8)
		//{
		//	Projectile.frame++;
		//	Projectile.frameCounter = 0;
		//}
		//if (Projectile.frame > 3)
		//{
		//	Projectile.frame = 0;
		//}
		if (Projectile.timeLeft < TimeUtils.SecondsToTicks(1))
		//if (Projectile.scale > 0.75f)
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 15)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
				Projectile.frame = 3;
			}
		}
	}
	public override bool? CanHitNPC(NPC target)
	{
		//if (target.whoAmI == LatchedNPC)
		//{
		//	return base.CanHitNPC(target);
		//}
		//else
		//{
		//	return false;
		//}
		return false;
	}
	public override bool CanHitPvp(Player target)
	{
		return false;
	}
	public override bool? CanCutTiles()
	{
		return false;
	}
	public override void OnKill(int timeLeft)
	{
		float dustRad = 35f;
		int dustCount = (int)(15 * Projectile.scale);
		Vector2 projCenter = Projectile.Center + (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2() * 16f;

		if (timeLeft == 0)
		{
			SoundEngine.PlaySound(SoundID.NPCDeath1, projCenter);
			dustRad *= 1.2f;
			dustCount *= 2;
			const int AttackRad = 80;
			Player player = Main.player[Projectile.owner];
			List<NPC> hitTargets = [];
			foreach (var npc in Main.ActiveNPCs)
			{
				if (Vector2.Distance(npc.Center, projCenter) < AttackRad && !npc.dontTakeDamage && (!npc.friendly || (npc.type == NPCID.Guide && player.killGuide) || (npc.type == NPCID.Clothier && player.killClothier)))
				{
					hitTargets.Add(npc);
				}
			}
			if (hitTargets.Count > 0)
			{
				foreach (var npc in hitTargets)
				{
					int DPS = npc.SimpleStrikeNPC((int)player.GetTotalDamage(DamageClass.Magic).ApplyTo((int)(Projectile.damage / (1f + (hitTargets.Count - 1) / 10f))), player.direction, false, Projectile.knockBack, DamageClass.Magic, true, player.luck);
					player.addDPS(DPS);

					if (Main.rand.NextBool(5))
					{
						npc.AddBuff(ModContent.BuffType<Pathogen>(), Math.Max(TimeUtils.SecondsToTicks(1), (int)(TimeUtils.SecondsToTicks(10) / (1f + (hitTargets.Count - 1) / 5f))));
					}
					if (Main.rand.NextBool(3))
					{
						npc.AddBuff(BuffID.Poisoned, Math.Max(TimeUtils.SecondsToTicks(1), (int)(TimeUtils.SecondsToTicks(10) / (1f + (hitTargets.Count - 1) / 5f))));
					}
				}
			}
		}

		for (int j = 0; j < dustCount; j++)
		{
			Dust d = Dust.NewDustPerfect(projCenter, DustID.Stone, Main.rand.NextVector2CircularEdge(dustRad, dustRad), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat(0.75f)), Main.rand.NextFloat(1f, 1.5f));
			d.velocity *= Main.rand.NextFloat(0.05f, 0.1f);
			d.noGravity = true;
			d.color.A = 200;
		}
	}
	public override bool PreDraw(ref Color lightColor)
	{
		//Texture2D projectileTexture = TextureAssets.Projectile[Type].Value;
		//Vector2 drawPos = Projectile.Center - Main.screenPosition;
		//Vector2 drawOrigin = projectileTexture.Size() * 0.5f;
		//Color color = Color.White;
		//color.A = 127;
		//color = Color.Lerp(color, lightColor, 0.5f);
		//color *= Projectile.Opacity;

		////for (int k = 0; k < 4; k++)
		////{
		////	float x = Main.rand.Next(-10, 11) * (k - 1.5f) * 0.75f;
		////	float y = Main.rand.Next(-10, 11) * (k - 1.5f) * 0.75f;
		////	Main.spriteBatch.Draw(projectileTexture, drawPos + new Vector2(x, y), null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
		////}
		//lightColor.R = (byte)(lightColor.R * (0.45f + Projectile.ai[1] * 0.43712f % 0.3f));
		//lightColor.G = (byte)(lightColor.G * (0.8f + Projectile.ai[1] * 0.62329f % 0.2f));
		//lightColor.B = (byte)(lightColor.B * (0.35f + Projectile.ai[1] * 0.52784f % 0.6f));
		//Main.spriteBatch.Draw(projectileTexture, drawPos, null, lightColor * Projectile.Opacity, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);

		////Main.spriteBatch.Draw(projectileTexture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
		//return false;
		return base.PreDraw(ref lightColor);
	}
}
