using Avalon.Common.Interfaces;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Items.Weapons.Melee.Swords;
using Avalon.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Swords;

public class SoulEdgeDash : ModProjectile, ISyncedOnHitEffect
{
	private const int initialTimeLeft = 20;
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(64);
		Projectile.DamageType = DamageClass.Melee;
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.tileCollide = false;
		Projectile.timeLeft = initialTimeLeft;
		Projectile.penetrate = -1;
		Projectile.netImportant = true;
		Projectile.hide = true;
	}
	public override void AI()
	{
		Projectile.netUpdate = true;

		Player player = Main.player[Projectile.owner];
		if (Projectile.timeLeft == initialTimeLeft)
		{
			SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost with { MaxInstances = 10}, player.position);
			SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost with { MaxInstances = 10, Pitch = -0.35f}, player.position);
			SoundEngine.PlaySound(SoundID.DD2_GhastlyGlaiveImpactGhost with { MaxInstances = 10, Pitch = -0.65f }, player.position);
			SoundEngine.PlaySound(SoundID.Zombie53 with { SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest, MaxInstances = 5}, player.position);
			player.immune = true;
			player.AddImmuneTime(ImmunityCooldownID.General, 60);
			player.GetModPlayer<SoulEdgePlayer>().SoulEdgeDamage = 0;
		}
		player.velocity = Projectile.velocity * new Vector2(4, 3);
		player.heldProj = Projectile.whoAmI;
		player.GetModPlayer<AvalonPlayer>().TurnOffDownwardsMovementRestrictions = true;
		player.SetDummyItemTime(2);

		Projectile.velocity *= 0.99f;

		if (Projectile.timeLeft == 1)
		{
			Projectile.velocity *= 0.3f;
			player.velocity *= 0.3f;
		}

		if (Main.myPlayer == Projectile.owner && Projectile.timeLeft % 5 == 0)
		{
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, (Projectile.velocity * 0.3f).RotatedBy(MathHelper.PiOver2 + (MathHelper.Pi * i)), ModContent.ProjectileType<SoulEaterFriendly>(), Projectile.damage / 9, Projectile.knockBack / 9, Projectile.owner, -1);
			}
		}

		for (int i = 0; i < Projectile.timeLeft / 5; i++)
		{
			Dust d = Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<PhantoplasmDust>());
			d.velocity += Projectile.velocity * 1.5f;
			d.noGravity = !Main.rand.NextBool(3);
			if (d.noGravity)
			{
				d.fadeIn = Main.rand.NextFloat(3);
				d.velocity *= d.fadeIn;
			}
			else
				d.scale *= 1.25f;
		}

		Projectile.Center = player.Center + new Vector2(0, player.gfxOffY) + Vector2.Normalize(Projectile.velocity) * 15 * MathF.Sin(Projectile.timeLeft / (float)initialTimeLeft * MathHelper.Pi) - Vector2.Normalize(Projectile.velocity) * 10;
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
		player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (Projectile.rotation + MathHelper.PiOver4 + MathHelper.Pi) * player.gravDir + (player.gravDir == -1 ? MathHelper.Pi : 0));
	}
	public override bool PreDraw(ref Color lightColor)
	{
		//float percent = Projectile.timeLeft / (float)initialTimeLeft;
		float percent = Utils.Remap(Projectile.timeLeft, 0, initialTimeLeft * 0.75f, 0, 1) * (1f - MathF.Pow(Projectile.timeLeft / (float)initialTimeLeft,5));
		for(int i = 0; i < 15; i++)
		{
			float percent2 = i / 15f;
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition - Projectile.velocity * i, null, Color.Gray * (1f - percent2) * percent, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()), Projectile.scale, SpriteEffects.None, 0);
		}
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()), Projectile.scale, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, null, Color.Cyan with { A = 0 } * percent, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()), Projectile.scale + percent * percent, SpriteEffects.None, 0);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition - Projectile.velocity * 3 * percent, null, Color.Red with { A = 128 } * 0.35f * percent, Projectile.rotation, new Vector2(0, TextureAssets.Projectile[Type].Height()), Projectile.scale + percent, SpriteEffects.None, 0);
		Vector2 flashPos = Projectile.Center - Main.screenPosition + Vector2.Normalize(Projectile.velocity) * 120;
		for (int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, flashPos, null, new Color(1f, 0.25f, 0.25f, 0f) * 0.8f * percent, MathHelper.PiOver2 * i + Projectile.rotation + MathHelper.PiOver4, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale * percent * 3 * percent, (Projectile.scale + 2 - i * 2) * (1 + percent) * percent) * 1.2f, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, flashPos, null, new Color(1f, 1f, 1f, 0f) * 0.4f * percent * percent, MathHelper.PiOver2 * i + Projectile.rotation + MathHelper.PiOver4, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale * percent * 3 * percent, (Projectile.scale + 2 - i * 2) * (0.75f + percent) * percent), SpriteEffects.None, 0);
		}
		for (int i = 0; i < 2; i++)
		{
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, flashPos, null, new Color(0f, 0.75f, 1f, 0f) * 0.8f * percent, MathHelper.PiOver2 * i + Projectile.rotation, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale * percent * 3 * percent, (Projectile.scale + 1) * (1 + percent) * percent) * 1.2f * percent, SpriteEffects.None, 0);
			Main.EntitySpriteDraw(TextureAssets.Extra[ExtrasID.SharpTears].Value, flashPos, null, new Color(1f, 1f, 1f, 0f) * 0.4f * percent * percent, MathHelper.PiOver2 * i + Projectile.rotation, TextureAssets.Extra[ExtrasID.SharpTears].Size() / 2, new Vector2(Projectile.scale * percent * 3 * percent, (Projectile.scale + 1) * (0.75f + percent) * percent) * percent, SpriteEffects.None, 0);
		}
		return false;
	}
	public void SyncedOnHitNPC(Player player, NPC target, bool crit, int hitDirection)
	{
		Vector2 point = Main.rand.NextVector2FromRectangle(target.Hitbox);
		for (int i = 0; i < 3; i++)
		{
			var p = VanillaParticles.RequestPrettySparkleParticle();
			p.ColorTint = new Color(1f, 0.2f, 0.2f);
			p.FadeInEnd = Main.rand.NextFloat(4, 7);
			p.FadeOutStart = p.FadeInEnd;
			p.FadeOutEnd = Main.rand.NextFloat(13, 18);
			p.Scale = new Vector2(7, 4);
			p.Rotation = (i * MathHelper.TwoPi / 3f) + Main.rand.NextFloat(-0.3f, 0.3f);
			//p.Velocity = Vector2.UnitY.RotatedBy(p.Rotation) * Main.rand.NextFloat(2,4);
			p.DrawHorizontalAxis = false;
			p.LocalPosition = point;
			Main.ParticleSystem_World_OverPlayers.Add(p);
		}
	}
}
