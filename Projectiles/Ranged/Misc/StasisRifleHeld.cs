using Avalon.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Ranged.Misc;

public class StasisRifleHeld : ModProjectile
{
	public override void SetStaticDefaults()
	{
		Main.projFrames[Type] = 4;
	}
	public override bool? CanDamage()
	{
		return false;
	}
	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(16);
		Projectile.scale = 0.85f;
		Projectile.aiStyle = -1;
		Projectile.timeLeft = 1;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.hide = true;
	}
	float frame = 0;
	bool Notify;
	private SlotId _chargeSound;
	public override void AI()
	{
		float Power = MathHelper.Clamp(Projectile.ai[1], 0, 1);
		Player player = Main.player[Projectile.owner];
		Projectile.spriteDirection = player.direction;
		player.heldProj = Projectile.whoAmI;
		if (player.channel && !player.CCed)
		{
			if (Projectile.ai[1] == 0)
			{
				_chargeSound = SoundEngine.PlaySound(SoundID.Item24 with {IsLooped = true, MaxInstances = 10}, Projectile.position);
			}
			if (SoundEngine.TryGetActiveSound(_chargeSound, out var s))
			{
				s.Position = Projectile.position;
				s.Pitch = Power * 2 -1;
				//s.Volume = 3 * Power;
			}
			Projectile.ai[1] += 1 / 120f;

			Projectile.timeLeft = 60;
			player.SetDummyItemTime(20);
			if (player.whoAmI == Main.myPlayer)
			{
				Projectile.velocity = player.Center.DirectionTo(Main.MouseWorld + new Vector2(0,-player.gfxOffY));
				player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
				Projectile.position.X -= player.direction * 6;
			}
		}
		if (Projectile.timeLeft == 59)
		{
			if (SoundEngine.TryGetActiveSound(_chargeSound, out var s))
			{
				s.Stop();
			}
			SoundEngine.PlaySound(SoundID.Item75, player.position);
			for (int i = 0; i < 30 * Power; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.Center + new Vector2(24, 0).RotatedBy(Projectile.rotation) + new Vector2(0, -8), 0, 0, DustID.FrostStaff);
				d.velocity *= 1.3f;
				d.velocity += Projectile.velocity * 3;
				d.noGravity = true;
			}

			StatModifier damageModifier = player.GetTotalDamage(Projectile.DamageType);
			damageModifier = damageModifier.CombineWith(player.specialistDamage);
			damageModifier = damageModifier.CombineWith(new StatModifier((float)player.HeldItem.damage / player.HeldItem.OriginalDamage, 1));
			CombinedHooks.ModifyWeaponDamage(player, player.HeldItem, ref damageModifier);
			int damage = (int)damageModifier.ApplyTo(Utils.Remap(Power,0,1,150,350));
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center + Projectile.velocity * 40 + new Vector2(0, -4), Projectile.velocity * player.HeldItem.shootSpeed, ModContent.ProjectileType<StasisShot>(), damage, Projectile.knockBack, Projectile.owner, Power);
		}
		if (Power == 1 && Main.myPlayer == player.whoAmI && !Notify)
		{
			Notify = true;
			SoundEngine.PlaySound(SoundID.MaxMana);
			for (int i = 0; i < 30 * Power; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center + Projectile.velocity * 34 + new Vector2(0, -2 * Projectile.spriteDirection).RotatedBy(Projectile.rotation), DustID.FrostStaff);
				d.velocity *= 3f;
				d.noGravity = true;
			}
		}
		Projectile.rotation = Projectile.velocity.ToRotation();

		Projectile.Center = player.RotatedRelativePoint(player.MountedCenter.Floor() + Vector2.Normalize(Projectile.velocity) * 20, false, false);
		player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, new Vector2(player.Center.X + player.direction * -6, player.Center.Y).DirectionTo(Projectile.Center).ToRotation() - MathHelper.PiOver2);
		if (player.channel)
			frame += Power;
		else
			frame += Projectile.timeLeft / 60f;

		if (frame > 2)
		{
			Projectile.frame++;
			frame = 0;
		}
		if (Projectile.frame > 3)
			Projectile.frame = 0;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		float Power = MathHelper.Clamp(Projectile.ai[1], 0, 1);
		SpriteEffects Flip = Projectile.direction == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
		Rectangle frame = TextureAssets.Projectile[Type].Frame(2, 4, 0, Projectile.frame);
		Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0, -4 + Main.player[Projectile.owner].gfxOffY);

		if (Projectile.timeLeft == 59)
		{
			var t = AssetReferences.Projectiles.Ranged.Misc.StasisSnowball.Asset;
			Main.EntitySpriteDraw(t.Value, drawPos + Projectile.velocity * (20 + Power * 10) + new Vector2(0, -2 * Projectile.spriteDirection).RotatedBy(Projectile.rotation), null, lightColor * Power, (float)Main.timeForVisualEffects * 0.2f * Projectile.spriteDirection, t.Size() / 2, Power, SpriteEffects.None, 0);
		}

		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame, lightColor, Projectile.rotation, frame.Size() / 2, Projectile.scale, Flip, 0);
		Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos, frame with { X = frame.Width }, Color.White with { A = 0 } * Power, Projectile.rotation, frame.Size() / 2, Projectile.scale, Flip, 0);
		for (int i = 0; i < 4; i++)
			Main.EntitySpriteDraw(TextureAssets.Projectile[Type].Value, drawPos + Vector2.UnitY.RotatedBy(i * MathHelper.PiOver2 + Projectile.rotation) * 2 * Projectile.scale, frame with { X = frame.Width}, Color.DodgerBlue with { A = 0 } * Power, Projectile.rotation, frame.Size() / 2, Projectile.scale, Flip, 0);
		
		if(Projectile.timeLeft == 59)
		{
			var t = AssetReferences.Projectiles.Ranged.Misc.StasisSnowball.Asset;
			Main.EntitySpriteDraw(t.Value, drawPos + Projectile.velocity * (20 + Power * 10) + new Vector2(0, -2 * Projectile.spriteDirection).RotatedBy(Projectile.rotation), null, lightColor * Power * 0.5f, (float)Main.timeForVisualEffects * 0.2f * Projectile.spriteDirection, t.Size() / 2, Power, SpriteEffects.None, 0);
		}
		return false;
	}
}
